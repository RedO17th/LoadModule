using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TestController : MonoBehaviour
{
    [SerializeField] private bool _isFirstLaunch = true;
    [SerializeField] private string _fileName;
    [SerializeField] private string _keyName;

    private string _dataPath = string.Empty;
    private string _keyPath = string.Empty;

    private ProjectState _projectState = null;

    private void Awake()
    {
        _projectState = new ProjectState();
        _projectState.UpdateDataFormat(DataType.FirstType);
        _projectState.UpdateStratagiesContainer(new FirstLoadStratagiesContainer());
        _projectState.UpdateBridgeHandler(new BridgeHandler());

        _dataPath = Path.Combine(Application.persistentDataPath, _fileName);
        _keyPath = Path.Combine(Application.persistentDataPath, _keyName);

        if (_isFirstLaunch)
        {
            IData fData = new FirstTypeData(DataType.FirstType, "FirstTypeData", 5);

            SaveDataBeforeTest(fData);
        }


        //—борка мостов дл€ трансл€ции данных
        InitializeBridgeHandler();
    }

    private void SaveDataBeforeTest(IData dataToSave)
    {
        ISaveSystem _saveSystem = new SaveSystem();
                    _saveSystem.SetPath(_dataPath);
                    _saveSystem.Save(dataToSave);

        var specifier = new DataTypeSpecifier();
            specifier.SaveKey(_keyPath, dataToSave.Type);
    }

    private void InitializeBridgeHandler()
    {
        Dictionary<FormatChecker, IDataBridge> bridges = new Dictionary<FormatChecker, IDataBridge>()
        {
            { new FormatChecker(DataType.FirstType, DataType.SecondType), new FirstToSecondDataFormat() },
            { new FormatChecker(DataType.SecondType, DataType.FirstDLC), new SecondToFirstDLCDataFormat() },
            { new FormatChecker(DataType.FirstDLC, DataType.SecondDLC), new FirstDLCToSecondDLCDataFormat() }
        };

        _projectState.BridgeHandler.SetBridges(bridges);
    }

    private void Start()
    {
        if (_isFirstLaunch)
            return;

        IData firstFormatData = LoadDataAfterGlobalUpdate(); 
        Debug.Log($"TestController: Current format is {firstFormatData.GetType()} ");

        //Update project. Template
        _projectState.UpdateDataFormat(DataType.SecondType); 

        IData secondFormatData_1 = TransferDataToAnotherFormat(firstFormatData);
        SaveDataBeforeTest(secondFormatData_1);

        //Update with DLC. Template
        _projectState.UpdateDataFormat(DataType.FirstDLC);
        _projectState.UpdateStratagiesContainer(new SecondLoadStratagiesContainer());


        IData secondFormatData_2 = LoadDataAfterGlobalUpdate();
        Debug.Log($"TestController: Current format is {secondFormatData_2.GetType()} ");

        IData firstDLCDataFormat_1 = TransferDataToAnotherFormat(secondFormatData_2);
        SaveDataBeforeTest(firstDLCDataFormat_1);

        //Update with DLC. Template
        _projectState.UpdateDataFormat(DataType.SecondDLC);
        _projectState.UpdateStratagiesContainer(new ThirdLoadStratagiesContainer());


        IData firstDLCDataFormat_2 = LoadDataAfterGlobalUpdate();
        Debug.Log($"TestController: Current format is {firstDLCDataFormat_2.GetType()} ");

        IData secondDLCDataFormat_1 = TransferDataToAnotherFormat(firstDLCDataFormat_2);
        SaveDataBeforeTest(secondDLCDataFormat_1);

        //Update with DLC. Template
        _projectState.UpdateStratagiesContainer(new FourthLoadStratagiesContainer());


        IData secondDLCDataFormat_2 = LoadDataAfterGlobalUpdate();
        Debug.Log($"TestController: Current format is {secondDLCDataFormat_2.GetType()} ");
    }

    private IData LoadDataAfterGlobalUpdate()
    {
        var stratagies = _projectState.StratagiesContainer.GetStratagies();

        IStrategiesFactory strategiesFactory = new StrategiesFactory();
                           strategiesFactory.SetStrategies(stratagies);

        DataType dataType = new DataTypeSpecifier().GetDataType(_keyPath);

        ILoadStrategy strategy = strategiesFactory.GetStrategyBy(dataType);
                      strategy.SetPath(_dataPath);

        return strategy.Load();
    }

    private IData TransferDataToAnotherFormat(IData oldData)
    {
        var bridge = _projectState.BridgeHandler.GetBridge(oldData.Type, _projectState.CurrentDataFormat);
        
        return bridge.Convert(oldData);
    }
}






