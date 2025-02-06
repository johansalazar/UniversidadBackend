using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UBack.Aplication.Dtos;
using UBack.Aplication.Interfaces;
using UBack.Transversal.Common;

namespace UBack.Aplication.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Response<AuthResponseDTO>> AuthenticateAsync(LoginDTO loginDTO)
        {
            // Buscar usuario en base de datos por correo electrónico
            var user = await _usuarioRepository.GetAllUsuariosAsync();
            var foundUser = user.FirstOrDefault(u => u.Email == loginDTO.Email);

            if (foundUser == null || foundUser.Contrasena != loginDTO.Contrasena) // Aquí deberías verificar la contraseña de manera segura (hashing)
                return null;

            // Crear los claims del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, foundUser.Nombre),
                new Claim(ClaimTypes.Email, foundUser.Email),
                new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()), // ⚠️ Usa el ID único del usuario
                new Claim(ClaimTypes.Role, foundUser.IdRol.ToString()) // 🔥 Si usas roles en autorización
            };

            // Obtener la clave secreta desde el archivo de configuración
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Expiración de 1 hora
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            AuthResponseDTO authResponseDTO = new AuthResponseDTO
            {
                Name = foundUser.Nombre,
                Token = jwtToken,
                Rol = foundUser.IdRol,
                Id = foundUser.Id,
                Expiration = token.ValidTo
            };
            Response<AuthResponseDTO> response = new Response<AuthResponseDTO>();

            if (authResponseDTO != null)
            {
                response.Data = authResponseDTO;
                response.IsSuccess = true;
                response.Message = "Usuario Existente.";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "No Existe el usuario.";
            }
            return response;
        }
    }
}

