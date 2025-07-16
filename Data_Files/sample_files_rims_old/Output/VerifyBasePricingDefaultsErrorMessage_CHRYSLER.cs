using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Originations.PageObjects;

[TestFixture]
public class VerifyBasePricingDefaultsErrorMessage_CHRYSLER
{
    private IWebDriver driver;
    private BasePricingDefaultsErrorMessage_CHRYSLER pageObject;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        pageObject = new BasePricingDefaultsErrorMessage_CHRYSLER(driver)   

    }

    [Test]
    public void TestSteps()
    {
        string jsonFilePath = "BasePricingDefaultsErrorValidationDetos.json";
        var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath));

        string username = jsonData["username"].ToString();
        string password = jsonData["password"].ToString();
        string captivePartner = jsonData["captivePartner"].ToString();
        string role = jsonData["role"].ToString();
        string effFrmDay = jsonData["effFrmDay"].ToString();
        string effFrmMonth = jsonData["effFrmMonth"].ToString();
        string effFrmYear = jsonData["effFrmYear"].ToString();
        string minTermDecimal = jsonData["minTermDecimal"].ToString();
        string maxTermDecimal = jsonData["maxTermDecimal"].ToString();
        string pricingAttributeType1 = jsonData["pricingAttributeType1"].ToString();
        string pricingAttributeType2 = jsonData["pricingAttributeType2"].ToString();
        string pricingAttributeType3 = jsonData["pricingAttributeType3"].ToString();
        string pricingAttributeType4 = jsonData["pricingAttributeType4"].ToString();
        string pricingAttributeMinAmountBase = jsonData["pricingAttributeMinAmountBase"].ToString();
        string pricingAttributeMaxAmountBase = jsonData["pricingAttributeMaxAmountBase"].ToString();
        string pricingAttributeAmountLeaseParticipation = jsonData["pricingAttributeAmountLeaseParticipation"].ToString();
        string pricingAttributeAmountSDLeast = jsonData["pricingAttributeAmountSDLeast"].ToString();
        string pricingAttributeAmountSDMost = jsonData["pricingAttributeAmountSDMost"].ToString();
        string lessMinTerm = jsonData["LessMinTerm"].ToString();
        string lessMaxTerm = jsonData["LessMaxTerm"].ToString();

        // Step 1: Login to RIMS
        driver.Navigate().GoToUrl("https://rims-tst1.app.corpint.net/#/");
        genericHelper.EnterText(driver.FindElement(By.Id("username")), username);
        genericHelper.EnterText(driver.FindElement(By.Id("password")), password);
        driver.FindElement(By.Id("loginButton")).Click(); 
        
        // Step 2: Select 'Chrysler' from captive partner drop-down
        pageObject.SelectCaptivePartner("Chrysler");

        // Step 3: Click on the Incentives tab
        pageObject.ClickIncentivesTab();

        // Step 4: Click on the 'Base Pricing Defaults'
        pageObject.ClickBasePricingDefaults();

        // Step 5: Click on the 'Create New Base Pricing Defaults'
        pageObject.ClickCreateNewBasePricingDefaults();

        // Step 6: Click "Save" button
        pageObject.ClickSaveButton();

        // Step 7: Verify the Error messages are displayed
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankEffectiveFrom), Is.True, "Effective From Date is required message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankPricingAttributeType), Is.True, "Pricing Attribute Type is required message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankPricingAttributeAmount), Is.True, "Pricing Attribute Amount is required message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankMinTerm), Is.True, "Min Term is required message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankMaxTerm), Is.True, "Max Term is required message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.blankTier), Is.True, "You must select at least one tier message is not displayed.");

        // Step 8: Select 31 days before Current Date as Effective From Date from Calendar
        DateTime targetDate = DateTime.Today.AddDays(-31);
        pageObject.SelectEffectiveFromDate(targetDate.Day.ToString(), targetDate.ToString("MMMM"), targetDate.Year.ToString());

        // Step 9: Verify the Error message for backdated Effective From Date
        Assert.That(pageObject.IsElementDisplayed(pageObject.invalidEffectiveFromDate), Is.True, "Effective From Date cannot be backdated more than 30 days from today message is not displayed.");

        // Step 10: Enter 4.5 for Min Term and 3.5 for Max Term
        pageObject.EnterMinTerm("4.5");
        pageObject.EnterMaxTerm("3.5");

        // Step 11: Verify the Error messages for decimal and Max < Min
        Assert.That(pageObject.IsElementDisplayed(pageObject.minDecimalValue), Is.True, "Min Term cannot have a decimal message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.maxDecimalValue), Is.True, "Max Term cannot have a decimal message is not displayed.");
        Assert.That(pageObject.IsElementDisplayed(pageObject.higherMinThanMax), Is.True, "Max Term cannot be less than Min Term message is not displayed.");

        // Step 12: Select "Base Lease Flat" for Pricing Attribute Type
        pageObject.SelectPricingAttributeType("Base Lease Flat");

        // Step 13: Enter 15 for Pricing Attribute Amount and verify error
        pageObject.EnterPricingAttributeAmount("15");
        Assert.That(pageObject.IsElementDisplayed(pageObject.baseLeastFifty), Is.True, "Pricing Attribute Amount must be at least 50 message is not displayed.");

        // Step 14: Enter 5000 for Pricing Attribute Amount and verify error
        pageObject.EnterPricingAttributeAmount("5000");
        Assert.That(pageObject.IsElementDisplayed(pageObject.baseMostThousandFiveHundred), Is.True, "Pricing Attribute Amount must be at most 1500 message is not displayed.");

        // Step 15: Select "Base Retail Flat" for Pricing Attribute Type
        pageObject.SelectPricingAttributeType("Base Retail Flat");

        // Step 16: Enter 15 for Pricing Attribute Amount and verify error
        pageObject.EnterPricingAttributeAmount("15");
        Assert.That(pageObject.IsElementDisplayed(pageObject.baseLeastFifty), Is.True, "Pricing Attribute Amount must be at least 50 message is not displayed.");

        // Step 17: Enter 5000 for Pricing Attribute Amount and verify error
        pageObject.EnterPricingAttributeAmount("5000");
        Assert.That(pageObject.IsElementDisplayed(pageObject.baseMostThousandFiveHundred), Is.True, "Pricing Attribute Amount must be at most 1500 message is not displayed.");

        // Step 18: Select "Lease Participation" for Pricing Attribute Type
        pageObject.SelectPricingAttributeType("Lease Participation");

        // Step 19: Enter 1 for Pricing Attribute Amount and verify error (greater than 0.00085)
        pageObject.EnterPricingAttributeAmount("1");
        Assert.That(pageObject.IsElementDisplayed(pageObject.leastParticipationMost), Is.True, 
            "Pricing Attribute Amount must be at most 0.00085 message is not displayed.");

        // Step 20: Select "Security Deposit" for Pricing Attribute Type
        pageObject.SelectPricingAttributeType("Security Deposit");

        // Step 21: Enter 0.20 for Pricing Attribute Amount and verify error (less than 0.25)
        pageObject.EnterPricingAttributeAmount("0.20");
        Assert.That(pageObject.IsElementDisplayed(pageObject.securityDepositLeast), Is.True, 
            "Pricing Attribute Amount must be at least 0.25 message is not displayed.");

        // Step 22: Enter 1.1 for Pricing Attribute Amount and verify error (greater than 1)
        pageObject.EnterPricingAttributeAmount("1.1");
        Assert.That(pageObject.IsElementDisplayed(pageObject.securityDepositMost), Is.True, 
            "Pricing Attribute Amount must be at most 1 message is not displayed.");

        // Step 23: Enter 0 for Min Term and verify error (must be at least 1)
        pageObject.EnterMinTerm("0");
        Assert.That(pageObject.IsElementDisplayed(pageObject.minTermValue), Is.True, 
            "Min term must be at least 1 message is not displayed.");

        // Step 24: Enter 101 for Max Term and verify error (must be at most 100)
        pageObject.EnterMaxTerm("101");
        Assert.That(pageObject.IsElementDisplayed(pageObject.maxTermValue), Is.True, 
            "Max Term must be at most 100 message is not displayed.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}