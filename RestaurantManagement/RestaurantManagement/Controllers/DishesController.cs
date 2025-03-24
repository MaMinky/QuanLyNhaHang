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
    public class DishesController : Controller
    {
        private readonly RestaurantContext _context;

        public DishesController(RestaurantContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var dishes = _context.Dishes.ToList();
            return View(dishes);
        }
    }
}
