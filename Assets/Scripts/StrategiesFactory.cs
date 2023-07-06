using System.Collections.Generic;

//Фабрика стратегий
public interface IStrategiesFactory
{
    void SetStrategies(Dictionary<DataType, ILoadStrategy> stratagies);
    ILoadStrategy GetStrategyBy(DataType type);
}
public class StrategiesFactory : IStrategiesFactory
{
    private Dictionary<DataType, ILoadStrategy> _stratagies;

    public StrategiesFactory() { }

    public void SetStrategies(Dictionary<DataType, ILoadStrategy> stratagies) => _stratagies = stratagies;

    public ILoadStrategy GetStrategyBy(DataType type)
    {
        return (_stratagies.ContainsKey(type)) ? _stratagies[type] : new BaseLoadStrategy();
    }
}



