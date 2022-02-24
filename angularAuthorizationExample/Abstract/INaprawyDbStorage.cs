using angularAuthorizationExample.Models;

namespace angularAuthorizationExample.Abstract;

public interface INaprawyDbStorage
{
    List<DictionaryItem> GetAllMaintenancesDictionary();
    List<PartDictionaryItem> GetAllPartsDictionary();
    List<VehicleModel> GetAllVehicles();
}
