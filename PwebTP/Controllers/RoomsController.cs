using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;
using PwebTP.ViewModels;

namespace PwebTP.Controllers
{
    public class RoomsController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Rooms
        [AllowAnonymous]
        [Authorize(Roles = "Administrator,Manager,Employee,Client")]
        public async Task<IActionResult> Catalog()
        {
            var applicationDBContext = _context.Rooms.Include(r => r.Host);
            return View(await applicationDBContext.ToListAsync());
        }
        
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ManagePortfolio()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var applicationDBContext = _context.Rooms.Include(r => r.Host).Where(r => r.HostId == userId);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        [AllowAnonymous]
        [Authorize(Roles = "Administrator,Manager,Employee,Client")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var rooms = await _context.Rooms
                .Include(r => r.Host)
                .FirstOrDefaultAsync(m => m.RoomsId == id);
            if (rooms == null)
            {
                return NotFound();
            }

            
            //var Media = await _context.Media.Where(m => m.RoomId == id).ToListAsync();
            var reviews = await _context.Reviews.Include(r => r.Reviewer).Include(r => r.Room).Where(r => r.RoomId == id).ToListAsync();
            

            RoomDetailsViewModel roomDetails = new RoomDetailsViewModel()
            {
                Room = rooms,
                RoomReviews = reviews

            };

            return View(roomDetails);
        }


        // GET: Rooms/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
           ViewData["RoomType"] = new SelectList(new[] {"Shared Room","Private Room","Residence"});
           return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Title,RoomType,CheckIn,CheckOut,NumberOfGuests,NumberofRooms,NumberofBathrooms,PricePerNight,CityName,PostalCode,StreetName,StreetNumber,CountryName,Latitude,Longitude,Description,CoverImg,HasInternet,HasDailyCleaning,HasCleaningProducts,HasFreeParking,HasAirConditioner,HasHeating,HasTv")] Rooms rooms)
        {

            if(rooms.CoverImg == null)
            {
                ModelState.AddModelError("Null", "Please upload the Cover Image");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    if (rooms.CoverImg != null)
                    {
                        string folder = "images/";
                        folder += Guid.NewGuid().ToString() + "_" + rooms.CoverImg.FileName;

                        rooms.CoverImgPath = "/" + folder;

                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        await rooms.CoverImg.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    }

                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    rooms.HostId = currentUser.Id;

                   
                    _context.Add(rooms);
                    await _context.SaveChangesAsync();

                    Checklist checklistFase1 = new Checklist()
                    {
                        RoomId = rooms.RoomsId,
                        Name = "Deliver"
                    };

                    Checklist checklistFase2 = new Checklist()
                    {
                        RoomId = rooms.RoomsId,
                        Name = "Receiver"
                    };

                    _context.Add(checklistFase1);
                    _context.Add(checklistFase2);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("ManagePortfolio");
                }
            }
           
            ViewData["RoomType"] = new SelectList(new[] { "Shared Room", "Private Room", "Residence" });
            return View(rooms);
        }



        // GET: Rooms/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }
            ViewData["RoomType"] = new SelectList(new[] { "Shared Room", "Private Room", "Residence" });
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(string id, [Bind("RoomsId,HostId,Title,RoomType,CheckIn,CheckOut,NumberOfGuests,NumberofRooms,NumberofBathrooms,PricePerNight,CityName,PostalCode,StreetName,StreetNumber,CountryName,Latitude,Longitude,Description,CoverImgPath,HasInternet,HasDailyCleaning,HasCleaningProducts,HasFreeParking,HasAirConditioner,HasHeating,HasTv")] Rooms rooms)
        {
            

            if (id != rooms.RoomsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
            

                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.RoomsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManagePortfolio));
            }

            
            ViewData["RoomType"] = new SelectList(new[] { "Shared Room", "Private Room", "Residence" });
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.Host)
                .FirstOrDefaultAsync(m => m.RoomsId == id);
            if (rooms == null)
            {
                return NotFound();
            }
            
            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rooms = await _context.Rooms.FindAsync(id);
            var reservations = await _context.Reservations.Where(r => r.RoomId == id).ToListAsync();
            var checklists = await _context.Checklist.Where(r => r.RoomId == id).ToListAsync();
            var procedures = await _context.Procedures.Where(p => p.Checklist.RoomId == id).ToListAsync();

            foreach(var item in procedures)
            {
                _context.Procedures.Remove(item);
            }

            foreach (var item in checklists)
            {
                _context.Checklist.Remove(item);
            }

            foreach (var item in reservations)
            {
                _context.Reservations.Remove(item);
            }
            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManagePortfolio");
        }

        private bool RoomsExists(string id)
        {
            return _context.Rooms.Any(e => e.RoomsId == id);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
