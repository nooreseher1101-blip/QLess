using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}