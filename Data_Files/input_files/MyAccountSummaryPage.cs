using log4net;
using MyAccount.Utils;
using OpenQA.Selenium;
using System;

namespace MyAccount.PageObjects
{
    public class MyAccountSummaryPage
    {
        private IWebDriver driver;
        private GenericHelper genericHelper;

        public MyAccountSummaryPage(IWebDriver driver)
        {
            this.driver = driver;
            this.genericHelper = new GenericHelper(driver);
        }

        public IWebElement AccountSummaryHeader => driver.FindElement(By.XPath("//h1//span[contains(text(),'Account Summary')]"));
        public IWebElement pastduepopup => driver.FindElement(By.XPath("//button[@data-click='closePastDueModal']//i"));
        public IWebElement UserDropDown => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[6]/a/span[2]"));
        public IWebElement LogOut => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[6]/ul/li[4]/a"));
        public IWebElement AcknowledgePopupBtn => driver.FindElement(By.XPath("//button[contains(text(),'Acknowledge')]"));
        public IWebElement closeBtnAfterAcknowledgePopup => driver.FindElement(By.XPath("//button[@data-bind='click: closeEstatementAcknowlegeModal']"));
        public IWebElement ViewOptionsButton => driver.FindElement(By.XPath("//*[@id='accountSummaryPage']//div/button[contains(text(),'View Options')]"));
        public IWebElement ViewOptionVerbiage => driver.FindElement(By.XPath("//span[@class='info-title mat-mod-text']"));
        public IWebElement VerbageMessage => driver.FindElement(By.XPath("//p[@class='alert-message' and contains(text(),'You have been enrolled in paperless statements')]"));
        public IWebElement ContactInformationLink => driver.FindElement(By.XPath("//p[@class='alert-message']//a[@href='/Profile/Personal?updateProfile=true']"));
        public IWebElement ClickHereLink => driver.FindElement(By.XPath("//p[@class='alert-message']//a[contains(text(),'click here')]"));
        public IWebElement UpdatePreferenceHeader => driver.FindElement(By.XPath("//h5[@class='modal-title' and contains(text(),'Update your statement preferences')]"));
        public IWebElement UsePaperRadioButton => driver.FindElement(By.XPath("//label[@for='statement-paper']"));
        public IWebElement SubmitButton => driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement PastDueAmount => driver.FindElement(By.XPath("//div[@class='col-xs-4']//span[@data-bind='currencyText: AmountDue']"));
        public IWebElement PopupHeader => driver.FindElement(By.XPath("//h5[@class='modal-title' and contains(text(),'We have switched your account to eÃƒÂ¢Ã¢â€šÂ¬Ã¢â‚¬Ëœstatements!')]"));
        public int pastduepopupCount => driver.FindElements(By.XPath("//button[@data-click='closePastDueModal']//i")).Count;
        public int AcknowledgePopupCount => driver.FindElements(By.XPath("//div[@id='estatement-acknowledge-modal' and contains(@style,'block')]//button[contains(text(),'Acknowledge')]")).Count;
        public IWebElement AmountVerbiageQuestionMark => driver.FindElement(By.XPath("//*[@id='accountSummaryPage']/div[3]/div[1]/div/div[4]/div[2]/div[2]/div[3]/div[1]/div[1]/div[1]/div/span[1]/a"));
        public IWebElement AmountVerbiage => driver.FindElement(By.XPath("//span[@class='info-title' and contains(text(),'Amount Due')]//a"));
        public IWebElement ViewMoreOptions => driver.FindElement(By.XPath("//button[contains(text(),'View more options')]"));
        public IWebElement AcknowledgePopupHeader => driver.FindElement(By.XPath("//h5[@class='modal-title' and contains(text(),'We have switched your account to eÃƒÂ¢Ã¢â€šÂ¬Ã¢â‚¬Ëœstatements!')]"));
        public IWebElement AcknowledgeButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-statement-round block' and text()='Acknowledge']"));
        public IWebElement ThankYouMessage => driver.FindElement(By.XPath("//h5[@class='modal-title' and contains(text(),'Thank you!')]"));
        public IWebElement SubmitButtonForStatementPreferance => driver.FindElement(By.XPath("//button[@data-bind='click: updateCommunicationPreferences']"));
        public IWebElement PastDuePopupHeader => driver.FindElement(By.XPath("//h2[@class='h5' and contains(text(),'multiple accounts currently past due')]"));
        public IWebElement VehicleDropdown => driver.FindElement(By.XPath("//div[@data-bind='visible: multipleAccounts']//div[@class='select-dropdown-heading']"));
        public IWebElement Vehicle1 => driver.FindElement(By.XPath("(//div[@class='select-dropdown-container'])[1]"));
        public IWebElement Vehicle2 => driver.FindElement(By.XPath("(//div[@class='select-dropdown-container'])[2]"));
        public IWebElement VehicleAndInsurance => driver.FindElement(By.XPath("//button[@class='btn btn-summary-round' and contains(text(),'Vehicle and Insurance')]"));
        public IWebElement YearMakeModel => driver.FindElement(By.XPath("//span[@data-bind='text: vehicleYearMakeModel']"));
        public IWebElement securityUpdates => driver.FindElement(By.XPath("//*[@id='loginSecurity']/div/div/div"));
        public IWebElement VehicleAndClaimButton => driver.FindElement(By.XPath("//button[@class='btn btn-summary-round' and @data-navto='/Account/vehicle']"));
        public IWebElement DoYouWantLeaveYesButton => driver.FindElement(By.XPath("//a[@class='small button active confirm-modal-button']"));
        public IWebElement TransactionHistory => driver.FindElement(By.XPath("//*[@id='accountSummaryPage']/div[2]/div[3]/div/div/div[4]/div[3]/div[3]/div[5]/div/a"));
        public IWebElement LogoutMessage => driver.FindElement(By.XPath("//*[@id='menu-secondary']/li[6]/ul/li[4]/a"));
        public IWebElement AccountProfileLink => driver.FindElement(By.XPath("//li[@class='menu-item account-profile']"));

        public string getsecurityUpdatesHeader()
        {
            genericHelper.waitForElement(securityUpdates);
            return securityUpdates.Text;
        }

        public void ValidatePopupHeaderDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(AcknowledgePopupHeader, "Popup Header");
        }

        public string GetPopupHeaderText()
        {
            genericHelper.waitForElement(PopupHeader);
            return PopupHeader.Text;
        }

        public void ClickAcknowledgeButton()
        {
            genericHelper.clickOn(AcknowledgeButton, "Acknowledge Button");
        }

        public void ValidateThankYouMessageDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(ThankYouMessage, "Thank You Message");
        }

        public void ValidateVerbageMessageDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(VerbageMessage, "Verbage Message");
        }

        public void ClickContactInformationLink()
        {
            genericHelper.clickOn(ContactInformationLink, "Contact Information Link");
        }

        public void ClickClickHereLink()
        {
            genericHelper.clickOn(ClickHereLink, "Click Here Link");
        }

        public void ValidateUpdatePreferenceHeaderDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(UpdatePreferenceHeader, "Update Preference Header");
        }

        public void ClickUsePaperRadioButton()
        {
            genericHelper.clickOn(UsePaperRadioButton, "Use Paper Radio Button");
        }

        public void ClickSubmitButton()
        {
            genericHelper.clickOn(SubmitButton, "Submit Button");
        }

        public bool closePastDuePopup()
        {
            genericHelper.waitForElement(pastduepopup);
            if (pastduepopup.Displayed)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", pastduepopup);
                Console.WriteLine("Past Due Popup is visible and closed successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Past Due pop up is not visible");
                return false;
            }
        }

        public string getAccountSummaryHeaderText()
        {
            genericHelper.waitForElement(AccountSummaryHeader);
            return AccountSummaryHeader.Text;
        }

        public string GetPastDueAmountText()
        {
            genericHelper.waitForElement(PastDueAmount);
            return PastDueAmount.Text;
        }

        public string GetPastDuePopupHeaderText()
        {
            genericHelper.waitForElement(PastDuePopupHeader);
            return PastDuePopupHeader.Text;
        }

        public string GetAccountSummaryHeaderText()
        {
            genericHelper.waitForElement(AccountSummaryHeader);
            return AccountSummaryHeader.Text;
        }

        public bool clickAcknowledgeAndClosePopup()
        {
            Thread.Sleep(2000);
            genericHelper.GetWebdriverWait(TimeSpan.FromSeconds(5));
            genericHelper.waitForElement(AcknowledgePopupBtn);
            if (AcknowledgePopupBtn.Displayed)
            {
                genericHelper.clickOn(AcknowledgePopupBtn, "Acknowledge");
                if (AcknowledgePopupBtn.Displayed)
                {
                    genericHelper.clickOn(AcknowledgePopupBtn, "Acknowledge");
                }
                genericHelper.clickOn(closeBtnAfterAcknowledgePopup, "Close Button After Acknowledge");
                return true;
            }
            else
            {
                Console.WriteLine("Acknowledge popup is not visible");
                return false;
            }
        }

        public void ClickUserDropDown()
        {
            genericHelper.clickOn(UserDropDown, "User DropDown");
        }

        public void MouseHoverOnAmountDueQuestionMark()
        {
            genericHelper.waitForElement(AmountVerbiageQuestionMark);
            genericHelper.clickOn(AmountVerbiageQuestionMark, "Amount Due Question Mark");
        }

        public string GetAmountVerbiageText()
        {
            genericHelper.waitForElement(AmountVerbiage);
            genericHelper.clickOn(AmountVerbiageQuestionMark, "Amount Due Question Mark");
            return AmountVerbiage.GetAttribute("data-original-title");
        }

        public void ClickViewOptionsButton()
        {
            genericHelper.clickOn(ViewOptionsButton, "View Options Button");
        }

        public void ClickViewMoreOptionsButton()
        {
            genericHelper.clickOn(ViewMoreOptions, "View More Options Button");
        }

        public string GetViewOptionVerbiageText()
        {
            genericHelper.waitForElement(ViewOptionVerbiage);
            return ViewOptionVerbiage.Text;
        }

        public void ValidateViewOptionsButtonDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(ViewOptionsButton, "View Options Button");
        }

        public string GetUpdatePreferenceHeaderText()
        {
            genericHelper.waitForElement(UpdatePreferenceHeader);
            return UpdatePreferenceHeader.Text;
        }

        public void SelectUsePaperRadioButton()
        {
            genericHelper.clickOn(UsePaperRadioButton, "Use Paper Radio Button");
        }

        public void ClickSubmitButtonForStatementPreferance()
        {
            genericHelper.clickOn(SubmitButtonForStatementPreferance, "Submit Button");
        }

        public string GetPopupHeaderAcknowledgeText()
        {
            genericHelper.waitForElement(AcknowledgePopupHeader);
            return AcknowledgePopupHeader.Text;
        }

        public string GetThankYouMessageText()
        {
            Thread.Sleep(1000);
            genericHelper.waitForElement(ThankYouMessage);
            return ThankYouMessage.Text;
        }

        public void CloseThankYouPupupAfterAcknowledge()
        {
            genericHelper.waitForElement(closeBtnAfterAcknowledgePopup);
            genericHelper.clickOn(closeBtnAfterAcknowledgePopup, "Close Button After Acknowledge");
        }

        public void ClickVehicleDropdown()
        {
            genericHelper.clickOn(VehicleDropdown, "Vehicle Dropdown");
        }

        public void ClickFirstVehicle()
        {
            genericHelper.clickOn(Vehicle1, "First Vehicle");
        }

        public void ClickSecondVehicle()
        {
            genericHelper.clickOn(Vehicle2, "Second Vehicle");
        }

        public void ClickVehicleAndInsurance()
        {
            genericHelper.clickOn(VehicleAndInsurance, "Vehicle and Insurance");
        }

        public string GetYearMakeModelText()
        {
            genericHelper.waitForElement(YearMakeModel);
            return YearMakeModel.Text;
        }

        public void ValidateYearMakeModelDisplayed()
        {
            genericHelper.ValidateElementPresentOrNot(YearMakeModel, "Year Make Model");
        }

        public void ClickVehicleAndClaimButton()
        {
            genericHelper.clickOn(VehicleAndClaimButton, "Vehicle And Claim Button");
        }

        public void ClickDoYouWantToLeavePageYesButton()
        {
            genericHelper.clickOn(DoYouWantLeaveYesButton, "Do You Want To Leave YesButton");
        }

        public void ClickTransactionHistory()
        {
            genericHelper.clickOn(TransactionHistory, "Transaction History");
        }

        public void ValidateLogoutMessage(string expectedMessage)
        {
            string actualMessage = LogoutMessage.Text;
            ExtendAssert.Assertion.That(driver, actualMessage, Is.EqualTo(expectedMessage),
                $"Logout message did not match. Expected: {expectedMessage}, Actual: {actualMessage}",
                $"Logout message matched. Expected: {expectedMessage}, Actual: {actualMessage}");
        }

        public void ClickAccountProfileLink()
        {
            genericHelper.clickOn(AccountProfileLink, "Account Profile Link");
        }
    }
}