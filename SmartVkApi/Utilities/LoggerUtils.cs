using Humanizer;
using SmartVkApi.Base;
using System.Runtime.CompilerServices;

namespace SmartVkApi.Utilities
{
    public class LoggerUtils
    {
        private static void LogStep(string stepInfo, string stepType)
        {
            var shift = new string('#', 10);
            BaseTest.Logger.Info($"{shift} {stepType} {shift} {Environment.NewLine} {stepInfo}");
        }

        public static void LogError(string description, Exception exception)
        {
            BaseTest.Logger.Fatal($"Fatal: {description}", exception);
        }

        public static void LogStep([CallerMemberName] string stepInfo = "")
        {
            LogStep(stepInfo, stepType: "Action");
        }
    }
}
