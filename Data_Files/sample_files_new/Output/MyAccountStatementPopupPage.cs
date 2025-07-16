using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountStatementPopupPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountStatementPopupPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement VerbageMessage => driver.FindElement(By.XPath("//p[@class='alert-message' and contains(text(),'You have been enrolled in paperless statements')]"));
        public IWebElement ContactInformationLink => driver.FindElement(By.XPath("//p[@class='alert-message']//a[@href='/Profile/Personal?updateProfile=true']"));
        public IWebElement ClickHereLink => driver.FindElement(By.XPath("//p[@class='alert-message']//a[contains(text(),'click here')]"));
        public IWebElement UpdatePreferenceHeader => driver.FindElement(By.XPath("//h5[@class='modal-title' and contains(text(),'Update your statement preferences')]"));
        public IWebElement UsePaperRadioButton => driver.FindElement(By.XPath("//input[@type='radio' and @value='paper']"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//button[@type='submit']"));

        public void ClickContactInformationLink()
        {
            genericHelper.clickOn(ContactInformationLink, "Contact Information Link");
        }

        public void ClickClickHereLink()
        {
            genericHelper.clickOn(ClickHereLink, "Click Here Link");
        }

        public string GetUpdatePreferenceHeaderText()
        {
            genericHelper.waitForElement(UpdatePreferenceHeader);
            return UpdatePreferenceHeader.Text;
        }

        public void SelectUsePaperRadioButton()
        {
            genericHelper.clickOn(UsePaperRadioButton, "Use Paper Radio Button");
        }

        public void ClickSubmitButton()
        {
            genericHelper.clickOn(SubmitButton, "Submit Button");
        }
    }
}