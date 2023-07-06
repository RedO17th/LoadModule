using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

//Фабрика стратегий
public interface IStrategiesFactory
{
    void SetStrategies(Dictionary<DataType, ILoadStrategy> stratagies);
    ILoadStrategy Create(DataType type);
}
public class StrategiesFactory : IStrategiesFactory
{
    private Dictionary<DataType, ILoadStrategy> _stratagies;

    public StrategiesFactory() { }

    public void SetStrategies(Dictionary<DataType, ILoadStrategy> stratagies) => _stratagies = stratagies;

    public ILoadStrategy Create(DataType type)
    {
        return (_stratagies.ContainsKey(type)) ? _stratagies[type] : new BaseLoadStrategy(string.Empty);
    }
}

//Определитель типа данных
public class DataKey
{ 
    public DataType Type { get; protected set; } = DataType.None;
    public DataKey(DataType type) => Type = type;
}
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