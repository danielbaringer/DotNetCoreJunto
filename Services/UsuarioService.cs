
using coreApi.Models;
using coreApi.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace coreApi.Services
{
    
    public interface IUsuarioService
    {
        Usuario Autenticar(string login, string senha);
        IEnumerable<Usuario> GetAll();
    }

    public class UsuarioService : IUsuarioService
    {

        /*private List<Usuario> _usuarios = new List<Usuario>()
        {
            new Usuario()
               {
                   Id = 2,
                   Nome = "Daniel",
                   Login="baringer",
                   Senha="0000"
               }
        }; */

        private List<Usuario> _usuarios  = new Usuario().verificaLoginSenha();

       private readonly AppSettings _appSettings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

       public Usuario Autenticar(string login, string senha)
        {
            //_usuarios  = new Usuario().verificaLoginSenha();

            var usuario = _usuarios.SingleOrDefault(x => x.Login == login && x.Senha == senha);

            if (usuario == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuario.Token = tokenHandler.WriteToken(token);


            usuario.Senha = null;

            return usuario;
        }

        public IEnumerable<Usuario> GetAll()
        {
            // return users without passwords
            return _usuarios.Select(x => {
                x.Senha = null;
                return x;
            });
        }


    }
}