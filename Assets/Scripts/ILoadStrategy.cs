using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoadStrategy
{
    void SetPath(string path);
    IData Load();
}

public class BaseLoadStrategy : ILoadStrategy
{
    protected string _path = string.Empty;

    public virtual void SetPath(string path) => _path = path;
    public virtual IData Load() => null;
}

public class FirstLoadStrategy : BaseLoadStrategy
{
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

public class FirstDLCLoadStrategy : BaseLoadStrategy
{
    public override IData Load()
    {
        string loadedData = string.Empty;

        using (StreamReader reader = new StreamReader(_path))
        {
            loadedData = reader.ReadLine();
        }

        return JsonConvert.DeserializeObject<FirstDLCData>(loadedData);
    }
}

public class SecondDLCLoadStrategy : BaseLoadStrategy
{
    public override IData Load()
    {
        string loadedData = string.Empty;

        using (StreamReader reader = new StreamReader(_path))
        {
            loadedData = reader.ReadLine();
        }

        return JsonConvert.DeserializeObject<SecondDLCData>(loadedData);
    }
}