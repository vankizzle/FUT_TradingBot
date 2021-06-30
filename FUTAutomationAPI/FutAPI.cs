using FUTAutomationAPI.Helpers;
using FUTAutomationAPI.Interfaces;
using FUTAutomationAPI.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FUTAutomationAPI
{
    public class FutAPI : IFutAPI
    {
        private LoginPage _loginPage;
        private IWebDriver _driver;
        private Utils _utils;

        public FutAPI()
        {
            Init();
            this._loginPage = new LoginPage(_driver, _utils);
        }

        public void Init()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-setuid-sandbox");
            options.AddArgument("--disable-automation");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--disable-infobars");
            options.AddArgument(
              "user-agent=\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3312.0 Safari/537.36"
            );

            //options.AddArgument("headless"); //hides browser window

            options.AddUserProfilePreference("dom.webdriver.enabled", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("credentails_enable_serivce", false);

            options.SetLoggingPreference("PERFORMANCE",LogLevel.All);

            _driver = new ChromeDriver(options);
            _utils = new Utils(_driver);
        }

        public LoginPage LoginPage => _loginPage;
    }
}
