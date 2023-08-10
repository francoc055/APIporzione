using backendAPIPorzione.Datos;
using backendAPIPorzione.Models;
using backendAPIPorzione.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace backendAPIPorzione.Repository
{
    public class ClaveHasherRepository : IClaveHasherRepository
    {
        readonly PorzioneapiContext _context;

        public ClaveHasherRepository(PorzioneapiContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteUsuario(string correo)
        {
            bool existeUser = await _context.Usuarios.AnyAsync(u => u.Correo == correo);

            if (!existeUser)
                return false;

            return true;
        }

        public async Task<Usuario> Login(string correo, string clave)
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario is null)
                return null;



            if (!VerificarClaveHash(clave, usuario.ClaveHash, usuario.ClaveSalt))
                return null;

            return usuario;
        }

        private bool VerificarClaveHash(string clave, byte[] claveHash, byte[] claveSalt)
        {
            using(HMACSHA256 hmac = new HMACSHA256(claveSalt))
            {
                byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));

                for(int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != claveHash[i])
                        return false;
                }
            }
             
            return true;
        }

        public async Task<Usuario> Register(Usuario usuario, string clave)
        {
            try
            {
                byte[] claveHash;
                byte[] claveSalt;
                CrearClaveHash(clave, out claveHash, out claveSalt);

                usuario.ClaveHash = claveHash;
                usuario.ClaveSalt = claveSalt;

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception e)
            {

                await Console.Out.WriteLineAsync(e.Message);
            }

            return null;
        }

        private void CrearClaveHash(string clave, out byte[] claveHash, out byte[] claveSalt)
        {
            using(HMACSHA256 hmac = new HMACSHA256())
            {
                claveSalt = hmac.Key;
                claveHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }
    }
}
