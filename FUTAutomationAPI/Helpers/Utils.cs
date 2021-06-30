using OpenQA.Selenium;
using System;
using System.Threading;

namespace FUTAutomationAPI.Helpers
{
    public class Utils
    {
        private IWebDriver _driver;

        public Utils(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        public void ClickPreventShield(IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("ElementClickInterceptedError"))
                {
                    ClickPreventShield(element);
                }
            }
        }

        public void UpdateFindTimeout(int number)
        {
           _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(number);
            
        }
    }
}