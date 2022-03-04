using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class PartsDictionaryController : ControllerBase
{
    private readonly ILogger<PartsDictionaryController> _logger;
    
    private readonly INaprawyDbStorage _dbStorage;

    public PartsDictionaryController(ILogger<PartsDictionaryController> logger, INaprawyDbStorage dbStorage)
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
