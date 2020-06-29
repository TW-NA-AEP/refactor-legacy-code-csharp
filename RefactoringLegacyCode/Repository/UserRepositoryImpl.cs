using System;
using RefactoringLegacyCode.Entity;

namespace RefactoringLegacyCode.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        public User Find(long id)
        {
            // Here is connecting to database server, please do not invoke directly
            throw new SystemException("Database server is connecting......");
        }
    }
}