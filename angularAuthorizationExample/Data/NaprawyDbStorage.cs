using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using FbCoreClientNameSpace;
using FirebirdSql.Data.FirebirdClient;

namespace angularAuthorizationExample.Data;

public class NaprawyDbStorage : INaprawyDbStorage
{
    private string _connectionString="";    
    public NaprawyDbStorage(string connectionString)
    {
        _connectionString = connectionString;
    }
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
                    Identity = context.GetInt32("id"),
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
                    Identity = context.GetInt32("id"),
                    Name = context.GetString("nazwa"),
                    InActive = context.IsDBNull("nieaktywny") ? false : true,
                    IsFuel = context.IsDBNull("paliwo") ? false : true,
                };
                dictionaryValues.Add(dictionaryItem);
            }
        }
        return dictionaryValues;
    }

    public List<VehicleModel> GetAllVehicles()
    {
        var vehicles = new List<VehicleModel>();
        using (var context = new FbCoreClient(_connectionString))
        {
            context.AddSQL(@"select p.id, p.marka, p.model, p.numer_vin, p.numer_silnika, p.numer_rejestracyjny, p.numer_rejestracyjny,
p.rocznik, p.data_zakupu, p.sprzedany, p.adnotacje, p.pojemnosc, n.przebieg, n.przebieg_data
from pojazdy p
join (select max(m.przebieg) as przebieg, max(m.data) as przebieg_data, m.id_pojazdy
         from main m
        group by m.id_pojazdy) n on n.id_pojazdy = p.id
order by p.marka collate pxw_plk, p.model, p.rocznik");

            context.Execute();
            while(context.Read())
            {
                var vehicle = new VehicleModel()
                {
                    Id = context.GetInt32("id"),
                    Brand = context.GetString("marka"),
                    Type = context.GetString("model"),
                    Vin = context.GetString("numer_vin"),
                    EngineNumber = context.GetString("numer_silnika"),
                    RegistrationNumber = context.GetString("numer_rejestracyjny"),
                    ProductionYear = context.GetInt16("rocznik"),
                    PurchaseDate = context.IsDBNull("data_zakupu") ? null : context.GetDateTime("data_zakupu"),
                    SoldDate = context.IsDBNull("sprzedany") ? null : context.GetDateTime("sprzedany"),
                    AdditionalInfo = context.GetString("adnotacje"),
                    EngineCapacity = context.IsDBNull("pojemnosc") ? null : context.GetInt16("pojemnosc"),
                    Mileage = context.IsDBNull("") ? 0 : context.GetInt32("przebieg"),
                    MileageDate = context.IsDBNull("przebieg_data") ? null : context.GetDateTime("przebieg_data")
                };
                vehicles.Add(vehicle);
            }
            return vehicles;


        }
        
    }

    public VehicleModel GetVehicleById(int vehicleId)
    {        
        VehicleModel vehicle = null;
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
            return vehicle;
        }
    }

    public void CreateOrUpdateVehicle(VehicleModel vehicleModel)
    {
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

            context.ExecuteNonQuery();
            if (context.Read())
            {
                vehicleModel.Id = context.GetInt32("id_pojazdy_wy");
            }
            
            
        }
    }
}