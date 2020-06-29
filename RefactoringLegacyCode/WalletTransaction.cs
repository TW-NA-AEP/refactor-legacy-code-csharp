using System;
using RefactoringLegacyCode.Enums;
using RefactoringLegacyCode.Service;
using RefactoringLegacyCode.Utils;

namespace RefactoringLegacyCode
{
    public class WalletTransaction
    {
        private string id;
        private long buyerId;
        private long sellerId;
        private long productId;
        private string orderId;
        private long createdTimestamp;
        private double amount;
        private STATUS status;
        private string walletTransactionId;

        public WalletTransaction(string preAssignedId, long buyerId, long sellerId, long productId, string orderId)
        {
            if (preAssignedId != null && !string.IsNullOrEmpty(preAssignedId))
            {
                this.id = preAssignedId;
            }
            else
            {
                this.id = IdGenerator.GenerateTransactionId();
            }

            if (!this.id.StartsWith("t_"))
            {
                this.id = "t_" + preAssignedId;
            }

            this.buyerId = buyerId;
            this.sellerId = sellerId;
            this.productId = productId;
            this.orderId = orderId;
            this.status = STATUS.TO_BE_EXECUTED;
            this.createdTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public bool Execute()
        {
            if (buyerId == null || (sellerId == null || amount < 0.0))
            {
                throw new InvalidTransactionException("This is an invalid transaction");
            }

            if (status == STATUS.EXECUTED) return true;

            bool isLocked = false;
            try
            {
                isLocked = RedisDistributedLock.GetSingletonInstance().Lock (id) ;

                // 锁定未成功，返回false
                if (!isLocked)
                {
                    return false;
                }

                if (status == STATUS.EXECUTED) return true; // double check
                long executionInvokedTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                // 交易超过20天
                if (executionInvokedTimestamp - createdTimestamp > 1728000000)
                {
                    this.status = STATUS.EXPIRED;
                    return false;
                }

                IWalletService walletService = new WalletServiceImpl();
                string walletTransactionId = walletService.MoveMoney(id, buyerId, sellerId, amount);
                if (walletTransactionId != null)
                {
                    this.walletTransactionId = walletTransactionId;
                    this.status = STATUS.EXECUTED;
                    return true;
                }
                else
                {
                    this.status = STATUS.FAILED;
                    return false;
                }
            }
            finally
            {
                if (isLocked)
                {
                    RedisDistributedLock.GetSingletonInstance().Unlock(id);
                }
            }
        }
    }
}