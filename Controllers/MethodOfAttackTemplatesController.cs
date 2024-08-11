using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IronDomeV2.Data;
using IronDomeV2.Models;
using IronDomeV2.ViewModels;

namespace IronDomeV2.Controllers
{
    public class MethodOfAttackTemplatesController : Controller
    {
        private readonly IronDomeV2Context _context;

        public MethodOfAttackTemplatesController(IronDomeV2Context context)
        {
            _context = context;
        }

        // GET: MethodOfAttackTemplates
        public async Task<IActionResult> Index(int attackerId)
        {
            if (attackerId < 0) return NotFound();

            var MethodOfAttacksTemplates = _context.MethodOfAttackTemplate.Include(m => m.Attacker).Where(m => m.Id == attackerId);
            ViewData["AttackerId"] = attackerId;
            return View(await MethodOfAttacksTemplates.ToListAsync());
        }

        // GET: MethodOfAttackTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var methodOfAttackTemplate = await _context.MethodOfAttackTemplate
                .Include(m => m.Attacker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (methodOfAttackTemplate == null)
            {
                return NotFound();
            }

            return View(methodOfAttackTemplate);
        }

        // GET: MethodOfAttackTemplates/Create
        [Route("MethodOfAttackTemplates/Create/{attackerId}")]
        public async Task<IActionResult> Create(int attackerId)
        {
            if (attackerId < 0)
            {
                return BadRequest();
            }
            var attacker = await _context.Attacker.FirstOrDefaultAsync(a => a.Id == attackerId);

            if (attacker == null)
            {
                return NotFound();
            }

            VMCreateMethodOfAttackTemplate vMCreateMethodOfAttackTemplate = new VMCreateMethodOfAttackTemplate();
            vMCreateMethodOfAttackTemplate.Attacker = attacker;
            return View(vMCreateMethodOfAttackTemplate);
        }

        // POST: MethodOfAttackTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Range,Velocity,AttackerId")] MethodOfAttackTemplate methodOfAttackTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(methodOfAttackTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", methodOfAttackTemplate.AttackerId);
            return View(methodOfAttackTemplate);
        }

        // GET: MethodOfAttackTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var methodOfAttackTemplate = await _context.MethodOfAttackTemplate.FindAsync(id);
            if (methodOfAttackTemplate == null)
            {
                return NotFound();
            }
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", methodOfAttackTemplate.AttackerId);
            return View(methodOfAttackTemplate);
        }

        // POST: MethodOfAttackTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Range,Velocity,AttackerId")] MethodOfAttackTemplate methodOfAttackTemplate)
        {
            if (id != methodOfAttackTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(methodOfAttackTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MethodOfAttackTemplateExists(methodOfAttackTemplate.Id))
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
            ViewData["AttackerId"] = new SelectList(_context.Attacker, "Id", "Id", methodOfAttackTemplate.AttackerId);
            return View(methodOfAttackTemplate);
        }

        // GET: MethodOfAttackTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var methodOfAttackTemplate = await _context.MethodOfAttackTemplate
                .Include(m => m.Attacker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (methodOfAttackTemplate == null)
            {
                return NotFound();
            }

            return View(methodOfAttackTemplate);
        }

        // POST: MethodOfAttackTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var methodOfAttackTemplate = await _context.MethodOfAttackTemplate.FindAsync(id);
            if (methodOfAttackTemplate != null)
            {
                _context.MethodOfAttackTemplate.Remove(methodOfAttackTemplate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MethodOfAttackTemplateExists(int id)
        {
            return _context.MethodOfAttackTemplate.Any(e => e.Id == id);
        }
    }
}
