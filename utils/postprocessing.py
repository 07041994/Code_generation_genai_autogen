import os
from utils.logger import logging
import re
import json

# # Setup logging
# logging = get_custom_logger()


class CodeExtractor:
    def __init__(self, full_code, output_directory="output"):
        self.full_code = full_code
        self.output_directory = output_directory
        self._create_output_directory()

    def _create_output_directory(self):
        """Creates the output directory if it doesn't exist."""
        if not os.path.exists(self.output_directory):
            os.makedirs(self.output_directory)

    def extract_and_save(self, validation=None,validated_summary=None,file_extension="cs"):

        code_blocks = self._extract_code_blocks()
        for file_path, file_content in code_blocks:
            file_path = os.path.join(self.output_directory, file_path.split("/")[-1])
            self._save_to_file(file_path, file_content.strip())
        # file_name = file_path.split("/")[-1]  # Extract the file name
        # with open(file_path.split("/")[-1], "w") as file:
            # file.write(file_content.strip())
            # print(f"Created: {file_name}")

        # for i,key in enumerate(code_blocks):
        #     file_name = f"{file_names[i]}.{file_extension}"
        #     file_path = os.path.join(self.output_directory, file_name)
        #     self._save_to_file(file_path, code_blocks[key])


        # if len(code_blocks) != len(file_names):
        #     raise ValueError("The number of code blocks and file names must be equal.")

        # for i, code_block in enumerate(code_blocks):
        #     file_name = f"{file_names[i]}.{file_extension}"
        #     file_path = os.path.join(self.output_directory, file_name)

            # Save the extracted code block to a file
            # self._save_to_file(file_path, code_block)

            logging.info(f"File saved: {file_path}")
            
            # if validation == True:
            #     validate_file_name = 'validated_summary.txt'
            #     validate_file_path = os.path.join(self.output_directory, validate_file_name)
            #     self._save_to_file(validate_file_path,validated_summary)


    def _extract_code_blocks(self):
        # matches = re.findall(r"#### Filename:\s*`(.*?)`\s*```csharp\n(.*?)\n```", self.full_code, re.DOTALL)
        # matches = re.findall(r"#### \*\*(.*?)\*\*\s*```csharp\n(.*?)\n```", self.full_code, re.DOTALL)
        
        # r"#### Filename:\s*([\w\.]+)\s*```csharp\n(.*?)\n```"
        # pattern = r"// File: (.*?)\n(.*?)(?=// File:|\Z)"
        # matches=re.findall(r"#### (.+?)\n```csharp\n(.*?)```",self.full_code,re.DOTALL)
        matches = re.findall(r"### \*\*(?P<filename>[\w\-.]+\.cs) \*\*[\r\n]+```csharp\s*(?P<code>.*?)```", self.full_code, re.DOTALL)
        return matches
        # json_format_code = re.sub('json|```','',self.full_code)
        # json_format_code1 = json.loads(json_format_code)
        
        # return json_format_code1
        
        # Remove unnecessary markers (such as ```csharp or ```)
        # full_code = self.full_code.replace("csharp", "").strip()

        # Splitting by the triple backticks and filtering out the code blocks
        # parts = full_code.split("```")
        # code_blocks = [part.strip() for i, part in enumerate(parts) if i % 2 != 0]
        # return code_blocks

    def _save_to_file(self, file_path, content):
        with open(file_path, "w") as file:
            file.write(content)

    def set_full_code(self, full_code):
        self.full_code = full_code

