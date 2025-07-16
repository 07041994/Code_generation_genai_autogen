using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class RequestPayoffQuotePage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public RequestPayoffQuotePage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement RequestPayoffQuoteLink => driver.FindElement(By.XPath("(//a[contains(text(),'Request Payoff Quote')])[2]"));
        public IWebElement RequestPayoffHeader => driver.FindElement(By.XPath("//*[@id='payoffQuotePage']/div[1]/div/h1"));
        public IWebElement EstimatedPayoffQuoteText => driver.FindElement(By.XPath("//*[@id='payoffQuotePage']/div[4]/div/div/div/h2"));
        public IWebElement PrintButton => driver.FindElement(By.XPath("//a[@class='button secondary']"));

        public void ClickRequestPayoffQuote()
        {
            genericHelper.clickOn(RequestPayoffQuoteLink, "Request Payoff Quote Link");
        }

        public string GetRequestPayoffHeaderText()
        {
            genericHelper.waitForElement(RequestPayoffHeader);
            return RequestPayoffHeader.Text;
        }

        public string GetEstimatedPayoffQuoteText()
        {
            genericHelper.waitForElement(EstimatedPayoffQuoteText);
            return EstimatedPayoffQuoteText.Text;
        }

        public void ValidatePrintButton()
        {
            genericHelper.ValidateElementPresentOrNot(PrintButton, "Print Button");
        }
    }
}