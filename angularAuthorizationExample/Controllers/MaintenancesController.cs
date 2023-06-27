using System.Diagnostics;
using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
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
                var vehicleIdFilter = new FilterMaintenancesModel() { VehicleId = vehicleId };
                return Ok(await _dbStorage.GetFilteredMaintenances(vehicleIdFilter));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllMaintenances)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"There was exception while receiving maintenances for vehicleId={vehicleId} by '{nameof(_dbStorage.GetFilteredMaintenances)}' from database: {ex.Message}");
            }                   
        }

        [HttpPost]
        [Route("GetFilteredMaintenances")]
        public async Task<ActionResult<MaintenanceModel[]>> GetFilteredMaintenances([FromBody] FilterMaintenancesModel maintenancesFilter)
        {
            //FilterMaintenancesModel maintenancesFilter = null;
            try
            {
                return Ok(await _dbStorage.GetFilteredMaintenances(maintenancesFilter));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllMaintenances)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"There was exception while receiving maintenances for vehicleId={maintenancesFilter.VehicleId} by '{nameof(_dbStorage.GetFilteredMaintenances)}' from database: {ex.Message}");
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

        [HttpPost]
        public async Task<ActionResult<MaintenanceModel>> SaveMaintenance([FromBody] MaintenanceModel maintenance)
        {
            Debug.WriteLine($"Changed maintenance {maintenance?.ToString()}");
            _logger.LogDebug($"Saving maintenance. {maintenance?.ToString()}");
            try
            {
                if (maintenance == null)
                    return BadRequest();
                await _dbStorage.CreateOrUpdateMaintenance(maintenance);

                return CreatedAtAction(nameof(SaveMaintenance), new { id = maintenance.Id }, maintenance);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error saving changes to Maintenance={maintenance?.ToString()}.";
                _logger.LogError(errorMessage, ex);
                return StatusCode(StatusCodes.Status405MethodNotAllowed, errorMessage);
            }
        }
    }
}