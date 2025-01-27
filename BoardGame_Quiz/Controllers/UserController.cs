using BoardGame_Quiz.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame_Quiz.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserController : Controller
    {
        // Form page to get user details
        public IActionResult UserDetails()
        {
            return View();
        }

        // Handle form submission
        [HttpPost]
        public IActionResult UserDetails(User model)
        {
            if (ModelState.IsValid)
            {
                // Redirect to category selection on successful validation
                return RedirectToAction("SelectCategory");
            }

            // Reload the form with validation errors
            return View(model);
        }

        // Page to select categories
        public ActionResult SelectCategory()
        {
            return View();
        }

        // Handle category selection
        [HttpPost]
        public IActionResult SelectCategory(string category)
        {
            // Redirect to difficulty selection based on the chosen category
            TempData["Category"] = category;
            return RedirectToAction("SelectDifficulty");
        }

        // Page to select difficulty level
        public IActionResult SelectDifficulty()
        {
            ViewBag.Category = TempData["Category"];
            return View();
        }
    }
}
