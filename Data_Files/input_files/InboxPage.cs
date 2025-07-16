using log4net;
using MyAccount.Utils;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace MyAccount.PageObjects
{
    public class InboxPage(IWebDriver driver)
    {
        public IWebElement inboxLink => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[2]/a"));
        //public IWebElement allMessagesText => driver.FindElement(By.XPath("//*[@id='inboxPage']/div[1]/div/div[1]/div[1]/div/div/div[1]/div[1]/p"));
        public IWebElement allMessagesText => driver.FindElement(By.XPath("//div[@class='col-xs-6']/p"));
        public IWebElement newMessageBoxLink => driver.FindElement(By.XPath("//img[@class='new-message']"));
        public IWebElement contactCustomerServiceHeader => driver.FindElement(By.XPath("//div[@class='modal-header']//h5[contains(text(),'Contact Customer Service')]"));
        public IWebElement selectAccountYouAreContactingAboutDropdown => driver.FindElement(By.Id("ccs-account"));
        public IWebElement subjectDropdown => driver.FindElement(By.Id("ccs-subject"));
        public IWebElement messageInputText => driver.FindElement(By.Id("ccs-message"));
        public IWebElement submitButton => driver.FindElement(By.XPath("(//button[@type='submit'])[2]"));
        //TC47 - cancel button is not able to find the webelement because xpath has changed //XPath("(//button[@type='submit'])[3]"));
        public IWebElement thankYouMessageText => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/p"));
        public IWebElement continueButton => driver.FindElement(By.XPath("//*[@id='Message']/div/div/div[2]/div[1]/button"));

        string inboxLinkXpath = "//*[@id='menu-secondary']/li[2]/a";
        string contactCustemerServiceXpath = "//div[@class='modal-header']//h5[contains(text(),'Contact Customer Service')]";
        string newMsgXpath = "//img[@class='new-message']";
        GenericHelper genericHelper = new GenericHelper(driver);

        public void selectSubject()
        {
            SelectElement select = new SelectElement(subjectDropdown);
            select.SelectByIndex(1);
            Console.WriteLine("Subject Selected");
        }
        public void selectAccount()
        {
            try
            {
                SelectElement select = new SelectElement(selectAccountYouAreContactingAboutDropdown);
                if (driver.FindElement(By.XPath("//*[@id='ccs-account']/option[1]")).Text.Contains("Select Account"))
                {
                    select.SelectByIndex(2);
                    Console.WriteLine("Account Selected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        public string ValidateNewMsgInInbox()
        {
            genericHelper.WaitForWebElementToBeClickable(By.XPath(inboxLinkXpath), TimeSpan.FromSeconds(10));
            genericHelper.clickOn(inboxLink, "Inbox Link");
            return allMessagesText.Text;
        }
        public string ValidateContactCustomerServiceHeaderText()
        {
            genericHelper.WaitForWebElementToBeClickable(By.XPath(newMsgXpath), TimeSpan.FromSeconds(30));
            genericHelper.clickOn(newMessageBoxLink, "Message Link");
            genericHelper.WaitForWebElementToBeClickable(By.XPath(contactCustemerServiceXpath), TimeSpan.FromSeconds(10));
            return contactCustomerServiceHeader.Text;
        }

        public string ValidateThankYouMessageText()
        {
            selectAccount();
            selectSubject();
            genericHelper.sendKeys(messageInputText, "This is a test message", "Message Box");
            genericHelper.clickOn(submitButton, "Submit Button");
            genericHelper.GetWebdriverWait(TimeSpan.FromSeconds(10));
            return thankYouMessageText.Text;
            //Assert.That(thankYouMessageText.Text, Is.EqualTo(thankyouMsgText));
        }

        public void ClickContinueButton()
        {
            genericHelper.clickOn(continueButton, "Continue Button");
        }
    }
}
