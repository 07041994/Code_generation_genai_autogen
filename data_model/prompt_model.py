from dataclasses import dataclass
import json
from pydantic import BaseModel, Field

@dataclass
class Message:
    main_prompt : str  = None
    prompt_with_example :str = None
    prompt_with_dependency : str = None
    prompt_without_dependency : str = None
    prompt_detos : str = None
    prompt_merging : dict = None
    prompt_generichelper : str = None
    prompt_mandatory : str = None
    prompt_ext :str = None
    prompt_validation : str = None
    full_prompt : str =None

    titles : str = None
    steps_list :list = None
    expected_outputs : list = None
    test_object_variables : str = None
    
    generichelper : str = None
    browserhelper : str = None
    databasehelper : str = None
    extendassert : str = None
    extendreporthelper : str = None
    myaccountsummarypage : str = None
    loginpage : str = None
    page_files : list = None
    detos : json = None
    
    titles_os : str = None
    step_list_os :str = None
    expected_outputs_os : str = None
    test_object_variables_os : str = None
    page_file_os : str = None
    detos_os : json = None
    page_os : str = None
    test_os : str = None
    
    old_f : str = None
    new_f : str = None
    input : str = None
    code : str = None
    validation : bool = None
    output_previous : str = None
                                


class AgentResponse(BaseModel):
    code: str = Field(..., description="Generated or updated code")
    status: str = Field(..., description="OK or ERROR")
    feedback: str | None = Field(None, description="If error, validation messages")

# @dataclass
# class SchemaResponse:
#     response: AgentResponse

# 🧰 Define structured data models
class CodeContent(BaseModel):
    code: str

class ReviewContent(BaseModel):
    issues: list[str]
    approved: bool

class promptt(BaseModel):
    prompt :str


