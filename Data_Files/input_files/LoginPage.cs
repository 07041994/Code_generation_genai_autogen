using log4net;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccount.PageObjects
{
    public class LoginPage(IWebDriver driver)
    {
        MyAccountSummaryPage accountSummaryPage = new MyAccountSummaryPage(driver);
        MaturityModificationPage matmodPage = new MaturityModificationPage(driver);
        GenericHelper genericHelper = new GenericHelper(driver);

        public static string AccountId;
    //    public static string username;


        public IWebElement UserName => driver.FindElement(By.Id("username"));
        public IWebElement Password => driver.FindElement(By.Id("password"));
        public IWebElement SignInButton => driver.FindElement(By.XPath("//*[@id='form-sign-in']/button"));
        public IWebElement ErrorMessage => driver.FindElement(By.XPath("//*[@id='form-sign-in']/div[11]/p/text()[1]"));
        public IWebElement DisableErrorMessage => driver.FindElement(By.XPath("//*[@id='form-sign-in']//*[contains(text(),'Your account has been disabled')]"));
        
        int loginerror = driver.FindElements(By.XPath("//div[@class='ko-error-message' and contains(text(),'combination you have entered does not match')]")).Count ;
        public IWebElement RememberUsernameCheckbox => driver.FindElement(By.XPath("//*[@id='rememberdevice']"));
        public IWebElement AccountSummaryHeader => driver.FindElement(By.XPath("//*[@id='accountSummaryPage']/h1/span[2]"));
        public IWebElement AcknowledgeButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-statement-round block' and text()='Acknowledge']"));
        public IWebElement PastDuePopupCloseButton => driver.FindElement(By.XPath("//button[@class='close']//i[@class='bi bi-x']"));
        public IWebElement LogoutButton => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[6]/ul/li[4]/a"));
        public IWebElement GoToSignInButton => driver.FindElement(By.XPath("//*[contains(text(),'Go to Sign-In')]"));
        public IWebElement LendingClubBanner => driver.FindElement(By.XPath("//*[@id='myaccount-signin-levi']/div/div/h2"));


        // Methods
        public void EnterUsername(string username)
        {
            genericHelper.waitForElement(UserName);
            UserName.Clear();
            genericHelper.sendKeys(UserName, username, "Username Field");
        }

        public void EnterEmailAddress(string EmailId)
        {
            genericHelper.waitForElement(UserName);
            UserName.Clear();
            genericHelper.sendKeys(UserName, EmailId, "Username Field");
        }

        public void EnterPassword(string password)
        {
            genericHelper.waitForElement(Password);
            Password.Clear();
            genericHelper.sendKeys(Password, password, "Password Field");
        }

        public void ClickRememberUsernameCheckbox()
        {
            genericHelper.clickOn(RememberUsernameCheckbox, "Remember Username Checkbox");
        }

        public void ClickGoToSignInButton()
        {
            genericHelper.waitForElement(GoToSignInButton);
            genericHelper.clickOn(GoToSignInButton, "Go to Sign-in Button");
        }
        
        
        public void ClickSignIn()
        {
            //This function is used to click on sign-in button

            SignInButton.Click();
            Console.WriteLine("SignIn is Clicked");
            Thread.Sleep(5000);
            //  GenericHelper.WaitForWebElementInPage(AccountSummaryTitlel, TimeSpan.FromSeconds(5000));
        }

        public void MyAccLoginInvalid()
        {
            //This function is build for testing purpose to test with hard coded data

            Random rnd = new Random();
            UserName.Clear();
            genericHelper.sendKeys(UserName, "ffmXXXXXXXXXXXX_17203583", "Username");
            //UserName.SendKeys("HappyGuy_" + rnd.Next(0, 100));
            Password.Clear();
         //   genericHelper.sendKeys(Password,"password", "Password");
            Password.SendKeys("password");
            ClickSignIn();

        }

        public string GetErrorMessageText()
        {
            genericHelper.waitForElement(ErrorMessage);
            return ErrorMessage.Text;
        }

        public void MyAccLogOut()
        {
            //This function is used to Logout from MyAccount

            //accountSummaryPage = new MyAccountSummaryPage(driver);
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(90));
            accountSummaryPage.UserDropDown.Click();
            Console.WriteLine("Userdropdown is Clicked on Account summary page");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));
            accountSummaryPage.LogOut.Click();
            Console.WriteLine("Logout button is clicked");
            driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(60));

        }

        public void MyAccLoginWithExistingUser()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            UserName.Clear();
           // UserName.SendKeys(username);
            genericHelper.sendKeys(UserName,Variables.username,"Username");
            Password.Clear();
            Password.SendKeys("password");
            genericHelper.clickOn(SignInButton, "SignIn Button");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        public void LoginEnterUsernameDetails()
        {
            //This function is used to login to MyAccount with query

            SignUpPage signUpPage = new SignUpPage(driver);
            MyAccountSummaryPage accountSummaryPage = new MyAccountSummaryPage(driver);
            GenericHelper genHelper = new GenericHelper(driver);
            DataBaseHelper dbHelper = new DataBaseHelper();
            string squery;
            int i = 0;
            do
            {
                genHelper.clickOn(signUpPage.SantanderLinkonSignUpPage, "Santander Link on SignUp");

                string brand = TestContext.Parameters.Get("Brand", "Scusa");
                string brandCondition = brand.Equals("Scusa") ? "SCUSA" : "Chrysler Capital";
                string financeType = brand.Equals("CCLease") ? "Lease" : "Retail";

                /*
                squery = $@"
                USE Servicing
                SELECT TOP 1 
                    Username, cShawRegion, ServicingStatus, CreditAppType, ssn, *
                FROM 
                    Servicing_Testing.[MyAccount].[TestAccounts] ST
                JOIN 
                    communicationpreferences cp ON cp.contactid = st.contactid
                JOIN 
                    Account a ON a.AccountID = st.AccountId
                JOIN 
                    AccountDaily AD ON ad.AccountID = a.AccountID
                WHERE 
                    Brand = '{brandCondition}'
                    AND LoanDisabled = '' 
                    AND UserName IS NOT NULL 
                    AND ServicingStatus = 'open Active'
                    AND a.AccountStatusID = 0 -- 0 means Not Purged
                    AND ad.cstatus = '0' -- 0 is Open Active
                    AND FinanceType = '{financeType}'
                ORDER BY 
                    NEWID()";
                */

                squery = ExcelHelper.ReadExcelData();

                Dictionary<string, object> objResults = new Dictionary<string, object>();
                objResults = dbHelper.fetchValuesFromDB(squery);
                if (objResults.Count > 0)
                {
                    
                    
                    if (objResults.ContainsKey("Username"))
                    {
                       Variables.username = objResults["Username"].ToString();
                    }
                    else
                    {
                        Variables.username = objResults["UserName"].ToString();
                    }

                    AccountId = objResults["AccountId"].ToString();

                    Console.WriteLine("Username is " + Variables.username);
                    PerformLogin(Variables.username, "password");

                    if (loginerror > 0)
                    {
                        //Console.WriteLine(loginerror.Text);
                        PerformLogin(Variables.username, "Password1");
                    }

                    
                    if (IsAccountSummaryPageDisplayed())
                    {
                        break;
                    }
                    
                    
                }

                
                
                bool IsAccountSummaryPageDisplayed()
                {
                    return (driver.FindElements(By.XPath("//span[contains(text(),'Account Summary')]")).Count > 0 ||
                          driver.FindElements(By.XPath("//span[contains(text(),'Accounts')]")).Count > 0 ||
                          accountSummaryPage.pastduepopupCount > 0 ||
                          accountSummaryPage.AcknowledgePopupCount > 0 );
                }
                

                i++;
            } while (i <= 5);


            void PerformLogin(string username, string password)
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                UserName.Clear();
                UserName.SendKeys(username);
                Password.Clear();
                Password.SendKeys(password);
                genHelper.clickOn(SignInButton, "SignIn Button");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            }

            string[] brands = { "MMNARetail", "MMNALease", "MMNACommRetail", "MMNACommLease" };

            //foreach (string brand in brands)
            //{
            //    if (TestContext.Parameters.Get("Brand")!= null)
            //    {
            //        GlobalVariables.Username = "";
            //        Console.WriteLine("Username is " + GlobalVariables.Username);
            //        UserName.Clear();
            //        UserName.SendKeys(GlobalVariables.Username);
            //        Password.Clear();
            //        Password.SendKeys("password");
            //        ClickSignIn();
            //        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //        break;
            //    }
            //}

            //Console.WriteLine("User details are entered");
        }


        public string GetLendingClubBannerText()
        {
            genericHelper.waitForElement(LendingClubBanner);
            return LendingClubBanner.Text;
        }
    }
}
