using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDApplication.Models;
using static CRUDApplication.Helper;
using Microsoft.AspNetCore.Diagnostics;

namespace CRUDApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerDbContext _context;

        public HomeController(CustomerDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            try
            {
                return _context.Customers != null ?
                    View(await _context.Customers.ToListAsync()) :
                    Problem("Entity set 'CustomerDbContext.Customers' is null.");
            }
            catch (Exception ex)
            {
                // Log exception here
                return RedirectToAction("Error");
            }
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            try
            {
                if (id == 0)
                    return View(new Customer());
                else
                {
                    var customer = await _context.Customers.FindAsync(id);
                    if (customer == null)
                    {
                        return NotFound();
                    }
                    return View(customer);
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                return RedirectToAction("Error");
            }
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }
            catch (Exception ex)
            {
                // Log exception here
                return RedirectToAction("Error");
            }
        }

        // POST: Home/AddOrEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,GenderId,StateId,DistrictId")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _context.Add(customer);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        try
                        {
                            _context.Update(customer);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!CustomerExists(customer.Id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", _context.Customers.ToList()) });
                }
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", customer) });
            }
            catch (Exception ex)
            {
                // Log exception here
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Error") });
            }
        }


        [NoDirectAccess]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Customers == null)
                {
                    return Json(new { isValid = false });
                }
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
                return Json(new { isValid = true });
            }
            catch (Exception ex)
            {
                // Log exception here
                return Json(new { isValid = false });
            }
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Error action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var errorModel = exception?.Error;

            if (HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                return View("ErrorDevelopment", errorModel);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
