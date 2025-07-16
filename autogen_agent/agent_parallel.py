# import asyncio
from autogen_agentchat.messages import StructuredMessage, TextMessage

from autogen_agentchat.teams import RoundRobinGroupChat
from data_model.prompt_model import Message,CodeContent,ReviewContent,promptt
from autogen_agent.agents1 import coder_agent,reviewer_agent,updator_agent
from utils.logger import logging
from prompt.prompt import chunk_prompt_fun,merge_prompt_fun
import asyncio
from data_model.prompt_model import Message


def chunk_steps(steps: list[str], size: int = 10) -> list[list[str]]:
    return [steps[i:i+size] for i in range(0, len(steps), size)]

async def generate_chunks_parallel(message: Message, steps: list[str]):
    chunks = chunk_steps(steps, size=10)

    async def run_chunk(idx: int, chunk: list[str]):
        prompt = (
            f"{message.main_prompt}\nSteps chunk {idx}/{len(chunks)}:\n" +
            "\n".join(f"{n+1}. {step}" for n, step in enumerate(chunk))
        )
        resp = await coder_agent.run(task = TextMessage(source="user", content=prompt))
        code_msg = next(m for m in resp.messages if isinstance(m.content, CodeContent))
        return code_msg.content.code

    results = await asyncio.gather(*(run_chunk(i+1, ch) for i,ch in enumerate(chunks)))
    return "\n\n".join(results)

team = RoundRobinGroupChat([coder_agent, reviewer_agent, updator_agent], max_turns=3, custom_message_types=[
        StructuredMessage[CodeContent], 
        StructuredMessage[ReviewContent]
    ])

async def merge_and_review(full_code: str):
    merge_prompt = (
        f"Merged code:\n```csharp\n{full_code}```\n"
        "Merge into two files (PageObject.cs & TestSteps.cs) and return JSON with keys 'page', 'test'."
    )
    merge_resp = await team.run(task=TextMessage(source="user", content=merge_prompt))
    logging.info(f'merging response {merge_resp}')
    merged = next(m for m in merge_resp.messages if isinstance(m.content, CodeContent))
    return merged.content.code


async def pipeline(message:Message, steps: list[str]):
    full_code = await generate_chunks_parallel(message, steps)
    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output_full.txt', "w", encoding="utf-8") as f:
        f.write(full_code)
    merged_code = await merge_and_review(full_code)
    logging.info(f"merged code {merged_code}")
    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output.txt', "w", encoding="utf-8") as f:
        f.write(merged_code)

    return merged_code
