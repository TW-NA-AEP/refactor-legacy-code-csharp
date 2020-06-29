using System;

namespace RefactoringLegacyCode.Utils
{
    public class RedisDistributedLock
    {
        private static readonly RedisDistributedLock INSTANCE = new RedisDistributedLock();

        public static RedisDistributedLock GetSingletonInstance()
        {
            return INSTANCE;
        }

        public bool Lock(string transactionId) {
            // Here is connecting to redis server, please do not invoke directly
            throw new SystemException("Redis server is connecting......");
        }

        public void Unlock(string transactionId)
        {
            // Here is connecting to redis server, please do not invoke directly
            throw new SystemException("Redis server is connecting......");
        }
    }
}