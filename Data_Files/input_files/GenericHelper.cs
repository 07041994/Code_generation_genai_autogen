using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using log4net;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MyAccount.Utils
{
    public class GenericHelper(IWebDriver driver)
    {

        public WebDriverWait GetWebdriverWait(TimeSpan timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(2));
            WebDriverWait wait = new WebDriverWait(driver, timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(500),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            Console.WriteLine("Wait Object Created ");
            return wait;
        }

        public void WaitForWebElementToBeClickable(By locator, TimeSpan timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            //Console.WriteLine(" Setting the Explicit wait to 1 sec ");
            var wait = GetWebdriverWait(timeout);
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(5000));
            //Console.WriteLine(" Setting the Explicit wait Configured value ");
            // return null;
        }

        public void clickOn(IWebElement webElement, string field)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            try
            {
                webElement.Click();
                Console.WriteLine("Clicked on " + field);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void sendKeys(IWebElement webElement, string message, string field)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));
            webElement.SendKeys(message);
            Console.WriteLine("Entered input in " + field + " as : " + message);
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void waitForElement(IWebElement element)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(180));
            wait.Until(driver => ExpectedConditions.ElementToBeClickable(element));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void selectValueFromDropdown(IWebElement element, int index)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByIndex(index);
            Console.WriteLine("Selected " + index + " index from Dropdown");
        }

        public void ValidateElementPresentOrNot(IWebElement element, string field)
        {
            if (element.Displayed)
            {
                Console.WriteLine(field+" displayed");
            }
            else
            {
                Console.WriteLine(field+" not displayed");
            }

        }

        public void waitForElementToBeVisible(IWebElement element, string textToBeVisible)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => ExpectedConditions.TextToBePresentInElement(element,textToBeVisible));
            wait.Until(driver => element.Displayed);
        }
    }
}
