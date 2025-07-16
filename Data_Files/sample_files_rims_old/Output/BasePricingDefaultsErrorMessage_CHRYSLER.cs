using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origination.Utils;

namespace PageObjects
{
    public class BasePricingDefaultsErrorMessage_CHRYSLER
    {
        private IWebDriver driver;
        public GenericHelper genericHelper;

        public BasePricingDefaultsErrorMessage_CHRYSLER(IWebDriver driver)
        {
            this.driver = driver;
            genericHelper = new GenericHelper(driver);
        }

        // Dashboard Elements
        public IWebElement captivePartnerDropDown => driver.FindElement(By.XPath("(//div[@class='rims-nav-item'])[2]/select"));

        // Incentive Elements
        public IWebElement incentiveTab => driver.FindElement(By.XPath("//label[text()='Incentives']"));
        public IWebElement basePricingDefaults => driver.FindElement(By.XPath("//div[text()='Base Pricing Defaults']"));
        public IWebElement createNewBasePricingDefaults => driver.FindElement(By.XPath("//button[@class='create-card-element round-button']"));
        public IWebElement saveButton => driver.FindElement(By.XPath("//button[text()='Save']"));
        public IWebElement pricingAttributeType => driver.FindElement(By.XPath("//select[@placeholder='Attribute Type']"));
        public IWebElement pricingAttributeAmount => driver.FindElement(By.XPath("//input[@placeholder='Attribute Amount']"));
        public IWebElement minTerm => driver.FindElement(By.XPath("//input[@placeholder='Min Term']"));
        public IWebElement maxTerm => driver.FindElement(By.XPath("//input[@placeholder='Max Term']"));
        public IWebElement selectTiers => driver.FindElement(By.XPath("//label[@for='tierAll']"));

        // Error Messages
        public IWebElement blankEffectiveFrom => driver.FindElement(By.XPath("//div[text()=' Effective From Date is required. ']"));
        public IWebElement blankPricingAttributeType => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Type is required. ']"));
        public IWebElement blankPricingAttributeAmount => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount is required. ']"));
        public IWebElement blankMinTerm => driver.FindElement(By.XPath("//div[text()=' Min Term is required. ']"));
        public IWebElement blankMaxTerm => driver.FindElement(By.XPath("//div[text()=' Max Term is required. ']"));
        public IWebElement blankTier => driver.FindElement(By.XPath("//div[text()=' You must select at least one tier. ']"));
        public IWebElement invalidEffectiveFromDate => driver.FindElement(By.XPath("//div[text()=' Effective From Date cannot be backdated more than 30 days from today. ']"));
        public IWebElement minDecimalValue => driver.FindElement(By.XPath("//div[text()=' Min Term cannot have a decimal. ']"));
        public IWebElement maxDecimalValue => driver.FindElement(By.XPath("//div[text()=' Max Term cannot have a decimal. ']"));
        public IWebElement higherMinThanMax => driver.FindElement(By.XPath("//div[text()=' Max Term cannot be less than Min Term. ']"));
        public IWebElement baseLeastFifty => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount must be at least 50 ']"));
        public IWebElement baseMostThousandFiveHundred => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount must be at most 1500 ']"));
        public IWebElement leastParticipationMost => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount must be at most 0.00085 ']"));
        public IWebElement securityDepositLeast => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount must be at least 0.25 ']"));
        public IWebElement securityDepositMost => driver.FindElement(By.XPath("//div[text()=' Pricing Attribute Amount must be at most 1 ']"));
        public IWebElement minTermValue => driver.FindElement(By.XPath("//div[text()=' Min term must be at least 1. ']"));
        public IWebElement maxTermValue => driver.FindElement(By.XPath("//div[text()=' Max Term must be at most 100. ']"));

        // Methods
        public void SelectCaptivePartner(string partner)
        {
            genericHelper.SelectDropdownValue(captivePartnerDropDown, partner);
        }

        public void ClickIncentivesTab()
        {
            genericHelper.clickElement(incentiveTab);
        }

        public void ClickBasePricingDefaults()
        {
            genericHelper.clickElement(basePricingDefaults);
        }

        public void ClickCreateNewBasePricingDefaults()
        {
            genericHelper.clickElement(createNewBasePricingDefaults);
        }

        public void ClickSaveButton()
        {
            genericHelper.clickElement(saveButton);
        }

        public void SelectPricingAttributeType(string type)
        {
            genericHelper.SelectDropdownValue(pricingAttributeType, type);
        }

        public void EnterPricingAttributeAmount(string amount)
        {
            genericHelper.EnterText(pricingAttributeAmount, amount);
        }

        public void EnterMinTerm(string term)
        {
            genericHelper.EnterText(minTerm, term);
        }

        public void EnterMaxTerm(string term)
        {
            genericHelper.EnterText(maxTerm, term);
        }

        public void SelectAllTiers()
        {
            genericHelper.clickElement(selectTiers);
        }

        public void SelectEffectiveFromDate(string day, string month, string year)
        {
            genericHelper.SelectDateFromCalendar(day, month, year);
        }

        public bool IsElementDisplayed(IWebElement element)
        {
            return genericHelper.AssertionFunction(element);
        }
    }
}
