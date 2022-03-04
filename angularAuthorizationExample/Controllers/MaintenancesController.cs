using angularAuthorizationExample.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MaintenancesController: ControllerBase
    {
        private readonly ILogger<MaintenancesController> _logger;
    
        private readonly INaprawyDbStorage _dbStorage;

        public MaintenancesController(ILogger<MaintenancesController> logger, INaprawyDbStorage dbStorage)
        {
            _logger = logger;
            _dbStorage = dbStorage;        
        }

        [HttpGet]       
        [Route("{vehicleId}")] 
        public async Task<ActionResult> GetAllMaintenances(int vehicleId)
        {
            
            try
            {
                return Ok(await _dbStorage.GetAllMaintenances(vehicleId));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllMaintenances)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"There was exception while receiving maintenances for vehicleId={vehicleId} by '{nameof(_dbStorage.GetAllMaintenances)}' from database: {ex.Message}");
            }                   
        }

    }
}