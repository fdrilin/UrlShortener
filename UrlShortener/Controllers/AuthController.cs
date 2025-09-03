using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using UrlShortener.Controllers.Shared;
using UrlShortener.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string tableName = "users";

        private string generateToken(LoginRequest user)
        {
            string token = Guid.NewGuid().ToString();
            Authenticator.tokens[user.Username] = token;
            return token;
        }

        private List<UserItem> getListFromReader(MySqlDataReader reader)
        {
            List<UserItem> list = new();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string username = reader.GetString("username");
                string password = reader.GetString("password");
                bool admin = reader.GetSByte("admin") == 1;

                list.Add(new(id, username, password, admin));

                Console.WriteLine($"id {id}, username {username}, p {password}, a {admin}");
            }
            return list;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Repository repository = new();
            MySqlDataReader reader = repository.select(tableName);
            List<UserItem>? list = getListFromReader(reader);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Repository repository = new();
            MySqlDataReader reader = repository.select(tableName, "id = " + id, 1);
            List<UserItem>? list = getListFromReader(reader);
            if (list != null)
            {
                UserItem item = list[0];
                return Ok(item);
            }
            else 
            {
                return StatusCode(404);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Repository repository = new();
            Console.WriteLine(loginRequest);
            MySqlDataReader reader = repository.select(tableName, $"username = \"{loginRequest.Username}\"", 1);
            List<UserItem>? list = getListFromReader(reader);
            if (list == null)
            {
                return StatusCode(404);
            }

            UserItem user = list[0];

            if (user.Password == loginRequest.Password)
            {
                return Ok(new ResultItem(generateToken(loginRequest) + " " + user.Id ));
            }
            else
            {
                return BadRequest("username or password is incorrect");
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginRequest request)
        {
            Repository repository = new();
            Console.WriteLine(request);

            MySqlDataReader reader = repository.select(tableName, $"username = '{request.Username}'", 1);
            List<UserItem>? list = getListFromReader(reader);
            if (list.Count() == 1)
            {
                return BadRequest(new ResultItem("User already exists"));
            }

            repository.insert(tableName, new Dictionary<string, string> {
                    { "username", request.Username },
                    { "password", request.Password }
                });

            return Ok(new ResultItem(generateToken(request)));
        }
    }
}
