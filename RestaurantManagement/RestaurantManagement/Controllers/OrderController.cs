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
    public class OrderController : Controller
    {
        private readonly RestaurantContext _context;
        public OrderController(RestaurantContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Dishes)
                .ToList();
            return View(orders);
        }
    }
}
