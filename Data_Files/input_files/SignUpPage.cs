using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.PageObjects
{
    public class SignUpPage(IWebDriver driver)
    {
        string squery;
        Random rnd = new Random();
        DataBaseHelper objDB = new DataBaseHelper();
        LoginPage loginPage = new LoginPage(driver);

        public IWebElement SantanderLinkonSignUpPage => driver.FindElement(By.XPath("//*[@id='header-navigation']//*[@class='navbar-brand']"));
        public IWebElement TermsCheckbox => driver.FindElement(By.XPath("//*[@id='section-terms']/div/div/div/div[1]/label"));
        public IWebElement AccountNumber => driver.FindElement(By.Id("account-number"));
        public IWebElement DateofBirth => driver.FindElement(By.XPath("//*[@id='section-verify']/div/div/div/div[3]/div/input"));
        public IWebElement Next1 => driver.FindElement(By.XPath("(//button[contains(text(),'Next')])[1]"));
        
        
    }
}
