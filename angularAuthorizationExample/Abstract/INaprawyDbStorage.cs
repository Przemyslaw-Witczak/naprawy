using angularAuthorizationExample.Models;

namespace angularAuthorizationExample.Abstract;

public interface INaprawyDbStorage
{
    List<DictionaryItem> GetAllCzynnosci();
}
