using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using DocumentFormat.OpenXml.Bibliography;
using log4net;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Origination.Utils
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
            Console.WriteLine("Wait Object Created ");
            return wait;
        }

        public void WaitForWebElementToBeClickable(By locator, TimeSpan timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(5000));
            
        }

        public void clickElement(IWebElement webElement)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            try
            {
               
                webElement.Click();
                Console.WriteLine($"Clicked on element: {webElement}");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void waitForElement(IWebElement element)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(180));
            wait.Until(driver => ExpectedConditions.ElementToBeClickable(element));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }
        public void EnterText(IWebElement element, string text)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine($"Entered text '{text}' into element: {element}");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void SelectDropdownValue(IWebElement dropdown, string value)
        {
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByText(value);
            Console.WriteLine($"Selected value {value} from dropdown: {dropdown}");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void ValidateElementPresence(IWebElement element, string elementName)
        {
            if (element.Displayed)
            {
                Console.WriteLine($"{elementName} is available on the page.");
            }
            else
            {
                Console.WriteLine($"{elementName} is not available on the page.");
            }
        }

        public void SelectDateFromCalendar(string targetDay, string targetMonth, string targetYear)
        {
            // Navigate to correct Year
            IWebElement YearElement = driver.FindElement(By.XPath($"//option[@value='{targetYear}']"));
            YearElement.Click();
            Thread.Sleep(3000);

            // Navigate to correct month
            IWebElement monthElement = driver.FindElement(By.XPath($"//option[text()='{targetMonth}']"));
            monthElement.Click();
            Thread.Sleep(3000);

            // Select the day
            IWebElement dayElement = driver.FindElement(By.XPath($"(//div[@class='btn-light ng-star-inserted'])[{targetDay}]"));
            dayElement.Click();
            Thread.Sleep(20000);


        }
        public bool AssertionFunction(IWebElement element)
        {
            try
            {
                bool isDisplayed = element.Displayed;
                Console.WriteLine($"Element [{element}] is displayed");
                return isDisplayed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Element is not displayed or could not be found. Exception: {ex.Message}");
                return false;
            }
        }

    }
}
