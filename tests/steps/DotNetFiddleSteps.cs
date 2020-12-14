using DotNetFiddleSeleniumTests.tests.PageObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace DotNetFiddleSeleniumTests.tests.steps
{
    [Binding]
    public class DotNetFiddleSteps
    {
       private readonly Homepage _homepage;

       public DotNetFiddleSteps(Homepage homepage)
        {
            _homepage = homepage;
        }

        [When("I run the program")]
        public void RunProgram()
        {
            _homepage.RunProgram();
        }

        [Then("(\".+\") is displayed in output window")]
        public void CheckProgramOutput(string text)
        {
            var actualText = _homepage.GetProgramOutput();
            var expectedText = text.Replace("\"", "");
            Assert.That(actualText, Is.EqualTo(expectedText));
        }

        [Given("I search for (\".+\") in Nuget packages")]
        public void SearchNugetPackages(string package)
        {
            _homepage.SearchPackage(package);
        }

        [When("I select (\".+\") version: (\\d+\\.\\d+\\.\\d+)")]
        public void SelectVersion(string package,string version)
        {
            _homepage.SelectPackage(package, version);
        }

        [Then("(\".+\") package is successfully added")]
        public void CheckPackageAdded(string package)
        {
            var actualPackageNames = _homepage.GetAddedPackages();
            var expectedPackageName = package.Replace("\"", "");
            Assert.That(actualPackageNames, Contains.Item(expectedPackageName));
        }
    }
}