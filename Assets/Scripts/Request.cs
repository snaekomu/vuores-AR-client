using System.Collections.Generic;
using System.Linq;
public class Request
{
    public static string host {get; private set;} = "http://snaekomu.xyz:3000/";
    public static string version {get; private set;} = "v1";
    private string[] path;
    private KeyValuePair<string, string>[] query;

    public delegate Request _d (params string[] p);
    public static _d New(params string[] p)
    {
        return q => new Request(p, q);
    }

    private Request(string[] path, string[] query)
    {
        this.path = path;
        this.query = new KeyValuePair<string, string>[query.Length / 2];
        for (int i = 0, j = 0; i < this.query.Length; i++, j++)
        {
            this.query[i] = new KeyValuePair<string, string>(query[j], query[++j]);
        }
    }

    public string Path
    {
        get { return string.Join("/", path); }
    }

    public string Query
    {
        get { return "?" + query.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + "&" + s2); }
    }

    public string Uri
    {
        get 
        {
            return string.Format("{0}/{1}{2}", host, Path, (query != null || query.Length > 0) ? Query : "");
        }
    }
}
