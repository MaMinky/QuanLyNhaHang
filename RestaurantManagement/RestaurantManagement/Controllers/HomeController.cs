using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.Filters;
using System.Linq;
using RestaurantManagement.DAL;
using System.Globalization;
using System.Text;

namespace RestaurantManagement.Controllers;
    [Authorize]
    [SessionAuthorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RestaurantContext _context;

    public HomeController(ILogger<HomeController> logger, RestaurantContext context)
    {
        _logger = logger;
        _context = context;
    }
    public static class StringHelper
    {
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC)
                .Replace("đ", "d").Replace("Đ", "D");
        }
    }
    public IActionResult Index()
    {
        var dishes = _context.Dishes.ToList();

        // Chia món ăn thành các danh mục
        ViewBag.WaterFoods = dishes.Where(d => d.Category == "WaterFood").ToList();
        ViewBag.DryFoods = dishes.Where(d => d.Category == "DryFood").ToList();
        ViewBag.Combos = dishes.Where(d => d.Category == "Combo").ToList();
        ViewBag.Desserts = dishes.Where(d => d.Category == "Dessert").ToList();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
