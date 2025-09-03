using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Ocsp;
using UrlShortener.Controllers.Shared;
using UrlShortener.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private const string tableName = "urls";

        private List<UrlItem> getListFromReader(MySqlDataReader reader)
        {
            List<UrlItem> list = new();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string full = reader.GetString("full");
                string shortUrl = reader.GetString("short");
                int createdBy = reader.GetInt32("created_by_id");
                string createdAt = reader.GetString("created_at");

                list.Add(new(id, full, shortUrl, createdBy, createdAt));
            }
            return list;
        }

        private string shortenUrl(string full)
        {
            string[] array = new string[2];
            array = full.Split('.');
            string result = "";
            for (int i = 2; i < array[0].Length; i += 3)
            {
                result += array[0][i];
            }
            return result + "." + array[1][0];
        }

        [HttpGet]
        public IActionResult Get()
        {
            Authenticator authenticator = new(Request);
            Repository repository = new();
            MySqlDataReader reader = repository.select(tableName);
            List<UrlItem>? list = getListFromReader(reader);
            if (!authenticator.IsLoggedIn)
            {
                list = list.Select(url => { url.CreatedAt = ""; url.CreatedById = 0; return url; }).ToList();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Authenticator authenticator = new(Request);
            Repository repository = new();
            MySqlDataReader reader = repository.select(tableName, "id = " + id, 1);
            List<UrlItem>? list = getListFromReader(reader);
            if (list != null)
            {
                UrlItem item = list[0];
                return Ok(item);
            }
            else
            {
                return StatusCode(404);
            }
        }

        [HttpPost]
        public IActionResult Post(UrlItem urlItem)
        {
            Authenticator authenticator = new(Request);
            Repository repository = new();

            MySqlDataReader reader = repository.select(tableName, $"full = '{urlItem.Full}'", 1);
            List<UrlItem> list = getListFromReader(reader);
            if (list.Count() == 1)
            {
                return BadRequest("Url already exists");
            }

            urlItem.Short = shortenUrl(urlItem.Full);

            repository.insert(tableName, new Dictionary<string, string> {
                    { "full", urlItem.Full },
                    { "short", urlItem.Short },
                    { "created_by_id", urlItem.CreatedById.ToString() },
                    { "created_at", urlItem.CreatedAt }
                });
            return Ok(new ResultItem("Url added"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Authenticator authenticator = new(Request);
            Repository repository = new();
            repository.delete(tableName, id).ToString();
            return Ok();
        }
    }
}
