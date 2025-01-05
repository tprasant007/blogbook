using Blogs_API.DTO;
using Blogs_API.Models;
using Blogs_API.Util;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogs_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly string secretKey;
        public UserRepository(IConfiguration configuration, IConfiguration configuration1)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            secretKey = configuration1.GetValue<string>("ApiSettings:Secret");
        }
        public async Task<bool> IsUniqueUserAsync(string username, string email)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "SELECT UserName FROM LocalUsers WHERE UserName = @username OR Email = @email";
            var user = await connection.QueryFirstOrDefaultAsync<string>(sql, new { username, email });
            return user == null;
        }
        public async Task<LoginResponseDTO?> Login(LoginRequestDTO loginRequestDto)
        {
            using var connection = new SqliteConnection(_connectionString);
            var sql = "SELECT * FROM LocalUsers WHERE LOWER(UserName) = LOWER(@userName)";
            var user = await connection.QueryFirstOrDefaultAsync<LocalUser>(sql, new { userName = loginRequestDto.UserName });

            if (user == null) return null;

            if (!PasswordHasher.VerifyPassword(loginRequestDto.Password, user.HashPassword)) return null;

            //generate jwt if email and password are ok

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Id.ToString())
                    //if users have roles such as admin, etc can be used here
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var responseDto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.UserName,
            };
            return responseDto;
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            var user = new LocalUser
            {
                Email = registrationRequestDTO.Email,
                UserName = registrationRequestDTO.UserName,
                HashPassword = PasswordHasher.HashPassword(registrationRequestDTO.Password)
            };

            using var connection = new SqliteConnection(_connectionString);
            var sql = "INSERT INTO LocalUsers(Email, UserName, HashPassword) VALUES (@Email, @UserName, @HashPassword)";
            await connection.ExecuteAsync(sql, new { user.Email, user.UserName, user.HashPassword});
            return user;
        }

        
    }
}
