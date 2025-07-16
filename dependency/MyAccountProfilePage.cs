using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;
using System.Net.Mail;

namespace MyAccount.PageObjects
{
    public class MyAccountProfilePage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountProfilePage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement AccountProfileLink => driver.FindElement(By.XPath("//li[@class='menu-item account-profile']"));
        public IWebElement PrimaryAddressText => driver.FindElement(By.XPath("//h3[contains(text(),'Primary Address')]"));
        public IWebElement MailingAddress => driver.FindElement(By.Id("m-address"));
        public IWebElement City => driver.FindElement(By.Id("m-city"));
        public IWebElement State => driver.FindElement(By.Id("m-state"));
        public IWebElement ZipCode => driver.FindElement(By.Id("m-zip-code"));
        public IWebElement ExistingCellPhone => driver.FindElement(By.XPath("//*[@data-bind='phoneText: Number']"));
        public IWebElement CellPhone => driver.FindElement(By.Id("profile-phone"));
        public IWebElement EditCellPhone => driver.FindElement(By.XPath("//a[@data-bind='click: $parent.phoneEditor.editPhone']"));
        public IWebElement IsThisMobileDeviceYesRadioButton => driver.FindElement(By.XPath("//*[@for='mobile-device-radio-yes']"));
        public IWebElement AuthorizePhoneYesRadioButton => driver.FindElement(By.XPath("//*[@for='authorize-device-radio-yes']"));
        public IWebElement UpdateButton => driver.FindElement(By.XPath("//a[@class='small button' and contains(text(),'Update')]"));
        public IWebElement EmailAddress => driver.FindElement(By.Id("email-address"));
        public IWebElement StatementPreferenceElectronic => driver.FindElement(By.XPath("//label[@for='statement-electronic']"));
        public IWebElement EmailPreference => driver.FindElement(By.XPath("(//span[@class='fa fa-chevron-right fa-2x'])[1]"));
        public IWebElement AccountRelatedEmail => driver.FindElement(By.XPath("//*[@data-bind='toggleButton: CommunicationPreferences.EmailAccountNotifications']"));
        public IWebElement SmsPreference => driver.FindElement(By.XPath("(//span[@class='fa fa-chevron-right fa-2x'])[2]"));
        public IWebElement PromotionSpecialOffer => driver.FindElement(By.XPath("(//span[@class='fa fa-square fa-2x inactive'])[4]"));
        public IWebElement SaveButton => driver.FindElement(By.Id("profile-save-btn"));
        public IWebElement SavedMessage => driver.FindElement(By.XPath("//span[contains(text(),'updates to your demographic information have been saved')]"));
        public IWebElement BackToAccountSummary => driver.FindElement(By.XPath("//p[@class='legal-text']"));

        public void ClickAccountProfileLink()
        {
            genericHelper.clickOn(AccountProfileLink, "Account Profile Link");
        }

        public string GetPrimaryAddressText()
        {
            genericHelper.waitForElement(PrimaryAddressText);
            return PrimaryAddressText.Text;
        }

        public void EnterMailingAddress(string address, string city, string state, string zipCode)
        {
            MailingAddress.Clear();
            genericHelper.sendKeys(MailingAddress, address, "Mailing Address");
            City.Clear();
            genericHelper.sendKeys(City, city, "City");
            genericHelper.sendKeys(State, state, "State");
            ZipCode.Clear();
            genericHelper.sendKeys(ZipCode, zipCode, "Zip Code");
        }

        public void EnterCellPhone(string phoneNumber)
        {
            genericHelper.sendKeys(CellPhone, phoneNumber, "Cell Phone");
        }

        public void UpdateCellPhone(string newPhoneNumber)
        {
            genericHelper.waitForElement(EditCellPhone);
            genericHelper.clickOn(EditCellPhone, "Edit Cell Phone");
            if (EditCellPhone.Displayed)
            {
                genericHelper.clickOn(EditCellPhone, "Edit Cell Phone");
            }
            genericHelper.waitForElement(CellPhone);
            CellPhone.Clear();
            genericHelper.sendKeys(CellPhone, newPhoneNumber, "New Cell Phone");
            genericHelper.waitForElement(IsThisMobileDeviceYesRadioButton);
            if (IsThisMobileDeviceYesRadioButton.Displayed)
            {
                genericHelper.GetWebdriverWait(TimeSpan.FromSeconds(5));
                genericHelper.clickOn(IsThisMobileDeviceYesRadioButton, "Is This Mobile Device Yes Radio Button");
                genericHelper.clickOn(IsThisMobileDeviceYesRadioButton, "Is This Mobile Device Yes Radio Button");
                genericHelper.clickOn(AuthorizePhoneYesRadioButton, "Authorize Phone Yes Radio Button");
            }

            genericHelper.clickOn(UpdateButton, "Update Button");
        }

        public void EnterEmailAddress()
        {
            Random rnd = new Random();
            string email = "MyAccountTest" + rnd.Next(10000, 100000) + "@gmail.com";
            genericHelper.waitForElement(EmailAddress);
            EmailAddress.Clear();
            genericHelper.sendKeys(EmailAddress, email, "Email Address");
        }

        public void SelectStatementPreferenceElectronic()
        {
            genericHelper.clickOn(StatementPreferenceElectronic, "Statement Preference Electronic");
        }

        public void SelectEmailPreference()
        {
            genericHelper.clickOn(EmailPreference, "Email Preference");
            genericHelper.clickOn(AccountRelatedEmail, "Account Related Email");
        }

        public void SelectSmsPreference()
        {
            genericHelper.clickOn(SmsPreference, "SMS Preference");
            genericHelper.clickOn(PromotionSpecialOffer, "Promotion & Special Offer");
        }

        public void ClickSaveButton()
        {
            genericHelper.clickOn(SaveButton, "Save Button");
        }

        public string GetSavedMessage()
        {
            Thread.Sleep(3000);
            genericHelper.waitForElement(SavedMessage);
            return SavedMessage.Text;
        }

        public void ClickBackToAccountSummary()
        {
            genericHelper.clickOn(BackToAccountSummary, "Back to Account Summary");
        }
    }
}