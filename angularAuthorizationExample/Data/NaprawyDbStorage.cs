using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using FbCoreClientNameSpace;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;

namespace angularAuthorizationExample.Data;

public class NaprawyDbStorage : INaprawyDbStorage
{
    private string _connectionString="";    
    public NaprawyDbStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    #region Dictionaries
    public List<DictionaryItem> GetAllMaintenancesDictionary()    
    {
        var dictionaryValues = new List<DictionaryItem>();
        using (var context = new FbCoreClient(_connectionString))
        {
            context.AddSQL("select id, opis, nieaktywny from czynnosci order by opis collate pxw_plk");
            context.Execute();
            while (context.Read())
            {
                var dictionaryItem = new DictionaryItem()
                {
                    Id = context.GetInt32("id"),
                    Name = context.GetString("opis"),
                    InActive = context.IsDBNull("nieaktywny") ? false : true
                };
                dictionaryValues.Add(dictionaryItem);
            }
        }
        return dictionaryValues;
    }

    public List<PartDictionaryItem> GetAllPartsDictionary()
    {
        var dictionaryValues = new List<PartDictionaryItem>();
        using (var context = new FbCoreClient(_connectionString))
        {
            context.AddSQL("select c.id, c.nazwa, c.nieaktywny, c.paliwo from czesci c order by c.nazwa collate pxw_plk");
            context.Execute();
            while (context.Read())
            {
                var dictionaryItem = new PartDictionaryItem()
                {
                    Id = context.GetInt32("id"),
                    Name = context.GetString("nazwa"),
                    InActive = context.IsDBNull("nieaktywny") ? false : true,
                    IsFuel = context.IsDBNull("paliwo") ? false : true,
                };
                dictionaryValues.Add(dictionaryItem);
            }
        }
        return dictionaryValues;
    }
    #endregion

    #region Vehicles
    public Task<List<VehicleModel>> GetAllVehicles()
    {
        Task<List<VehicleModel>> getAllVehiclesTask = Task.Factory.StartNew(() => {
            var vehicles = new List<VehicleModel>();
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select *from POKAZ_POJAZDY");

                context.Execute();
                while(context.Read())
                {
                    var vehicle = new VehicleModel()
                    {
                        Id = context.GetInt32("id_pojazdy"),
                        Brand = context.GetString("marka"),
                        Type = context.GetString("model"),
                        Vin = context.GetString("vin"),
                        EngineNumber = context.GetString("engine"),
                        RegistrationNumber = context.GetString("rejestracja"),
                        ProductionYear = context.GetInt16("rocznik"),
                        PurchaseDate = context.IsDBNull("data_zakupu") ? null : context.GetDateTime("data_zakupu"),
                        SoldDate = context.IsDBNull("sprzedany") ? null : context.GetDateTime("sprzedany"),
                        AdditionalInfo = context.GetString("adnotacje"),
                        EngineCapacity = context.IsDBNull("pojemnosc") ? null : context.GetInt16("pojemnosc"),
                        Mileage = context.IsDBNull("") ? 0 : context.GetInt32("przebieg"),
                        MileageDate = context.IsDBNull("przebieg_data") ? null : context.GetDateTime("przebieg_data"),
                        Distance = context.GetInt32("dystans")
                    };
                    vehicles.Add(vehicle);
                }
                return vehicles;
            }
        });
        return getAllVehiclesTask;
        
    }

    public Task<VehicleModel> GetVehicleById(int vehicleId)
    {        
        var getVehicleModelTask = Task.Factory.StartNew(()=>
        {
            VehicleModel? vehicle = null;
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select p.marka, p.model, p.numer_vin, p.numer_silnika, p.numer_rejestracyjny, p.numer_rejestracyjny,
                                p.rocznik, p.data_zakupu, p.sprzedany, p.adnotacje, p.pojemnosc
                                from pojazdy p
                                where p.id = @id");

                context.ParamByName("id", FirebirdSql.Data.FirebirdClient.FbDbType.Integer).Value = vehicleId;
                context.Execute();
                while(context.Read())
                {
                    vehicle = new VehicleModel()
                    {
                        Id = vehicleId,
                        Brand = context.GetString("marka"),
                        Type = context.GetString("model"),
                        Vin = context.GetString("numer_vin"),
                        EngineNumber = context.GetString("numer_silnika"),
                        RegistrationNumber = context.GetString("numer_rejestracyjny"),
                        ProductionYear = context.GetInt16("rocznik"),
                        PurchaseDate = context.IsDBNull("data_zakupu") ? null : context.GetDateTime("data_zakupu"),
                        SoldDate = context.IsDBNull("sprzedany") ? null : context.GetDateTime("sprzedany"),
                        AdditionalInfo = context.GetString("adnotacje"),
                        EngineCapacity = context.IsDBNull("pojemnosc") ? null : context.GetInt16("pojemnosc")
                    };                
                }
                if (vehicle==null)
                    throw new Exception($"Nie odnaleziono pojazdu o id:{vehicleId}");
                return vehicle;
            }
        });
        return getVehicleModelTask;
    }

    public Task CreateOrUpdateVehicle(VehicleModel vehicleModel)
    {
        var createdTask = Task.Factory.StartNew(()=> {
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select * from DODAJ_EDYTUJ_POJAZD(:id_pojazdy_we,
                            :marka,
                            :model,
                            :numer_vin,
                            :numer_silnika,
                            :numer_rejestracyjny,
                            :rocznik,
                            :data_zakupu,
                            :sprzedany,
                            :adnotacje,
                            :pojemnosc)");

                if (vehicleModel.Id!=0)
                    context.ParamByName("id_pojazdy_we", FbDbType.Integer).Value = vehicleModel.Id;
                else    
                    context.SetNull("id_pojazdy_we");

                context.ParamByName("marka", FbDbType.VarChar).Value = vehicleModel.Brand;
                context.ParamByName("model", FbDbType.VarChar).Value = vehicleModel.Type;
                context.ParamByName("numer_vin", FbDbType.VarChar).Value = vehicleModel.Vin;
                
                if (string.IsNullOrEmpty(vehicleModel.EngineNumber))
                    context.SetNull("numer_silnika");
                else
                    context.ParamByName("numer_silnika", FbDbType.VarChar).Value = vehicleModel.EngineNumber;
                if (string.IsNullOrEmpty(vehicleModel.RegistrationNumber))
                    context.SetNull("numer_rejestracyjny");
                else
                    context.ParamByName("numer_rejestracyjny", FbDbType.VarChar).Value = vehicleModel.RegistrationNumber;
                
                context.ParamByName("rocznik", FbDbType.SmallInt).Value = vehicleModel.ProductionYear;
                if (vehicleModel.PurchaseDate==null)
                    context.SetNull("data_zakupu");
                else
                    context.ParamByName("data_zakupu", FbDbType.Date).Value = vehicleModel.PurchaseDate;
                if (vehicleModel.SoldDate==null)
                    context.SetNull("sprzedany");
                else
                    context.ParamByName("sprzedany", FbDbType.Date).Value = vehicleModel.SoldDate;
                if (string.IsNullOrEmpty(vehicleModel.AdditionalInfo))
                    context.SetNull("adnotacje");
                else
                context.ParamByName("adnotacje", FbDbType.VarChar).Value = vehicleModel.AdditionalInfo;
                
                context.ParamByName("pojemnosc", FbDbType.SmallInt).Value = vehicleModel.EngineCapacity;

                context.Execute();
                if (context.Read())
                {
                    vehicleModel.Id = context.GetInt32("id_pojazdy_wy");
                }
                            
            }
        });
        return createdTask;
        
    }

    public Task<string> DeleteVehicle(int vehicleId)    
    {
        Task<string> deleteTask = Task.Factory.StartNew(() =>
        {
            using(var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL("select komunikat from CZY_MOZNA_USUNAC_POJAZD(:id_pojazdy)");
                context.ParamByName("id_pojazdy", FbDbType.Integer).Value = vehicleId;
                context.Execute();
                if (context.Read())
                {
                    return $"Nie można usunąć pojazdu, ponieważ istnieją skojarzone z nim {context.GetString("komunikat")}";
                }
                return string.Empty;
            }
        });
        return deleteTask;
    }

    

    #endregion

    #region Maintenances
    public Task<List<MaintenanceModel>> GetAllMaintenances(int VehicleId)
    {
        Task<List<MaintenanceModel>> getAllMaintenancesTask = Task.Factory.StartNew(() => {
            var maintenances = new List<MaintenanceModel>();
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select * from POKAZ_NAPRAWY(:id_pozycji, :id_pojazdu, :data_od, :data_do, :przebieg, :licznik_od, :licznik_do, :tankowania)");
                context.SetNull("id_pozycji");
                context.ParamByName("id_pojazdu", FbDbType.Integer).Value = VehicleId;
                context.SetNull("data_od");
                context.SetNull("data_do");
                context.SetNull("przebieg");
                context.SetNull("licznik_od");
                context.SetNull("licznik_do");
                context.SetNull("tankowania");

                context.Execute();
                while(context.Read())
                {
                    var vehicle = new MaintenanceModel()
                    {
                        IdVehicle = VehicleId,
                        Id = context.GetInt32("id_main"),
                        MaintenanceDate = context.GetDateTime("data"),
                        Mileage = context.GetInt32("przebieg"),
                        Description = context.GetString("opis"),
                        Cost = context.GetDecimal("koszt"),
                        Distance = context.GetInt32("przejechano")
                    };
                    maintenances.Add(vehicle);
                }
                return maintenances;
            }
        });
        return getAllMaintenancesTask;
    }

    public Task<MaintenanceModel> GetMaintenanceById(int maintenanceId)
    {
        Task<MaintenanceModel> getMaintenanceTask = Task.Factory.StartNew(() => {            
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select * from ATR_DE_MAIN(:id_pozycji)");
                context.ParamByName("id_pozycji", FbDbType.Integer).Value = maintenanceId;            
                context.Execute();
                MaintenanceModel maintenance = new MaintenanceModel();
                while (context.Read())
                {                                    
                    maintenance.IdVehicle = context.GetInt32("wy_id_pojazdy");
                    maintenance.Id = maintenanceId;
                    maintenance.MaintenanceDate = context.GetDateTime("wy_data");
                    maintenance.Mileage = context.GetDecimal("wy_przebieg");
                    maintenance.Description = context.GetString("wy_opis");
                    maintenance.Cost = context.GetDecimal("wy_koszt");
                }
                return maintenance;
            }
        });
        return getMaintenanceTask;
    }

    public Task<List<MaintenanceDetailsModel>> GetMaintenanceDetails(int maintenanceId)
    {
        Task<List<MaintenanceDetailsModel>> getMaintenanceDetailsTask = Task.Factory.StartNew(() => {
            var details = new List<MaintenanceDetailsModel>();
            using (var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL(@"select * from POKAZ_SZCZEGOLY_NAPRAWY(:id_main, :tankowania)");
                
                context.ParamByName("id_main", FbDbType.Integer).Value = maintenanceId;
                context.SetNull("tankowania");

                context.Execute();
                while(context.Read())
                {
                    DictionaryItem? part = null;
                    if (!context.IsDBNull("id_czesci"))
                        part = new DictionaryItem()
                        {
                            Id = context.GetInt32("id_czesci"),
                            Name = context.GetString("czesc")
                        };
                    DictionaryItem? maintenance = null;
                    if (!context.IsDBNull("id_czynnosci"))
                        maintenance = new DictionaryItem()
                        {
                            Id = context.GetInt32("id_czynnosci"),
                            Name = context.GetString("czynnosc")
                        };

                    var detail = new MaintenanceDetailsModel()
                    {
                        IdMaintenance = maintenanceId,
                        IdMaintenanceDetails = context.GetInt32("id_pozycji"),
                        Part = part,
                        Maintenance = maintenance,                    
                        Description = context.GetString("opis"),
                        Quantity = context.GetInt32("ilosc"),
                        Price = context.GetDecimal("cena_jedn"),
                        Cost = context.GetDecimal("koszt")
                    };
                    details.Add(detail);
                }
                return details;
            }
        });
        return getMaintenanceDetailsTask;
    }

    public Task CreateOrUpdateMaintenance(MaintenanceModel maintenance)
    {
        var updateTask = Task.Factory.StartNew(()=>{
            using(var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL("select new_id from DODAJ_EDYTUJ_NAPRAWE(:id_main, :id_pojazdy, :data, :przebieg, :przejechano, :opis)");
                context.ParamByName("id_pojazdy", FbDbType.Integer).Value = maintenance.IdVehicle;
                if (maintenance.Id>0)
                    context.ParamByName("id_main", FbDbType.Integer).Value = maintenance.Id;
                else
                    context.SetNull("id_main");
                context.ParamByName("data", FbDbType.Date).Value = maintenance.MaintenanceDate;
                context.ParamByName("przebieg", FbDbType.Integer).Value = maintenance.Mileage;
                context.SetNull("przejechano");
                if (string.IsNullOrEmpty(maintenance.Description))
                    context.SetNull("opis");
                else
                    context.ParamByName("opis", FbDbType.VarChar).Value = maintenance.Description;
                context.Execute();
                if (context.Read() && !context.IsDBNull("new_id"))
                    maintenance.Id = context.GetInt32("new_id");
            }

            var positionsToDelete = maintenance.MaintenanceDetailsList?.FindAll(x=>x.Delted && x.IdMaintenanceDetails>0);
            var positionsToUpdate = maintenance.MaintenanceDetailsList?.FindAll(x=>!x.Delted);
            //delete positions marked as deleted
            if (positionsToDelete!=null)
            {
                foreach(var detail in positionsToDelete)
                {
                    using (var childContext = new FbCoreClient(_connectionString))
                    {
                        childContext.AddSQL("delete from naprawy n where n.id = :id_naprawy");
                        childContext.ParamByName("id_naprawy", FbDbType.Integer).Value = detail.IdMaintenanceDetails;
                        childContext.ExecuteNonQuery();
                    }
                }
            }
            //save updated positions..            
            if (positionsToUpdate!=null)
            {
                foreach(var detail in positionsToUpdate)
                {
                    using (var childContext = new FbCoreClient(_connectionString))
                    {
                        childContext.AddSQL("execute procedure DODAJ_EDYTUJ_POZYCJE_NAPRAWY(:id_naprawy, :id_main, :id_czesci, :id_czynnosci, :ilosc, :cena, :koszt, :opis)");
                        childContext.ParamByName("id_main", FbDbType.Integer).Value = maintenance.Id;
                        if (detail.IdMaintenanceDetails!=0)
                            childContext.ParamByName("id_naprawy", FbDbType.Integer).Value = detail.IdMaintenanceDetails;
                        else
                            childContext.SetNull("id_naprawy");
                        
                        if (detail.Maintenance!=null)
                            childContext.ParamByName("id_czynnosci", FbDbType.Integer).Value = detail.Maintenance.Id;
                        else
                            childContext.SetNull("id_czynnosci");

                        if (detail.Part!=null)
                            childContext.ParamByName("id_czesci", FbDbType.Integer).Value = detail.Part.Id;
                        else
                            childContext.SetNull("id_czesci");

                        childContext.ParamByName("ilosc", FbDbType.Decimal).Value = detail.Quantity;
                        childContext.ParamByName("cena", FbDbType.Decimal).Value = detail.Price;
                        childContext.ParamByName("koszt", FbDbType.Decimal).Value = detail.Cost;
                        if (string.IsNullOrEmpty(detail.Description))
                            childContext.SetNull("opis");
                        else
                        
                            childContext.ParamByName("opis", FbDbType.VarChar).Value = detail.Description;
                        childContext.ExecuteNonQuery();
                    }
            
                }
            }

        });
        return updateTask;
    }

    public Task<string> DeleteMaintenance(int maintenanceId)
    {
        Task<string> deleteTask = Task.Factory.StartNew(() =>
        {
            using(var context = new FbCoreClient(_connectionString))
            {
                context.AddSQL("select komunikat from USUN_NAPRAWE(:ID_MAIN)");
                context.ParamByName("ID_MAIN", FbDbType.Integer).Value = maintenanceId;
                context.Execute();
                if (context.Read() && !context.IsDBNull("komunikat"))
                {
                    return $"Nie można usunąć pozycji, ponieważ istnieją skojarzone z nią {context.GetString("komunikat")}";
                }
                return string.Empty;
            }
        });
        return deleteTask;
    }
   
    #endregion
}