namespace RefactoringLegacyCode.Service
{
    public interface IWalletService
    {
        string MoveMoney(string id, long buyerId, long sellerId, double amount);
    }
}