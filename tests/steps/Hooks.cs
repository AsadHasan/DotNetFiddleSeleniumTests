using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using BoDi;
using DotNetFiddleSeleniumTests.tests.config;
using DotNetFiddleSeleniumTests.tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using RestSharp;
using TechTalk.SpecFlow;

namespace DotNetFiddleSeleniumTests.tests.steps
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly Config _config;
        private BasePage _homepage;
        
        public Hooks(IObjectContainer objectContainer,ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            var configFile = File.ReadAllText("tests/config/Config.json");
            _config = JsonSerializer.Deserialize<Config>(configFile);
        }

        private void CheckSeleniumGridIsReady()
        {
            if (!_config.SeleniumGrid) return;
            var client = new RestClient(_config.SeleniumHubUrl);
            var request = new RestRequest("wd/hub/status", DataFormat.Json);
            var response = client.Get(request);
            while (!response.Content.Contains("Selenium Grid ready"))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                response = client.Get(request);
            }
        }

        private IWebDriver GetDriver()
        {
            var browser = _config.Browser;
            var seleniumHubUrl = _config.SeleniumHubUrl;
            if (_config.SeleniumGrid)
            {
                return browser switch
                {
                    "Chrome" => new RemoteWebDriver(seleniumHubUrl, new ChromeOptions()),
                    "Firefox" => new RemoteWebDriver(seleniumHubUrl, new FirefoxOptions()),
                    "Opera" => new RemoteWebDriver(seleniumHubUrl, new OperaOptions()),
                    _ => throw new WebDriverArgumentException("Unsupported browser specified")
                };
            }
            return new ChromeDriver();
        }
        
        [BeforeScenario]
        public void Setup()
        {
            CheckSeleniumGridIsReady();
            var webDriver = GetDriver();
            _homepage = new Homepage(webDriver,_config.MaxWait).Open(_config.BaseUrl);
            Assert.That(_homepage.Open(_config.BaseUrl).GetPageTitle(), 
                Is.EqualTo("C# Online Compiler | .NET Fiddle"));
            _objectContainer.RegisterInstanceAs(_homepage);
        }
        
        [AfterScenario]
        public void Teardown()
        {
            _homepage.SaveScreenshot(_scenarioContext.ScenarioInfo.Title);
            _homepage.Close();
        }
    }
}