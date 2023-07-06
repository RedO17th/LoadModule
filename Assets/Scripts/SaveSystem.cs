using System.IO;
using Newtonsoft.Json;

public interface ISaveSystem
{
    void SetPath(string path);
    void Save(IData data);
}

public class SaveSystem : ISaveSystem
{
    private string _path = string.Empty;

    public void SetPath(string path) => _path = path;

    public void Save(IData data)
    {
        using (StreamWriter writer = new StreamWriter(_path, false))
        {
            writer.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}
