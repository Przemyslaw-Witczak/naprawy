using System.Diagnostics;
using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> GetAllVehicles()
        {
            
            try
            {
                return Ok(await _dbStorage.GetAllVehicles());

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetAllVehicles)}.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"There was exception while receiving data by '{nameof(_dbStorage.GetAllVehicles)}' from database: {ex.Message}");
            }                   
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleById(int vehicleId)
        {
            
            try
            {
                Task<VehicleModel> vehicleToReturn;
                if (vehicleId>0)
                    vehicleToReturn = _dbStorage.GetVehicleById(vehicleId);
                else
                {
                    vehicleToReturn = Task.Factory.StartNew(()=>
                    {
                        var vehicleModel = new VehicleModel()
                        {
                            Distance = 0,
                            Mileage = 0,
                            PurchaseDate = DateTime.Now,
                            ProductionYear = Convert.ToInt16(DateTime.Now.Year)
                        };
                        return vehicleModel;
                    });
                }
                    
                return Ok(await vehicleToReturn);
            }
            catch(AggregateException aex)
            {
                _logger.LogError($"Exception in {nameof(GetVehicleById)}.", aex);                
                return StatusCode(StatusCodes.Status405MethodNotAllowed, $"There was exception while receiving data by '{nameof(_dbStorage.GetVehicleById)}'for vehicle {vehicleId}. {aex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetVehicleById)}.", ex);                
                return StatusCode(StatusCodes.Status405MethodNotAllowed, $"There was exception in {nameof(GetVehicleById)} while receiving data by '{nameof(_dbStorage.GetVehicleById)}'for vehicle {vehicleId}. {ex.Message}");
            }                  
        }
    
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> SaveVehicle([FromBody]VehicleModel vehicle)
        {
            Debug.WriteLine($"Changed vehicle {vehicle?.ToString()}");
            _logger.LogDebug($"Saving vehicle. {vehicle?.ToString()}");
            try
            {          
                if (vehicle==null)
                    return BadRequest();
                await _dbStorage.CreateOrUpdateVehicle(vehicle);
                //Tu można dodać sprawdzenie.. dlaczego nie można dodać pojazdu i zwrócić inny błąd                 
                // {
                //     ModelState.AddModelError("email", "Employee email already in use");
                //     return BadRequest(ModelState);
                // }       
                return CreatedAtAction(nameof(SaveVehicle), new {id=vehicle.Id}, vehicle);
            }
            catch(Exception ex)
            {
                var errorMessage = $"Error saving changes to Vehicle={vehicle?.ToString()}.";
                _logger.LogError(errorMessage, ex);
                return StatusCode(StatusCodes.Status405MethodNotAllowed, errorMessage);
            }
        }

        [HttpDelete("{vehicleId:int}")]
        public async Task<ActionResult<string>> DeleteVehicle(int vehicleId)
        {
            try
            {
                if (vehicleId<1)
                    return BadRequest();
                // var employeeToDelete = await employeeRepository.GetEmployee(id);

                var message = await _dbStorage.DeleteVehicle(vehicleId);
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
                    $"Error deleting data of vehicle {vehicleId}");
            }
        }
    }
}