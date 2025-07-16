using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Origination.Utils;
using NUnit.Framework;

public class CPOVAPROffersFieldValidation_INEOS
{
    public IWebDriver driver;
    public GenericHelper genericHelper;

    // WebElement Definitions
    public IWebElement captivePartnerDropDown => driver.FindElement(By.XPath("(//div[@class='rims-nav-item'])[2]/select"));
    public IWebElement IncentivesTab => driver.FindElement(By.XPath("//label[text()='Incentives']"));
    public IWebElement incentivesHeader => driver.FindElement(By.XPath("//h1[text()='Incentives']"));
    public IWebElement createOrViewCPOVAPROffers => driver.FindElement(By.XPath("//div[text()='View or Create new CPOV APR Offers']"));
    public IWebElement CPOVAPROfferHeader => driver.FindElement(By.XPath("//h1[text()='CPOV APR Offer - ChangeSet Summary']"));
    public IWebElement createCard => driver.FindElement(By.XPath("//button[@class='create-card-element round-button']"));
    public IWebElement NewCPOVAPROfferHeader => driver.FindElement(By.XPath("//div[text()='New CPOV APR Offer - Create New']"));
    public IWebElement selectVehicles => driver.FindElement(By.XPath("//button[text()='Select Vehicles']"));
    public IWebElement BlankEffectiveThru => driver.FindElement(By.XPath("//div[text()='Effective Thru Date is required.']"));
    public IWebElement PastEffectiveThruDate1 => driver.FindElement(By.XPath("//div[text()='Effective Thru Date cannot be set before today']"));
    public IWebElement PastEffectiveThruDate2 => driver.FindElement(By.XPath("//div[text()='Effective From Date must not be after Effective Thru Date.']"));

    public IWebElement EffectiveFromField => driver.FindElement(By.XPath("(//*[@data-automation-id='calendar'])[1]"));

    public IWebElement EffectiveThruField => driver.FindElement(By.XPath("(//*[@data-automation-id='calendar'])[2]"));

    public IWebElement EffectiveYear => driver.FindElement(By.XPath("//select[@title='Select year']"));
    public CPOVAPROffersFieldValidation_INEOS(IWebDriver driver)
    {
        this.driver = driver;
        genericHelper = new GenericHelper(driver);
    }

    // Function Definitions
    public void SelectCaptivePartner(string partnerName)
    {
        genericHelper.SelectDropdownValue(captivePartnerDropDown, partnerName);
    }

    public void ClickIncentivesTab()
    {
        genericHelper.clickElement(IncentivesTab);
    }

    public void ClickCreateOrViewCPOVAPROffers()
    {
        genericHelper.clickElement(createOrViewCPOVAPROffers);
    }

    public void ClickCreateCard()
    {
        genericHelper.clickElement(createCard);
    }

    public void ClickSelectVehicles()
    {
        genericHelper.clickElement(selectVehicles);
    }

    public void ValidateEffectiveThruDateError()
    {
        genericHelper.ValidateElementPresence(BlankEffectiveThru, "Effective Thru Date Error");
    }
    public bool IsElementDisplayed(IWebElement element)
    {
        bool isDisplayed = element.Displayed;
        Console.WriteLine($"Element displayed status: {isDisplayed}");
        return isDisplayed;
    }

    public void ValidatePastEffectiveThruDateErrors()
    {
        genericHelper.ValidateElementPresence(PastEffectiveThruDate1, "Effective Thru Date cannot be set before today");
        genericHelper.ValidateElementPresence(PastEffectiveThruDate2, "Effective From Date must not be after Effective Thru Date");
    }
}