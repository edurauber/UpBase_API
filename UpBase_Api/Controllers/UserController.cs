using Microsoft.AspNetCore.Mvc;
using Dapper;
using UpBase_Api.Entity;
using MySql.Data.MySqlClient;
using UpBase_Api.Model;

namespace UpBase_Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;
        public UserController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UpBase");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using (MySqlConnection conn = SQLConnection())
            {
                string sql = "SELECT * FROM User WHERE IsActive = 1";
                IEnumerable<UserEntity> users = await conn.QueryAsync<UserEntity>(sql);

                return Ok(users);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            using (MySqlConnection conn = SQLConnection())
            {
                string sql = "SELECT * FROM User WHERE Id = @ID";
                var parameters = new { id };
                UserEntity users = await conn.QueryFirstOrDefaultAsync<UserEntity>(sql, parameters);
                if (users is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(users);
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserInputModel model)
        {
            var user = new UserEntity(model.Name, model.Username, model.Email, model.Password);
            var parameters = new
            {
                user.Name,
                user.Username,
                user.Email,
                user.Password,
                user.IsActive
            };

            using (MySqlConnection connection = SQLConnection())
            {
                string sql = "INSERT INTO User VALUES (NULL, @NAME, @USERNAME, @EMAIL, @PASSWORD, @ISACTIVE)";
                int id = await connection.ExecuteScalarAsync<int>(sql, parameters);
                return Ok(id);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserInputModel model)
        {
            var user = new UserEntity(model.Name, model.Username, model.Email, model.Password);
            var parameters = new
            {
                id,
                user.Name,
                user.Email,
                user.Password
            };

            using (MySqlConnection connection = SQLConnection())
            {
                string sql = "UPDATE User SET Name = @NAME, Email = @EMAIL, Password = @PASSWORD WHERE Id = @ID";
                id = await connection.ExecuteAsync(sql, parameters);
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parameters = new { id };
            using (MySqlConnection connection = SQLConnection())
            {
                string sql = "UPDATE User SET IsActive = 0 WHERE Id = @ID";
                id = await connection.ExecuteAsync(sql, parameters);
                return NoContent();
            }
        }
        private MySqlConnection SQLConnection()
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                return con;
            }
        }
        private int SQLExecute(string sql, object parameters = null)
        {
            return SQLConnection().Execute(sql, parameters);
        }
    }
}

