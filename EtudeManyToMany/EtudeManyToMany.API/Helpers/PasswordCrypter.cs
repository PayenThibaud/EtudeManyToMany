using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EtudeManyToMany.API.Helpers
{
    public class PasswordCrypter
    {

        public static string EncryptPassword(string? password, string secretKey)
        {
            if (string.IsNullOrEmpty(password)) return "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password + secretKey));
        }
    }
}
