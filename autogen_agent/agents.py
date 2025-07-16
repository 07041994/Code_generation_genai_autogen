from autogen_agentchat.agents import AssistantAgent
from autogen_agent.autogen_model import model_client
from data_model.prompt_model import CodeContent,ReviewContent


# 2️⃣ Coder agent — outputs structured CodeContent
coder = AssistantAgent(
    name="coder",
    model_client=model_client,
    
    
    # system_message=(
    #     """You are an expert C# automation engineer. 
    #     return the response in this format 
    #      {
    #         "code": "```csharp\n<full code here>\n```"
    #         }"""
           
    # ),
    output_content_type=CodeContent
)

# valid JSON with the key \"code\" containing the complete C# script."""
# 3️⃣ Reviewer agent — inputs structured code and returns structured ReviewContent
reviewer = AssistantAgent(
    name="reviewer",
    model_client=model_client,
    system_message=(
        """You are a skilled software engineer. review the generated code."
        "Return JSON with:\n"
        "- issues: list of strings\n"
        "- approved: boolean (True if code is correct False if code is not correct)"""
    ),
    output_content_type=ReviewContent
)

# 3️⃣ Reviewer agent — inputs structured code and returns structured ReviewContent
updator = AssistantAgent(
    name="updator",
    model_client=model_client,
    system_message=(
        """You are a skilled software engineer. Update the C# code :{code} based on feedback:{feedback}.
        The updated code must:
            1. **Include all original code**, preserving structure, file headers, and namespace.
            2. Apply all feedback exactly, modifying only where needed.
            3. Not remove or rewrite parts that aren't relevant to the feedback.

        return valid JSON with the key \"code\" containing the complete C# script."
        """
    ),
    output_content_type=CodeContent
)

