using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class MaintenancesController : ControllerBase
{
    private readonly ILogger<MaintenancesController> _logger;
    
    private readonly INaprawyDbStorage _dbStorage;

    public MaintenancesController(ILogger<MaintenancesController> logger, INaprawyDbStorage dbStorage)
    {
        _logger = logger;
        _dbStorage = dbStorage;        
    }

    [HttpGet]
    public IEnumerable<DictionaryItem> GetAllMaintenances()
    {
        
        try
        {
            return _dbStorage.GetAllMaintenancesDictionary();

        }
        catch(Exception ex)
        {
            _logger.LogError($"Exception in {nameof(GetAllMaintenances)}.", ex);
        }      
        return new List<DictionaryItem>();  
    }
}
