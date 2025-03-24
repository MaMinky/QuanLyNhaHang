using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL;
using RestaurantManagement.Models;
using RestaurantManagement.Filters;

namespace RestaurantManagement.Controllers
{
    [Authorize]
    [SessionAuthorize]
    public class BookingTableController : Controller
    {
        private readonly RestaurantContext _context;

        public BookingTableController(RestaurantContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.BookingTables
                .Include(b => b.User)
                .ToList();
            return View(bookings);
        }
    }
}
