using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyMyAccountViewOptions : BrowserHelper
    {
        LoginPage loginPage;
        MyAccountSummaryPage accountSummaryPage;
        MyAccountViewOptionsPage viewOptionsPage;
        MaturityModificationPage MatModPage; 

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            viewOptionsPage = new MyAccountViewOptionsPage(driver);
            MatModPage = new MaturityModificationPage(driver);
            
        }

        [Test]
        public void ViewOptionsValidation()
        {
            driver.Navigate().GoToUrl(appUrl);

            // Step 1: Open MyAccount on Stage.
            Console.WriteLine("Navigated to MyAccount URL: " + appUrl);

            // Step 2: Enter Username and Password and click Signin.
            loginPage.MyAccLoginInvalid();

            // Step 3: Verify Matmod header displayed string as "Maturity Date".
            string matmodHeaderText = MatModPage.GetMatmodHeaderText();
            Assertion.That(driver, matmodHeaderText, Is.EqualTo("Maturity Date"),
                "Matmod header text didn't match. Expected: Maturity Date but Actual: " + matmodHeaderText,
                "Matmod header text matched. Expected: Maturity Date and Actual: " + matmodHeaderText);

            // Step 4: Click on Acknowledgement Button on mature mod page.
            MatModPage.ClickAcknowledgementButton();

            // Step 5: Verify if ViewOptions button is displayed or not.
            accountSummaryPage.ValidateViewOptionsButtonDisplayed();

            // Step 6: If ViewOptions is displayed, verify if ViewOptionVerbiage is displayed or not.
            string viewOptionVerbiageText = accountSummaryPage.GetViewOptionVerbiageText();
            Assertion.That(driver, viewOptionVerbiageText, Is.EqualTo("Based on our records, you may have a balance due at maturity."),
                "View Option Verbiage text didn't match. Expected: Based on our records, you may have a balance due at maturity. but Actual: " + viewOptionVerbiageText,
                "View Option Verbiage text matched. Expected: Based on our records, you may have a balance due at maturity. and Actual: " + viewOptionVerbiageText);

            // Step 7: Click on Signout on login page.
            loginPage.MyAccLogOut();
        }
    }
}