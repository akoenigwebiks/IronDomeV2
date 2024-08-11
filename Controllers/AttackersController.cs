using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronDomeV2.Data;
using IronDomeV2.Models;

namespace IronDomeV2.Controllers
{
    public class AttackersController : Controller
    {
        private readonly IronDomeV2Context _context;

        public AttackersController(IronDomeV2Context context)
        {
            _context = context;
        }

        // GET: Attackers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attacker.ToListAsync());
        }

        // GET: Attackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacker == null)
            {
                return NotFound();
            }

            return View(attacker);
        }

        // GET: Attackers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Distance")] Attacker attacker)
        {

            var errMsg = attacker switch
            {
                { Name: null } => "Name is required",
                { Distance: < 0 } => "Distance must be greater than or equal to 0",
                { Name: string name } when _context.Attacker.Any(a => a.Name == name) => "Attacker already exists",
                _ => null
            };

            if (errMsg != null)
            {
                ModelState.AddModelError("Name", errMsg);
                return View();
            }

            _context.Add(attacker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Attackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker.FindAsync(id);
            if (attacker == null)
            {
                return NotFound();
            }
            return View(attacker);
        }

        // POST: Attackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Distance")] Attacker attacker)
        {
            if (id != attacker.Id)
            {
                return NotFound();
            }

            var errMsg = attacker switch
            {
                { Name: null } => "Name is required",
                { Distance: < 0 } => "Distance must be greater than or equal to 0",
                { Name: string name } when _context.Attacker.Any(a => a.Name == name && a.Id != attacker.Id) => "Attacker with this name already exists",
                _ when _context.Attacker.Any(e => e.Id == id) == false => "Attacker does not exist",
                _ => null
            };

            if (errMsg != null)
            {
                ModelState.AddModelError("Name", errMsg);
                return View(attacker); // Pass the model back to the view
            }

            _context.Update(attacker);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Attackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacker = await _context.Attacker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacker == null)
            {
                return NotFound();
            }

            return View(attacker);
        }

        // POST: Attackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attacker = await _context.Attacker.FindAsync(id);
            if (attacker != null)
            {
                _context.Attacker.Remove(attacker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
