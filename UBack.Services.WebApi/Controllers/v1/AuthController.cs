﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UBack.Aplication.Dtos;
using UBack.Aplication.Services;

namespace UBack.Services.WebApi.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authService"></param>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        /// <summary>
        /// Método para autenticar al usuario y generar un token JWT.
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>Token JWT</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var authResponse = await _authService.AuthenticateAsync(loginDTO);

            if (authResponse == null)
                return Unauthorized("Correo o contraseña incorrectos.");

            return Ok(authResponse);
        }
    }
}
