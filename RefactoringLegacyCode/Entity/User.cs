namespace RefactoringLegacyCode.Entity
{
    public class User
    {
        private long id;
        private double balance;

        public double GetBalance()
        {
            return balance;
        }

        public void SetBalance(double balance)
        {
            this.balance = balance;
        }
    }
}