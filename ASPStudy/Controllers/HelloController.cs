using Microsoft.AspNetCore.Mvc;

namespace ASPStudy {
    public class HelloController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
