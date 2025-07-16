using MyAccount.PageObjects;
using MyAccount.Utils;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyMyAccountStatementPopup : BrowserHelper
    {
        LoginPage loginPage;
        MyAccountSummaryPage accountSummaryPage;
        MyAccountProfilePage profilePage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            profilePage = new MyAccountProfilePage(driver);
        }

        [Test]
        public void ValidateMyAccountSteps()
        {
            driver.Navigate().GoToUrl(appUrl);

            // Step 1: Open MyAccount on Stage.
            
            // Step 2: Enter Username and Password and click Signin.
            //    loginPage.LoginEnterUsernameDetails();
            loginPage.MyAccLoginInvalid();

            // Step 3: Verify popup header.
            string popupHeaderText = accountSummaryPage.GetPopupHeaderText();
            Assertion.That(driver, popupHeaderText, Is.EqualTo("We have switched your account to e‑statements!"),
                "Popup header text didn't match. Expected: We have switched your account to e‑statements! but Actual: " + popupHeaderText,
                "Popup header text matched. Expected: We have switched your account to e‑statements! and Actual: " + popupHeaderText);

            // Step 4: Click Acknowledge button.
            accountSummaryPage.ClickAcknowledgeButton();

            // Step 5: Verify Thank You message.
            string thankYouMessageText = accountSummaryPage.GetThankYouMessageText();
            Assertion.That(driver, thankYouMessageText, Is.EqualTo("Thank you!"),
                "Thank You message text didn't match. Expected: Thank you! but Actual: " + thankYouMessageText,
                "Thank You message text matched. Expected: Thank you! and Actual: " + thankYouMessageText);

            accountSummaryPage.CloseThankYouPupupAfterAcknowledge();
            accountSummaryPage.closePastDuePopup();

            // Step 6: Click on UserDropDown and Signout.
            loginPage.MyAccLogOut();

            // Step 7: Repeat Step 2 and Step 3.
            //           loginPage.LoginEnterUsernameDetails();
            loginPage.MyAccLoginInvalid();

            accountSummaryPage.closePastDuePopup();

            // Step 8: Verify Verbage message.
            string verbageMessageText = accountSummaryPage.VerbageMessage.Text;
            Assertion.That(driver, verbageMessageText, Is.EqualTo("You have been enrolled in paperless statements. Please make sure your contact information is updated. To manage your preferences, click here."),
                "Verbage message text didn't match. Expected: You have been enrolled in paperless statements. but Actual: " + verbageMessageText,
                "Verbage message text matched. Expected: You have been enrolled in paperless statements. and Actual: " + verbageMessageText);

            // Step 9: Click on Contact Information link.
            accountSummaryPage.ClickContactInformationLink();

            // Step 10: Enter Email address.
            profilePage.EnterEmailAddress();

            // Step 11: Select Statement Preference as Electronic.
            profilePage.SelectStatementPreferenceElectronic();

            // Step 12: Click Save button.
            profilePage.ClickSaveButton();

            // Step 13: Verify Saved message.
            string savedMessageText = profilePage.GetSavedMessage();
            Assertion.That(driver, savedMessageText, Is.EqualTo("The updates to your demographic information have been saved."),
                "Saved message text didn't match. Expected: The updates to your demographic information have been saved. but Actual: " + savedMessageText,
                "Saved message text matched. Expected: The updates to your demographic information have been saved. and Actual: " + savedMessageText);

            // Step 14: Click Back to Account Summary.
            profilePage.ClickBackToAccountSummary();

            // Step 15: Click on Click Here link.
            accountSummaryPage.ClickClickHereLink();

            // Step 16: Verify Update Preference header.
            string updatePreferenceHeaderText = accountSummaryPage.GetUpdatePreferenceHeaderText();
            Assertion.That(driver, updatePreferenceHeaderText, Is.EqualTo("Update your statement preferences"),
                "Update Preference header text didn't match. Expected: Update your statement preferences but Actual: " + updatePreferenceHeaderText,
                "Update Preference header text matched. Expected: Update your statement preferences and Actual: " + updatePreferenceHeaderText);

            // Step 17: Select Use Paper radio button and click Submit.
            accountSummaryPage.SelectUsePaperRadioButton();

            accountSummaryPage.ClickSubmitButtonForStatementPreferance();

            // Step 18: Verify Verbage message is not visible.
            Assertion.That(driver, accountSummaryPage.VerbageMessage.Displayed, Is.False,
                "Verbage message is still visible.",
                "Verbage message is not visible.");

            // Step 19: Click on UserDropDown and Signout.
            loginPage.MyAccLogOut();
        }
    }
}