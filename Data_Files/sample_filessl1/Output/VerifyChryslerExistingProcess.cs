using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyChryslerExistingProcess : BrowserHelper
    {
        LoginPage loginPage;
        MaturityModificationPage maturityModificationPage;
        FilenetPage filenetPage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            maturityModificationPage = new MaturityModificationPage(driver);
            filenetPage = new FilenetPage(driver);
        }

        [Test]
        public void ValidateChryslerSteps()
        {
            // Step 1: Open My Account on stage
            driver.Navigate().GoToUrl(appUrl);

            // Step 2: Login with valid username and password
            loginPage.LoginEnterUsernameDetails(Variables.Username, Variables.Password);

            // Step 3: Click on Sign in
            loginPage.SignInButton.Click();

            // Step 4: Verify Maturity Date text
            string maturityDateText = maturityModificationPage.SplashPageHeader.Text;
            Assertion.That(driver, maturityDateText, Is.EqualTo("Maturity Date"),
                "Maturity Date text didn't match. Expected: Maturity Date but Actual: " + maturityDateText,
                "Maturity Date text matched. Expected: Maturity Date and Actual: " + maturityDateText);

            // Step 5: Click on 'Click Here' link
            maturityModificationPage.ClickHereLink.Click();

            // Step 6: Verify Account Maturity Modification text
            string accountMaturityModificationText = maturityModificationPage.AccountMaturityModificationHeader.Text;
            Assertion.That(driver, accountMaturityModificationText, Is.EqualTo("Account Maturity Modification"),
                "Account Maturity Modification text didn't match. Expected: Account Maturity Modification but Actual: " + accountMaturityModificationText,
                "Account Maturity Modification text matched. Expected: Account Maturity Modification and Actual: " + accountMaturityModificationText);

            // Step 7: Click on 'Request a maturity Modification' option
            maturityModificationPage.RequestMaturityModification.Click();

            // Step 8: Verify the current text
            string currentText = maturityModificationPage.Current.Text;
            Assertion.That(driver, currentText, Is.EqualTo("Current:"),
                "Current text didn't match. Expected: Current: but Actual: " + currentText,
                "Current text matched. Expected: Current: and Actual: " + currentText);

            // Step 9: Verify Proposed Modification text
            string proposedModificationText = maturityModificationPage.ProposedModification.Text;
            Assertion.That(driver, proposedModificationText, Is.EqualTo("Proposed Modification:"),
                "Proposed Modification text didn't match. Expected: Proposed Modification: but Actual: " + proposedModificationText,
                "Proposed Modification text matched. Expected: Proposed Modification: and Actual: " + proposedModificationText);

            // Step 10: Click on Continue button
            maturityModificationPage.ClickContinueButton();

            // Step 11: Click on consent check box
            maturityModificationPage.ClickConsentCheckbox();

            // Step 12: Verify error message
            string errorMessageText = maturityModificationPage.ErrorMessage.Text;
            Assertion.That(driver, errorMessageText, Is.EqualTo("Please download and view Maturity Modification Agreement Document to Consent."),
                "Error message text didn't match. Expected: Please download and view Maturity Modification Agreement Document to Consent. but Actual: " + errorMessageText,
                "Error message text matched. Expected: Please download and view Maturity Modification Agreement Document to Consent. and Actual: " + errorMessageText);

            // Step 13: Click on download PDF agreement
            maturityModificationPage.PDFAgreement.Click();

            // Step 14: Click on submit button
            maturityModificationPage.ClickSubmitButton();

            // Step 15: Login to Filenet and open the account ID
            driver.Navigate().GoToUrl("https://icn.us.pre.corp/navigator/?desktop=SCGeneral");
            filenetPage.AcquisitionByAccountNumber.Click();
            filenetPage.EnterAccountNumber(Variables.AccountNumber);
            filenetPage.ClickSearchButton();

            // Step 16: Verify the PDF document is visible
            Assertion.That(driver, filenetPage.PDFDocument.Displayed, Is.True,
                "PDF document is not visible.",
                "PDF document is visible.");
        }
    }
}