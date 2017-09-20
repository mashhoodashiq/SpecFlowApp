using System;
using TechTalk.SpecFlow;

namespace SpecFlowTestApp.Tests
{
    [Binding]
    public class GoogleSteps
    {
        private IWebDriver _driver;
        readonly TestingBotDriver _tbDriver;

        public GoogleSteps()
        {
            _tbDriver = (TestingBotDriver)ScenarioContext.Current["tbDriver"];
        }

        [Given(@"I am on the google page for (.*) and (.*)")]
        public void GivenIAmOnTheGooglePage(string profile, string environment)
        {
            _driver = _tbDriver.Init(profile, environment);
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
            Assert.That(_driver.Title, Is.EqualTo(title));
        }
    }
}
