using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountInboxPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountInboxPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement InboxLink => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[2]/a"));
        public IWebElement AllMessagesText => driver.FindElement(By.XPath("//div[@class='col-xs-6']/p"));
        public IWebElement NewMessageBoxLink => driver.FindElement(By.XPath("//*[@id='inboxPage']/div[1]/div/div[1]/div[1]/div/div/div[1]/div[2]/a/img"));
        public IWebElement ContactCustomerServiceHeader => driver.FindElement(By.XPath("//div[@class='modal-header']//h5[contains(text(),'Contact Customer Service')]"));
        public IWebElement AccountDropdown => driver.FindElement(By.Id("ccs-account"));
        public IWebElement SubjectDropdown => driver.FindElement(By.XPath("//*[@id='ccs-subject']"));
        public IWebElement MessageInputText => driver.FindElement(By.XPath("//*[@id='ccs-message']"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[3]/button[1]"));
        public IWebElement ThankYouMessageText => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/p"));
        public IWebElement ContinueButton => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/button"));

        public void ClickInboxLink()
        {
            genericHelper.clickOn(InboxLink, "Inbox Link");
        }

        public bool IsAllMessagesTextDisplayed()
        {
            return AllMessagesText.Displayed;
        }

        public void ClickNewMessageBoxLink()
        {
            genericHelper.clickOn(NewMessageBoxLink, "New Message Box Link");
        }

        public bool IsContactCustomerServiceHeaderDisplayed()
        {
            return ContactCustomerServiceHeader.Displayed;
        }

        public void SelectAccountFromDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(AccountDropdown, index);
        }

        public void SelectSubjectFromDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(SubjectDropdown, index);
        }

        public void EnterMessage(string message)
        {
            genericHelper.sendKeys(MessageInputText, message, "Message Input Text");
        }

        public void ClickSubmitButton()
        {
            genericHelper.clickOn(SubmitButton, "Submit Button");
        }

        public bool IsThankYouMessageDisplayed()
        {
            return ThankYouMessageText.Displayed;
        }

        public void ClickContinueButton()
        {
            genericHelper.clickOn(ContinueButton, "Continue Button");
        }
    }
}