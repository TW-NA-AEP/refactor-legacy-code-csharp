using RefactoringLegacyCode.Entity;

namespace RefactoringLegacyCode.Repository
{
    public interface IUserRepository
    {
        User Find(long id);
    }
}