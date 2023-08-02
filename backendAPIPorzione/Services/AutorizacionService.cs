using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Models.cliente_servidor;
using backendAPIPorzione.Services.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backendAPIPorzione.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        readonly PorzioneapiContext _context;
        readonly IConfiguration _configuration;

        public AutorizacionService(PorzioneapiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(Usuario usuario)
        {
            try
            {
                var key = _configuration.GetValue<string>("JwtSettings:key"); //accedo al valor del .json
                var keyBytes = Encoding.ASCII.GetBytes(key); //codifico la key en un array de bytes.

                var claims = new ClaimsIdentity(); //intancio un claim, para agregar la info.
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())); //el subject va a ser el id del usuario.
                claims.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol));

                var credencialesToken = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature //tipo del algoritmo para encriptar el token
                    );



                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = credencialesToken
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return tokenCreado;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return null;
        }


        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(u =>
           u.Clave == autorizacion.Clave &&
           u.Correo == autorizacion.Correo
           );

            if (usuarioEncontrado is null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string tokenCreado = GenerarToken(usuarioEncontrado);

            return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Mensaje = "Ok" };
        }

    }
}
