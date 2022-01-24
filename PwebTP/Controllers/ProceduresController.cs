using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;

namespace PwebTP.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProceduresController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Procedures
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ManageProcedures(string id)
        {
            var applicationDBContext = _context.Procedures.Include(p => p.Checklist).Where(p => p.ChecklistId == id);
            return View(await applicationDBContext.ToListAsync());
        }



        // GET: Procedures/Create
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAsync(string id)
        {
            var checklist = await  _context.Checklist.FindAsync(id);
            ViewData["ChecklistId"] = id;
            ViewData["BackRoomId"] = checklist.RoomId;
            return View();
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("ChecklistId,ProcedureName")] Procedures procedures)
        {
            

            if (ModelState.IsValid)
            {
                _context.Add(procedures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageProcedures),new { id = procedures.ChecklistId });
            }
            ViewData["ChecklistId"] = procedures.ChecklistId;
            ViewData["BackRoomId"] = procedures.Checklist.RoomId;
            return View(procedures);
        }

        // GET: Procedures/Edit/5
       
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedures = await _context.Procedures.FindAsync(id);
            if (procedures == null)
            {
                return NotFound();
            }
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "ChecklistId", "ChecklistId", procedures.ChecklistId);
            return View(procedures);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ChecklistId,ProcedureName,ProcedureResult")] Procedures procedures)
        {
            if (id != procedures.ProceduresId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProceduresExists(procedures.ProceduresId))
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
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "ChecklistId", "ChecklistId", procedures.ChecklistId);
            return View(procedures);
        }

        // GET: Procedures/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedures = await _context.Procedures
                .Include(p => p.Checklist)
                .FirstOrDefaultAsync(m => m.ProceduresId == id);
            if (procedures == null)
            {
                return NotFound();
            }

            return View(procedures);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var procedures = await _context.Procedures.FindAsync(id);
            _context.Procedures.Remove(procedures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProceduresExists(string id)
        {
            return _context.Procedures.Any(e => e.ProceduresId == id);
        }
    }
}
