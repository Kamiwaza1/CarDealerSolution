using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using CarDealer.Core.Services;

namespace CarDealer.Web.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ICarService _carService;
        
        public IndexModel(ICarService carService)
        {
            _carService = carService;   
            Cars = new List<Car>(); 
        }
        public List<Car> Cars { get; set; } = new();

        public void OnGet()
        {
            Cars = _carService.GetAllCars();
        }
    }
}
