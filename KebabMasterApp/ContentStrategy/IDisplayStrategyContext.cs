namespace KebabMasterApp.ContentStrategy
{
    public interface IDisplayStrategyContext
    {
        IStrategy GetStrategyFromCommandArgs();
    }
}
