using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.IO;

[TestFixture]
public class VerifyCPOVAPROffersFieldValidation_INEOS
{
    private IWebDriver driver;
    private CPOVAPROffersFieldValidation_INEOS pageObject;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        pageObject = new CPOVAPROffersFieldValidation_INEOS(driver);
    }

    [Test]
    public void TestCPOVAPROffersFieldValidation()
    {
        // Load Datos Variables
        string jsonFilePath = "INEOSCPOVAPROffersFieldValidationsDetos.json";
        var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath));
        string username = jsonData["username"].ToString();
        string password = jsonData["password"].ToString();
        string captivePartner = jsonData["CaptivePartner"].ToString();
        string effFrmDay = jsonData["effFrmDay"].ToString();
        string effFrmMonth = jsonData["effFrmMonth"].ToString();
        string effFrmYear = jsonData["effFrmYear"].ToString();
        string effThruDay = jsonData["effThruDay"].ToString();
        string effThruMonth = jsonData["effThruMonth"].ToString();
        string effThruYear = jsonData["effThruYear"].ToString();

        // Step 1: Log into the RIMS
        driver.Navigate().GoToUrl("https://rims-tst1.app.corpint.net/#/");
        genericHelper.EnterText(driver.FindElement(By.Id("username")), username);
        genericHelper.EnterText(driver.FindElement(By.Id("password")), password);
        driver.FindElement(By.Id("loginButton")).Click(); 
        Console.WriteLine("Logged into RIMS successfully.");
        Thread.Sleep(10000);

        // Step 2: Select Captive Partner
        pageObject.SelectCaptivePartner(captivePartner);
        Console.WriteLine($"Captive Partner '{captivePartner}' selected successfully.");

        // Step 3: Click on Incentives Tab and Verify Header
        pageObject.ClickIncentivesTab();
       Assert.That(pageObject.IsElementDisplayed(pageObject.incentivesHeader), Is.True, "Incentives header is not displayed.");

        // Step 4: Click on 'View or Create New CPOV APR Offers' button and Verify Header
        pageObject.ClickCreateOrViewCPOVAPROffers();
        Assert.That(pageObject.IsElementDisplayed(pageObject.CPOVAPROfferHeader), Is.True, "CPOV APR Offer - ChangeSet Summary header is not displayed.");

        // Step 5: Click on 'Create a New CPOV APR Offer' button and Verify Header
        pageObject.ClickCreateCard();
       Assert.That(pageObject.IsElementDisplayed(pageObject.NewCPOVAPROfferHeader), Is.True, "New CPOV APR Offer - Create New header is not displayed.");

        // Step 6: Click Select Vehicles
        pageObject.ClickSelectVehicles();
        Console.WriteLine("Select Vehicles button clicked successfully.");

        // Step 7: Verify Effective Thru Date Error
        pageObject.ValidateEffectiveThruDateError();
        Console.WriteLine("Effective Thru Date error validated successfully.");
        Thread.Sleep(10000);
        // Step 8: Select Past Date for Effective Thru Date
        pageObject.EffectiveThruField.Click();
        pageObject.EffectiveYear.Click();
        pageObject.genericHelper.SelectDateFromCalendar(effThruDay, effThruMonth, effThruYear);
        Console.WriteLine($"Past Effective Thru Date '{effThruDay}-{effThruMonth}-{effThruYear}' selected successfully.");

        // Step 9: Verify Past Effective Thru Date Errors
        pageObject.ValidatePastEffectiveThruDateErrors();
        Console.WriteLine("Past Effective Thru Date errors validated successfully.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}