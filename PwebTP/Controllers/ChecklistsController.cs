using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;
using PwebTP.ViewModels;

namespace PwebTP.Controllers
{
    public class ChecklistsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChecklistsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Checklists
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Checklist.Include(c => c.Room);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Manage

        public async Task<IActionResult> ManageChecklists(string id)
        {
           
            var applicationDBContext = _context.Checklist.Include(c => c.Room).Where(c => c.RoomId == id);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Checklists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklist
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.ChecklistId == id);
            if (checklist == null)
            {
                return NotFound();
            }

            var procs = await _context.Procedures.Where(p => p.ChecklistId == checklist.ChecklistId).ToListAsync();

            CheckListProcedureViewModel checkListProcedureViewModel = new CheckListProcedureViewModel
            {
                checklist = checklist,
                procedures = procs
            };

            return View(checkListProcedureViewModel);
        }

        // GET: Checklists/Edit/5
        public async Task<IActionResult> FillDeliverChecklist(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await  _context.Checklist.Where(c => c.RoomId == id && c.Name == "Deliver").FirstOrDefaultAsync();
            if (checklist == null)
            {
                return NotFound();
            }

            var proc = await _context.Procedures.Where(p => p.ChecklistId == checklist.ChecklistId).ToListAsync();
            if(proc == null)
            {
                return NotFound();
            }

            CheckListProcedureViewModel checkListProcedureViewModel = new CheckListProcedureViewModel()
            {
                checklist = checklist,
                procedures = proc
            };

           
            return View(checkListProcedureViewModel);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FillDeliverChecklist(string id, [Bind("ChecklistId,RoomId,Name,Description")] Checklist checklist,[Bind("ProceduresId,ChecklistId,ProcedureName,ProcedureResult")]List<Procedures> procedures)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(var item in procedures)
                    {
                        _context.Update(procedures);
                    }
                    _context.Update(checklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistExists(checklist.ChecklistId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("~/Views/Reservations/ListReservations");
            }
            
            return View(checklist);
        }

        private bool ChecklistExists(string id)
        {
            return _context.Checklist.Any(e => e.ChecklistId == id);
        }

    }
}
