using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController
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
    }
}