using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountAdminPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountAdminPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement UsernameInput => driver.FindElement(By.XPath("//input[@data-bind='value: searchCriteria']"));
        public IWebElement SearchByDropdown => driver.FindElement(By.XPath("//select[@id='type']"));
        public IWebElement SearchButton => driver.FindElement(By.XPath("//button[@class='btn btn-success' and contains(text(),'Search')]"));
        public IWebElement DisableAccountButton => driver.FindElement(By.XPath("//button[@href='#disableModal']"));
        public IWebElement DisableAccountText => driver.FindElement(By.Id("disableModalLabel"));
        public IWebElement DisableAccountPopupButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary']"));
        public IWebElement EnableAccountButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block']"));
        public IWebElement EnableAccountText => driver.FindElement(By.Id("disableModalLabel"));
        public IWebElement EnableAccountPopupButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary']"));
        public IWebElement ImpersonateButton => driver.FindElement(By.Id("btnImmpersonate"));
        public IWebElement TokenValidationText => driver.FindElement(By.XPath("//span[@class='subtitle']"));
        public IWebElement SecondFactorTokenField => driver.FindElement(By.XPath("//div[@id='AdminLogin']//input[2]"));
        public IWebElement AuthenticateButton => driver.FindElement(By.XPath("//button[@class='btn btn-success']"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//button[@class='btn btn-success-icon btn-login']"));
        public IWebElement PleaseEnterTokenText => driver.FindElement(By.XPath("//span[@class='subtitle']"));
        public IWebElement SecondFactorTokenValue => driver.FindElement(By.Id("SecondToken"));
        public IWebElement ImpersonateErrorText => driver.FindElement(By.XPath("//*[contains(text(),'action is not allowed while impersonating')]"));
        public IWebElement EmailEditButton => driver.FindElement(By.ClassName("btn-info"));
        public IWebElement EmailAddressInput => driver.FindElement(By.CssSelector("[data-bind='value: newEmail']"));
        public IWebElement SaveButton => driver.FindElement(By.CssSelector("[data-bind='click: updateEmail']"));
        public IWebElement UnlockAccountButton => driver.FindElement(By.Id("btnUnlock"));
        public IWebElement EnablePopupButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary']"));
        public IWebElement ResetPasswordButton => driver.FindElement(By.XPath("//button[contains(text(),'Reset Password')]"));
        public IWebElement ResetPasswordPopupButton => driver.FindElement(By.XPath("//button[contains(text(),'Reset Password')]"));
        public IWebElement TokenKeyInput => driver.FindElement(By.XPath("//*[@id='AdminLogin']/input[2]"));

        public void EnterUsername(string username)
        {
            UsernameInput.Clear();
            genericHelper.sendKeys(UsernameInput, username, "Username Input");
        }

        public void SelectSearchByDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(SearchByDropdown, index);
        }

        public void ClickSearchButton()
        {
            genericHelper.clickOn(SearchButton, "Search Button");
        }

        public void ClickDisableAccountButton()
        {
            genericHelper.waitForElement(DisableAccountButton);
            genericHelper.clickOn(DisableAccountButton, "Disable Account Button");
        }

        public string GetDisableAccountText()
        {
            genericHelper.waitForElement(DisableAccountText);
            return DisableAccountText.Text;
        }

        public void ClickDisableAccountPopupButton()
        {
            genericHelper.clickOn(DisableAccountPopupButton, "Disable Account Popup Button");
        }

        public void ClickEnableAccountButton()
        {
            genericHelper.waitForElement(EnableAccountButton);
            genericHelper.clickOn(EnableAccountButton, "Enable Account Button");
        }

        public string GetEnableAccountText()
        {
            genericHelper.waitForElement(EnableAccountText);
            return EnableAccountText.Text;
        }

        public void ClickEnableAccountPopupButton()
        {
            genericHelper.clickOn(EnableAccountPopupButton, "Enable Account Popup Button");
        }

        public void ClickImpersonateButton()
        {
            genericHelper.waitForElement(ImpersonateButton);
            genericHelper.clickOn(ImpersonateButton, "Impersonate Button");
        }

        public string GetTokenValidationText()
        {
            genericHelper.waitForElement(TokenValidationText);
            return TokenValidationText.Text;
        }

        public string CopySecondFactorToken()
        {
            genericHelper.waitForElement(SecondFactorTokenField);
            return SecondFactorTokenField.GetAttribute("value");
        }

        public void EnterSecondFactorToken(string token)
        {
            SecondFactorTokenValue.Clear();
            genericHelper.sendKeys(SecondFactorTokenValue, token, "Second Factor Token Field");
        }

        public void ClickAuthenticateButton()
        {
            genericHelper.clickOn(AuthenticateButton, "Authenticate Button");
        }

        public void ClickLoginButton()
        {
            genericHelper.clickOn(LoginButton, "Login Button");
            Thread.Sleep(2000);
        }

        public void SelectSearchByDropdownValue(int value)
        {
            genericHelper.selectValueFromDropdown(SearchByDropdown, value);
        }

        public void ClickEmailEditButton()
        {
            genericHelper.clickOn(EmailEditButton, "Email Edit Button");
        }

        public void EnterEmailAddress(string email)
        {
            EmailAddressInput.Clear();
            genericHelper.sendKeys(EmailAddressInput, email, "Email Address Input");
        }

        public void ClickSaveButton()
        {
            genericHelper.clickOn(SaveButton, "Save Button");
        }

        public void ClickUnlockAccountButton()
        {
            genericHelper.clickOn(UnlockAccountButton, "Unlock Account Button");
        }

        public void ClickEnablePopupButton()
        {
            genericHelper.clickOn(EnablePopupButton, "Enable Popup Button");
        }

        public void ClickResetPasswordButton()
        {
            genericHelper.clickOn(ResetPasswordButton, "Reset Password Button");
        }

        public void ClickResetPasswordPopupButton()
        {
            genericHelper.clickOn(ResetPasswordPopupButton, "Reset Password Popup Button");
        }

        public string CopyTokenKey()
        {
            return TokenKeyInput.GetAttribute("value");
        }

        public void EnterSecondToken(string token)
        {
            SecondFactorTokenValue.Clear();
            genericHelper.sendKeys(SecondFactorTokenValue, token, "Second Token Input");
        }

        public string GetPleaseEnterTokenText()
        {
            genericHelper.waitForElement(PleaseEnterTokenText);
            return PleaseEnterTokenText.Text;
        }

        public string GetImpersonateErrorText()
        {
            genericHelper.waitForElement(ImpersonateErrorText);
            return ImpersonateErrorText.Text;
        }

        public string GetUsernameText()
        {
            genericHelper.waitForElement(UsernameInput);
            return UsernameInput.GetAttribute("value");
        }
    }
}