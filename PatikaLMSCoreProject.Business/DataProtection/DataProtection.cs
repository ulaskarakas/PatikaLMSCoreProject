using Microsoft.AspNetCore.DataProtection;

namespace PatikaLMSCoreProject.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;
        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("PatikaLMSCoreProject-Security");
        }
        public string Protect(string text)
        {
            return _protector.Protect(text);
        }

        public string Unprotect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}