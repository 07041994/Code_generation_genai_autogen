# import pdb
import os, asyncio
from orchestration.prompt_generator import PromptGeneratorAgent
from utils.logger import logging
import re

from utils.postprocessing import CodeExtractor
import json
from config.config import get_configuration
from autogen_agent.agent_final import run_pipeline
from autogen_agent.agent_parallel import pipeline

config = get_configuration()


# # Setup logging
# logging = get_custom_logger()

class PlannerAgent:
    def __init__(self, test_cases_file, test_data_file, test_object_file, llm, 
                 input_files_path, sample_files_in_path, sample_files_out_path,
                 sample_files_in_path_rims,sample_files_out_path_rims,
                 application_type=None,input_page_file=None,filename=None):
        self.prompt_generator = PromptGeneratorAgent(test_cases_file, test_data_file, test_object_file,
                                                     input_files_path, sample_files_in_path,
                                                     sample_files_out_path,sample_files_in_path_rims,
                                                     sample_files_out_path_rims,application_type,
                                                     input_page_file,filename)
    def orchestrate(self):
        try:
            # Step 1: Generate Prompt
            chunks, message_data,app,test_titles = self.prompt_generator.generate_prompt()
            # pdb.set_trace()
            # return prompt
            # message_data1=message_data(full_prompt = prompt)
            # logging.info(f"prompt_message : {prompt}")
            logging.info(f"message_data : {message_data}")     
            logging.info(f"application: {app}")
            final_code = asyncio.run(pipeline(message=message_data,steps=chunks))
            # final_code = asyncio.run(run_pipeline(message_data,chunks))
            logging.info(f'final_code : {final_code}')
            # return final_code

            if app == "MyAccount":
                             
                dir_safe_title = re.sub(r'\W+', '_', test_titles.strip().lower())
                
                # # Initialize the CodeExtractor with the full code
                code_extractor = CodeExtractor(final_code, output_directory=f"output/{dir_safe_title}")

                logging.info(f"1: C# Code saved in the specified directory")

                code_extractor.extract_and_save()
                logging.info(f"C# Code saved in the specified directory")
                return final_code

                # with open('output/file.json', "w",encoding='utf-8') as file:
                #     file.write(csharp_code)
                # with open('output/file_prompt.json', "w",encoding='utf-8') as file:
                #     file.write(prompt)

                # logging.info("Generated C# Code")
                    
            
            elif app == "RIMS":   
            
                # CodeExtractor = CodeExtractor()             
                dir_safe_title = re.sub(r'\W+', '_', test_titles.strip().lower())
                
                # # Initialize the CodeExtractor with the full code
                code_extractor = CodeExtractor(final_code, output_directory=f"output/{dir_safe_title}")

                logging.info(f"1: C# Code saved in the specified directory")

                code_extractor.extract_and_save()
                logging.info(f"C# Code saved in the specified directory")

                # return csharp_code if is_valid_code else None
                return final_code
        
        except Exception as e:
            logging.error(f"Error during orchestration: {e}")
            raise

