namespace angularAuthorizationExample.Models;

public class DictionaryItem
{
    public int Identity { get; set; }

    public string? Name { get; set; }

    public bool InActive { get; set; }

    public string InActiveString {
        get
        {
            return InActive ? "Nieaktyne" : string.Empty;
        }
    }
}
