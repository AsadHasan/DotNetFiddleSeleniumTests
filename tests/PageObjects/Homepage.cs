using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace DotNetFiddleSeleniumTests.tests.PageObjects
{
    public class Homepage:BasePage
    {
        private readonly IWebDriver _webDriver;
        private const string RunButtonSelector = "#run-button";
        private const string SearchPackageTextFieldSelector = "input.new-package[type=search]";
        private const string OutputFieldSelector = "#output";
        private const string AddedPackagesSelector = ".nuget-packages>li";

        public Homepage(IWebDriver webDriver,int waitTime) : base(webDriver,waitTime)
        {
            _webDriver = webDriver;
        }

        public override BasePage Open(Uri url)
        {
            _webDriver.Navigate().GoToUrl(url);
            return this;
        }

        public Homepage RunProgram()
        {
            ClickWhenReady(RunButtonSelector);
            return this;
        }

        public string GetProgramOutput()
        {
            return GetElementTextWhenReady(OutputFieldSelector);
        }
        
        public Homepage SearchPackage(string packageName)
        {
            TypeWhenReady(SearchPackageTextFieldSelector,packageName);
            return this;
        }
        
        public Homepage SelectPackage(string packageName,string version)
        {
            var packageSelector = $"[package-id={packageName}]";
            ClickWhenReady(packageSelector);
            var versionSelector = $"[package-id={packageName}][version-name='{version}.0']";
            ClickWhenReady(versionSelector);
            return this;
        }

        public IEnumerable<string> GetAddedPackages()
        {
            return GetElementsWhenReady(AddedPackagesSelector)
                .Select(element => element.Text.Trim());
        }
    }
}