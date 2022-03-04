using angularAuthorizationExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Abstract;

public interface INaprawyDbStorage
{
    #region Dictionaries
    List<DictionaryItem> GetAllMaintenancesDictionary();
    List<PartDictionaryItem> GetAllPartsDictionary();
    #endregion

    #region Vehicles
    Task<List<VehicleModel>> GetAllVehicles();
    Task<VehicleModel> GetVehicleById(int vehicleId);
    Task CreateOrUpdateVehicle(VehicleModel vehicleModel);        
    Task<string> DeleteVehicle(int vehicleId);
    #endregion

    #region Maintenances
    Task<List<MaintenanceModel>> GetAllMaintenances(int VehicleId);
    Task<MaintenanceModel> GetMaintenanceById(int maintenanceId);
    Task<List<MaintenanceDetailsModel>>GetMaintenanceDetails(int maintenanceId);
    Task CreateOrUpdateMaintenance(MaintenanceModel maintenance);

    Task<string> DeleteMaintenance(int maintenanceId);
    #endregion
}
