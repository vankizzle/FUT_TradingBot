using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUTAutomationAPI.Pages
{
    public abstract class Page
    {
        protected IWebDriver _driver;

        protected Page(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetCoins()
        {
            By xCoins = By.XPath("//div[contains(@class, 'view-navbar-currency-coins')]");
            IWebElement coinsElem =  this._driver.FindElement(xCoins);
            return coinsElem.Text;
        }
    }
}
