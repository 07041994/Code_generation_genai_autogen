prompt = '''To accomplish the given task, below are the mandatory, must and should instructions to follow without any excuses/
                                Generate **full code** for **each test step**, even if the step is repetitive or identical.
                                - **Do not** summarize, merge, skip, or use placeholder comments like:
                                - "// Repeat similar steps..."
                                - "// Implement all steps as per the Steps_list"
                                - "// Continue with all steps as per the Steps_list"
                                - " // Continue implementing all steps sequentially..."
                                - "// Continue as above..."
                                - "// See previous step..."
                                - " // Continue with remaining steps..."
                                -"// Add logic to execute stored procedure here"
                                - "// Additional steps will follow same pattern..."
                                -"// Continue implementing all steps sequentially..."
                                -"// Implementation for clicking checkbox goes here..."
                                -"// Repeat similar steps for the second card creation and submission process as per the Steps_list.
                                -" // (Steps 26-48 will follow the same pattern as above.)"
                                -"// Repeat Steps 26-48 as per the Steps_list"
                                -"// (Implementation follows the same pattern as above.)"
                                -"// Repeat similar steps for the second card creation and submission process as per the Steps_list."
                                -"// Each step will be implemented exactly as described in the Steps_list."
                                Under no circumstances should any step be skipped or compressed.
                                And below is the task that you have to generate code You are a skilled software engineer specializing in test automation who will write code for all test steps given in Steps_list. 
                                Your task is to Generate the complete C# code that would do the following: 
                                Follow the Example provided at end as a format to generate code.
                                Let us go through each step to generate an optimized and complete C# solution.
                                Read the Steps list and Generate the following Output C# Files. 
                                You  have only generic helper. Use existing functions from generic helper if already defined else all required functions or logic must be created from the inputs provided

                        Step 1: Generate Object and Function Files
                                Read the test_object_variables, test_case, and Steps_list.
                                create the function for every variables in test_object_variables strictly.
                                Always use the following genericHelper ( imported from Origination.Utils) methods for Selenium actions to ensure stability and maintainability:
                                Functions must return meaningful values—no empty or placeholder returns.
                                Ensure functions align with test_case steps and synchronize variable definitions.
                                Avoid duplicate code across all generated files.
                                Keep all webelements definition and functions which are required to perform operation according to test steps within filename.cs file
                                Keep all functions and genericHelper related functions within Page object file. Do not extend it to Verify file at any cost.

                        Step 2: Generate Test Execution Logic
                                Generate a complete C# code based on Steps_list and Expected_Outputs.
                                Execute each step exactly as mentioned in the Steps_list.
                                Implement code for all test steps present in Steps_list one by one in sequential manner until reaching end of steps list.
                                Ensure to generate complete, executable test code that covers all the test steps provided in Steps_list.
                                Strictly generate code for all steps given in the steps_list provided. If Steps_list contains 30 steps, then code should also generate for 30 steps with out missing or skipping any step that is given in the Steps_list strictly. 
                                Generate code for all steps strictly. do not miss any single steps.
                                Use Console.WriteLine() for every steps strictly.
                                for login steps use 'RIMS_Helper.LaunchApplicationRIMS()'  strictly.
                                Use/Keep URL always as a part of test steps, dont include any URLS in setup.
                                Add import statement using Originations.PageObjects; without fail.
                                Do not hardcode any data from detos, instead use its variables to fill the data which are defined within text fixture section.
                                Always call the functions created in Step 1, or those defined within helper file attached.
                                Use if-else logic where applicable for decision-based test steps.
                                Strictly,You MUST return a complete C# implementation, covering every step in Steps_list, without skipping or summarizing. The code must span all logic needed, and the model must use all output token space if needed.
                        Step 3: 
                                Provide the C# code strictly in the following format without any introductory explanations or additional text. Create or use filename based on title porvide in test case.
                                ---
                                        ### **FileName.cs **
                                        ```csharp
                                        c# code
                                        ```
                                        ### **VerifyFileName.cs **
                                        ```csharp
                                        c# code
                                        ```        
                                Expected Output: 2 C# Files:
                                        One Page Object File named filename.cs which have all information related to web elements, function definitions.
                                        One Test Steps File named verifyfilename.cs which have driver initilization to performing all test steps as listed.
                        
                                The input for the prompt would be:
                                A) test_case:
                                1.Titles: CPOVAPROFFERSManagerRejectedCard_INEOS
                                2.Steps_list: Step1 Log into the RIMS - https://rims-tst1.app.corpint.net/#/ by entering username(user1) and password from "INEOSCPOVAPROFFERSManagerRejectedCardDetos" file  
Step2 Click on the captive partner drop-down and select 'INEOS'

Step3 Click on the 'Incentives' tab and Verify Header is displayed with string as "Incentives"

Step4 Click on the 'View or Create New CPOV APR Offers' button and  Verify Header is displayed with string as "CPOV APR Offer - ChangeSet Summary"

Step5 Click on 'Create a New CPOV APR Offer' button and Verify Header is displayed with string as "New CPOV APR Offer - Create New"
Step6 Select Effective Thru Date from Calendar 
Step7 Click 'Rate%' Column Field
Step8 Click 'Rate%' Input Field and Enter value in TextBox
Step9 Click 'Min Term' Column Field
Step10 Click 'Min Term' Input Field and Enter value in TextBox
Step11 Click 'Max Term' Column Field
Step12 Click 'Max Term' Input Field and Enter value in TextBox
Step13 Click 'Tier' Column Field
Step14 Select value for 'Tier Drop-down'
Step15 Click 'Select Vehicles' button
Step16 Select 'MODELYEARS'
Step17 Select 'MAKE'
Step18 Select 'MODEL' and Select 'First Record'
Step19 Click "Save" button
Step20 Capture and Save the FirstChangeSetID 
Step21 Click 'Submit' button
Step22 Click 'Pending Approval' Tab
Step23 Click 'Reject' button
Step24 Select value for 'Rejection Reason' from dropdown
Step25 Click 'Save' button
Step26 Click 'In Progress' Tab
Step27 Click 'Pen Icon ' and Click 'Edit' option
Step28 Select Effective Thru Date from Calendar 
Step29 Click 'Save' button
Step30 Click 'Submit' button
Step31 Log into the RIMS - https://rims-tst1.app.corpint.net/#/ by entering username(user1) and password from "INEOSCPOVAPROFFERSManagerRejectedCardDetos" file  
Step32 Click on the captive partner drop-down and select 'INEOS'

Step33 Click on the 'Incentives' tab 
Step34 Click on the 'View or Create New CPOV APR Offers' button 
Step35 Click 'Pending Approval' Tab
Step36 Click 'Approve' button
Step37 Click on 'Manager Approved'
Step38 Verify ChangeSetID is same as 'FirstChangeSetID'
                                3.Expected_Outputs: User should be logged in successfully and Chrysler should be selected by default..

Capitive Partner drop down field should be clicked and the value "INEOS" should be selected
Incentives' Tab should be clicked and Header should be displayed with string as "Incentives"
View or Create New CPOV APR Offers'' button should be clicked and Header should be displayed with string as "CPOV APR Offer - ChangeSet Summary"

Create a New CPOV APR Offer' button should be clicked  and Header should be displayed with string as "New CPOV APR Offer - Create New"
User should be allowed to select the Date from Calendar
Rate %' column field should be clicked
Rate %' input field should be clicked and value should be entered
Min Term' column field should be clicked
Min Term' input field should be clicked and value should be entered
Max Term' column field should be clicked
Max Term' input field should be clicked and value should be entered
Tier' column field should be clicked
Selected value should be populated
Select Vehicles' button should be clicked
Model Years option should be clicked
Make Option should be selected
Model Option should be selected and 'First Record' should be selected
Save button should be clicked
ChangeSetID should be saved

Submit' button should be clicked
Pending Approval' should be clicked
Reject' button should be clicked
Selected value should be populated
Save' button should be clicked
In Progress' Tab should be clicked
Pen icon' should be clicked and 'Edit' option should be clicked
User should be allowed to select the Date from Calendar
Save' button should be clicked
Submit' button should be clicked
User should be logged in successfully and Chrysler should be selected by default..

Capitive Partner drop down field should be clicked and the value "INEOS" should be selected
Incentives' Tab should be clicked 
View or Create New CPOV APR Offers'' button should be clicked 
Pending Approval' should be clicked
Approve' button should be clicked
Manager Approved' should be clicked
ChangeSetID should be same as 'FirstChangeSetID'
              
                                
                                B) test_object_variables:{'Dashboard': {'captivePartnerDropDown': {'element_type': 'dropdown', 'by': 'xpath', 'id-obj': "(//div[@class='rims-nav-item'])[2]/select"}}, 'Incentives': {'incentives': {'element_type': 'label', 'by': 'xpath', 'id-obj': "//label[text()='Incentives']"}, 'CPOVAPROffers': {'element_type': 'label', 'by': 'xpath', 'id-obj': "(//div[@class='card-text text-center'])[4]"}, 'CreateCPOVAPROffers': {'element_type': 'button', 'by': 'xpath', 'id-obj': "//button[@class='create-card-element round-button']"}, 'EffectiveThru': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//input[@name='dp'])[2]"}, 'RatePercentageColumn': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='rate'])[2]"}, 'RatePercentageClickandEnter': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='rate'])[2]//input"}, 'minTermColumn': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='minTerm'])[2]"}, 'minTermClickandEnter': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='minTerm'])[2]//input"}, 'maxTermColumn': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='maxTerm'])[2]"}, 'maxTermClickandEnter': {'element_type': 'input', 'by': 'xpath', 'id-obj': "(//div[@col-id='maxTerm'])[2]//input"}, 'TierColumn': {'element_type': 'text', 'by': 'xpath', 'id-obj': "(//div[@col-id='tier'])[2]"}, 'TierClickandSelect': {'element_type': 'input', 'by': 'xpath', 'id-obj': "(//div[@col-id='tier'])[2]//select"}, 'SelectVehicles': {'element_type': 'button', 'by': 'xpath', 'id-obj': "//button[text()='Select Vehicles']"}, 'modelYears': {'element_type': 'checkbox', 'by': 'xpath', 'id-obj': "//label[@for='2024']"}, 'make': {'element_type': 'checkbox', 'by': 'xpath', 'id-obj': "//label[@for='INEOS']"}, 'model': {'element_type': 'checkbox', 'by': 'xpath', 'id-obj': "//label[@for='Grenadier Station Wagon']"}, 'selectVehicleRecord': {'element_type': 'checkbox', 'by': 'xpath', 'id-obj': "(//span[@class='ag-icon ag-icon-checkbox-unchecked'])[11]"}, 'Save': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//button[text()='Save'])[1]"}, 'Submit': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//div[contains(text(),'12')])[1]/following::div[6]/button"}, 'Cancel': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//button[text()='Cancel'])[1]"}, 'PendingApproval': {'element_type': 'label', 'by': 'xpath', 'id-obj': "//label[contains(text(),'Pending Approval')]"}, 'FirstChangeSetID': {'element_type': 'label', 'by': 'xpath', 'id-obj': "(//div[@class='changeset-id col ng-star-inserted'])[1]"}, 'InProgress': {'element_type': 'label', 'by': 'xpath', 'id-obj': "//label[contains(text(),'In-Progress')]"}, 'ManagerApproved': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//label[@for='Manager-Approved'])[1]"}, 'Reject': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//div[contains(text(),'12')])[1]/following::div[6]/button[1]"}, 'EditOption': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//p[contains(@class,'mini-edit')])[1]"}, 'Penicon': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//button[contains(@class,'edit-icon')])[1]"}, 'Approve': {'element_type': 'button', 'by': 'xpath', 'id-obj': "(//div[contains(text(),'12')])[1]/following::div[6]/button[2]"}}}
                        
             
                                        C) **Test data or Datos Variables:**
                                        Without fail, take the json filename given in test steps and read all variables present in detos to the verify file.
                                                string jsonFilePath = "filename.json";
                                                var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath)); 
                                        and load all available info into respective datatypes in one by one  in below format
                                                string variable1 = jsonData["variable1"].ToString();
                                                DateTime variable2 = jsonData["variable2"].ToString();
                                        Like above steps load all variables present in Detos file along with its associated datatype in the beginning, and the variables in Datos file are as follows strictly.
                                        Datos file: {'username': 'Brower\\Lruser002', 'password': 'March@002', 'captivePartner': 'INEOS', 'role': 'RIMS.ALL', 'effFrmDay1': '28', 'effFrmMonth1': 'Jun', 'effFrmYear1': '2025', 'effFrmDay2': '28', 'effFrmMonth2': 'Aug', 'effFrmYear2': '2025', 'RatePercentageInput': '12', 'TierDropdown': '2', 'MinTerm': '5', 'MaxTerm': '9', 'RejectionReason': 'Incorrect Amount(s)'}Use functions from generic helper wherever required instead of regenrating them . The contents of generic helper are as follows. ï»¿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using DocumentFormat.OpenXml.Bibliography;
using log4net;
using log4net.Repository.Hierarchy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Origination.Utils
{
    public class GenericHelper(IWebDriver driver)
    {

        public WebDriverWait GetWebdriverWait(TimeSpan timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(2));
            WebDriverWait wait = new WebDriverWait(driver, timeout)
            {
                PollingInterval = TimeSpan.FromMilliseconds(500),
            };
            Console.WriteLine("Wait Object Created ");
            return wait;
        }

        public void WaitForWebElementToBeClickable(By locator, TimeSpan timeout)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));
            var wait = GetWebdriverWait(timeout);
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(5000));
            
        }

        public void clickElement(IWebElement webElement)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            try
            {
               
                webElement.Click();
                Console.WriteLine($"Clicked on element: {webElement}");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void waitForElement(IWebElement element)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(180));
            wait.Until(driver => ExpectedConditions.ElementToBeClickable(element));
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }
        public void EnterText(IWebElement element, string text)
        {
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(40));
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine($"Entered text '{text}' into element: {element}");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void SelectDropdownValue(IWebElement dropdown, string value)
        {
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByText(value);
            Console.WriteLine($"Selected value {value} from dropdown: {dropdown}");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
        }

        public void ValidateElementPresence(IWebElement element, string elementName)
        {
            if (element.Displayed)
            {
                Console.WriteLine($"{elementName} is available on the page.");
            }
            else
            {
                Console.WriteLine($"{elementName} is not available on the page.");
            }
        }

        public void SelectDateFromCalendar(string targetDay, string targetMonth, string targetYear)
        {
            // Navigate to correct Year
            IWebElement YearElement = driver.FindElement(By.XPath($"//option[@value='{targetYear}']"));
            YearElement.Click();
            Thread.Sleep(3000);

            // Navigate to correct month
            IWebElement monthElement = driver.FindElement(By.XPath($"//option[text()='{targetMonth}']"));
            monthElement.Click();
            Thread.Sleep(3000);

            // Select the day
            IWebElement dayElement = driver.FindElement(By.XPath($"(//div[@class='btn-light ng-star-inserted'])[{targetDay}]"));
            dayElement.Click();
            Thread.Sleep(20000);


        }
        public bool AssertionFunction(IWebElement element)
        {
            try
            {
                bool isDisplayed = element.Displayed;
                Console.WriteLine($"Element [{element}] is displayed");
                return isDisplayed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Element is not displayed or could not be found. Exception: {ex.Message}");
                return false;
            }
        }

    }
}
D) Below is the example to follow the code format for all drivers initialization, detos and variable definition and other structures of program. You can use it as a sample ** Example **                
                        
                                        Input:
                                        Titles: CPOVAPROffersFieldValidation_INEOS
                                        Steps_list: Step 1 Log into the RIMS - https://rims-tst1.app.corpint.net/#/ by entering username(user1) and password from "INEOSCPOVAPROffersFieldValidationsDetos" file  
Step 2 Click on the captive partner drop-down and select 'INEOS'

Step 3 Click on the 'Incentives' tab and Verify Header is displayed with string as "Incentives"

Step 4 Click on the 'View or Create New CPOV APR Offers' button and  Verify Header is displayed with string as "CPOV APR Offer - ChangeSet Summary"

Step 5 Click on 'Create a New CPOV APR Offer' button and Verify Header is displayed with string as "New CPOV APR Offer - Create New"
Step 6 Click 'Select Vehicles'
Step 7 Verify Error message "Effective Thru Date is required." is displayed below Effective Thru Field
Step 8 Select any Past Date before the Effective From Date  for the 'Effective Thru Date'  Field
Step 9 Verify the below Error message is displayed for the form Elements : 
Effective From Date must not be after Effective Thru Date.
Effective Thru Date cannot be set before today

                                        Exptected_Outputs: User should be logged in successfully and Chrysler should be selected by default..

Capitive Partner drop down field should be clicked and the value "INEOS" should be selected
Incentives' Tab should be clicked and Header should be displayed with string as "Incentives"
View or Create New CPOV APR Offers'' button should be clicked and Header should be displayed with string as "CPOV APR Offer - ChangeSet Summary"

Create a New CPOV APR Offer' button should be clicked  and Header should be displayed with string as "New CPOV APR Offer - Create New"
Select Vehicles' button should be clicked
Error message "Effective Thru Date is required." should be displayed below Effective Thru Field
Selected Date should be populated
Error message should be displayed for the form Elements : 
Effective From Date must not be after Effective Thru Date.
Effective Thru Date cannot be set before today

                                        Test_object_variables:{'Dashboard': {'captivePartnerDropDown': {'element_type': 'dropdown', 'by': 'xpath', 'id-obj': "(//div[@class='rims-nav-item'])[2]/select"}}, 'Incentive': {'incentive': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//label[text()='Incentives']"}, 'createOrViewCPOVAPROffers': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//div[text()='View or Create new CPOV APR Offers']"}, 'createCard': {'element_type': 'button', 'by': 'xpath', 'id-obj': "//button[@class='create-card-element round-button']"}, 'selectVehicles': {'element_type': 'button', 'by': 'xpath', 'id-obj': "//button[text()='Select Vehicles']"}, 'BlankEffectiveThru': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//div[text()='Effective Thru Date is required.']"}, 'PastEffectiveThruDate1': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//div[text()='Effective Thru Date cannot be set before today']"}, 'PastEffectiveThruDate2': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//div[text()='Effective From Date must not be after Effective Thru Date.']"}, 'NewCPOVAPROfferHeader': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//div[text()='New CPOV APR Offer - Create New']"}, 'CPOVAPROfferHeader': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//h1[text()='CPOV APR Offer - ChangeSet Summary']"}, 'IncentivesHeader': {'element_type': 'text', 'by': 'xpath', 'id-obj': "//h1[text()='Incentives']"}}}
                                        dataos:{'username': 'Brower\\Lruser002', 'password': 'March@002', 'CaptivePartner': 'INEOS', 'role': 'RIMS.ALL', 'effFrmDay': '29', 'effFrmMonth': 'May', 'effFrmYear': '2025', 'effThruDay': '26', 'effThruMonth': 'May', 'effThruYear': '2025'} 
                                        
                                        output:
                                        1) page : using OpenQA.Selenium;
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
                                        2) test : using OpenQA.Selenium;
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
                                        '''