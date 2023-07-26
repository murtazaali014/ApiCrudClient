using ApiCrudClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudClient.Controllers
{
    public class UserController : Controller
    {

        private readonly APIGateway apiGateway;

        public UserController(APIGateway ApiGateway)
        {
            this.apiGateway = ApiGateway;
        }

        public IActionResult Index()
        {
            List<User> user;
            user = apiGateway.ListUsers();
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            apiGateway.CreateUsers(user);
            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            User user = new User();
            apiGateway.GetUsers(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User user;
            user = apiGateway.GetUsers(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            apiGateway.UpdateUsers(user);
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            User user;
            user = apiGateway.GetUsers(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            apiGateway.DeleteUsers(user.id);
            return RedirectToAction("index");
        }
    }
}
