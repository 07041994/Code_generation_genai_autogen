using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MaturityModificationPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MaturityModificationPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement MatmodHeader => driver.FindElement(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']"));
        public IWebElement ClickHereLink => driver.FindElement(By.XPath("//a[contains(@data-bind, 'goToAutomateMatMod')]"));
        public IWebElement AccountMaturityHeader => driver.FindElement(By.XPath("//h3[text()='Account Maturity Modification']"));
        public IWebElement VisitFaqLink => driver.FindElement(By.XPath("//a[@href='https://santanderconsumerusa.com/support#/categories/5d8346e1-55cf-4b62-bfef-fd76bce4c47c']"));
        public IWebElement PayoffRemainingBalance => driver.FindElement(By.XPath("//h4[text()='Payoff your remaining balance']"));
        public int matModHeaderCount => driver.FindElements(By.XPath("//h4[@class='h4']/strong[text()='Maturity Date']")).Count;
        
        public void ClickClickHereLink()
        {
            genericHelper.clickOn(ClickHereLink, "Click Here Link");
        }

        public string GetMatmodHeaderText()
        {
            genericHelper.waitForElement(MatmodHeader);
            return MatmodHeader.Text;
        }

        public string GetAccountMaturityHeaderText()
        {
            genericHelper.waitForElement(AccountMaturityHeader);
            return AccountMaturityHeader.Text;
        }

        public void ClickVisitFaqLink()
        {
            genericHelper.clickOn(VisitFaqLink, "Visit FAQ Link");
        }

        public void ClickPayoffRemainingBalance()
        {
            genericHelper.clickOn(PayoffRemainingBalance, "Payoff Remaining Balance");
        }
    }
}