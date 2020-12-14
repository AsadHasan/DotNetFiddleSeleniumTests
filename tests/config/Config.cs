using System;

namespace DotNetFiddleSeleniumTests.tests.config
{
    public class Config    {
        public Uri BaseUrl { get; set; }
        public bool SeleniumGrid { get; set; }
        public Uri SeleniumHubUrl { get; set; } 
        public string Browser { get; set; }
        public int MaxWait { get; set; } 
    }
}