using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Abstract;

public interface INaprawyDbStorage
{
    List<DictionaryItem> GetAllMaintenancesDictionary();
    List<PartDictionaryItem> GetAllPartsDictionary();
    Task<List<VehicleModel>> GetAllVehicles();
    Task<VehicleModel> GetVehicleById(int vehicleId);
    Task CreateOrUpdateVehicle(VehicleModel vehicleModel);        
    Task<string> DeleteVehicle(int vehicleId);
}
