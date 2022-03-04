using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class MaintenancesDictionaryController : ControllerBase
{
    private readonly ILogger<MaintenancesDictionaryController> _logger;
    
    private readonly INaprawyDbStorage _dbStorage;

    public MaintenancesDictionaryController(ILogger<MaintenancesDictionaryController> logger, INaprawyDbStorage dbStorage)
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
