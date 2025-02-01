using System;

namespace Limupa.IdentityServer.Tools
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel(string Token, DateTime ExpireDate)
        {
            this.Token = Token;
            this.ExpireDate = ExpireDate;
        }

        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
