using System.Web;
using System.Web.Mvc;

namespace PasswordEncrypt_Decrypt
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
