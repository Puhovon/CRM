using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Data;
using WebApplication2.DTO;

namespace WebApplication2.Pages
{
    public class TanksModel : PageModel
    {
        public List<Data.Tank> Tanks { get; private set; }  
        public void OnGet()
        {
            Tanks = new List<Data.Tank>();
            var jsonData = System.IO.File.ReadAllText(@"./Data/Tanks.json");
            Console.WriteLine(jsonData);
            var tanks = JsonSerializer.Deserialize<TankData>(jsonData);
            Tanks = tanks.Data;
        }
    }
    public class TankData
    {
        public List<Data.Tank> Data { get; set; }
    }
}
