from utils.logger import logging
# from utils.call_llm import LLMModels
from config.config import get_configuration
from orchestration.planner import PlannerAgent

config = get_configuration()



class TestCaseGenerator:
    
    def __init__(self):
        
        # Define file paths from the config
        self.input_files_path= config['sample_files']['input_files']
        self.sample_files_in_path = config['sample_files']['sample_files_input']
        self.sample_files_out_path = config['sample_files']['sample_files_output']
        self.sample_files_in_path_rims = config['sample_files']['sample_files_input_rims']
        self.sample_files_out_path_rims = config['sample_files']['sample_files_output_rims']
      
    def code_generator(self,test_cases_file,test_data_file,test_object_file,
                       input_page_file=None,application_type = None,filename=None):
        
        try:
            # Initialize the LLM
            # llm = LLMModels()
            llm = 'llm'
            # Initialize Planner Agent
            planner = PlannerAgent(test_cases_file, test_data_file, test_object_file,
                                llm, self.input_files_path, self.sample_files_in_path,
                                self.sample_files_out_path,self.sample_files_in_path_rims,
                                self.sample_files_out_path_rims,application_type,
                                input_page_file,filename)

            # Orchestrate the prompt generation and validation
            final_code = planner.orchestrate()

            if final_code:
                logging.info("Successfully generated and validated the C# code.")
                result = 'Done'
            else:
                logging.error("Failed to generate a valid C# code.")
                result = 'Failed to generate a code'
        except Exception as e:
            logging.error(f"Application encountered an error: {e}")
            result = 'Application encountered an error'
        return result

test_case_generator = TestCaseGenerator()
