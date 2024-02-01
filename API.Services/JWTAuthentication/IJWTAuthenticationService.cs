using API.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.JWTAuthentication
{
    public interface IJWTAuthenticationService
    {
        AccessTokenModel GenerateToken(TokenModel userToken, string JWT_Secret, int JWT_Validity_Mins);
        TokenModel GetUserTokenData(string jwtToken);
    }
}
