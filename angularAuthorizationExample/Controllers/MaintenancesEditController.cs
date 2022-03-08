using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MaintenancesEditController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
    
        private readonly INaprawyDbStorage _dbStorage;

        public MaintenancesEditController(ILogger<VehiclesController> logger, INaprawyDbStorage dbStorage)
        {
            _logger = logger;
            _dbStorage = dbStorage;        
        }

        [HttpGet]
        [Route("{maintenanceId}")]
        public async Task<ActionResult<MaintenanceModel>> GetMaintenanceWithDetailsById(int maintenanceId)
        {
            
            try
            {
                Task<MaintenanceModel> maintenanceToReturn;
                maintenanceToReturn = Task.Factory.StartNew(()=>
                {
                    MaintenanceModel maintenanceModel;
                    if (maintenanceId>0)
                    {
                        maintenanceModel =  _dbStorage.GetMaintenanceById(maintenanceId).Result;
                        maintenanceModel.MaintenanceDetailsList = _dbStorage.GetMaintenanceDetails(maintenanceId).Result;
                    }
                    else
                    {
                        maintenanceModel = new MaintenanceModel()
                        {
                            Distance = 0,
                            Mileage = 0,
                            MaintenanceDate = DateTime.Now,
                            MaintenanceDetailsList = new List<MaintenanceDetailsModel>()
                        };
                    }
                    return maintenanceModel;
                });
                
                    
                return Ok(await maintenanceToReturn);
            }
            catch(AggregateException aex)
            {
                _logger.LogError($"Exception in {nameof(GetMaintenanceWithDetailsById)}.", aex);                
                return StatusCode(StatusCodes.Status405MethodNotAllowed, $"There was exception while receiving data by '{nameof(GetMaintenanceWithDetailsById)}'for maintenanceId={maintenanceId}. {aex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception in {nameof(GetMaintenanceWithDetailsById)}.", ex);                
                return StatusCode(StatusCodes.Status405MethodNotAllowed, $"There was exception in {nameof(GetMaintenanceWithDetailsById)} while receiving data for maintenanceId={maintenanceId}. {ex.Message}");
            }                  
        }
    }
}