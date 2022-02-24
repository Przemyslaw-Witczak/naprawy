using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class PartsController : ControllerBase
{
    private readonly ILogger<PartsController> _logger;
    
    private readonly INaprawyDbStorage _dbStorage;

    public PartsController(ILogger<PartsController> logger, INaprawyDbStorage dbStorage)
    {
        _logger = logger;
        _dbStorage = dbStorage;        
    }

    [HttpGet]
    public IEnumerable<PartDictionaryItem> GetAllParts()
    {
        
        try
        {
            return _dbStorage.GetAllPartsDictionary();

        }
        catch(Exception ex)
        {
            _logger.LogError($"Exception in {nameof(GetAllParts)}.", ex);
        }      
        return new List<PartDictionaryItem>();  
    }
}
