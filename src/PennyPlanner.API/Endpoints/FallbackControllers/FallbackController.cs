using Microsoft.AspNetCore.Mvc;

namespace PennyPlanner.API.Endpoints.FallbackControllers
{
    public sealed class FallbackController : Controller
    {
        public IActionResult Index() =>
            PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
    }
}
