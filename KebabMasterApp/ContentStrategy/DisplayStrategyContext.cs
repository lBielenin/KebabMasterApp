using System;

namespace KebabMasterApp.ContentStrategy
{
    internal class DisplayStrategyContext
    {
        public IStrategy GetStrategyFromCommandArgs()
        {
            string[]? args = Environment.GetCommandLineArgs();

            if (args is null || args.Length == 0)
            {
                return new MenuStrategy();
            }  

            var lastArg = args[args.Length - 1];

            return GetStrategyBasedOnArgs(lastArg);
        }

        private IStrategy GetStrategyBasedOnArgs(string lastArg)
        {
            switch (lastArg)
            {
                case "-management":
                    return new OrderManagementStrategy();
                case "-view":
                    return new OrderViewerStrategy();
                default:
                    return new MenuStrategy();
            }
        }
    }
}
