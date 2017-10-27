using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Heimdallr.Authentication.Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "DevelopmentAccess")]
        public IActionResult Secured()
        {
            return View();
        }

        [Route("Logout")]
        [Authorize(Policy = "DevelopmentAccess")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(DevelopmentAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }
    }
}
