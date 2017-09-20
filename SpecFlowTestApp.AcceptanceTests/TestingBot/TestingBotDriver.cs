using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Edge;
using System.Collections.Specialized;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecFlowTestApp.AcceptanceTests.TestingBot
{
    [Binding]
    public sealed class TestingBot
    {
        private TestingBotDriver tbDriver;
        private string[] tags;

        [BeforeScenario]
        public void BeforeScenario()
        {
            tbDriver = new TestingBotDriver(ScenarioContext.Current);
            ScenarioContext.Current["tbDriver"] = tbDriver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            tbDriver.Cleanup();
        }
    }
    public class TestingBotDriver
    {
        private IWebDriver driver;
        private string profile;
        private string environment;
        private ScenarioContext context;

        public TestingBotDriver(ScenarioContext context)
        {
            this.context = context;
        }

        public IWebDriver Init(string profile, string environment, string url)
        {
            driver= GetBrowserDriver(environment, url);           
            return driver;
        }
        private IWebDriver GetBrowserDriver(string environment, string url) {
            switch (environment) {
                case "chrome": {
                        return new ChromeDriver();
                    }
                case "ie": {
                        var ieOptions = new InternetExplorerOptions();
                        ieOptions.InitialBrowserUrl = url;
                        return new InternetExplorerDriver(ieOptions);
                    }
                case "firefox": {                       
                        return new FirefoxDriver();
                    }
                case "opera": {
                        return new OperaDriver();
                    }
                case "edge": {
                        var options = new EdgeOptions();
                        options.PageLoadStrategy = EdgePageLoadStrategy.Eager;
                        
                        return new EdgeDriver(@"C:\Users\mashhood.m\source\repos\SpecFlowTestApp\SpecFlowTestApp.AcceptanceTests\bin\Debug\"); }
            }
            return null;
        }
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
