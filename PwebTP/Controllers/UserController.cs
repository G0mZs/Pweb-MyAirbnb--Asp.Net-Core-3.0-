using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PwebTP.Data;
using PwebTP.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PwebTP.Controllers
{
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Employees()
        {
            
            var employees = await  _userManager.Users.Where(u => u.ManagerId.Equals(GetUserId())).ToListAsync();

            return View(employees);
          
        }

        // GET: /Clients/
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Clients()
        {
          
            var clients = await _userManager.GetUsersInRoleAsync("Client");
               
            return View(clients);
          
        }


        // GET: /Owners/
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Owners()
        {
            var clients = await _context.Users.Where(u => u.Role == "Manager" || u.Role == "Employee").ToListAsync();

            return View(clients);
        }

        // GET: /User/Details/
        [Authorize(Roles = "Administrator,Manager,Employee,Client")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                id = GetUserId();
            }

            var applicationUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Users/CreateEmployee
        [Authorize(Roles = "Manager")]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        // POST: Users/CreateEmployee
        //Faltam restrições de criação
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,PasswordHash")] ApplicationUser users)
        {
            if (ModelState.IsValid)
            {
                
                    if (users.Email == null || users.Name == null || users.PhoneNumber == null || users.PasswordHash == null)
                    {
                        ModelState.AddModelError("Null", "Please fill all the fields below.");
                    }
                    else
                    {
                        var auxUser = await _userManager.FindByEmailAsync(users.Email);

                        if (auxUser != null)
                        {
                            ModelState.AddModelError("Null", "This Email is already in use.");
                        }
                        else {


                            var user = new ApplicationUser { UserName = users.Email, Email = users.Email, PhoneNumber = users.PhoneNumber, Name = users.Name, Role = "Employee", ManagerId = GetUserId() };
                            var result = await _userManager.CreateAsync(user, users.PasswordHash);

                            await _userManager.AddToRoleAsync(user, user.Role);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Employees");

                        }
                        

                    }
                
             
            }
            return View("CreateEmployee",users);
        }





        // GET: User/Edit/5
        [Authorize(Roles = "Administrator,Manager,Client")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager,Client")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,UserName,Email,PhoneNumber")] ApplicationUser applicationUser)
        {
            if (id == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                
                var existItem = await _context.Set<ApplicationUser>().FindAsync(applicationUser.Id);
                if (existItem == null)
                {
                    return NotFound();
                }
                else
                {
                    var checkEmail = await _userManager.FindByEmailAsync(applicationUser.Email);
                    var checkUsername = await _userManager.FindByNameAsync(applicationUser.UserName);

                    if (checkUsername == null || checkUsername.Id.Equals(applicationUser.Id))
                    {
                        if (checkEmail == null || checkEmail.Id.Equals(applicationUser.Id))
                        {
                            _context.Entry(existItem).Property("Name").CurrentValue = applicationUser.Name;
                            _context.Entry(existItem).Property("UserName").CurrentValue = applicationUser.UserName;
                            _context.Entry(existItem).Property("Email").CurrentValue = applicationUser.Email;
                            _context.Entry(existItem).Property("PhoneNumber").CurrentValue = applicationUser.PhoneNumber;
                            var result = await _context.SaveChangesAsync();
                            return RedirectToAction("Details", new { id = applicationUser.Id });
                        }
                        else
                        {
                            ModelState.AddModelError("Null", "This email is already in use");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Null", "This username is already in use");
                    }
                    
                }
                
            }

            return View(applicationUser);
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.Users.FindAsync(id);

            var rooms = await _context.Rooms.Where(r => r.HostId == applicationUser.Id).ToListAsync();
         
            foreach(var item in rooms)
            {
                var reservations = await _context.Reservations.Where(r => r.RoomId == item.RoomsId).ToListAsync();

                foreach(var item1 in reservations)
                {
                    _context.Reservations.Remove(item1);
                }

                var procedures = await _context.Procedures.Where(p => p.Checklist.RoomId == item.RoomsId).ToListAsync();

                foreach(var item2 in procedures)
                {
                    _context.Procedures.Remove(item2);
                }

                var checklists = await _context.Checklist.Where(c => c.RoomId == item.RoomsId).ToListAsync();

                foreach(var item3 in checklists)
                {
                    _context.Checklist.Remove(item3);
                }

                _context.Rooms.Remove(item);
            }

            var employees = await _context.Users.Where(u => u.ManagerId == applicationUser.Id).ToListAsync();

            foreach(var emp in employees)
            {
                _context.Users.Remove(emp);
            }

            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();

            if(User.IsInRole("Administrator"))
            {
                
                return RedirectToAction(nameof(Owners));
            }
            else
            {
                return RedirectToAction(nameof(Employees));
            }
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

