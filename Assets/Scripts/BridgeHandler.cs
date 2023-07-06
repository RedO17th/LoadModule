using System.Collections.Generic;

public class FormatChecker
{
    public DataType OldFormat { get; private set; }
    public DataType NewFormat { get; private set; }

    public FormatChecker(DataType oldFormat, DataType newFormat)
    {
        OldFormat = oldFormat;
        NewFormat = newFormat;
    }

    public bool IsMatch(DataType oldFormat, DataType newFormat)
    {
        return (OldFormat == oldFormat && NewFormat == newFormat) ? true : false;
    }
}

public interface IBridgeHandler
{
    void SetBridges(Dictionary<FormatChecker, IDataBridge> bridges);
    IDataBridge GetBridge(DataType oldFormat, DataType newFormat);
}

public class BridgeHandler : IBridgeHandler
{ 
    private Dictionary<FormatChecker, IDataBridge> _bridges;

    public void SetBridges(Dictionary<FormatChecker, IDataBridge> bridges)
    {
        _bridges = bridges;
    }

    public IDataBridge GetBridge(DataType oldFormat, DataType newFormat)
    {
        IDataBridge result = null;

        foreach (var bridge in _bridges)
        {
            if (bridge.Key.IsMatch(oldFormat, newFormat))
            {
                result = bridge.Value;
                break;
            }
        }

        return result;
    }
}

public interface IDataBridge
{
    IData Convert(IData oldData);
}

public class FirstToSecondDataFormat : IDataBridge
{
    public FirstToSecondDataFormat() { }

    public IData Convert(IData oldData)
    {
        FirstTypeData data = oldData as FirstTypeData;

        return new SecondTypeData(DataType.SecondType, data.Data, data.Number);
    }
}

public class SecondToFirstDLCDataFormat : IDataBridge
{
    public SecondToFirstDLCDataFormat() { }

    public IData Convert(IData oldData)
    {
        SecondTypeData data = oldData as SecondTypeData;

        return new FirstDLCData(DataType.FirstDLC, data.Data, new PointPosition(1, 5));
    }
}

public class FirstDLCToSecondDLCDataFormat : IDataBridge
{
    public FirstDLCToSecondDLCDataFormat() { }

    public IData Convert(IData oldData)
    {
        FirstDLCData data = oldData as FirstDLCData;

        return new SecondDLCData(DataType.SecondDLC, data.Data, "Hello");
    }
}
