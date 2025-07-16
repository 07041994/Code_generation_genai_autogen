using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.IO;

[TestFixture]
public class VerifyCPOVAPROFFERSManagerRejectedCard_INEOS
{
    private IWebDriver driver;
    private CPOVAPROFFERSManagerRejectedCard_INEOS pageObject;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        pageObject = new CPOVAPROFFERSManagerRejectedCard_INEOS(driver);
    }

    [Test]
    public void TestCPOVAPROFFERSManagerRejectedCard()
    {
        // Load Datos Variables
        string jsonFilePath = "INEOSCPOVAPROFFERSManagerRejectedCardDetos.json";
        var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath));
        string username = jsonData["username"].ToString();
        string password = jsonData["password"].ToString();
        string captivePartner = jsonData["captivePartner"].ToString();
        string effFrmDay1 = jsonData["effFrmDay1"].ToString();
        string effFrmMonth1 = jsonData["effFrmMonth1"].ToString();
        string effFrmYear1 = jsonData["effFrmYear1"].ToString();
        string effFrmDay2 = jsonData["effFrmDay2"].ToString();
        string effFrmMonth2 = jsonData["effFrmMonth2"].ToString();
        string effFrmYear2 = jsonData["effFrmYear2"].ToString();
        string ratePercentageInput = jsonData["RatePercentageInput"].ToString();
        string tierDropdown = jsonData["TierDropdown"].ToString();
        string minTerm = jsonData["MinTerm"].ToString();
        string maxTerm = jsonData["MaxTerm"].ToString();
        string rejectionReason = jsonData["RejectionReason"].ToString();

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
        Assert.That(pageObject.incentivesHeader.Displayed, Is.True, "Incentives header is not displayed.");

        // Step 4: Click on 'View or Create New CPOV APR Offers' button and Verify Header
        pageObject.ClickViewOrCreateCPOVAPROffers();
        Assert.That(pageObject.CPOVAPROfferHeader.Displayed, Is.True, "CPOV APR Offer - ChangeSet Summary header is not displayed.");

        // Step 5: Click on 'Create a New CPOV APR Offer' button and Verify Header
        pageObject.ClickCreateCPOVAPROffers();
        Assert.That(pageObject.newCPOVAPROfferHeader.Displayed, Is.True, "New CPOV APR Offer - Create New header is not displayed.");

        // Step 6: Select Effective Thru Date
        pageObject.SelectEffectiveThruDate(effFrmDay1, effFrmMonth1, effFrmYear1);
        Console.WriteLine($"Effective Thru Date '{effFrmDay1}-{effFrmMonth1}-{effFrmYear1}' selected successfully.");

        // Step 7-14: Enter Rate Percentage, Min Term, Max Term, and Tier
        pageObject.EnterRatePercentage(ratePercentageInput);
        Console.WriteLine($"Rate Percentage '{ratePercentageInput}' entered successfully.");
        pageObject.EnterMinTerm(minTerm);
        Console.WriteLine($"Min Term '{minTerm}' entered successfully.");
        pageObject.EnterMaxTerm(maxTerm);
        Console.WriteLine($"Max Term '{maxTerm}' entered successfully.");
        pageObject.SelectTier(tierDropdown);
        Console.WriteLine($"Tier '{tierDropdown}' selected successfully.");

        // Step 15-18: Select Vehicles
        pageObject.ClickSelectVehicles();
        pageObject.SelectVehicleOptions();
        Console.WriteLine("Vehicle options selected successfully.");

        // Step 19: Click Save
        pageObject.ClickSave();
        Console.WriteLine("Save button clicked successfully.");

        // Step 20: Capture and Save the FirstChangeSetID
        string firstChangeSetID = pageObject.GetFirstChangeSetID();
        Console.WriteLine($"First ChangeSetID captured: {firstChangeSetID}");

        // Step 21: Click Submit
        pageObject.ClickSubmit();
        Console.WriteLine("Submit button clicked successfully.");

        // Step 22-25: Reject the ChangeSet
        pageObject.ClickPendingApprovalTab();
        pageObject.ClickReject();
        pageObject.SelectRejectionReason(rejectionReason);
        pageObject.ClickSave();
        Console.WriteLine("ChangeSet rejected successfully.");

        // Step 26-30: Edit and Submit the ChangeSet
        pageObject.ClickInProgressTab();
        pageObject.ClickPenIcon();
        pageObject.ClickEditOption();
        pageObject.SelectEffectiveThruDate(effFrmDay2, effFrmMonth2, effFrmYear2);
        pageObject.ClickSave();
        pageObject.ClickSubmit();
        Console.WriteLine("ChangeSet edited and submitted successfully.");

        // Step 31-38: Approve the ChangeSet
        driver.Navigate().GoToUrl("https://rims-tst1.app.corpint.net/#/");
        genericHelper.EnterText(driver.FindElement(By.Id("username")), username);
        genericHelper.EnterText(driver.FindElement(By.Id("password")), password);
        driver.FindElement(By.Id("loginButton")).Click(); 
        Console.WriteLine("Logged into RIMS successfully.");
        Thread.Sleep(10000);

        pageObject.SelectCaptivePartner(captivePartner);
        pageObject.ClickIncentivesTab();
        pageObject.ClickViewOrCreateCPOVAPROffers();
        pageObject.ClickPendingApprovalTab();
        pageObject.ClickApprove();
        pageObject.ClickManagerApproved();
        Assert.That(pageObject.GetFirstChangeSetID(), Is.EqualTo(firstChangeSetID), "ChangeSetID does not match.");
        Console.WriteLine("ChangeSet approved successfully.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}