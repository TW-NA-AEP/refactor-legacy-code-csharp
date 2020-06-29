using System;
using RefactoringLegacyCode.Entity;
using RefactoringLegacyCode.Repository;

namespace RefactoringLegacyCode.Service
{
    public class WalletServiceImpl : IWalletService
    {
        private IUserRepository userRepository = new UserRepositoryImpl();

        public string MoveMoney(string id, long buyerId, long sellerId, double amount)
        {
            User buyer = userRepository.Find(buyerId);
            if (buyer.GetBalance() >= amount)
            {
                User seller = userRepository.Find(sellerId);
                seller.SetBalance(seller.GetBalance() + amount);
                buyer.SetBalance(buyer.GetBalance() - amount);
                return Guid.NewGuid() + id;
            }
            else
            {
                return null;
            }
        }
    }
}