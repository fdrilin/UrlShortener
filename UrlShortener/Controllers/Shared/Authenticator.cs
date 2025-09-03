using Microsoft.Extensions.Primitives;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;

namespace UrlShortener.Controllers.Shared
{
    public class Authenticator
    {
        public string? Token { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; } = false;

        public static Dictionary<string, string> tokens = new();

        public Authenticator(string bearer)
        {
            Authenticate(bearer);
        }

        public Authenticator( HttpRequest request)
        {
            Authenticate(request.Headers["Authorization"].ToString());
        }

        private void Authenticate(string bearer)
        {
            Token = bearer;

            if (bearer == "")
            {
                return;
            }
            if (!bearer.StartsWith("Bearer "))
            {
                return;
            }
            Token = bearer.Substring("Bearer ".Length).Trim();

            IsLoggedIn = tokens.Values.Contains(Token);

            if (IsLoggedIn) 
            {
                Repository repository = new();
                MySqlDataReader reader = (
                    repository.select(
                        "users", 
                        
                        $"username = '{tokens.FirstOrDefault(v => v.Value == Token).Key}'", 
                        1
                    )
                );
                reader.Read();
                IsAdmin = reader.GetSByte("admin") == 1;
            }
        }
    }
}
