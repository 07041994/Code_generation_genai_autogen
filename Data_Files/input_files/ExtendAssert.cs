using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Reflection;


namespace MyAccount.Utils
{
    public class ExtendAssert
    {
        public static class Assertion
        {
            public static void That<TActual>(IWebDriver driver, TActual actual, IResolveConstraint expression, string failureMessage = null, string successMessage = null)
            {
                try
                {
                    Assert.That(actual, expression, failureMessage);
                    if (!string.IsNullOrEmpty(successMessage))
                    {
                        Console.WriteLine($"Assertion is successful : {successMessage}");
                        ExtentReporterHelper.LogCustomAssertSuccess($"Assertion Passed: {successMessage}", ExtentReporterHelper.CaptureScreenshot(driver));
                    }
                }
                catch (AssertionException)
                {
                    if (!string.IsNullOrEmpty(failureMessage))
                    {
                        Console.WriteLine($"Assertion is unsuccessful : {failureMessage}");
                        ExtentReporterHelper.LogCustomAssertFailure($"Assertion Failed: {successMessage}", ExtentReporterHelper.CaptureScreenshot(driver));
                    }
                    throw;
                }
                finally
                {
                    string testCaseId = GetTestCaseIdFromCaller();
                    if (!string.IsNullOrEmpty(testCaseId))
                    {
                        Console.WriteLine($"Test Case ID: {testCaseId}");
                    }
                }
            }
            private static string GetTestCaseIdFromCaller()
            {
                var stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(2).GetMethod();
                var attributes = method.GetCustomAttributes(typeof(PropertyAttribute), false);
                foreach (PropertyAttribute attribute in attributes)
                {
                    if (attribute.Properties.ContainsKey("TestCaseID"))
                    {
                        var testCaseId = attribute.Properties["TestCaseID"];
                        if (testCaseId is IList<object> list && list.Count > 0)
                        {
                            return list[0].ToString();
                        }
                        return testCaseId.ToString();
                    }
                }
                return null;
            }
        }
    }
}
