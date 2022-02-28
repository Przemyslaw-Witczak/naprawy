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
                return _dbStorage.GetVehicleById(vehicleId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllVehicles)}.", ex);
            }      
            return null;  
        }
    
        [HttpPost]
        public ActionResult<VehicleModel> SaveVehicle([FromBody]VehicleModel vehicle)
        {
            _dbStorage.CreateOrUpdateVehicle(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new {id=vehicle.Id}, vehicle);
        }

        // [HttpPost]
        // public void SaveVehicle([FromBody]string message)
        // {
        //     Console.WriteLine($"Dupa: {message}");
        //     //return CreatedAtAction(nameof(GetVehicleById), new {echo=message}, message);
        // }
    }
}