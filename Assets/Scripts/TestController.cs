using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum DataType { None = -1, FirstType, SecondType }

public interface IData
{
    public DataType Type { get; }
}
public class BaseData : IData
{
    public DataType Type { get; protected set; } = DataType.None;
    public string Data { get; protected set; }

    public BaseData(DataType type, string data)
    {
        Type = type;

        Data = data;
    }
}

public class FirstTypeData : BaseData
{
    public int Number { get; private set; }
    public FirstTypeData(DataType type, string data, int number) : base(type, data) 
    {
        Number = number;
    }
}
public class SecondTypeData : BaseData
{
    public float Number { get; private set; } 
    public SecondTypeData(DataType type, string data, float number) : base(type, data) 
    {
        Number = number;
    }
}

public class TestController : MonoBehaviour
{
    [SerializeField] private bool _isFirst = true;
    [SerializeField] private string _fileName;
    [SerializeField] private string _keyName;

    private string _dataPath = string.Empty;
    private string _keyPath = string.Empty;

    private void Awake()
    {
        _dataPath = Path.Combine(Application.persistentDataPath, _fileName);
        _keyPath = Path.Combine(Application.persistentDataPath, _keyName);

        SaveDataBeforeTest();
    }

    private void SaveDataBeforeTest()
    {
        IData fData = new FirstTypeData(DataType.FirstType, "FirstTypeData", 5);
        IData sData = new SecondTypeData(DataType.SecondType, "SecondTypeData", 5f);

        IData dataToSave = _isFirst ? fData : sData;

        ISaveSystem _saveSystem = new SaveSystem();
                    _saveSystem.SetPath(_dataPath);
                    _saveSystem.Save(dataToSave);

        var specifier = new DataTypeSpecifier();
            specifier.SaveKey(_keyPath, dataToSave.Type);
    }

    private void Start()
    {
        var stratagies = new Dictionary<DataType, ILoadStrategy>()
        {
            { DataType.FirstType, new FirstLoadStrategy(_dataPath) },
            { DataType.SecondType, new SecondLoadStrategy(_dataPath) }
        };

        IStrategiesFactory strategiesFactory = new StrategiesFactory();
                           strategiesFactory.SetStrategies(stratagies);

        var specifier = new DataTypeSpecifier();

        DataType dataType = specifier.GetDataType(_keyPath);

        ILoadStrategy strategy = strategiesFactory.Create(dataType);
        IData data = strategy.Load();

        Debug.Log($"TestController: Type is {data.GetType()}");
    }
}






