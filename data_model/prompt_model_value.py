from data_model.prompt_model import Message
# from prompt.prompt import result_format
from utils.logger import logging

def prompt_gen_func(data:Message,application_type,input_page_file=None):
    if application_type == 'RIMS':

        chunks = chunk_steps(data.steps_list)
        
        chunks1 = ['\n'.join(i) for i in chunks]

        # logging.info(f'chunks {chunks1}')
        # logging.info(f'main_prompt {data.main_prompt}')
        data.main_prompt = data.main_prompt.format(titles = data.titles,steps = data.steps_list,
                                expected_outputs=data.expected_outputs,variable_names=data.test_object_variables)
        # logging.info(f'main_prompt1 {data.main_prompt}')
        # logging.info(f'main_prompt {data.prompt_with_example}')
        data.prompt_with_example = data.prompt_with_example.format(titles_os=data.titles_os,
                                                                   steps_os=data.step_list_os,
                                                    expected_outputs_os = data.expected_outputs_os,
                                                    variable_names_os=data.test_object_variables_os,
                                                    dataos_os=data.detos_os,
                                                    page_os=data.page_os,
                                                    test_os=data.test_os)
        data.prompt_detos = data.prompt_detos.format(key_value_pairs=data.detos)
        # logging.info(f"generic {data.generichelper}")
        # logging.info(f"generic prompt {data.prompt_generichelper}")
        
        data.prompt_generichelper = data.prompt_generichelper.format(generic_helper = data.generichelper)
        data.prompt_mandatory = data.prompt_mandatory
        # logging.info(f'mandatory {data.prompt_mandatory}')
        # full_prompt1 = main_prompt1 +  helper_prompt + one_shot_prompt1
        # full_prompt1 = mandatory_prompt+main_prompt1 + detos_prompt + helper_prompt + one_shot_prompt1
        # data1 = data(full_prompt =  full_prompt1)
        return chunks,data
    elif application_type == 'MyAccount':
        if input_page_file:

            prompt_with_dependency1 = data.prompt_with_dependency.format(titles = data.titles,steps = data.steps_list,
                                expected_outputs = data.expected_outputs,variable_names = data.test_object_variables,
                                summ_page= data.myaccountsummarypage,login_page=data.loginpage,generic=data.generichelper,
                                browser=data.browserhelper,databasehelper=data.databasehelper,extendassert=data.extendassert,
                                extendreport=data.extendreporthelper,page_files = data.page_files)    
            main_prompt1 = data.main_prompt+prompt_with_dependency1
        else:
            prompt_without_dependency1 = data.prompt_without_dependency.format(titles = data.titles,steps = data.steps_list,
                                expected_outputs = data.expected_outputs,variable_names = data.test_object_variables,
                                summ_page= data.myaccountsummarypage,login_page=data.loginpage,generic=data.generichelper,
                                browser=data.browserhelper,databasehelper=data.databasehelper,extendassert=data.extendassert,
                                extendreport=data.extendreporthelper)    
            main_prompt1 = data.main_prompt+prompt_without_dependency1    
        one_shot_prompt1 = data.prompt_with_example.format(titles_os = data.titles_os,steps_os = data.step_list_os,
                                                    expected_outputs_os = data.expected_outputs_os,
                                                    variable_names_os = data.test_object_variables_os,
                                                    page_file_os =  data.page_file_os,
                                                    dataos_os = data.detos_os,
                                                    page_os = data.page_os,
                                                    test_os = data.test_os)
        detos_prompt = data.prompt_detos.format(key_value_pairs=data.detos)
        # full_prompt = main_prompt1 + detos_prompt
        
        full_prompt1 = main_prompt1 + detos_prompt + one_shot_prompt1
        # data1 = data(full_prompt =full_prompt1)
        return full_prompt1,data
  

def chunk_steps(steps_list, chunk_size=20):
    return [steps_list[i:i + chunk_size] for i in range(0, len(steps_list), chunk_size)]
