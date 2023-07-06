using Newtonsoft.Json;
using System.IO;

public class DataKey
{
    public DataType Type { get; protected set; } = DataType.None;
    public DataKey(DataType type) => Type = type;
}

//Определитель типа данных
public class DataTypeSpecifier
{
    public void SaveKey(string pathToKey, DataType type)
    {
        using (StreamWriter writer = new StreamWriter(pathToKey, false))
        {
            writer.WriteLine(JsonConvert.SerializeObject(new DataKey(type)));
        }
    }

    public DataType GetDataType(string pathToKey) 
    {
        string loadedData = string.Empty;

        using (StreamReader reader = new StreamReader(pathToKey))
        {
            loadedData = reader.ReadLine();
        }

        var key = JsonConvert.DeserializeObject<DataKey>(loadedData);

        return key.Type;
    }
}
