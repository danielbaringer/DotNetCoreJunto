using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using coreApi.Models;
using coreApi.Services;

namespace coreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult AutenticaUsr([FromBody]Usuario usrLogin)
        {
            var usrAcesso = _usuarioService.Autenticar(usrLogin.Login, usrLogin.Senha);

            if (usrAcesso == null)
                return BadRequest(new { message = "Usuário ou senha é inválido" });

            return Ok(usrAcesso);
        }

    }
}