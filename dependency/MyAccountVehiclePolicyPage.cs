using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountVehiclePolicyPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountVehiclePolicyPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement VehicleAndInsuranceLink => driver.FindElement(By.XPath("//a[@data-navto='/Account/vehicle']"));
        public IWebElement VehicleInfoText => driver.FindElement(By.XPath("/html/body/div[2]/section/div/div/div/h1"));
        public IWebElement PolicyTab => driver.FindElement(By.XPath("//*[@id='vehicleTabs']/li[2]/a"));
        public IWebElement PolicyCard => driver.FindElement(By.XPath("//*[@id='policy']/div[2]/div[2]/a/div"));
        public IWebElement AutoInsuranceText => driver.FindElement(By.XPath("//*[@id='policy-edit']/div[1]/h2[1]"));
        public IWebElement InsuranceProviderField => driver.FindElement(By.XPath("//*[@id='insurance-provider']"));
        public IWebElement PolicyNumberField => driver.FindElement(By.XPath("//*[@id='insurance-policynumber']"));
        public IWebElement EffectiveDateField => driver.FindElement(By.XPath("//*[@id='insurance-effective']"));
        public IWebElement ExpirationDateField => driver.FindElement(By.XPath("//*[@id='insurance-expiration']"));
        public IWebElement AgentNameField => driver.FindElement(By.XPath("//*[@id='insurance-agent']"));
        public IWebElement AgentPhoneField => driver.FindElement(By.XPath("//*[@id='insurance-phone']"));
        public IWebElement SaveButton => driver.FindElement(By.XPath("//*[@id='policy-edit']/div[5]/div[1]/a"));
        public IWebElement UpdatedMessageText => driver.FindElement(By.XPath("//span[contains(text(),'Updated')]"));
        public IWebElement ClaimsTab => driver.FindElement(By.XPath("//*[@id='vehicleTabs']/li[3]/a"));
        public IWebElement NewClaimLink => driver.FindElement(By.XPath("//*[@id='claims']/div[1]/p[2]/a"));
        public IWebElement NewClaimHeaderText => driver.FindElement(By.XPath("//*[@id='claims-edit']/div[1]/h2"));
        public IWebElement TypeOfClaimDropdown => driver.FindElement(By.XPath("//*[@id='claimtype']"));
        public IWebElement LocationOfVehicleDropdown => driver.FindElement(By.Id("placetype"));
        public IWebElement PolicyHolderField => driver.FindElement(By.Id("PolicyHolder"));
        public IWebElement PolicyNumber2Field => driver.FindElement(By.Id("PolicyNumber"));
        public IWebElement ClaimNumberField => driver.FindElement(By.XPath("//*[@id='ClaimNumber']"));
        public IWebElement AccidentDateField => driver.FindElement(By.XPath("//*[@id='LossDate']"));
        public IWebElement ClaimAcceptedDropdown => driver.FindElement(By.XPath("//*[@id='ClaimAccepted']"));
        public IWebElement ClaimAmountField => driver.FindElement(By.XPath("//*[@id='ClaimAmount']"));
        public IWebElement ClaimDeductibleField => driver.FindElement(By.XPath("//*[@id='ClaimDeductible']"));
        public IWebElement MileageField => driver.FindElement(By.Id("Mileage"));
        public IWebElement VINField => driver.FindElement(By.XPath("//*[@id='VIN']"));
        public IWebElement MakeField => driver.FindElement(By.Id("Make"));
        public IWebElement ModelField => driver.FindElement(By.Id("Model"));
        public IWebElement YearField => driver.FindElement(By.Id("Year"));
        public IWebElement InsuranceProvider2Field => driver.FindElement(By.XPath("//*[@id='InsuranceCompanyName']"));
        public IWebElement InsurancePhoneField => driver.FindElement(By.XPath("//*[@id='InsuranceCompanyPhone']"));
        public IWebElement AdjusterNameField => driver.FindElement(By.XPath("//*[@id='AdjusterName']"));
        public IWebElement AgentPhone2Field => driver.FindElement(By.XPath("//*[@id='AgentPhone']"));
        public IWebElement AdjusterPhoneField => driver.FindElement(By.XPath("//*[@id='AdjusterPhone']"));
        public IWebElement CollateralLocationField => driver.FindElement(By.XPath("//*[@id='UnitLocationName']"));
        public IWebElement CollateralCityField => driver.FindElement(By.XPath("//*[@id='CollateralCity']"));
        public IWebElement CollateralStateField => driver.FindElement(By.XPath("//*[@id='CollateralState']"));
        public IWebElement CollateralPhoneField => driver.FindElement(By.XPath("//*[@id='CollateralPhone']"));
        public IWebElement CollateralZipField => driver.FindElement(By.XPath("//*[@id='CollateralZip']"));
        public IWebElement Save2Button => driver.FindElement(By.XPath("//*[@id='claims-edit']/div[6]/div[1]/a"));
        public IWebElement ClaimAddedMessageText1 => driver.FindElement(By.XPath("(//span[contains(text(),'Added')])[1]"));
        public IWebElement ClaimAddedMessageText2 => driver.FindElement(By.XPath("(//span[contains(text(),'Added')])[2]"));
        public IWebElement GarageAddressLink => driver.FindElement(By.XPath("//div[@class='vcard']//div[@class='fn']"));
        public IWebElement GarageAddressText => driver.FindElement(By.XPath("//h2[contains(text(),'Garage Address')]"));
        public IWebElement AddressInputText => driver.FindElement(By.Id("garage-street1"));
        public IWebElement CityInputText => driver.FindElement(By.Id("garage-city"));
        public IWebElement StateDropdown => driver.FindElement(By.Id("garage-state"));
        public IWebElement ZipCodeInputText => driver.FindElement(By.Id("garage-zipcode"));
        public IWebElement NicknameForYourVehicle => driver.FindElement(By.XPath("//*[@id='input-nickname']"));
        public IWebElement VehicleInfo => driver.FindElement(By.XPath("//span[@data-bind='text: vehicleYearMakeModel"));
        public IWebElement VehicleName => driver.FindElement(By.XPath("//*[@id='garage-address']/div/div[2]/a/div/div[1]/div[1]/h2"));

        public void EnterNicknameForVehicle(string nickname)
        {
            NicknameForYourVehicle.Clear();
            genericHelper.sendKeys(NicknameForYourVehicle, nickname, "Nickname for Vehicle");
        }

        public string GetVehicleInfoText()
        {

            genericHelper.waitForElement(VehicleInfo);
            return VehicleInfo.Text;

        }
        public void ClickVehicleInfo()
        {
            genericHelper.clickOn(VehicleName, "Vehicle Name Button");
        }

        public void ClickVehicleAndInsuranceLink()
        {
            genericHelper.waitForElement(VehicleAndInsuranceLink);
            genericHelper.clickOn(VehicleAndInsuranceLink, "Vehicle and Insurance Link");
        }

    /*    public string GetVehicleInfoText()
        {
            genericHelper.waitForElement(VehicleInfoText);
            return VehicleInfoText.Text;
        }
    */
        public void ClickPolicyTab()
        {
            genericHelper.clickOn(PolicyTab, "Policy Tab");
        }

        public void ClickPolicyCard()
        {
            genericHelper.clickOn(PolicyCard, "Policy Card");
        }

        public string GetAutoInsuranceText()
        {
            genericHelper.waitForElement(AutoInsuranceText);
            return AutoInsuranceText.Text;
        }

        public void EnterInsuranceProvider(string value)
        {
            InsuranceProviderField.Clear();
            genericHelper.sendKeys(InsuranceProviderField, value, "Insurance Provider Field");
        }

        public void EnterPolicyNumber(string value)
        {
            PolicyNumberField.Clear();
            genericHelper.sendKeys(PolicyNumberField, value, "Policy Number Field");
        }

        public void EnterEffectiveDate(string value)
        {
            EffectiveDateField.Clear();
            genericHelper.sendKeys(EffectiveDateField, value, "Effective Date Field");
        }

        public void EnterExpirationDate(string value)
        {
            ExpirationDateField.Clear();
            genericHelper.sendKeys(ExpirationDateField, value, "Expiration Date Field");
        }

        public void EnterAgentName(string value)
        {
            AgentNameField.Clear();
            genericHelper.sendKeys(AgentNameField, value, "Agent Name Field");
        }

        public void EnterAgentPhone(string value)
        {
            AgentPhoneField.Clear();
            genericHelper.sendKeys(AgentPhoneField, value, "Agent Phone Field");
        }

        public void ClickSaveButton()
        {
            genericHelper.waitForElement(SaveButton);
            genericHelper.clickOn(SaveButton, "Save Button");
        }

        public string GetUpdatedMessageText()
        {
            Thread.Sleep(2000);
            genericHelper.waitForElement(UpdatedMessageText);
            return UpdatedMessageText.Text;
        }

        public void ClickClaimsTab()
        {
            genericHelper.waitForElement(ClaimsTab);
            genericHelper.clickOn(ClaimsTab, "Claims Tab");
            genericHelper.clickOn(ClaimsTab, "Claims Tab");
        }

        public void ClickNewClaimLink()
        {
            genericHelper.waitForElement(NewClaimLink);
            genericHelper.clickOn(NewClaimLink, "New Claim Link");
        }

        public string GetNewClaimHeaderText()
        {
            genericHelper.waitForElement(NewClaimHeaderText);
            return NewClaimHeaderText.Text;
        }

        public void SelectTypeOfClaimDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(TypeOfClaimDropdown, index);
        }

        public void SelectLocationOfVehicleDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(LocationOfVehicleDropdown, index);
        }

        public void EnterPolicyHolder(string value)
        {
            PolicyHolderField.Clear();
            genericHelper.sendKeys(PolicyHolderField, value, "Policy Holder Field");
        }

        public void EnterPolicyNumber2(string value)
        {
            PolicyNumber2Field.Clear();
            genericHelper.sendKeys(PolicyNumber2Field, value, "Policy Number 2 Field");
        }

        public void EnterClaimNumber(string value)
        {
            ClaimNumberField.Clear();
            genericHelper.sendKeys(ClaimNumberField, value, "Claim Number Field");
        }

        public void EnterAccidentDate(string value)
        {
            AccidentDateField.Clear();
            genericHelper.sendKeys(AccidentDateField, value, "Accident Date Field");
        }

        public void SelectClaimAcceptedDropdown(int index)
        {
            genericHelper.selectValueFromDropdown(ClaimAcceptedDropdown, index);
        }

        public void EnterClaimAmount(string value)
        {
            ClaimAmountField.Clear();
            genericHelper.sendKeys(ClaimAmountField, value, "Claim Amount Field");
        }

        public void EnterClaimDeductible(string value)
        {
            ClaimDeductibleField.Clear();
            genericHelper.sendKeys(ClaimDeductibleField, value, "Claim Deductible Field");
        }

        public void EnterMileage(string value)
        {
            MileageField.Clear();
            genericHelper.sendKeys(MileageField, value, "Mileage Field");
        }

        public void EnterVIN(string value)
        {
            VINField.Clear();
            genericHelper.sendKeys(VINField, value, "VIN Field");
        }

        public void EnterMake(string value)
        {
            MakeField.Clear();
            genericHelper.sendKeys(MakeField, value, "Make Field");
        }

        public void EnterModel(string value)
        {
            ModelField.Clear();
            genericHelper.sendKeys(ModelField, value, "Model Field");
        }

        public void EnterYear(string value)
        {
            YearField.Clear();
            genericHelper.sendKeys(YearField, value, "Year Field");
        }

        public void EnterInsuranceProvider2(string value)
        {
            InsuranceProvider2Field.Clear();
            genericHelper.sendKeys(InsuranceProvider2Field, value, "Insurance Provider 2 Field");
        }

        public void EnterInsurancePhone(string value)
        {
            InsurancePhoneField.Clear();
            genericHelper.sendKeys(InsurancePhoneField, value, "Insurance Phone Field");
        }

        public void EnterAdjusterName(string value)
        {
            AdjusterNameField.Clear();
            genericHelper.sendKeys(AdjusterNameField, value, "Adjuster Name Field");
        }

        public void EnterAgentPhone2(string value)
        {
            AgentPhone2Field.Clear();
            genericHelper.sendKeys(AgentPhone2Field, value, "Agent Phone 2 Field");
        }

        public void EnterAdjusterPhone(string value)
        {
            AdjusterPhoneField.Clear();
            genericHelper.sendKeys(AdjusterPhoneField, value, "Adjuster Phone Field");
        }

        public void EnterCollateralLocation(string value)
        {
            CollateralLocationField.Clear();
            genericHelper.sendKeys(CollateralLocationField, value, "Collateral Location Field");
        }

        public void EnterCollateralCity(string value)
        {
            CollateralCityField.Clear();
            genericHelper.sendKeys(CollateralCityField, value, "Collateral City Field");
        }

        public void SelectState(int index)
        {
            genericHelper.selectValueFromDropdown(CollateralStateField, index);
        }

        public void EnterCollateralPhone(string value)
        {
            CollateralPhoneField.Clear();
            genericHelper.sendKeys(CollateralPhoneField, value, "Collateral Phone Field");
        }

        public void EnterCollateralZip(string value)
        {
            CollateralZipField.Clear();
            genericHelper.sendKeys(CollateralZipField, value, "Collateral Zip Field");
        }

        public void ClickSave2Button()
        {
            genericHelper.waitForElement(Save2Button);
            genericHelper.clickOn(Save2Button, "Save 2 Button");
        }

        public string GetClaimAddedMessageText()
        {
            Thread.Sleep(5000);
            if (ClaimAddedMessageText1.Displayed)
            {
                return ClaimAddedMessageText1.Text;
            }
            else
            {
                return ClaimAddedMessageText2.Text;
            }
        }

        public void ClickGarageAddressLink()
        {
            genericHelper.clickOn(GarageAddressLink, "Garage Address Link");
        }

        public string GetGarageAddressText()
        {
            genericHelper.waitForElement(GarageAddressText);
            return GarageAddressText.Text;
        }

        public void EnterGarageAddressDetails(string address, string city, string state, string zipCode)
        {
            AddressInputText.Clear();
            genericHelper.sendKeys(AddressInputText, address, "Address Input Text");
            CityInputText.Clear();
            genericHelper.sendKeys(CityInputText, city, "City Input Text");
            genericHelper.selectValueFromDropdown(StateDropdown, int.Parse(state));
            ZipCodeInputText.Clear();
            genericHelper.sendKeys(ZipCodeInputText, zipCode, "Zip Code Input Text");
        }
    }
}