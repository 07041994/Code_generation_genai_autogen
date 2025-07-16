using MyAccount.PageObjects;
using MyAccount.Utils;
using NUnit.Framework;
using static MyAccount.Utils.ExtendAssert;

namespace MyAccount.TestCases
{
    public class VerifyMyAccountInbox : BrowserHelper
    {
        private LoginPage loginPage;
        private MyAccountSummaryPage accountSummaryPage;
        private MyAccountInboxPage inboxPage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
            inboxPage = new MyAccountInboxPage(driver);
        }

        [Test]
        public void ValidateInboxFunctionality()
        {
            // Step 1: Open MyAccount on Stage
            driver.Navigate().GoToUrl(appUrl);
            Console.WriteLine("Navigated to MyAccount URL");

            // Step 2: Enter Username and Password and click Signin
            loginPage.LoginEnterUsernameDetails();
            Console.WriteLine("Logged in successfully");

            // Step 3: Handle Acknowledge popup if visible
            accountSummaryPage.clickAcknowledgeAndClosePopup();

            // Step 4: Close past due popup if visible
            accountSummaryPage.closePastDuePopup();

            // Step 5: Verify Account Summary header
            string accountSummaryHeaderText = accountSummaryPage.getAccountSummaryHeaderText();
            Assertion.That(driver, accountSummaryHeaderText, Is.EqualTo("Account Summary"),
                "Account Summary header text did not match",
                "Account Summary header text matched");

            // Step 6: Click on Inbox
            inboxPage.ClickInboxLink();

            // Step 7: Verify All_messages text is displayed
            bool isAllMessagesTextDisplayed = inboxPage.IsAllMessagesTextDisplayed();
            Assertion.That(driver, isAllMessagesTextDisplayed, Is.True,
                "All_messages text is not displayed",
                "All_messages text is displayed");

            // Step 8: If new_message is displayed, then click on new_message
            if (inboxPage.NewMessageBoxLink.Displayed)
            {
                inboxPage.ClickNewMessageBoxLink();
            }

            // Step 9: Verify Contact_customer_service_header text is displayed
            bool isContactCustomerServiceHeaderDisplayed = inboxPage.IsContactCustomerServiceHeaderDisplayed();
            Assertion.That(driver, isContactCustomerServiceHeaderDisplayed, Is.True,
                "Contact_customer_service_header text is not displayed",
                "Contact_customer_service_header text is displayed");

            // Step 10: Select the Account in Account dropdown
            inboxPage.SelectAccountFromDropdown(1);

            // Step 11: Select the subject in Subject dropdown
            inboxPage.SelectSubjectFromDropdown(1);

            // Step 12: Enter message_box value in Message_Box field
            inboxPage.EnterMessage("Hi, Raising a complaint regarding payments");

            // Step 13: Click on submitButton
            inboxPage.ClickSubmitButton();

            // Step 14: Verify If Thankyou text is displayed
            bool isThankYouMessageDisplayed = inboxPage.IsThankYouMessageDisplayed();
            Assertion.That(driver, isThankYouMessageDisplayed, Is.True,
                "Thankyou text is not displayed",
                "Thankyou text is displayed");

            // Step 15: If Thankyou text is displayed, then click on continue
            if (isThankYouMessageDisplayed)
            {
                inboxPage.ClickContinueButton();
            }

            // Step 18: Click on UserDropDown on the top right corner
            loginPage.MyAccLogOut();
            Console.WriteLine("Signout clicked");
        }
    }
}