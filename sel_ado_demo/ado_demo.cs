using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sel_ado_demo
{
    [TestClass]
    public class ado_demo
    {
        [TestMethod]
        public void SearchBarTest()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            //using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                //Notice navigation is slightly different than the Java version
                //This is because 'get' is a keyword in C#
                driver.Navigate().GoToUrl("http://www.google.com/");

                // Maximise browser windows if not maximized already
                driver.Manage().Window.Maximize();

                // Accept google cookies
                IWebElement cookies = driver.FindElement(By.XPath("./html/body/div[2]/div[3]/div[3]/span/div/div/div/div[3]/div[1]/button[2]/div"));
                cookies.Click();

                // Find the text input element by its name
                IWebElement query = driver.FindElement(By.Name("q"));

                // Enter something to search for
                query.SendKeys("Comino");

                // Now submit the form. WebDriver will find the form for us from the element
                query.Submit();

                // Google's search is rendered dynamically with JavaScript.
                // Wait for the page to load, timeout after 10 seconds
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.StartsWith("comino", StringComparison.OrdinalIgnoreCase));

                // Should see: "Comino - Google Search" (for an English locale)
                Assert.AreEqual(driver.Title, "Comino - Google Search");

            }
        }
    }
}
