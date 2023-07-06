using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoadStrategy
{
    IData Load();
}

public class BaseLoadStrategy : ILoadStrategy
{
    protected string _path = string.Empty;

    public BaseLoadStrategy(string path)
    {
        _path = path;
    }

    public virtual IData Load() => null;
}

public class FirstLoadStrategy : BaseLoadStrategy
{
    public FirstLoadStrategy(string path) : base(path) { }

    public override IData Load()
    {
        string loadedData = string.Empty;

        using (StreamReader reader = new StreamReader(_path))
        {
            loadedData = reader.ReadLine();
        }

        return JsonConvert.DeserializeObject<FirstTypeData>(loadedData);
    }
}

public class SecondLoadStrategy : BaseLoadStrategy
{
    public SecondLoadStrategy(string path) : base(path) { }

    public override IData Load()
    {
        string loadedData = string.Empty;

        using (StreamReader reader = new StreamReader(_path))
        {
            loadedData = reader.ReadLine();
        }

        return JsonConvert.DeserializeObject<SecondTypeData>(loadedData);
    }
}