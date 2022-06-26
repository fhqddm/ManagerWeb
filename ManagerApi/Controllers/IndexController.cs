using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solo.Common;
using Solo.Model.ViewModel;

namespace ManagerApi.Controllers
{
    public class IndexController : Controller
    {
        [HttpPost]
        public IActionResult Indexes()
        {
            RedisConn readConn = new RedisConn(true);
            var result = readConn.GetRedisData<List<IndexView>>("IndexInfos");
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            Console.WriteLine(userId.ToString() + " IndexInfos");
            return Ok(result);
        }

        [HttpGet]
        public IActionResult IndexDetail(string name)
        {
            RedisConn readConn = new RedisConn(true);
            var result = readConn.GetRedisData<IndexDetailView>(name);
            int userId = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            Console.WriteLine(userId.ToString() + " IndexDetail "+ name);
            return Ok(result);
        }
    }
}