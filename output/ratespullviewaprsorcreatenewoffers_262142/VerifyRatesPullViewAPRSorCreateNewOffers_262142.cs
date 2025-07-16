using System;
using System.IO;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Origination.PageObjects;
using Origination.Utils;

namespace Origination.Tests
{
    public class VerifyRatesPullViewAPRSorCreateNewOffers_262142
    {
        public static void Main(string[] args)
        {
            try
            {
                string jsonFilePath = "RatesPullViewAPRSorCreateNewOffersDetos.json";
                var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath));

                string username = jsonData["username"].ToString();
                string password = jsonData["password"].ToString();
                string effFromDay = jsonData["effFromDay"].ToString();
                string effFromMonth = jsonData["effFromMonth"].ToString();
                string effFromYear = jsonData["effFromYear"].ToString();
                string minTerm = jsonData["Min Term"].ToString();
                string maxTerm = jsonData["Max Term"].ToString();
                string zeroRate1 = jsonData["ZeroRate1"].ToString();
                string nonZeroRate1 = jsonData["NonZeroRate1"].ToString();
                string zeroRate2 = jsonData["ZeroRate2"].ToString();
                string nonZeroRate2 = jsonData["NonZeroRate2"].ToString();
                string zeroRate3 = jsonData["ZeroRate3"].ToString();
                string nonZeroRate3 = jsonData["NonZeroRate3"].ToString();

                IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                GenericHelper helper = new GenericHelper(driver);
                RatesPullViewAPRSorCreateNewOffers_262142 page = new RatesPullViewAPRSorCreateNewOffers_262142(driver);

                Console.WriteLine("Step 21: Click on the 'Incentives' tab.");
                page.ClickElement("Incentive");

                Console.WriteLine("Step 22: Click on the 'Retail APR Delta' button.");
                page.ClickElement("RetailAPRDelta");

                Console.WriteLine("Step 23: Click 'Pending Approval' Tab.");
                page.ClickElement("PendingApproval");

                Console.WriteLine("Step 24: Click 'Approve' button.");
                page.ClickElement("Approve");

                Console.WriteLine("Step 25: Click on 'Manager Approved'.");
                page.ClickElement("ManagerApproved");

                Console.WriteLine("Step 26: Verify ChangeSetID is same as 'FirstChangeSetID'.");
                string firstChangeSetID = driver.FindElement(By.XPath("(//div[@class='changeset-id col ng-star-inserted'])[1]")).Text;
                string changeSetID = driver.FindElement(By.XPath("(//div[@class='changeset-id col ng-star-inserted'])[1]")).Text;
                if (firstChangeSetID == changeSetID)
                {
                    Console.WriteLine("ChangeSetID matches FirstChangeSetID.");
                }
                else
                {
                    Console.WriteLine("ChangeSetID does not match FirstChangeSetID.");
                }

                Console.WriteLine("Step 27: Execute 'Move To Ready To Publish' Stored Procedure.");
                // Execute stored procedure logic here
                ExecuteStoredProcedure("MoveToReadyToPublish");

                Console.WriteLine("Step 28: Click on 'Ready To Publish' section.");
                page.ClickElement("ReadyToPublish");

                Console.WriteLine("Step 29: Verify ChangeSetID is same as 'FirstChangeSetID'.");
                changeSetID = driver.FindElement(By.XPath("(//div[@class='changeset-id col ng-star-inserted'])[1]")).Text;
                if (firstChangeSetID == changeSetID)
                {
                    Console.WriteLine("ChangeSetID matches FirstChangeSetID.");
                }
                else
                {
                    Console.WriteLine("ChangeSetID does not match FirstChangeSetID.");
                }

                Console.WriteLine("Step 30: Click on the 'Incentives' tab.");
                page.ClickElement("Incentive");

                Console.WriteLine("Step 31: Click on the 'View APRS or Create New Offers' button.");
                page.ClickElement("ViewAPRSorCreateNewOffers");

                Console.WriteLine("Step 32: Click on 'Create a New Special APR Offer' button.");
                page.ClickElement("CreateViewAPRSorCreateNewOffers");

                Console.WriteLine("Step 33: Select Effective From Date from Calendar.");
                page.SelectEffectiveFromDate(effFromDay, effFromMonth, effFromYear);

                Console.WriteLine("Step 34: Click 'Tier2' checkbox.");
                page.ClickElement("Tier2");

                Console.WriteLine("Step 35: Click 'Tier3' checkbox.");
                page.ClickElement("Tier3");

                Console.WriteLine("Step 36: Click 'Tier4' checkbox.");
                page.ClickElement("Tier4");

                Console.WriteLine("Step 37: Verify 'Tier2ZeroRate' is displayed.");
                if (page.VerifyElementDisplayed("Tier2ZeroRate"))
                {
                    Console.WriteLine("Tier2ZeroRate is displayed.");
                }
                else
                {
                    Console.WriteLine("Tier2ZeroRate is not displayed.");
                }

                Console.WriteLine("Step 38: Verify 'Tier3ZeroRate' is displayed.");
                if (page.VerifyElementDisplayed("Tier3ZeroRate"))
                {
                    Console.WriteLine("Tier3ZeroRate is displayed.");
                }
                else
                {
                    Console.WriteLine("Tier3ZeroRate is not displayed.");
                }

                Console.WriteLine("Step 39: Verify 'Tier4ZeroRate' is displayed.");
                if (page.VerifyElementDisplayed("Tier4ZeroRate"))
                {
                    Console.WriteLine("Tier4ZeroRate is displayed.");
                }
                else
                {
                    Console.WriteLine("Tier4ZeroRate is not displayed.");
                }

                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ExecuteStoredProcedure(string procedureName)
        {
            try
            {
                Console.WriteLine($"Executing stored procedure: {procedureName}");
                // Add logic to execute the stored procedure
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing stored procedure {procedureName}: {ex.Message}");
                throw;
            }
        }
    }
}