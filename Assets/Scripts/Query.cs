using System.Collections.Generic;

public class Query : List<string>
{
    public string Url
    {
        get {
            return "?" + string.Join("&", this);
        }
    }

    public void Add(string a, string b)
    {
        this.Add(string.Join("=", new string[] {a, b}));
    }
}
