using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.AutomationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver;
            string baseURL = "http://criends.azurewebsites.net";
            driver = new ChromeDriver(@"C:\Users\Lap.lk\Downloads\chromedriver_win32\");
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Window.Maximize();
        }
    }
}
