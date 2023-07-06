using System.Collections.Generic;
using UnityEngine;

public enum DataType { None = -1, FirstType, SecondType, FirstDLC, SecondDLC }

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

public struct PointPosition
{
    public int X;
    public int Y;

    public PointPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class FirstDLCData : BaseData
{
    public PointPosition Position { get; private set; }
    public FirstDLCData(DataType type, string data, PointPosition position) : base(type, data)
    {
        Position = position;
    }
}
public class SecondDLCData : BaseData
{
    public string Letter { get; private set; }

    public SecondDLCData(DataType type, string data, string letter) : base(type, data)
    {
        Letter = letter;
    }
}






