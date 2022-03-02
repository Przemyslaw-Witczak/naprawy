using System.Diagnostics;
using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
    
        private readonly INaprawyDbStorage _dbStorage;

        public VehiclesController(ILogger<VehiclesController> logger, INaprawyDbStorage dbStorage)
        {
            _logger = logger;
            _dbStorage = dbStorage;        
        }

        [HttpGet]        
        public IEnumerable<VehicleModel> GetAllVehicles()
        {
            
            try
            {
                return _dbStorage.GetAllVehicles();

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllVehicles)}.", ex);
            }      
            return new List<VehicleModel>();  
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public VehicleModel GetVehicleById(int vehicleId)
        {
            
            try
            {
                if (vehicleId>0)
                    return _dbStorage.GetVehicleById(vehicleId);
                else
                    return new VehicleModel()
                    {                                              
                        Distance = 0,
                        Mileage = 0,
                        PurchaseDate = DateTime.Now,
                        ProductionYear = Convert.ToInt16(DateTime.Now.Year)
                    };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetVehicleById)}.", ex);
                var result = Content(ex.Message
                , "application/json; charset=utf-8");
                HttpContext.Response.StatusCode = 405; //method not allowed
                return null;
            }                  
        }
    
        [HttpPost]
        public ActionResult<VehicleModel> SaveVehicle([FromBody]VehicleModel vehicle)
        {
            _logger.LogDebug("Saving vehicle.");
            try
            {          
            _dbStorage.CreateOrUpdateVehicle(vehicle);
            Debug.WriteLine($"Changed vehicle id={vehicle.Id}");            
            return CreatedAtAction(nameof(GetVehicleById), new {id=vehicle.Id}, vehicle);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error saving changes to Vehicle={vehicle?.ToString()}.", ex);
                var result = Content(ex.Message
                , "application/json; charset=utf-8");
                HttpContext.Response.StatusCode = 405;
                return result;
            }
        }

        // [HttpPost]
        // public void SaveVehicle([FromBody]string message)
        // {
        //     Console.WriteLine($"Dupa: {message}");
        //     //return CreatedAtAction(nameof(GetVehicleById), new {echo=message}, message);
        // }
    }
}