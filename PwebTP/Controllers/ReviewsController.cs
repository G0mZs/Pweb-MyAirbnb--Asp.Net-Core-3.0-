using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;

namespace PwebTP.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ReviewsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> ClientHistory()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var client = await _userManager.FindByIdAsync(userId);
            var applicationDBContext = _context.Reviews.Include(r => r.Reviewer).Where(r => r.ReviewerId == client.Id);
            return View(await applicationDBContext.ToListAsync());
        }

     
        // GET: Reviews/Create
        public IActionResult Create(string id)
        {
            ViewData["Id"] = id;
            
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,LocationRating,UserRating,EmployeeRating,RoomRating,Comment")] Reviews reviews)
        {
            double total = (reviews.UserRating + reviews.RoomRating + reviews.LocationRating + reviews.EmployeeRating) / 4;
            reviews.ReviewerId = GetUserId();
            
            var Room = await _context.Rooms.FindAsync(reviews.RoomId);
            var Reservations = await _context.Reservations.Where(r => r.ClientId == reviews.ReviewerId && r.RoomId == reviews.RoomId).ToListAsync();
            var Reviews = await _context.Reviews.Where(r => r.RoomId == reviews.RoomId && r.ReviewerId == reviews.ReviewerId).ToListAsync();
            reviews.TotalRating = (int)Math.Round(total);

            if (!checkIfAccepted(Reservations)){
                ModelState.AddModelError("null", "You dont have a accepted reservation for this room");
            }
            else
            {
                if(Reviews.Count > 0)
                {
                    ModelState.AddModelError("null", "You already made a comment about this room");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        reviews.Room = Room;
                        _context.Add(reviews);
                        await _context.SaveChangesAsync();

                    }
                }
                
            }
           

            ViewData["Id"] = reviews.RoomId;
            return View(reviews);
        }

        public Boolean checkIfAccepted(List<Reservations> reservations)
        {
            foreach(var item in reservations)
            {
                if(item.ReservationState == "Accepted")
                {
                    return true;
                }
            }

            return false;
        }

        private bool ReviewsExists(string id)
        {
            return _context.Reviews.Any(e => e.ReviewsId == id);
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
