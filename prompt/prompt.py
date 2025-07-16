from data_model.prompt_model import Message
prompts = {'MyAccount':{'main':""" You are a skilled software engineer specializing in test automation.
                               Let us go through each step to give an optimized solution.
                                Your task is to Generate the C# code that would do the following:
                                Read the Steps list and Generate the following Output C# Files.
                                step 1. 
                                Read the below pageobject files,page_file dictionary values,utils files, test_object_variables ,test_case carefully,and Generate the C# code files.
                                Ensure ,Generate C# files for test_object_variables dictionary keys and the number of generated files should be equal to length of test_object_variables dictionary keys strictly without fail.
                                the name of files belong to test_object_variables dictionary keys. 
                                                        
                                For any function definitions, ensure a proper return statement that accurately reflects the function's purpose and expected output strictly.
                                Avoid using empty or placeholder returns; always return meaningful values or objects as required.                                
                                These files should contain the class with the necessary WebElement and driver properties.
                                For each element defined in the test_object_variables follow test_case Steps_list common functions and their information also. 
                                These functions should be written based on test_object_variables and test_case Steps_list. 
                                Generate function for all variables of test_object_variables, which using in test_case Steps_list strictly.
                                 
                                create the function for test_case Steps_list,which use functions like click,wait activity,text in this files.
                                Write a function for variables but synchronize it with another variable and test_case Steps_list according to their definition.
                                write test_case functionalities common function.
                                Read the test_case and write test_case common functions as classes, in their respective files.
                                function name should not be variable name.
                                do not generate function for username,password,login,signin functionalities.
                                Use the signin,signout,login,logout,dropdown function from Loginpage strictly. Do not use logout variable here. Example: "for logout use 'loginpage.MyAccLogOut'".
                                Use the common function form GenericHelper, which works for click,wait,validate.
                                do not generate duplicate code strictly.
                                Save these files in the folder 'myaccount/PageObjects'. 
                                                                
                                step 2. 
                                Create file based on test_case, and test_object_variables. 
                                This file should contain the class and method for implementing the test_case steps and their logic. Execute the each Steps which is in Steps_list, and Compare generated output with the Expected_Outputs, if there is difference between Expected_Outputs and generated outputs, generate the correct code which produces the result same as Expected_Outputs. 
                                Use step 1 generated file, utils and pageobject files in this step to write the code for test_case.
                                Ensure All verify statements in the test steps, have Assertion.That instead of Assert.IsTrue for validation without fail strictly.
                                Ensure the assertion has clear, descriptive failure messages to enhance test result readability and debugging.
                                generate code for all verify statement strictly.
                                use the page_file dictionary values  class to create function in test_case steps where it is require to use in test_case steps strictly.Example: 'profilepage.EnterCellPhone'.
                                Use if-else statement  for test_case steps condition strictly and properly.
                                Use the 'clear' function in generated code where test_case steps mentioned 'clear'.
                                use the function 'ValidateElementPresentOrNot' to check the condition of displaying or not from GenericHelper.
                                Read the url variable from BrowserHelper file.
                                
                                Use the signin,signout,login,logout,dropdown function from Loginpage strictly. Example: "for logout use 'loginpage.MyAccLogOut'".
                                Read the account summary function from account summary page.
                                For login steps,use the login,signin function from Loginpage strictly. Example:'loginpage.LoginEnterUsernameDetails()'.
                                do not use these login function. Example: loginpage.username(),loginpage.password(),loginpage.signinbutton().
                                use Acknowledge , ClosePastDuePopup from MyAccountSummaryPage. Example: 'closePastDuePopup()' and 'clickAcknowledgeAndClosePopup()'.
                                For the common functions in test_case, generate the common function if it is absent from step 1 and pageobject files.    
                                Use all object variables in only step 1.
                                Ensure,Generate code for each test_case steps with functionalities in detail strictly.
                                Ensure number of steps present in the test_case Steps_list are matching with the steps in generated code strictly.
                                Generate code for each test_case steps strictly.
                                use function from step 1 generated file for all variable which has function in step 1 output strictly. Example: "maturityModificationPage.GetSplashPageHeaderText()".
                                do not use generic helper file strictly here.
                                do not variable value in code strictly.
                                do not mention 'Additional steps continue here...' strictly and generate full code for all test_case steps strictly.
                                Step 3. Generate the C# code only; there is no need to explain the code, advantages, and dependencies. 
                                        mention the step number with their definition in a comment.
                                step 4. Ensure best practices in C# for readability, maintainability, and efficiency.
                                step 5. Provide the comments also with code.
                                step 6. Use libraries in each file.
                                
                                step 7. Ensure generate code for every steps in test_case steps list strictly.
                                step 8. do not mention any notes,step,explanations.                                
                                       
                                You MUST return a complete C# implementation, covering every step in Steps_list, without skipping or summarizing. The code must span all logic needed, and the model must use all output token space if needed.

                                ### Expected Output Format
                                Example output:
                                        {
                                        "code": "### **LoginPage.cs**\\n```csharp\\nusing System;\\npublic class LoginPage { public void Login() { Console.WriteLine(\\"Login Successful\\"); } }\\n```",
                                        "status": "OK",
                                        "feedback": null
                                        }

                                Return strictly in this JSON structure:

                                ```json
                                {
                                "code": "### **Filename.cs**\\n```csharp\\n// Your full C# code here\\n```",
                                "status": "OK",
                                "feedback": null
                                }
                                'DO NOT respond with "// C# code here" or any placeholder. Always return real, functional C# code using the steps and variables provided. Return it ONLY inside the "code" key of the JSON format.'

                                           """,
                        
                        'one_shot':"""E) ** Example **                
                        
                                        Input:
                                        Titles: {titles_os}
                                        Steps_list: {steps_os}
                                        Exptected_Outputs: {expected_outputs_os}
                                        Test_object_variables:{variable_names_os}
                                        page_file:{page_file_os}
                                        dataos:{dataos_os} 
                                        
                                        output:
                                        "code" : {page_os},{test_os}
                                        "status" : "OK"
                                        "feedback": null
                                                """,
                        
                        'prompt_without_dependency': """
                                The input for the prompt would be:
                                A) test_case:
                                1.Titles: {titles}
                                2.Steps_list: {steps}
                                3.Expected_Outputs: {expected_outputs}              
                                
                                B) test_object_variables:{variable_names}
                                C) pageobjects:
                                1. MyAccountSummaryPage: {summ_page}
                                2. Loginpage: {login_page}
                        
                                D) utils:
                                1. GenericHelper: {generic}
                                2. BrowserHelper: {browser}
                                3. DatabaseHelper: {databasehelper}
                                4. Extendassert: {extendassert}
                                5. ExtendReportHelper: {extendreport}
                                
                                """,
                        
                        'prompt_with_dependency': """ 
                                The input for the prompt would be:
                                A) test_case:
                                1.Titles: {titles}
                                2.Steps_list: {steps}
                                3.Expected_Outputs: {expected_outputs}              
                                
                                B) test_object_variables:{variable_names}
                                
                                                
                                C) utils:
                                1. GenericHelper: {generic}
                                2. BrowserHelper: {browser}
                                3. DatabaseHelper: {databasehelper}
                                4. Extendassert: {extendassert}
                                5. ExtendReportHelper: {extendreport}


                                D) pageobjects:
                                1. MyAccountSummaryPage: {summ_page}
                                2. Loginpage: {login_page}
                                E) page_file: {page_files}
                                """,

                        'detos': '''D) **Test data or Datos Variables:**
                                        Generate the C# class which read the json file using C# directly,
                                        The Dataos file is a JSON file. 
                                        use only variable names instead of variable value in the code as per the above details strictly.
                                        Datos file: {key_value_pairs}''',
                'merging_prompt':{'main': """ You are a skilled software engineer specializing in test automation. Let us go through each step to give an optimized solution.
                                step 1. read below both old_file,new_file files carefully,compare and find the relevent class from new_file list to old_file.
                                step 2. append the step 1 output into old_file and generate updated C# code.append every webelement and their function strictly.
                                        append every step 1 output function which is not present in old_file.
                                  
                                        if web element  is present in old_file and new_file both,keep old_file web element,its value with old_file syntax.
                                        do not delete old_file C# code.
                                        class name should be old_file files class name.
                                        name of the files should be their class name.

                                        
                                        
                                        do not generate duplicate C# code.
                                        do not give any notes,instructions.
                                """,
                        'main_data':"""old_file : {old_f}
                                new_file : {new_f}
                                output should be in below format strictly.
                                Expected output : 
                                 ### **FileName.cs **
                                        ```csharp
                                        c# code
                                        ```
                                """},
                        'code_ext':'''You are a skilled software engineer specializing in test automation. Let us go through each step to give an optimized solution.
                                Your task is to Generate the C# code that would do the following:
                                Read the below input carefully.
                                Extract exact same C# code from input file strictly.  
                                Extract all C# code strictly.                                   
                                
                                input : {text}
                                expected output: 
                                the output structure of file is below
                                ```
                                     ### **FileName.cs **
                                        ```csharp
                                        c# code
                                        ```
                                '''
                },
                'RIMS':{'main':""" You are a skilled software engineer specializing in test automation,
                                Let us go through each step to give an optimized solution.
                                Your task is to Generate the C# code that would do the following:
                                Read the Steps list and Generate the following Output C# Files.        
                                
                        Step 1: Generate Object and Function Files
                                Read the test_object_variables, test_case, and Steps_list.
                                create the function for every variables in test_object_variables strictly.
                                Always use the following genericHelper ( imported from Origination.Utils) methods for Selenium actions to ensure stability and maintainability:
                                Functions must return meaningful values—no empty or placeholder returns.
                                Ensure functions align with test_case steps and synchronize variable definitions.
                                Avoid duplicate code across all generated files.
                                Keep all webelements definition and functions which are required to perform operation according to test steps within filename.cs file
                                Keep all functions and genericHelper related functions within Page object file. Do not extend it to Verify file at any cost.
                                Generate function in proper format of C# function.
                                generate function for all web elements which are using test case file.
                                                                               
                        Step 2: Generate Test Execution Logic
                                Generate a complete C# code based on Steps_list and Expected_Outputs.

                                Strictly,generate executable test code for all test steps present in Steps_list . Example :'If Steps_list contains 30 steps,then code should also generate for 30 steps with out missing or skipping any step that is given in the Steps_list strictly. '
                                Use Console.WriteLine() for every steps strictly.
                                for login steps use 'RIMS_Helper.LaunchApplicationRIMS()'  strictly.
                                Use/Keep URL always as a part of test steps, dont include any URLS in setup.
                                Add import statement using Originations.PageObjects; without fail.
                                Do not hardcode any data from detos, instead use its variables to fill the data which are defined within text fixture section.
                                Always call the functions created in Step 1, or those defined within helper file attached.
                                Use if-else logic where applicable for decision-based test steps.
                                Strictly,You MUST return a complete C# implementation, covering every step in Steps_list, without skipping or summarizing. 
                                The code must span all logic needed, and the model must use all output token space if needed.
                        Step 3: 
                                Provide the C# code strictly in the following format without any introductory explanations or additional text.
                                Create or use filename based on title porvide in test case.
                                ---
                                        ### **FileName.cs **
                                        ```csharp
                                        c# code
                                        ```
                                        ### **VerifyFileName.cs **
                                        ```csharp
                                        c# code
                                        ```        
                                Expected Output: 2 C# Files:
                                        One Page Object File named filename.cs which have all information related to web elements, function definitions.
                                        One Test Steps File named verifyfilename.cs which have driver initilization to performing all test steps as listed.
                        
                                The input for the prompt would be:
                                A) test_case:
                                1.Titles: {titles}
                                       
                                
                                B) test_object_variables:{variable_names}
                        
             
                                        """,
                        'detos': '''C) **Test data or Datos Variables:**
                                        Without fail, take the json filename given in test steps and read all variables present in detos to the verify file.
                                                string jsonFilePath = "filename.json";
                                                var jsonData = JObject.Parse(File.ReadAllText(jsonFilePath)); 
                                        and load all available info into respective datatypes in one by one  in below format
                                                string variable1 = jsonData["variable1"].ToString();
                                                DateTime variable2 = jsonData["variable2"].ToString();
                                        Like above steps load all variables present in Detos file along with its associated datatype in the beginning, and the variables in Datos file are as follows strictly.
                                        Datos file: {key_value_pairs}''',
                        'generic_helper' : '''Use functions from generic helper wherever required instead of regenrating them . The contents of generic helper are as follows. {generic_helper}''',
                        'one_shot':"""D) Below is the example to follow the code format for all drivers initialization, detos and variable definition and other structures of program. 
                                        You can use it as a sample ** Example **                
                        
                                        Input:
                                        Titles: {titles_os}
                                        Steps_list: {steps_os}
                                        Exptected_Outputs: {expected_outputs_os}
                                        Test_object_variables:{variable_names_os}
                                        dataos:{dataos_os} 
                                        
                                        output:
                                        1) page : {page_os}
                                        2) test : {test_os}
                                        """,
                        'mandatory_instructions': """ To accomplish the given task, below are the mandatory, must and should instructions to follow without any excuses/
                                Generate **full code** for **each test step**, even if the step is repetitive or identical.
                                - **Do not** summarize, merge, skip, or use placeholder comments like:
                                - "// Repeat similar steps..."
                                - "// Implement all steps as per the Steps_list"
                                - "// Continue with all steps as per the Steps_list"
                                - " // Continue implementing all steps sequentially..."
                                - "// Continue as above..."
                                - "// See previous step..."
                                - " // Continue with remaining steps..."
                                -"// Add logic to execute stored procedure here"
                                - "// Additional steps will follow same pattern..."
                                -"// Continue implementing all steps sequentially..."
                                -"// Implementation for clicking checkbox goes here..."
                                -"// Repeat similar steps for the second card creation and submission process as per the Steps_list.
                                -" // (Steps 26-48 will follow the same pattern as above.)"
                                -"// Repeat Steps 26-48 as per the Steps_list"
                                -"// (Implementation follows the same pattern as above.)"
                                -"// Repeat similar steps for the second card creation and submission process as per the Steps_list."
                                -"// Each step will be implemented exactly as described in the Steps_list."
                                -" // Continue with the remaining steps as per the Steps_list..."
                                -" // Continue with remaining steps..."
                                -"// Continue with the remaining steps..."
                                -"// Logic to verify ChangeSetID"
                                Under any circumstances, should not skip or compressed any step.
                                And below is the task that you have to generate code.
                                Generate code for all steps strictly if you did not generate for all steps re-run again."""
                } 
                
                  
}                  
prompt_validation = ''' You are  C# coder. Read the below step carefully.

                    step 1: Read below C# code carefully.
                    step 2: Summarize the code and give details of each step in details.             
                    step 3: Ensure that C# code is satisfying criteria like readability, maintainability, memory, latency,and efficiency.        
                    step 4: Check the syntax of C# code and tell us about any error in C# code and return the results.
                    step 5: Give us any suggestion to that which needs to be improved in C# code.
                    step 6: Check each step of code with each step of  {steps} and expected output {expected_output} in details.
                            Check on definition of code functionalities only. 
                    step 7: Provide the result of step 1 to step 5 in details.
                    step 8: Provide the result of step 6.the format of result is JSON which have two information, first is valid or invalid and second is description of step 6.
                            JSON has two key is 'result' and 'description'.
                    
                    code : {code1}
                    output :'''


result_format="""
You must reply ONLY with JSON that matches this exactly:

{"code": "<python code>", "status": "OK" or "ERROR", "feedback": null or "<details>"}
"""

def chunk_prompt_fun(message:Message,chunk,full_code):

        # for idx, chunk in enumerate(chunks, start=1):
        # Craft integrated prompt
        return (
                f"{message.main_prompt}\n"
                
                "\n".join(f"{n+1}. {step}" for n, step in enumerate(chunk)) +
                f"\n\n{message.prompt_detos}\n\n{message.prompt_generichelper}\n\n{message.prompt_mandatory}"
                f"\n\nPrevious code so far:\n```csharp\n{full_code}```\n\n"
                "Generate ONLY the C# code for these steps using Page Object methods, Console.WriteLine(), JSON data. "
                "No skipping, no summaries."
                )        

def merge_prompt_fun(message:Message,full_code):
        
        return (
        f"{message.main_prompt}\n"
        "Here is the FULL GENERATED CODE across chunks:\n\n"
        f"```csharp\n{full_code}\n```"
        "\nPlease:\n"
        "1. Merge and generate into two files (PageObject.cs and TestSteps.cs)\n"
        "2. Validate correctness; if issues found, update code\n"
        "Return result as CodeContent."
            )
        