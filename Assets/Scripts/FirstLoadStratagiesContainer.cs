using System.Collections.Generic;

public interface ILoadStratagiesContainer
{
    Dictionary<DataType, ILoadStrategy> GetStratagies();
}

public class FirstLoadStratagiesContainer : ILoadStratagiesContainer
{
    public Dictionary<DataType, ILoadStrategy> GetStratagies()
    {
        return new Dictionary<DataType, ILoadStrategy>()
        {
            { DataType.FirstType, new FirstLoadStrategy() }
        };
    }
}

public class SecondLoadStratagiesContainer : ILoadStratagiesContainer
{
    public Dictionary<DataType, ILoadStrategy> GetStratagies()
    {
        return new Dictionary<DataType, ILoadStrategy>()
        {
            { DataType.FirstType, new FirstLoadStrategy() },
            { DataType.SecondType, new SecondLoadStrategy() }
        };
    }
}

public class ThirdLoadStratagiesContainer : ILoadStratagiesContainer
{
    public Dictionary<DataType, ILoadStrategy> GetStratagies()
    {
        return new Dictionary<DataType, ILoadStrategy>()
        {
            { DataType.FirstType, new FirstLoadStrategy() },
            { DataType.SecondType, new SecondLoadStrategy() },
            { DataType.FirstDLC, new FirstDLCLoadStrategy() }
        };
    }
}

public class FourthLoadStratagiesContainer : ILoadStratagiesContainer
{
    public Dictionary<DataType, ILoadStrategy> GetStratagies()
    {
        return new Dictionary<DataType, ILoadStrategy>()
        {
            { DataType.FirstType, new FirstLoadStrategy() },
            { DataType.SecondType, new SecondLoadStrategy() },
            { DataType.FirstDLC, new FirstDLCLoadStrategy() },
            { DataType.SecondDLC, new SecondDLCLoadStrategy() }
        };
    }
}
