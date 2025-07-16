from fastapi.middleware.cors import CORSMiddleware
from fastapi import FastAPI,UploadFile,Header,BackgroundTasks,File
from utils.logger import logging
from config.config import get_configuration 
from generator.TestCaseGenerators import test_case_generator
from utils.loading_files import reading_files
from typing import Annotated,Optional,Union,List
import pandas as pd
import os
# from utils.payload_schema import GenerateCodePayload, GetFileContentPayload, GetGeneratedCodePayload, WriteFileContentPayload


fast_app = FastAPI()

config = get_configuration()

origins = ["*"]
fast_app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)



@fast_app.get("/")
def root():
    '''
    Base route
    '''
    return {"message": "Welcome to Test code generator for Santander"}


@fast_app.post("/Testcase/files")
async def readInputDataAndPassOntoLLM(test_cases_file: UploadFile,test_data_file: UploadFile,test_object_file:UploadFile,
                                      user_id: Annotated[str | None, Header()],
                                    session_id: Annotated[str | None, Header()],
                                    application_type: Annotated[str | None, Header()],
                                    background:BackgroundTasks,
                                    input_page_file: Optional[List[UploadFile]] = File(None)):
                                    # input_page_file: Union[UploadFile, None] = None):
                                    # input_page_file: Annotated[UploadFile | None, File()]):
    
    logging.info('files are uplaoding by user id {0}, session id {1} and application_type {2} and input page file {3} '.format(user_id,session_id,
                                                                                                                               application_type,input_page_file))
    test_cases_file,test_data_file,test_object_file,input_page_file1,filename = await reading_files(test_cases_file,test_data_file,test_object_file,
                                                                                           input_page_file)    
    background.add_task(test_case_generator.code_generator,test_cases_file,test_data_file,test_object_file,
                        application_type,input_page_file1,filename)
    
    # result = test_case_generator.code_generator(test_cases_file,test_data_file,test_object_file)
    
    return 'file has been uploaded and processing.'

# @fast_app.post("/generatecode")
# async def generate_code(payload: GenerateCodePayload,background:BackgroundTasks):
#     # Parse the DataFrame from the incoming JSON
#     test_steps = pd.read_json(payload.test_steps, orient="split")  # Use the correct orientation based on what you send
#     # test_data = pd.read_json(payload.test_data, orient="split")
#     test_data = payload.test_data
#     test_object = payload.test_object
#     application_type = payload.application_type
#     input_depend_file1 = payload.input_depend_files
#     filenames =  payload.filenames
#     logging.info('application_type {0} '.format(application_type))
#     background.add_task(test_case_generator.code_generator,test_steps,test_data,test_object,application_type,input_depend_file1,filenames)
#     # result = test_case_generator.code_generator(test_steps,test_data,test_object)
    
#     return 'file has been uploaded and processing.'


# @fast_app.post("/getgeneratedfiles")
# async def get_generated_code(payload: GetGeneratedCodePayload):
#     path = payload.path
#     response = {'folders':[], 'files':[]}
#     try:
#         # List directories and files at the current path
#         items = os.listdir(path)
#         folders = [item for item in items if os.path.isdir(os.path.join(path, item))]
#         files = [item for item in items if os.path.isfile(os.path.join(path, item))]
#         response['folders'] = folders
#         response['files'] = files
#     except Exception as e:
#         logging.info(f"Error accessing {path}: {e}")
#     return response


# @fast_app.post("/getfilecontent")
# async def get_file_content(payload: GetFileContentPayload):
#     file_path = payload.file_path
#     response = {'content':''}
#     try:
#         with open(file_path, 'r') as file:
#             response['content'] = file.read()
#     except Exception as e:
#         logging.info(f"Error accessing {file_path}: {e}")
#     return response

    
# @fast_app.post("/writefilecontent")
# async def write_file_content(payload: WriteFileContentPayload):
#     file_path = payload.file_path
#     content = payload.content
#     response = {'response':''}
#     try:
#         with open(file_path, 'w') as file:
#             file.write(content)
#         response['response'] = 'true'
#     except Exception as e:
#         response['response'] = 'false'
#         logging.info(f"Error accessing {file_path}: {e}")
#     return response