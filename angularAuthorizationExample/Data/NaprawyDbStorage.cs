using angularAuthorizationExample.Abstract;
using angularAuthorizationExample.Models;
using FbCoreClientNameSpace;

namespace angularAuthorizationExample.Data;

public class NaprawyDbStorage : INaprawyDbStorage
{
    private string _connectionString="";
    //public NaprawyDbStorage()
    public NaprawyDbStorage(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<DictionaryItem> GetAllCzynnosci()
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
}