using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;

namespace PwebTP.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private UserManager<ApplicationUser> _userManager;


        public ReservationsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: All Reservations
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ListReservations()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var employee = await _userManager.FindByIdAsync(userId);
            var applicationDBContext = _context.Reservations.Include(r => r.Client).Include(r => r.Room).Where(r => r.Room.HostId.Equals(employee.ManagerId));
            foreach(var item in applicationDBContext)
            {

            }
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Client Reservations
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var applicationDBContext = _context.Reservations.Include(r => r.Client).Include(r => r.Room).Where(r => r.ClientId == userId);
            return View(await applicationDBContext.ToListAsync());
        }



        // GET: Reservations/Details/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationsId == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "Client")]
        public IActionResult Create(string id)
        {
            ViewData["RoomId"] = id;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([Bind("RoomId,BeginsAt,EndsAt,GuestsNumber")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                var ReservationRoom = await _context.Rooms.FindAsync(reservations.RoomId);
                var AllReservations = await _context.Reservations.Where(r => r.RoomId == reservations.RoomId).ToListAsync();

                var availableDates = checkAvailableDates(AllReservations, reservations.BeginsAt, reservations.EndsAt);

                if (availableDates == true)
                {
                    if (reservations.GuestsNumber > ReservationRoom.NumberOfGuests)
                    {
                        ModelState.AddModelError("null", "The number of guests exceeds the limit of this room");
                    }
                    else if (reservations.EndsAt < reservations.BeginsAt)
                    {
                        ModelState.AddModelError("null", "The Reservation End date must be after the Start Date");
                    }
                    else if (reservations.BeginsAt.Date == reservations.EndsAt.Date)
                    {
                        ModelState.AddModelError("null", "The Reservation End Date and Start Date can't be in the same day");
                    }
                    else if (reservations.BeginsAt < DateTime.Now || reservations.EndsAt < DateTime.Now)
                    {
                        ModelState.AddModelError("null", "The Dates you introduced must be after the actual DateTime");
                    }
                    else
                    {

                        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                        reservations.ClientId = currentUser.Id;
                        reservations.ReservationState = "Pending";

                        int totalDays = (reservations.EndsAt - reservations.BeginsAt).Days;
                        reservations.TotalCost = ReservationRoom.PricePerNight * totalDays;

                        _context.Add(reservations);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["RoomId"] = reservations.RoomId;
            return View(reservations);
        }

        public Boolean checkAvailableDates(List<Reservations> reservations,DateTime StartTime,DateTime EndDate)
        {
            List<DateTime> ReservedDate = new List<DateTime>();
            Boolean check = true;
            foreach(var item in reservations)
            {

                if (StartTime >= item.BeginsAt && StartTime <= item.EndsAt && item.ReservationState.Equals("Accepted") || item.ReservationState.Equals("Delivered To Client") || EndDate >= item.BeginsAt && EndDate <= item.EndsAt && item.ReservationState.Equals("Accepted") || item.ReservationState.Equals("Delivered To Client"))
                {
                    ReservedDate.Add(item.BeginsAt);
                    ReservedDate.Add(item.EndsAt);
                    ModelState.AddModelError("null", "There is already a reservation beetween " + ReservedDate[0] + " and " + ReservedDate[1]);
                    ReservedDate.Clear();
                    check = false;
                    
                }

            }

            return check;
        }

        // GET: Reservations/Accept/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Accept(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations.FindAsync(id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);

        }

        // GET: Reservations/Accept/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Accept(string id, [Bind("ReservationsId,ClientId,RoomId,ReservationState,BeginsAt,EndsAt,TotalCost,GuestsNumber")] Reservations reservations)
        {
            if (id != reservations.ReservationsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ReservationRoom = await _context.Rooms.FindAsync(reservations.RoomId);
                    
                    await ChangeReservationState(reservations.BeginsAt, reservations.EndsAt,reservations.ReservationsId,reservations.RoomId);

                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsExists(reservations.ReservationsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListReservations));
            }

            return View(reservations);
        }


        public async Task ChangeReservationState(DateTime StartTime,DateTime EndDate,string id,string idRoom)
        {

            var AllReservations = await _context.Reservations.Where(r => r.RoomId == idRoom).ToListAsync();
            List<Reservations> reservations = AllReservations;

            foreach (var item in reservations)
            {
                
                    if (item.BeginsAt >= StartTime && item.BeginsAt <= EndDate && item.ReservationState.Equals("Pending") || item.EndsAt >= StartTime && item.EndsAt <= EndDate && item.ReservationState.Equals("Pending"))
                    {
                            if (item.ReservationsId == id)
                            {
                                _context.Entry(item).Property("ReservationState").CurrentValue = "Accepted";
                            }
                            else
                            {
                                _context.Entry(item).Property("ReservationState").CurrentValue = "Declined";
                                
                            }

                        await _context.SaveChangesAsync();

                    }
                
            }



            
        }


        // GET: Reservations/Delete/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Decline(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations.FindAsync(id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Decline(string id, [Bind("ReservationsId,ClientId,RoomId,ReservationState,BeginsAt,EndsAt,TotalCost,GuestsNumber")] Reservations reservations)
        {
            if (id != reservations.ReservationsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reservations.ReservationState = "Declined";
                    _context.Update(reservations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsExists(reservations.ReservationsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListReservations));
            }
        
            return View(reservations);
        }


        private bool ReservationsExists(string id)
        {
            return _context.Reservations.Any(e => e.ReservationsId == id);
        }
    }
}
