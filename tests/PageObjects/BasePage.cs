using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace DotNetFiddleSeleniumTests.tests.PageObjects
{
    public abstract class BasePage
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _wait;

        protected BasePage(IWebDriver webDriver,int waitTime)
        {
            _webDriver = webDriver;
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(waitTime));
        }

        public abstract BasePage Open(Uri url);

        protected void ClickWhenReady(string cssSelector)
        {
            GetElementWhenReady(cssSelector).Click();
        }

        protected void TypeWhenReady(string cssSelector,string text)
        {
            GetElementWhenReady(cssSelector).SendKeys(text);
        }

        protected string GetElementTextWhenReady(string cssSelector)
        {
            return GetElementWhenReady(cssSelector).Text;
        }
        
        public void Close()
        {
            _webDriver.Quit();
        }

        public string GetPageTitle()
        {
            return _webDriver.Title;
        }

        public void SaveScreenshot(string fileName)
        {
            var fileNameWithoutInvalidCharacters = fileName.Replace(".", "_")
                .Replace("\"", "")
                .Replace(" ", "");
            _webDriver.TakeScreenshot().
                SaveAsFile($"../../../screenshots/{fileNameWithoutInvalidCharacters}.png",
                    ScreenshotImageFormat.Png);
        }

        private IWebElement GetElementWhenReady(string cssSelector)
        {
            _wait.Until(driver =>
            {
                var element = driver.FindElement(By.CssSelector(cssSelector));
                return element.Displayed && element.Enabled;
            });
            return _webDriver.FindElement(By.CssSelector(cssSelector));
        }
        
        protected IEnumerable<IWebElement> GetElementsWhenReady(string cssSelector)
        {
            _wait.Until(driver =>
            {
                var elements = driver.FindElements(By.CssSelector(cssSelector));
                return elements.All(element => element.Displayed && element.Enabled);
            });
            return _webDriver.FindElements(By.CssSelector(cssSelector));
        }
    }
}