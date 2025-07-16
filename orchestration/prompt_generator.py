import pandas as pd
import json
import tiktoken
from utils.logger import logging
from utils.token_count import num_tokens_from_string
from utils.loading_files import read_sample_files, read_folder_files
from data_model.prompt_model import Message
from prompt.prompt import prompts,prompt_validation
from data_model.prompt_model_value import prompt_gen_func
from config.config import get_configuration

config = get_configuration()

class PromptGeneratorAgent:
    def __init__(self, test_cases_file, test_data_file, test_object_file,  
                 input_files_path,sample_files_in_path, sample_files_out_path,
                 sample_files_in_path_rims,sample_files_out_path_rims,
                 input_page_file=None, application_type=None,filename=None):
        self.test_cases_file = test_cases_file
        self.test_data_file = test_data_file
        self.test_object_file = test_object_file
        self.input_files_path = input_files_path
        self.sample_files_in_path = sample_files_in_path
        self.sample_files_out_path = sample_files_out_path
        # self.llm = llm
        self.tokenizer = tiktoken.get_encoding("cl100k_base")
        self.application = application_type
        self.input_page_file = input_page_file
        self.filename =filename
        self.sample_files_in_path_rims = sample_files_in_path_rims
        self.sample_files_out_path_rims = sample_files_out_path_rims
        logging.info(f"PromptGeneratorAgent initialized with application type: {self.application}")

    def calculate_token_size(self, prompt):
        tokens = self.tokenizer.encode(prompt)
        logging.debug(f"Calculated token size: {len(tokens)}")
        return len(tokens)

    def generate_prompt(self,new_code = None):
        logging.info("Starting prompt generation.")
        test_cases = self.test_cases_file
        test_data = self.test_data_file
        key_value_pairs = test_data
        titles = test_cases["Title"].iloc[0]
        logging.debug(f"Extracted test title: {titles}")
        
        step = []
        expected_out = []
        for i, j in test_cases.iloc[0:, :].iterrows():
            if pd.notna(j.values[3]) and pd.notna(j.values[4]):
                step.append(j.values[3] + ' ' + j.values[4])
            if pd.notna(j.values[5]):
                expected_out.append(j.values[5])
        # steps1 = '\n'.join(step)
        # expected_out1 = '\n'.join(expected_out)
        logging.debug(f"Loaded test data and test cases.")

        if self.application == 'MyAccount':
            myaccount = prompts[self.application]
            try:
                app = "MyAccount"
                logging.info("Generating prompt for 'MyAccount' application.")
                
                # logging.info(f'Loaded prompt templates: {prompts}')
                titles = test_cases["Title"].iloc[0]
                # logging.debug(f"Extracted test title: {titles}")

            
                # step = []
                # expected_out = []
                # for i, j in test_cases.iloc[0:, :].iterrows():
                #     if pd.notna(j.values[3]) and pd.notna(j.values[4]):
                #         step.append(j.values[3] + ' ' + j.values[4])
                #     if pd.notna(j.values[5]):
                #         expected_out.append(j.values[5])
                # steps1 = '\n'.join(step)
                # expected_out1 = '\n'.join(expected_out)

                # logging.debug(f"Constructed test steps: {steps1}")
                # logging.debug(f"Constructed expected outputs: {expected_out1}")

                input_files = read_folder_files(self.input_files_path)
                # logging.info(f"Input files loaded: {input_files}")
                
                sample_in_files = read_folder_files(self.sample_files_in_path)
                # logging.info(f"Sample input files loaded: {sample_in_files}")
                
                sample_out_files = read_folder_files(self.sample_files_out_path)
                # logging.info(f"Sample output files loaded: {sample_out_files}")

                input_filess = {i: read_sample_files(i, self.input_files_path) for i in input_files}
                sample_in_filess = {i: read_sample_files(i, self.sample_files_in_path) for i in sample_in_files}
                sample_out_filess = {i: read_sample_files(i, self.sample_files_out_path) for i in sample_out_files}

            except Exception as e:
                logging.error(f"Error loading files: {e}")
                raise e

            for filename in sample_in_filess:
                if filename.endswith('.xlsx'):
                    logging.info(f"Processing XLSX file: {filename}")
                    titles_s = sample_in_filess[filename]["Title"].iloc[0]
                    step_s = []
                    expected_out_s = []
                    for i, j in sample_in_filess[filename].iloc[0:, :].iterrows():
                        if pd.notna(j['Test Step']) and pd.notna(j['Step Action']):
                            step_s.append(j['Test Step'] + ' ' + j['Step Action'])
                        if pd.notna(j['Step Expected']):
                            expected_out_s.append(j['Step Expected'])
                    steps_s1 = '\n'.join(step_s)
                    expected_out_s1 = '\n'.join(expected_out_s)
                    # logging.debug(f"One-shot steps: {steps_s1}")
                    # logging.debug(f"One-shot expected outputs: {expected_out_s1}")
                elif filename.lower().startswith('object'):
                    variable_name_one_shot = sample_in_filess[filename]
                    # logging.debug(f"Loaded object file: {filename}")
                elif filename.lower().startswith('detos'):
                    detos_one_shot = sample_in_filess[filename]
                    # logging.debug(f"Loaded detos file: {filename}")
                elif filename.lower().startswith('page'):
                    page_file_os1 = sample_in_filess[filename]
                    # logging.debug(f"Loaded page file: {filename}")

            for filename in sample_out_filess:
                if filename.lower().startswith('verify'):
                    test_one_shot = sample_out_filess[filename]
                    # logging.debug(f"Loaded verification file: {filename}")
                else:
                    page_one_shot = sample_out_filess[filename]
                    # logging.debug(f"Loaded output page file: {filename}")
            old_code = []
            old_code.append(input_filess['MyAccountSummaryPage.cs'])
            old_code.append(input_filess['LoginPage.cs'])
            input_page = {}
            if self.input_page_file:
                # logging.info('Page file provided. Using files_with_pagefile template.')
                # input_page = {}
                for i in range(len(self.input_page_file)):
                    input_page.update({self.filename[i]:self.input_page_file[i]})
                # logging.info(f'Files with pagefile prompt prepared.')
                old_code.extend(self.input_page_file)
                
            else:
                logging.info('No page file provided. Using files_without_pagefile template.')
            message_data = Message(main_prompt = myaccount['main'],
                                   prompt_with_example=myaccount['one_shot'],
                    prompt_detos = myaccount['detos'],
                    prompt_with_dependency=myaccount['prompt_with_dependency'],
                    prompt_without_dependency = myaccount['prompt_without_dependency'],
                    prompt_merging = myaccount['merging_prompt'],
                    prompt_ext = myaccount['code_ext'],
                    prompt_validation=prompt_validation,
                    titles = titles, steps_list = step,expected_outputs = expected_out,
                    test_object_variables = self.test_object_file,
                    myaccountsummarypage = input_filess['MyAccountSummaryPage.cs'],
                    loginpage = input_filess['LoginPage.cs'],
                    generichelper = input_filess['GenericHelper.cs'], 
                    browserhelper = input_filess['BrowserHelper.cs'],
                    databasehelper = input_filess['DataBaseHelper.cs'],
                    extendassert = input_filess['ExtendAssert.cs'],
                    extendreporthelper = input_filess['ExtendReportHelper.cs'],
                    detos = key_value_pairs,titles_os = titles_s,
                    step_list_os = steps_s1,expected_outputs_os = expected_out_s1,
                    page_file_os = page_file_os1,
                    test_object_variables_os = variable_name_one_shot,detos_os = detos_one_shot,
                    page_os = page_one_shot,test_os = test_one_shot,page_files = input_page,
                    old_f = old_code,validation = config['Validation'])
            full_prompt,message_data = prompt_gen_func(message_data,self.application,self.input_page_file)
            # return full_prompt,message_data,app,titles
            
                
        elif self.application == 'RIMS':
            
            app = "RIMS"
            logging.info("Generating prompt for 'RIMS' application.")
            # main_prompt = prompts[self.application]['main']
            # logging.info("Read Main prompt for 'RIMS' application. %s", main_prompt)
            
            # logging.info("Read Detos prompt for 'RIMS' application %s",datos_prompt)
            
            
            logging.info("Main prompt format is Completed for 'RIMS' application.")
            # datos_prompt1 = datos_prompt.format(key_value_pairs=key_value_pairs)
            with open(r".\Data_Files\rims-helper\GenericHelper.cs", 'r') as gen_helper:
                gen_helper=gen_helper.read()
            # helper_prompt1 = helper_prompt.format(generic_helper=gen_helper)
            logging.debug(f"Datos prompt generated.")
            
            ## One shot Prompting Code
            sample_in_files = read_folder_files(self.sample_files_in_path_rims)
            # logging.info(f"Sample input files loaded: {sample_in_files}")
            
            sample_out_files = read_folder_files(self.sample_files_out_path_rims)
            # logging.info(f"Sample output files loaded: {sample_out_files}")

            sample_in_filess = {i: read_sample_files(i, self.sample_files_in_path_rims) for i in sample_in_files}
            sample_out_filess = {i: read_sample_files(i, self.sample_files_out_path_rims) for i in sample_out_files}

            for filename in sample_in_filess:
                if filename.endswith('.xlsx'):
                    logging.info(f"Processing XLSX file: {filename}")
                    titles_s = sample_in_filess[filename]["Title"].iloc[0]
                    step_s = []
                    expected_out_s = []
                    for i, j in sample_in_filess[filename].iloc[0:, :].iterrows():
                        if pd.notna(j['Test Step']) and pd.notna(j['Step Action']):
                            step_s.append(j['Test Step'] + ' ' + j['Step Action'])
                        if pd.notna(j['Step Expected']):
                            expected_out_s.append(j['Step Expected'])
                    steps_s1 = '\n'.join(step_s)
                    expected_out_s1 = '\n'.join(expected_out_s)
                    # logging.debug(f"One-shot steps: {steps_s1}")
                    # logging.debug(f"One-shot expected outputs: {expected_out_s1}")
                elif filename.lower().startswith('object'):
                    variable_name_one_shot = sample_in_filess[filename]
                    # logging.debug(f"Loaded object file: {filename}")
                elif filename.lower().startswith('detos'):
                    detos_one_shot = sample_in_filess[filename]
                    # logging.debug(f"Loaded detos file: {filename}")
            for filename in sample_out_filess:
                if filename.lower().startswith('verify'):
                    test_one_shot = sample_out_filess[filename]
                    # logging.debug(f"Loaded verification file: {filename}")
                else:
                    page_one_shot = sample_out_filess[filename]
                    # logging.debug(f"Loaded output page file: {filename}")
            rims = prompts[self.application]
            message_data = Message(main_prompt=rims['main'],prompt_with_example=rims['one_shot'],
                    prompt_detos=rims['detos'],prompt_generichelper=rims['generic_helper'],
                    prompt_mandatory=rims['mandatory_instructions'],
                    prompt_validation=prompt_validation,
                    titles=titles, steps_list=step,expected_outputs=expected_out,test_object_variables=self.test_object_file,
                    detos=key_value_pairs,generichelper=gen_helper,titles_os=titles_s,
                    step_list_os=steps_s1,expected_outputs_os=expected_out_s1,
                    test_object_variables_os=variable_name_one_shot,detos_os=detos_one_shot,
                    page_os=page_one_shot,test_os=test_one_shot,validation=config['Validation'])
            full_prompt,message_data = prompt_gen_func(message_data,self.application)
            
        # token_size = self.calculate_token_size(full_prompt)
        # logging.info(f"Prompt token size: {token_size} tokens")
        # logging.debug(f"Final generated prompt: {full_prompt.encode('utf-8')}")

        # if new_code == None:
        return full_prompt,message_data,app,titles
        # else:
            # return merging_prompt1
    def ext_prompt(self,csharp_code1,prompt):
        prompt1 = prompt.format(text = csharp_code1)
        # logging.info('code_ext_prompt {0}'.format(prompt1))
        return prompt1
    
    def generate_merging_prompt(self,prompt,old_code,new_code):
        # logging.info(f'old_code : {old_code}')
        # logging.info(f'account_summary : {input_filess['MyAccountSummaryPage.cs']}')
        # logging.info(f'input_page_file : {self.input_page_file}')
        # logging.info(f'new_code : {len(new_code)}')
        merging_prompt1 = prompt.format(old_f = old_code,new_f = new_code)
        # logging.info(f'merging_prompt : {merging_prompt1.encode('utf-8')}')
        return merging_prompt1

    @staticmethod
    def validation_prompt(prompt_validation, final_code, test_steps, expected_outputs):
        prompt_validation1 = prompt_validation.format(
            code1=final_code,
            steps=test_steps,
            expected_output=expected_outputs
        )
        token_size = num_tokens_from_string(prompt_validation1)
        # logging.info(f'Validation Prompt token size: {token_size} tokens')
        # logging.debug(f"Validation Prompt content: {prompt_validation1}")
        return prompt_validation1
