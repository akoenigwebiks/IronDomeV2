using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IronDomeV2.Data;
using IronDomeV2.Models;
using IronDomeV2.ViewModels.Volleys;
using Microsoft.AspNetCore.SignalR;

namespace IronDomeV2.Controllers
{
    public class VolleysController : Controller
    {
        private readonly IronDomeV2Context _context;
        private readonly IHubContext<ChatHub> _hubContext;


        public VolleysController(IronDomeV2Context context,
             IHubContext<ChatHub> hubContext
            )
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Volleys
        public async Task<IActionResult> Index()
        {
            var ironDomeV2Context = _context.Volley.Include(v => v.Attacker);
            return View(await ironDomeV2Context.ToListAsync());
        }

        // GET: Volleys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley
                .Include(v => v.Attacker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volley == null)
            {
                return NotFound();
            }

            return View(volley);
        }

        // GET: Volleys/Create
        [Route("Volleys/Create/{AttackerId}")]
        public async Task<IActionResult> Create(int AttackerId)
        {


            var attacker = await _context.Attacker
                                         .FirstOrDefaultAsync(a => a.Id == AttackerId);
            // notify defenders of volley created
            _hubContext.Clients.All.SendAsync("BE_VolleyCreatedAlert", attacker);

            if (attacker == null)
            {
                return NotFound();
            }

            var methodOfAttacks = await _context.MethodOfAttack.ToListAsync();

            VMCreateVolley vmCreateVolley = new()
            {
                Attacker = attacker,
                MethodOfAttacks = methodOfAttacks
            };

            return View(vmCreateVolley);
        }



        // POST: Volleys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttackerId")] VMCreateVolley vMCreateVolley)
        {
            var volley = new Volley();
            if (ModelState.IsValid)
            {
                _context.Add(volley);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", volley.AttackerId);
            return View(volley);
        }

        // GET: Volleys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley.FindAsync(id);
            if (volley == null)
            {
                return NotFound();
            }
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", volley.AttackerId);
            return View(volley);
        }

        // POST: Volleys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttackerId")] Volley volley)
        {
            if (id != volley.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volley);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolleyExists(volley.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", volley.AttackerId);
            return View(volley);
        }

        // GET: Volleys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {



            if (id == null)
            {
                return NotFound();
            }

            var volley = await _context.Volley
                .Include(v => v.Attacker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volley == null)
            {
                return NotFound();
            }

            return View(volley);
        }

        // POST: Volleys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volley = await _context.Volley.FindAsync(id);
            if (volley != null)
            {
                _context.Volley.Remove(volley);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolleyExists(int id)
        {
            return _context.Volley.Any(e => e.Id == id);
        }
    }
}
