using angularAuthorizationExample.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MaintenancesDetailsController: ControllerBase
    {
        private readonly ILogger<MaintenancesController> _logger;
    
        private readonly INaprawyDbStorage _dbStorage;

        public MaintenancesDetailsController(ILogger<MaintenancesController> logger, INaprawyDbStorage dbStorage)
        {
            _logger = logger;
            _dbStorage = dbStorage;        
        }

        [HttpGet]       
        [Route("{maintenanceId}")] 
        public async Task<ActionResult> GetMaintenanceDetails(int maintenanceId)
        {
            
            try
            {
                return Ok(await _dbStorage.GetMaintenanceDetails(maintenanceId));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetMaintenanceDetails)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"There was exception while receiving maintenances for maintenance id={maintenanceId} by '{nameof(_dbStorage.GetMaintenanceById)}' from database: {ex.Message}");
            }                   
        }

    }
}