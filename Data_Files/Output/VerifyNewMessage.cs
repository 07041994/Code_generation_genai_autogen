using MyAccount.PageObjects;
using MyAccount.Utils;
using OpenQA.Selenium.BiDi.Communication;
using static MyAccount.Utils.ExtendAssert;


/*
-------------------------------------------------------------------------------------------------------------------------------------
| Step | Action                                                                 | Expected Result                                   |
|------|------------------------------------------------------------------------|---------------------------------------------------|
| 1    | Login to MyAccount                                                     | Login is successful.                              |
| 2    | If acknowledge pop up is displayed then click the Acknowledge button   | Acknowledgement is successful.                    |
| 3    | If past due pop up is displayed then close the pop up                  | Past due pop up is closed.                        |
| 4    | Verify Account summary header                                          | Account Summary page is displayed.                |
| 5    | Click on Inbox Link and verify all messages text                       | "All Messages" text is displayed.                 |
| 6    | Click on New Message Link                                              | Contact customer header is displayed.             |
| 7    | Fill all the details and click on the submit button                    | Mandatory details are filled and submitted.       |
| 8    | Verify thank you message                                               | Thank you message is displayed.                   |
| 9    | Click on Continue Button                                               | Thank you pop up is closed.                       |
| 10   | LogOut                                                                 | Logout is successful.                             |
------------------------------------------------------------------------------------------------------------------------------------|
 */


namespace MyAccount.TestCases
{
    public class MyAccountInboxVerifyNewMessage:BrowserHelper
    {
        LoginPage loginPage;
        InboxPage inboxPage;
        MyAccountSummaryPage accountSummaryPage;

        [SetUp]
        public void SetupTest()
        {
            loginPage = new LoginPage(driver);
            inboxPage = new InboxPage(driver);
            accountSummaryPage = new MyAccountSummaryPage(driver);
        }

        [Test]
        public void InboxValidation()
        {
            driver.Navigate().GoToUrl(appUrl);

            //Verify Login is successful.
            loginPage.LoginEnterUsernameDetails();

            //Verify Past due pop up is closed.
            accountSummaryPage.closePastDuePopup();

            //Verify Account Summary text.
            Assertion.That(driver, accountSummaryPage.getAccountSummaryHeaderText(), Is.EqualTo("Account Summary"),
                "Account Summary displayed text didn't match. Expected : Account Summary displayed text but Actual is : " + accountSummaryPage.getAccountSummaryHeaderText()
                , "Account Summary displayed text matched. Expected : Account Summary displayed text and Actual is : " + accountSummaryPage.getAccountSummaryHeaderText());

            string inboxMessageText = inboxPage.ValidateNewMsgInInbox();

            //Verify "All Messages" text is displayed. 
            Assertion.That(driver, inboxMessageText, Is.EqualTo("All Messages"), 
                "Inbox message didn't match. Expected : All Messages but Actual is : "+ inboxMessageText
                , "Inbox message matched. Expected : All Messages and Actual is : " + inboxMessageText);

            string custServiceHeadText = inboxPage.ValidateContactCustomerServiceHeaderText();

            //Verify Contact customer header is displayed.
            Assertion.That(driver, custServiceHeadText, Is.EqualTo("Contact Customer Service"),
                "Contact customer service message didn't match. Expected : Contact Customer Service but Actual is : " + custServiceHeadText
                , "Contact customer service message matched. Expected : Contact Customer Service and Actual is : " + custServiceHeadText);

            string thankYouMessageText = inboxPage.ValidateThankYouMessageText();

            //Verify Mandatory details are filled and submitted.
            Assertion.That(driver, thankYouMessageText, Is.EqualTo("Thank you for contacting us. Your message will be routed to the appropriate personnel."),
                "Thank you message didn't match. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. but Actual is : " + thankYouMessageText
                , "Thank you message matched. Expected : Thank you for contacting us. Your message will be routed to the appropriate personnel. and Actual is : " + thankYouMessageText);


            //Verify Thank you pop up is closed. 
            inboxPage.ClickContinueButton();
            
            //Verify Logout is successful.
            loginPage.MyAccLogOut();

        }
        
    }
}
