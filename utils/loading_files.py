import os
import pandas as pd
from utils.logger import logging
import json
from io import BytesIO
from os import listdir
from os.path import isfile, join


async def reading_files(test_step_file,test_data_file,test_object_file,input_page_file=None):
    
    step_content = await test_step_file.read()
    test_step_file1 = pd.read_excel(BytesIO(step_content))
    logging.info('filename {0}'.format(test_step_file.filename))
    
    #extracting the steps,data,json file.
    # step_files = []
    # for i in test_step_file:
    #     step_content = await i.read()
    #     test_step_file1 = pd.read_excel(BytesIO(step_content))
    #     logging.info('filename {0}'.format(i.filename))
    #     step_files.append(test_step_file1)
    # test_step_file1.to_csv(os.path.splitext(test_step_file.filename)[0]+'.csv')
    
    test_data_file1 = json.loads(test_data_file.file.read())
    # data_content = await test_data_file.read()
    # test_data_file1 = pd.read_excel(BytesIO(data_content))
    logging.info('filename {0}'.format(test_data_file.filename))
    # test_data_file1.to_csv(os.path.splitext(test_data_file.filename)[0]+'.csv')

    test_object_file1 = json.loads(test_object_file.file.read())
    logging.info('filename {0}'.format(test_object_file.filename))
    logging.info('file_page {0}'.format(input_page_file))
    if input_page_file != None:
        # input_page_file1 = input_page_file
        # input_page_file1 = input_page_file1.decode('UTF-8')
        filename = []
        input_page_file1 = []
        for i in input_page_file:
            input_page_file1.append(i.file.read())
            filename.append(i.filename)
            # input_page_file1.append(i.decode('UTF-8'))
        
        logging.info('file_page {0}'.format(input_page_file1))
    else:
        input_page_file1 = None
        filename = None
        
    return test_step_file1,test_data_file1,test_object_file1,input_page_file1,filename
    
def read_sample_files(filename,path):
    # sample_files = {}
    full_path = os.path.join(path,filename)
    if filename.endswith(".cs"):
        with open(full_path, 'r') as file:
            # sample_files[filename] = file.read()
            return file.read()
    elif filename.endswith(".xlsx"):
        return pd.read_excel(full_path) 
    elif filename.endswith(".json"):
        with open(full_path,'r') as file:
            return json.load(file)


def read_folder_files(path):
    files = []
    for entry in os.scandir(path):
        if entry.is_dir():
            # print(entry.name)
            files.append([f for f in listdir(os.path.join(path,entry.name)) if isfile(join(path,entry.name,f))])
            # print(files)
        else:
            files = [f for f in listdir(path) if isfile(join(path, f))]
            # print(files)
    return files
