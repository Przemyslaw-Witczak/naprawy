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

        [HttpDelete("{maintenanceId:int}")]
        public async Task<ActionResult<string>> DeleteMaintenance(int maintenanceId)
        {
            try
            {
                if (maintenanceId<1)
                    return BadRequest();
                // var employeeToDelete = await employeeRepository.GetEmployee(id);

                var message = await _dbStorage.DeleteMaintenance(maintenanceId);
                if (!string.IsNullOrEmpty(message))                
                {                     
                    ModelState.AddModelError("komunikat", message);
                    return StatusCode(StatusCodes.Status403Forbidden, ModelState);
                }

                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting maintenance id={maintenanceId}");
            }
        }
    }
}