using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Contexts;
using WebApplication2.DTO;

namespace WebApplication2.Pages
{
    public class EditFactoryModel : PageModel
    {
        [BindProperty]
        public Factory Factory { get; set; }
        private TestContext _context;

        public EditFactoryModel(TestContext context)
        {
            _context = context;
        }
       
        public IActionResult OnGet(int id)
        {
            Factory = _context.Factories.FirstOrDefault(f => f.Id == id);
            if (Factory == null) {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) { 
                return Page();
            }

            var factoryToUpdate = _context.Factories.FirstOrDefault(f => f.Id == Factory.Id);
            if (factoryToUpdate == null) {
                return NotFound();
            }

            Console.WriteLine(Factory.Name + " " + Factory.Production + " " + Factory.Description);

            factoryToUpdate.Name = Factory.Name;
            factoryToUpdate.Description = Factory.Description;
            factoryToUpdate.Production = Factory.Production;
            
            _context.Factories.Update(factoryToUpdate);
            _context.SaveChanges();
            return RedirectToPage("/Factories");
        }
    }
}
