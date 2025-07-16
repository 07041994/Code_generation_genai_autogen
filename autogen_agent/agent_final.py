# import asyncio
from autogen_agentchat.messages import StructuredMessage, TextMessage

from autogen_agentchat.teams import RoundRobinGroupChat
from data_model.prompt_model import Message,CodeContent,ReviewContent,promptt
from autogen_agent.agents import coder,reviewer,updator
from utils.logger import logging
from prompt.prompt import chunk_prompt_fun,merge_prompt_fun

# 4️⃣ Orchestration: coder → reviewer > updator
team = RoundRobinGroupChat([reviewer,updator], max_turns=2,    
                           custom_message_types=[
        StructuredMessage[CodeContent], 
        StructuredMessage[ReviewContent]
    ])


# async def run_pipeline(message:Message):
async def run_pipeline(message:Message,chunks:list):
    full_code = ""
    
    
    for idx, chunk in enumerate(chunks, start=1):
        f"Steps chunk ({idx} of {len(chunks)}):\n"
        # chunk_prompt = chunk_prompt_fun(message,chunks,full_code)
        # Craft integrated prompt
        chunk_prompt = (
            f"{message.main_prompt}\n"
            f"Steps chunk ({idx} of {len(chunks)}):\n" +
            "\n".join(f"{n+1}. {step}" for n, step in enumerate(chunk)) +
            f"\n\n{message.prompt_detos}\n\n{message.prompt_generichelper}\n\n{message.prompt_mandatory}"
            f"\n\nPrevious code so far:\n```csharp\n{full_code}```\n\n"
            "Generate ONLY the C# code for these steps using Page Object methods, Console.WriteLine(), JSON data. "
            "No skipping, no summaries."
        )    
        # a) Run coder
        initial = TextMessage(
            source="user",
            # content=prompt
            content=chunk_prompt
        )
        logging.info(f'chunk_prompt {chunk_prompt}')
        history = await coder.run(task=initial)
        logging.info(f' initial : {initial}')
        # b) Extract structured code result
        code_msg = history.messages[1]  # StructuredMessage[CodeContent]
        code_struct: CodeContent = code_msg.content
        full_code += code_struct.code + "\n\n"
        logging.info(f"Chunk output {idx} generated with {code_struct.code} characters.")

        # # merge_prompt = merge_prompt_fun(message,full_code)
        # merge_prompt = (
        #     f"{message.main_prompt}\n"
        #     "Below is the FULL GENERATED CODE from all previous chunks (clearly delimited):\n\n"
        #     f"{full_code}\n"
        #     "⚙️ Now perform the following:\n"
        #     " 1. merge the code in respective class and Consolidate into exactly **two files**:\n"
        #     "   • `PageObject.cs`: definitions and methods\n"
        #     "   • `TestSteps.cs`: setup, Console.WriteLine per step, and test execution logic\n"
        #     "2. Remove duplicates and correct inconsistencies.\n"
        #     "3. Validate the combined code for syntax issues and completeness.\n"
        
        # "Return result as CodeContent."
        #     )

    # team_result = await team.run(task=TextMessage(source="user", content=merge_prompt))
    # final_msg = next(m for m in team_result.messages if isinstance(m.content, CodeContent))
    # team_result.messages[1].conten
    


    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output_full.txt', "w", encoding="utf-8") as f:
        f.write(full_code)

    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output.txt', "w", encoding="utf-8") as f:
        f.write(code_struct.code)
    # return final_msg
    
    user_message =  message.prompt_validation.format(steps = message.steps_list,
                                         expected_output = message.expected_outputs,
                                         code1=code_struct.code)
    Reviewer_task = TextMessage(
        source="user",
        # content=prompt
        content=user_message
    )
    # team_result = await team.run(task=TextMessage(source="user", content=user_message))
    # logging.info(f'team_result {team_result}')
    # review_msg: StructuredMessage[ReviewContent] = team_result
    # review_struct: ReviewContent = review_msg.content
    # final_msg = next(m for m in team_result.messages if isinstance(m.content, CodeContent))

    review_history = await reviewer.run(task=Reviewer_task)
    review_msg: StructuredMessage[ReviewContent] = review_history.messages[1]
    review_struct: ReviewContent = review_msg.content
    
    if review_struct.approved == True:
         # return code_struct.code, review_struct
        return code_struct.code
    else:
        logging.info(f'system_message : {updator._system_messages[0].content}')

        updator_system_msg = updator._system_messages[0].content.format(
        code = code_struct.code,
        feedback = review_struct.issues,
            )
        logging.info(f'system_message_with_value : {updator_system_msg}')
        updator_history = await updator.run(
        task=TextMessage(source="system", content=updator_system_msg)
        )  
        logging.info(f'system_message_with_output : {updator_history}')
        update_msg = updator_history.messages[1]  # StructuredMessage[CodeContent]
        update_struct: CodeContent = update_msg.content
        with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/u_output.txt', "w", encoding="utf-8") as f:
            f.write(update_struct.code)
        return update_struct.code

# async def main(prompt):
#     # test_plan = "1. Reverse 'abc'\n2. Reverse ''"
#     # expected = "'cba'\n''"
#     # code, review = await run_pipeline(prompt)
#     code = await run_pipeline(prompt)
#     print("💻 Code:\n", code)

#     # print("🧠 Review:", review)

#     # if not review.approved:
#     #     print("⚠ Issues:", review.issues)
# from output.dubug_propmt import prompt

# if __name__ == "__main__":
#     asyncio.run(main(promptt(prompt = prompt+" \n return JSON: {\"code\": \"...\"}.")))
