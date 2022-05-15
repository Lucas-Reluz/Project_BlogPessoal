using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoal.src.services.implementations
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IAutenticacao</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 15/05/2022 | 15:46 </para>
    /// </summary>
    public class AuthenticationServices : IAuthentication
    {
        #region Atributos
        private readonly IUser _repository;
        public IConfiguration Settings;
        #endregion

        #region Construtores

        public AuthenticationServices(IUser repository, IConfiguration settings)
        {
            _repository = repository;
            Settings = settings;
        }

        #endregion

        #region Metodos
        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel por criar usuario sem duplicar no banco</para>
        /// </summary>
        /// <param name="dto">NewUserDTO</param>
        public async Task CreateUserWithoutDuplicatingAsync(NewUserDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user != null) throw new Exception("This email is already being used");
            dto.Password = EncodePassword(dto.Password);
            await _repository.NewUserAsync(dto);
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por criptografar senha</para>
        /// </summary>
        /// <param name="password">Senha a ser criptografada</param>
        /// <returns>string</returns>
        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por gerar token JWT</para>
        /// </summary>
        /// <param name="user">UserModel</param>
        /// <returns>string</returns>
        public string GenToken(UserModel user)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Settings["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
            new Claim[]
            {
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(chave),
            SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenManipulador.CreateToken(tokenDescricao);
            return tokenManipulador.WriteToken(token);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono responsavel devolver autorização para usuario autenticado</para>
        /// </summary>
        /// <param name="dto">AuthenticationDTO</param>
        /// <returns>AuthorizationDTO</returns>
        /// <exception cref="Exception">Usuário não encontrado</exception>
        /// <exception cref="Exception">Senha incorreta</exception>
        public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user == null) throw new Exception("User not found");
            if (user.Password != EncodePassword(dto.Password)) throw new
            Exception("Incorrect Password");
            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GenToken(user));

        }
        #endregion
    }
}
