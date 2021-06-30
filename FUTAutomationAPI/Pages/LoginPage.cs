using FUTAutomationAPI.Helpers;
using FUTAutomationAPI.Interfaces;
using FUTAutomationAPI.Interfaces.Pages;
using OpenQA.Selenium;
using System;

namespace FUTAutomationAPI.Pages
{
    public class LoginPage : Page, ILoginPage
    {
        private Utils _util;

        private By xEmailInput = By.XPath("//input[@name='email']");
        private By xPasswordInput = By.XPath("//input[@name='password']");
        private By xLoginBtn = By.XPath("//a[@id='btnLogin']");
        private By xAppAuthRadioBtn = By.XPath("//strong[contains(text(), 'App Authenticator')]");
        private By xSendCodeBtn = By.XPath("//a[@id='btnSendCode']");
        private By xCodeInput = By.XPath("//input[@name='oneTimeCode']");
        private By xSubmitCodeBtn = By.XPath("//a[@id='btnSubmit']");
        private By xWrongCreds = By.XPath("//div[contains(text(), 'credentials are incorrect')]");
        private By xWrongCode = By.XPath("//span[contains(text(), 'Incorrect code entered')]");
        private By xGotoLoginBtn = By.XPath("//button[@class='btn-standard call-to-action']");

        public LoginPage(IWebDriver driver, Utils util) : base(driver)
        {
            _util = util;
        }

        public void GoTo()
        {
            this._driver.Navigate().GoToUrl("https://www.ea.com/fifa/ultimate-team/web-app/");
            _util.Sleep(7000);
            var loginBtn = this._driver.FindElement(xGotoLoginBtn);

            try
            {
                loginBtn.Click();
            }
            catch (Exception e)
            {
                //log error
            }
        }

        public void Login(ILoginOptions options)
        {
            //Fill and submit login form
            var emailInput = this._driver.FindElement(xEmailInput);
            var passwordInput = this._driver.FindElement(xPasswordInput);
            var loginBtn = this._driver.FindElement(xLoginBtn);
            emailInput.SendKeys(options.Email);
            passwordInput.SendKeys(options.Password);

            try
            {
                loginBtn.Click();
            }
            catch (Exception e)
            {
                ///log ex
            }

            CheckForWrongCredentials();

            Fill2FA(options);
        }

        private void CheckForWrongCredentials()
        {
            var areCredsWrong = false;
            try
            {
                this._util.UpdateFindTimeout(1000);
                this._driver.FindElement(xWrongCreds);
                areCredsWrong = true;
            }
            catch (Exception e)
            {
                this._util.UpdateFindTimeout(20000);
            }

            if (areCredsWrong) throw new Exception("Wrong credentials provided.");
        }

        private void Fill2FA(ILoginOptions options)
        {
            ////Fill and submit 2fa form
            string code = "";

            if (options.Code.HasValue) code = options.Code.ToString();
            if (options.Token.Length > 0) code = options.Token;

            ////Some accounts only have 1 option to verify login
            ////so no radio buttons are shown
            try
            {
                _util.UpdateFindTimeout(1000);
                var appAuthRadioBtn = this._driver.FindElement(xAppAuthRadioBtn);
                appAuthRadioBtn.Click();
            }
            catch (Exception e)
            {
                this._util.UpdateFindTimeout(20000);
            }

            try
            {
                var sendCodeBtn = this._driver.FindElement(xSendCodeBtn);
                sendCodeBtn.Click();
                var codeInput = this._driver.FindElement(xCodeInput);
                var submitCodeBtn = this._driver.FindElement(xSubmitCodeBtn);
                codeInput.SendKeys(code);
                this._util.Sleep(500);
                submitCodeBtn.Click();
            }
            catch (Exception e)
            {
                //log
            }
        }
    }
}