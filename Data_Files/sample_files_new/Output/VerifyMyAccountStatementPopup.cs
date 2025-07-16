using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyMyAccountStatementPopup : BrowserHelper
    {
        LoginPage loginPage;
        MyAccountSummaryPage accountSummaryPage;
        MyAccountStatementPopupPage statementPopupPage;
        MyAccountProfilePage profilePage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            statementPopupPage = new MyAccountStatementPopupPage(driver);
            profilePage = new MyAccountProfilePage(driver);    
        }

        [Test]
        public void StatementPopupValidation()
        {
            driver.Navigate().GoToUrl(appUrl);

            // Step 1: Verify Login is successful.
            loginPage.LoginEnterUsernameDetails();

            // Step 2: Verify Acknowledge pop up is closed.
            accountSummaryPage.clickAcknowledgeAndClosePopup();

            // Step 3: Verify Past due pop up is closed.
            accountSummaryPage.closePastDuePopup();

            // Step 4: Verify Account Summary text.
            string accountSummaryText = accountSummaryPage.getAccountSummaryHeaderText();
            Assertion.That(driver, accountSummaryText, Is.EqualTo("Account Summary"),
                "Account Summary displayed text didn't match. Expected : Account Summary but Actual is : " + accountSummaryText,
                "Account Summary displayed text matched. Expected : Account Summary and Actual is : " + accountSummaryText);

            // Step 5: Click on UserDropDown and Signout.
            loginPage.MyAccLogOut();

            // Step 6: Repeat Step 2 and Step 3.
            loginPage.LoginEnterUsernameDetails();
            accountSummaryPage.clickAcknowledgeAndClosePopup();

            // Step 7: Verify Verbage Message.
            string verbageMessage = statementPopupPage.VerbageMessage.Text;
            Assertion.That(driver, verbageMessage, Is.EqualTo("You have been enrolled in paperless statements. Please make sure your contact information is updated. To manage your preferences, click here."),
                "Verbage message text didn't match. Expected : You have been enrolled in paperless statements. Please make sure your contact information is updated. To manage your preferences, click here. but Actual is : " + verbageMessage,
                "Verbage message text matched. Expected : You have been enrolled in paperless statements. Please make sure your contact information is updated. To manage your preferences, click here. and Actual is : " + verbageMessage);

            // Step 8: Click on Contact Information Link.
            statementPopupPage.ClickContactInformationLink();

            // Step 9: Enter Email Address and select Statement Preference as Electronics.
            
            profilePage.EnterEmailAddress();
            profilePage.SelectStatementPreferenceElectronic();

            // Step 10: Click Save Button and verify Saved Message.
            profilePage.ClickSaveButton();
            string savedMessage = profilePage.GetSavedMessage();
            Assertion.That(driver, savedMessage, Is.EqualTo("The updates to your demographic information have been saved."),
                "Saved message text didn't match. Expected : The updates to your demographic information have been saved. but Actual is : " + savedMessage,
                "Saved message text matched. Expected : The updates to your demographic information have been saved. and Actual is : " + savedMessage);

            // Step 11: Click Back to Account Summary.
            profilePage.ClickBackToAccountSummary();

            // Step 12: Click on Click Here Link.
            statementPopupPage.ClickClickHereLink();

            // Step 13: Verify Update Preference Header.
            string updatePreferenceHeader = statementPopupPage.GetUpdatePreferenceHeaderText();
            Assertion.That(driver, updatePreferenceHeader, Is.EqualTo("Update your statement preferences"),
                "Update Preference Header text didn't match. Expected : Update your statement preferences but Actual is : " + updatePreferenceHeader,
                "Update Preference Header text matched. Expected : Update your statement preferences and Actual is : " + updatePreferenceHeader);

            // Step 14: Select Use Paper Radio Button and click Submit Button.
            statementPopupPage.SelectUsePaperRadioButton();
            statementPopupPage.ClickSubmitButton();

            // Step 15: Verify Verbage Message is not visible.
            Assertion.That(driver, statementPopupPage.VerbageMessage.Displayed, Is.False,
                "Verbage message is still visible after selecting Use Paper Radio Button.",
                "Verbage message is not visible after selecting Use Paper Radio Button.");

            // Step 16: Click on UserDropDown and Signout.
            loginPage.MyAccLogOut();
        }
    }
}