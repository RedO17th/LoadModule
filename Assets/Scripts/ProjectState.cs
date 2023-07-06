
//For test
public class ProjectState
{
    public DataType CurrentDataFormat { get; private set; } = DataType.None;
    public ILoadStratagiesContainer StratagiesContainer { get; private set; }
    public IBridgeHandler BridgeHandler { get; private set; }

    public void UpdateDataFormat(DataType format) => CurrentDataFormat = format;
    public void UpdateStratagiesContainer(ILoadStratagiesContainer container) => StratagiesContainer = container;
    public void UpdateBridgeHandler(IBridgeHandler handler) => BridgeHandler = handler;
}



