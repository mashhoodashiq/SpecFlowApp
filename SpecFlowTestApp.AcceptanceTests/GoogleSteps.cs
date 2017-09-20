using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlowTestApp.AcceptanceTests
{
    // Google Steps
    [Binding]
    public class GoogleSteps
    {
        private IWebDriver _driver;
        readonly TestingBot.TestingBotDriver _tbDriver;

        public GoogleSteps()
        {
            _tbDriver = (TestingBot.TestingBotDriver)ScenarioContext.Current["tbDriver"];
        }

        [Given(@"I am on the google page for (.*) and (.*)")]
        public void GivenIAmOnTheGooglePageForParallelAndChrome(string profile, string environment)
        {
            _driver = _tbDriver.Init(profile, environment, "https://www.google.com/ncr");
            _driver.Navigate().GoToUrl("https://www.google.com/ncr");
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string keyword)
        {
            var q = _driver.FindElement(By.Name("q"));
            q.SendKeys(keyword);
            q.Submit();
        }

        [Then(@"I should see title ""(.*)""")]
        public void ThenIShouldSeeTitle(string title)
        {
            Thread.Sleep(5000);
            Assert.AreEqual(_driver.Title, title);
        }
    }
}
