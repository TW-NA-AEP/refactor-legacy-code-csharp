using System;

namespace RefactoringLegacyCode.Utils
{
    public class IdGenerator
    {
        public static string GenerateTransactionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}