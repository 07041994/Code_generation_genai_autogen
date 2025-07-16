using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Origination.Utils;
using NUnit.Framework;

public class CPOVAPROFFERSManagerRejectedCard_INEOS
{
    public IWebDriver driver;
    public GenericHelper genericHelper;

    // WebElement Definitions
    public IWebElement captivePartnerDropDown => driver.FindElement(By.XPath("(//div[@class='rims-nav-item'])[2]/select"));
    public IWebElement incentivesTab => driver.FindElement(By.XPath("//label[text()='Incentives']"));
    public IWebElement incentivesHeader => driver.FindElement(By.XPath("//h1[text()='Incentives']"));
    public IWebElement viewOrCreateCPOVAPROffers => driver.FindElement(By.XPath("(//div[@class='card-text text-center'])[4]"));
    public IWebElement CPOVAPROfferHeader => driver.FindElement(By.XPath("//h1[text()='CPOV APR Offer - ChangeSet Summary']"));
    public IWebElement createCPOVAPROffers => driver.FindElement(By.XPath("//button[@class='create-card-element round-button']"));
    public IWebElement newCPOVAPROfferHeader => driver.FindElement(By.XPath("//div[text()='New CPOV APR Offer - Create New']"));
    public IWebElement effectiveThruField => driver.FindElement(By.XPath("(//input[@name='dp'])[2]"));
    public IWebElement ratePercentageColumn => driver.FindElement(By.XPath("(//div[@col-id='rate'])[2]"));
    public IWebElement ratePercentageInput => driver.FindElement(By.XPath("(//div[@col-id='rate'])[2]//input"));
    public IWebElement minTermColumn => driver.FindElement(By.XPath("(//div[@col-id='minTerm'])[2]"));
    public IWebElement minTermInput => driver.FindElement(By.XPath("(//div[@col-id='minTerm'])[2]//input"));
    public IWebElement maxTermColumn => driver.FindElement(By.XPath("(//div[@col-id='maxTerm'])[2]"));
    public IWebElement maxTermInput => driver.FindElement(By.XPath("(//div[@col-id='maxTerm'])[2]//input"));
    public IWebElement tierColumn => driver.FindElement(By.XPath("(//div[@col-id='tier'])[2]"));
    public IWebElement tierDropdown => driver.FindElement(By.XPath("(//div[@col-id='tier'])[2]//select"));
    public IWebElement selectVehiclesButton => driver.FindElement(By.XPath("//button[text()='Select Vehicles']"));
    public IWebElement modelYearsCheckbox => driver.FindElement(By.XPath("//label[@for='2024']"));
    public IWebElement makeCheckbox => driver.FindElement(By.XPath("//label[@for='INEOS']"));
    public IWebElement modelCheckbox => driver.FindElement(By.XPath("//label[@for='Grenadier Station Wagon']"));
    public IWebElement selectVehicleRecordCheckbox => driver.FindElement(By.XPath("(//span[@class='ag-icon ag-icon-checkbox-unchecked'])[11]"));
    public IWebElement saveButton => driver.FindElement(By.XPath("(//button[text()='Save'])[1]"));
    public IWebElement submitButton => driver.FindElement(By.XPath("(//div[contains(text(),'12')])[1]/following::div[6]/button"));
    public IWebElement pendingApprovalTab => driver.FindElement(By.XPath("//label[contains(text(),'Pending Approval')]"));
    public IWebElement rejectButton => driver.FindElement(By.XPath("(//div[contains(text(),'12')])[1]/following::div[6]/button[1]"));
    public IWebElement rejectionReasonDropdown => driver.FindElement(By.XPath("(//div[@col-id='rejectionReason'])[2]//select"));
    public IWebElement inProgressTab => driver.FindElement(By.XPath("//label[contains(text(),'In-Progress')]"));
    public IWebElement penIcon => driver.FindElement(By.XPath("(//button[contains(@class,'edit-icon')])[1]"));
    public IWebElement editOption => driver.FindElement(By.XPath("(//p[contains(@class,'mini-edit')])[1]"));
    public IWebElement managerApprovedButton => driver.FindElement(By.XPath("(//label[@for='Manager-Approved'])[1]"));
    public IWebElement approveButton => driver.FindElement(By.XPath("(//div[contains(text(),'12')])[1]/following::div[6]/button[2]"));
    public IWebElement firstChangeSetID => driver.FindElement(By.XPath("(//div[@class='changeset-id col ng-star-inserted'])[1]"));

    public CPOVAPROFFERSManagerRejectedCard_INEOS(IWebDriver driver)
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
        genericHelper.clickElement(incentivesTab);
    }

    public void ClickViewOrCreateCPOVAPROffers()
    {
        genericHelper.clickElement(viewOrCreateCPOVAPROffers);
    }

    public void ClickCreateCPOVAPROffers()
    {
        genericHelper.clickElement(createCPOVAPROffers);
    }

    public void SelectEffectiveThruDate(string day, string month, string year)
    {
        genericHelper.SelectDateFromCalendar(day, month, year);
    }

    public void EnterRatePercentage(string rate)
    {
        genericHelper.clickElement(ratePercentageColumn);
        genericHelper.EnterText(ratePercentageInput, rate);
    }

    public void EnterMinTerm(string minTerm)
    {
        genericHelper.clickElement(minTermColumn);
        genericHelper.EnterText(minTermInput, minTerm);
    }

    public void EnterMaxTerm(string maxTerm)
    {
        genericHelper.clickElement(maxTermColumn);
        genericHelper.EnterText(maxTermInput, maxTerm);
    }

    public void SelectTier(string tier)
    {
        genericHelper.clickElement(tierColumn);
        genericHelper.SelectDropdownValue(tierDropdown, tier);
    }

    public void ClickSelectVehicles()
    {
        genericHelper.clickElement(selectVehiclesButton);
    }

    public void SelectVehicleOptions()
    {
        genericHelper.clickElement(modelYearsCheckbox);
        genericHelper.clickElement(makeCheckbox);
        genericHelper.clickElement(modelCheckbox);
        genericHelper.clickElement(selectVehicleRecordCheckbox);
    }

    public void ClickSave()
    {
        genericHelper.clickElement(saveButton);
    }

    public void ClickSubmit()
    {
        genericHelper.clickElement(submitButton);
    }

    public void ClickPendingApprovalTab()
    {
        genericHelper.clickElement(pendingApprovalTab);
    }

    public void ClickReject()
    {
        genericHelper.clickElement(rejectButton);
    }

    public void SelectRejectionReason(string reason)
    {
        genericHelper.SelectDropdownValue(rejectionReasonDropdown, reason);
    }

    public void ClickInProgressTab()
    {
        genericHelper.clickElement(inProgressTab);
    }

    public void ClickPenIcon()
    {
        genericHelper.clickElement(penIcon);
    }

    public void ClickEditOption()
    {
        genericHelper.clickElement(editOption);
    }

    public void ClickManagerApproved()
    {
        genericHelper.clickElement(managerApprovedButton);
    }

    public void ClickApprove()
    {
        genericHelper.clickElement(approveButton);
    }

    public string GetFirstChangeSetID()
    {
        return firstChangeSetID.Text;
    }
}