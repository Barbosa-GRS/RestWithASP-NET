using Microsoft.IdentityModel.Tokens;
using RestWithASP_NET.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASP_NET.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private TokenConfiguration _configuration;

        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));// gera uma chave simetrica baseada na chave configurada no appsettings
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);//passa a secret acima para gerar o signincredentials usando o Hmac... 

            var Options = new JwtSecurityToken(        // gera as options setando issuar, audience....
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(Options); // gera o token
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32]; //gera uma array de 32
            using (var rng = RandomNumberGenerator.Create()) // cria um numero aleatório
            {
                rng.GetBytes(randomNumber); //salva o numero aleatório na variavel
            }
            return Convert.ToBase64String(randomNumber); // retorna o numero convertido para base 64
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false,
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            ClaimsPrincipal principal = tokenhandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");
            return principal;
        }
    }
}
