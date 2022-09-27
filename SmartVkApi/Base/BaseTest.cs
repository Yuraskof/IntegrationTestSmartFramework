using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using Humanizer;
using NUnit.Framework.Interfaces;
using SmartVkApi.Constants;
using SmartVkApi.Models;
using SmartVkApi.Utilities;

namespace SmartVkApi.Base
{
    public abstract class BaseTest
    {
        protected string ScenarioName
            => TestContext.CurrentContext.Test.Properties.Get("Description")?.ToString()
            ?? TestContext.CurrentContext.Test.Name.Replace("_", string.Empty).Humanize();

        public static Logger Logger => AqualityServices.Get<Logger>();
        private TestContext.ResultAdapter Result => TestContext.CurrentContext.Result;

        public static readonly TestData testData = JsonUtils.ReadJsonDataFromPath<TestData>(ProjectConstants.PathToTestData);

        [SetUp]
        public void Setup()
        {
            //WallPostModel postModel = new WallPostModel();
            //postModel.message = "Hello";
            //ApiApplicationRequest.CreatePostOnTheWall(postModel);
            

            Logger.Info($"Start scenario [{ScenarioName}]");
            AqualityServices.Browser.GoTo(testData.Url);
            Logger.Info("Step 1 completed.");
            AqualityServices.Browser.Maximize();
        }

        [TearDown]
        public virtual void AfterEach()
        {
            AqualityServices.Browser.Quit();
            LogScenarioResult();
        }

        private void LogScenarioResult()
        {
            Logger.Info($"Scenario [{ScenarioName}] result is {Result.Outcome.Status}!");
            if (Result.Outcome.Status != TestStatus.Passed)
            {
                Logger.Error(Result.Message);
            }
            Logger.Info(new string('=', 100));
        }
    }
}