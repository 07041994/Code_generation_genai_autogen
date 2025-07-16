using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class InboxPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public InboxPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement AllMessagesText => driver.FindElement(By.XPath("//div[@class='col-xs-6']/p"));
        public IWebElement NewMessageButton => driver.FindElement(By.XPath("//*[@id='inboxPage']/div[1]/div/div[1]/div[1]/div/div/div[1]/div[2]/a/img"));
        public IWebElement ContactCustomerServiceHeader => driver.FindElement(By.XPath("//div[@class='modal-header']//h5[contains(text(),'Contact Customer Service')]"));
        public IWebElement AccountDropdown => driver.FindElement(By.Id("ccs-account"));
        public IWebElement SubjectDropdown => driver.FindElement(By.XPath("//*[@id='ccs-subject']"));
        public IWebElement MessageBox => driver.FindElement(By.XPath("//*[@id='ccs-message']"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[3]/button[1]"));
        public IWebElement ThankYouText => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/p"));
        public IWebElement ContinueButton => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/button"));

        public void ClickInbox()
        {
            genericHelper.clickOn(AllMessagesText, "Inbox");
        }

        public void ClickNewMessage()
        {
            genericHelper.clickOn(NewMessageButton, "New Message");
        }

        public void SelectAccount(int index)
        {
            genericHelper.selectValueFromDropdown(AccountDropdown, index);
        }

        public void SelectSubject(int index)
        {
            genericHelper.selectValueFromDropdown(SubjectDropdown, index);
        }

        public void EnterMessage(string message)
        {
            genericHelper.sendKeys(MessageBox, message, "Message Box");
        }

        public void ClickSubmit()
        {
            genericHelper.clickOn(SubmitButton, "Submit Button");
        }

        public void ClickContinue()
        {
            genericHelper.clickOn(ContinueButton, "Continue Button");
        }

        public void ValidateThankYouText()
        {
            genericHelper.ValidateElementPresentOrNot(ThankYouText, "Thank You Text");
        }
    }
}