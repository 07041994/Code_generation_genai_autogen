---
*Page object summarization*

**MyAccountSummaryPage**  
- **Libraries**:  
  - `log4net`  
  - `MyAccount.Utils`  
  - `OpenQA.Selenium`  
  - `System`  

- **Functions**:  
  - `closePastDuePopup()`: Closes the past due popup if visible. Uses `GenericHelper` for element waiting.  
  - `getAccountSummaryHeaderText()`: Returns the text of the account summary header.  
  - `clickAcknowledgeAndClosePopup()`: Handles the acknowledgment popup by clicking the acknowledge button and closing it.  

- **Variables**:  
  - `AccountSummaryHeader`: WebElement for the account summary header.  
  - `pastduepopup`: WebElement for the past due popup.  
  - `UserDropDown`: WebElement for the user dropdown menu.  
  - `LogOut`: WebElement for the logout button.  
  - `AcknowledgePopupBtn`: WebElement for the acknowledgment button.  
  - `closeBtnAfterAcknowledgePopup`: WebElement for the close button after acknowledgment.  
  - `pastduepopupCount`: Count of past due popups.  
  - `AcknowledgePopupCount`: Count of acknowledgment popups.  

- **Dependencies**:  
  - Depends on `GenericHelper` for element interactions and waiting.  

---

**LoginPage**  
- **Libraries**:  
  - `log4net`  
  - `Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources`  
  - `MyAccount.Utils`  
  - `OpenQA.Selenium`  
  - `System`  

- **Functions**:  
  - `ClickSignIn()`: Clicks the sign-in button and waits for the page to load.  
  - `MyAccLogOut()`: Logs out from the account summary page using `MyAccountSummaryPage`.  
  - `LoginEnterUsernameDetails()`: Logs in using database query results and handles login errors.  

- **Variables**:  
  - `UserName`: WebElement for the username field.  
  - `Password`: WebElement for the password field.  
  - `SignInButton`: WebElement for the sign-in button.  
  - `loginerror`: WebElement for login error messages.  
  - `UserDropDown`: WebElement for the user dropdown menu.  
  - `LogOut`: WebElement for the logout button.  

- **Dependencies**:  
  - Depends on `MyAccountSummaryPage` for logout functionality.  
  - Uses `GenericHelper` for element interactions.  
  - Uses `DataBaseHelper` for fetching login credentials.  

---

*Utils summarization*

**GenericHelper**  
- **Libraries**:  
  - `AventStack.ExtentReports`  
  - `log4net`  
  - `OpenQA.Selenium`  
  - `SeleniumExtras.WaitHelpers`  

- **Functions**:  
  - `GetWebdriverWait()`: Creates a WebDriverWait object with a specified timeout.  
  - `WaitForWebElementToBeClickable()`: Waits for a web element to be clickable.  
  - `clickOn()`: Clicks on a web element and handles exceptions.  
  - `sendKeys()`: Sends input to a web element.  
  - `waitForElement()`: Waits for an element to be clickable.  
  - `selectValueFromDropdown()`: Selects a value from a dropdown by index.  
  - `ValidateElementPresentOrNot()`: Validates if an element is displayed or not.  

- **Dependencies**:  
  - Used by `MyAccountSummaryPage` and `LoginPage` for element interactions.  

---

**BrowserHelper**  
- **Libraries**:  
  - `OpenQA.Selenium.Chrome`  
  - `OpenQA.Selenium.Edge`  
  - `OpenQA.Selenium.Firefox`  
  - `OpenQA.Selenium`  

- **Functions**:  
  - `SetupReporting()`: Sets up reporting using `ExtentReporterHelper`.  
  - `Setup()`: Initializes the browser driver based on parameters and maximizes the window.  
  - `Teardown()`: Quits the browser driver and logs test results.  

- **Variables**:  
  - `driver`: WebDriver instance.  
  - `appUrl`: Application URL.  
  - `environment`: Test environment.  
  - `platform`: Platform information.  

- **Dependencies**:  
  - Uses `ExtentReporterHelper` for reporting.  

---

**DataBaseHelper**  
- **Libraries**:  
  - `System.Data.SqlClient`  

- **Functions**:  
  - `fetchValuesFromDB()`: Executes a SQL query and returns results as a dictionary.  

- **Dependencies**:  
  - Used by `LoginPage` for fetching login credentials.  

---

**ExtendAssert**  
- **Libraries**:  
  - `NUnit.Framework`  
  - `OpenQA.Selenium`  

- **Functions**:  
  - `That()`: Performs assertions and logs results using `ExtentReporterHelper`.  
  - `GetTestCaseIdFromCaller()`: Retrieves the test case ID from the caller method.  

- **Dependencies**:  
  - Uses `ExtentReporterHelper` for logging assertion results.  

---

**ExtentReporterHelper**  
- **Libraries**:  
  - `AventStack.ExtentReports`  
  - `OpenQA.Selenium`  
  - `System.Runtime.InteropServices`  

- **Functions**:  
  - `SetupReporting()`: Configures the ExtentReports instance.  
  - `CreateTest()`: Creates a test in the report.  
  - `LogTestResult()`: Logs the test result in the report.  
  - `CaptureScreenshot()`: Captures a screenshot and saves it to a file.  
  - `FormatBold()`: Formats a message in bold.  
  - `LogCustomAssertSuccess()`: Logs a successful assertion with a screenshot.  
  - `LogCustomAssertFailure()`: Logs a failed assertion with a screenshot.  
  - `Flush()`: Flushes the report to save changes.  

- **Dependencies**:  
  - Used by `BrowserHelper` and `ExtendAssert` for reporting and logging.  

---