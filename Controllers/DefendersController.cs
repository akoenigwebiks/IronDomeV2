using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDomeV2.Data;
using IronDomeV2.Models;

namespace IronDomeV2.Controllers
{
    public class DefendersController : Controller
    {
        private readonly IronDomeV2Context _context;

        public DefendersController(IronDomeV2Context context)
        {
            _context = context;
        }

        // GET: Defenders
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
