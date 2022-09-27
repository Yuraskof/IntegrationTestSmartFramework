﻿namespace SmartVkApi.Constants
{
    public class ProjectConstants
    {
        public const int Timeout = 1;
        public const double PollingInterval = 0.5;
        public const int TimeoutForElements = 2;
        public const int TimeoutForForm = 2;
        public const string PathToLogFile = @"..\..\..\Log\log.log";
        public const string PathToLoginUser = @"Resources\TestData\LoginUser.json";
        public const string PathToTestData = @"Resources\TestData\TestData.json";
        public const string PathToLocalizedTestData = @"Resources\TestData\LocalizedTestData.json";
        public const string MediaType = "application/json";
        public const string PathToApiMethods = @"Resources\TestData\ApiMethods.json";
    }
}