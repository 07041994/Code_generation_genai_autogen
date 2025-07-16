using OpenQA.Selenium;
using Origination.Utils;
using System;
using System.Collections.Generic;

namespace Origination.PageObjects
{
    public class RatesPullViewAPRSorCreateNewOffers_262142
    {
        private IWebDriver driver;
        private GenericHelper helper;

        // Define constants for XPath values
        private static readonly Dictionary<string, string> XPaths = new Dictionary<string, string>
        {
            { "CaptivePartnerDropDown", "(//div[@class='rims-nav-item'])[2]/select" },
            { "Incentive", "//label[text()='Incentives']" },
            { "RetailAPRDelta", "(//div[contains(text(),'Retail APR Delta')])[1]" },
            { "NewRetailAPRDelta", "(//button[contains(@class,'create-card-element round-button')])[1]" },
            { "EffectiveFrom", "(//input[contains(@name,'dp')])[1]" },
            { "MinTerm", "(//input[contains(@name,'minTerm')])[1]" },
            { "MaxTerm", "(//input[contains(@name,'maxTerm')])[1]" },
            { "Tier2Delta", "//label[@for='tier2']" },
            { "Tier3Delta", "//label[@for='tier3']" },
            { "Tier4Delta", "//label[@for='tier4']" },
            { "ZeroRate1", "//input[@id='zeroRateTier2Amount']" },
            { "NonZeroRate1", "//input[@id='nonZeroRateTier2Amount']" },
            { "ZeroRate2", "//input[@id='zeroRateTier3Amount']" },
            { "NonZeroRate2", "//input[@id='nonZeroRateTier3Amount']" },
            { "ZeroRate3", "//input[@id='zeroRateTier4Amount']" },
            { "NonZeroRate3", "//input[@id='nonZeroRateTier4Amount']" },
            { "Save", "(//button[text()='Save'])[1]" },
            { "Submit", "(//button[text()='Submit'])[1]" },
            { "PendingApproval", "//label[contains(text(),'Pending Approval')]" },
            { "ManagerApproved", "(//label[@for='Manager-Approved'])[1]" },
            { "Approve", "(//div[contains(text(),'7')])[1]/following::div[1]/button[2]" },
            { "ReadyToPublish", "(//label[@for='ReadyToPublish'])[1]" },
            { "ViewAPRSorCreateNewOffers", "(//div[@class='card-text text-center'])[3]" },
            { "CreateViewAPRSorCreateNewOffers", "//button[@class='create-card-element round-button']" },
            { "Tier2", "//label[@for='tier2']" },
            { "Tier3", "//label[@for='tier3']" },
            { "Tier4", "//label[@for='tier4']" },
            { "Tier2ZeroRate", "//td[contains(text(),'3')]" },
            { "Tier3ZeroRate", "//td[contains(text(),'4')]" },
            { "Tier4ZeroRate", "//td[contains(text(),'5')]" }
        };

        public RatesPullViewAPRSorCreateNewOffers_262142(IWebDriver driver)
        {
            this.driver = driver;
            this.helper = new GenericHelper(driver);
        }

        public IWebElement GetElement(string key)
        {
            try
            {
                return driver.FindElement(By.XPath(XPaths[key]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error locating element with key {key}: {ex.Message}");
                throw;
            }
        }

        public void ClickElement(string key)
        {
            try
            {
                helper.clickElement(GetElement(key));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clicking element with key {key}: {ex.Message}");
                throw;
            }
        }

        public void EnterText(string key, string value)
        {
            try
            {
                helper.EnterText(GetElement(key), value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error entering text in element with key {key}: {ex.Message}");
                throw;
            }
        }

        public bool VerifyElementDisplayed(string key)
        {
            try
            {
                return helper.AssertionFunction(GetElement(key));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verifying element with key {key}: {ex.Message}");
                throw;
            }
        }
    }
}