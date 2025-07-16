using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountViewOptionsPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountViewOptionsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement ViewOptionsButton => driver.FindElement(By.XPath("//*[@id='accountSummaryPage']//div/button[contains(text(),'View Options')]"));
        public IWebElement ViewOptionVerbiage => driver.FindElement(By.XPath("(//*[@id='accountSummaryPage']//span)[67]"));

        public void ClickViewOptionsButton()
        {
            genericHelper.clickOn(ViewOptionsButton, "View Options Button");
        }

        public string GetViewOptionVerbiageText()
        {
            genericHelper.waitForElement(ViewOptionVerbiage);
            return ViewOptionVerbiage.Text;
        }

        public void ValidateViewOptionsButtonDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(ViewOptionsButton, "View Options Button");
        }

        public void ValidateViewOptionVerbiageDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(ViewOptionVerbiage, "View Option Verbiage");
        }
    }
}