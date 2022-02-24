//[Authorize]
using angularAuthorizationExample;
using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Data;
using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class DictionariesController : ControllerBase
{
    private readonly ILogger<DictionariesController> _logger;
    
    private readonly INaprawyDbStorage _dbStorage;

    public DictionariesController(ILogger<DictionariesController> logger, INaprawyDbStorage dbStorage)
    {
        _logger = logger;
        _dbStorage = dbStorage;        
    }

    [HttpGet]
    public IEnumerable<DictionaryItem> GetAllCzynnosci()
    {
        
        try
        {
            return _dbStorage.GetAllCzynnosci();

        }
        catch(Exception ex)
        {
            _logger.LogError($"Exception in {nameof(GetAllCzynnosci)}.", ex);
        }      
        return new List<DictionaryItem>();  
    }
}
