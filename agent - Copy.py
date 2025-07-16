# 🧩 agent_pipeline.py

import asyncio
from dataclasses import dataclass
from autogen_ext.models.openai import AzureOpenAIChatCompletionClient
from config.config import get_model_configuration
from autogen_core.models import SystemMessage, UserMessage, ChatCompletionClient
from autogen_ext.models.openai import AzureOpenAIChatCompletionClient
from autogen_core import (
    RoutedAgent, message_handler,
    ClosureAgent, ClosureContext,
    DefaultSubscription, DefaultTopicId,
    SingleThreadedAgentRuntime, MessageContext
)

# 💡 Config and model client setup
config = get_model_configuration()
model_client = AzureOpenAIChatCompletionClient(
    azure_deployment=config['config']["azure_deployment"],
    azure_endpoint=config['config']["azure_endpoint"],
    api_key=config['config']["api_key"],
    api_version=config['config']["api_version"],
    model=config['config']['model'],
    temperature=config['temperature'],
    max_tokens=4096,
)

# 🎯 Data models
@dataclass
class CodeRequest:
    prompt: str

@dataclass
class FinalResult:
    content: str

# 🛠 Code Generation Agent
class CodeGenerationAgent(RoutedAgent):
    def __init__(self):
        super().__init__("code_generation_agent")
        self.system_prompt = "You are a code generator."
        self.model = model_client

    @message_handler
    async def handle(self, message: CodeRequest, ctx: MessageContext) -> None:
        sys_msg = SystemMessage(content=self.system_prompt, source="system")
        usr_msg = UserMessage(content=message.prompt, source="user")
        resp = await self.model.create(
            [sys_msg, usr_msg],
            cancellation_token=ctx.cancellation_token
        )
        await self.publish_message(
            FinalResult(content=resp.content),
            DefaultTopicId(),
            cancellation_token=ctx.cancellation_token
        )
        # return FinalResult(content=resp.content)
        return None
# 🧠 Runner and output capture
async def run_and_save(prompt: str, out_file: str) -> str:
    queue: asyncio.Queue[FinalResult] = asyncio.Queue()

    async def capture(_ctx: ClosureContext, msg: FinalResult, ctx: MessageContext):
        await queue.put(msg)

    rt = SingleThreadedAgentRuntime()
    await CodeGenerationAgent.register(rt, "code_gen", lambda: CodeGenerationAgent())
    await ClosureAgent.register_closure(
        rt, "capture_output", capture,
        subscriptions=lambda: [DefaultSubscription()]
    )

    rt.start()
    await rt.publish_message(CodeRequest(prompt=prompt), DefaultTopicId())
    await rt.stop_when_idle()
    await model_client.close()

    final: FinalResult = await queue.get()
    with open(out_file, "w", encoding="utf-8") as f:
        f.write(final.content)
    return final.content
from output.dubug_propmt import prompt

if __name__ == "__main__":
    # prompt = "Write a Python function to reverse a string."
    output = asyncio.run(run_and_save(prompt, "output.txt"))
    print("✅ Output saved:", output[:60], "...")
    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output.txt', "w", encoding="utf-8") as f:
        f.write(output)

    # return final.content
# if __name__ == "__main__":
#     result = asyncio.run(run_and_save(prompt))
#     print("Saved output:", result)

# # --- 5. Orchestrator using topics & retries ---
# async def run_pipeline( message:Message, prompt: str, retries: int = 1):
#     # TOPIC = TopicId(type="session", source="session")
#     rt = SingleThreadedAgentRuntime()
#     await CodeGenratinAgent.register( rt,type='generate',factory=lambda:CodeGenratinAgent(model_client=model_client))
#     # for cls in [CodeGenerator,CodeValidator,CodeUpdater, CodeMerger]:
#         # await cls.register(rt, cls.__name__, lambda cls=cls: cls())
#         # await rt.add_subscription(TypeSubscription(topic_type=cls.__name__.lower(),
#                                                     # agent_type=cls.__name__))
#     rt.start()
#     rt.publish_message(message.full_prompt,topic_id=TopicId('generate',source='user'))
#     # gen = await rt.send_message(message.full_prompt, AgentId("CodeGeneratorAgent", 'session'))
#     await rt.stop_when_idle()
#     await model_client.close()
    # 2. Publish initial prompt
    # await rt.publish_message(CodeRequest(prompt), TopicId('codegenerator','session'))
    # await rt.stop_when_idle()
    # 1️⃣ Generate
    # await rt.publish_message(CodeRequest(prompt+'generate code for all steps  strictly. do not give any excuse.'), topic_id=TopicId(type="generate", source="session"))
    # gen = await rt.send_message(CodeRequest(prompt+'generate code for all steps  strictly. do not give any excuse.'), AgentId("CodeGenerator", 'session'))
    # code = gen.code
    # logging.info(f'code_generator {code}')
    # with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/dubug_propmt.txt', "w", encoding="utf-8") as f:
    #     f.write(prompt)
    # with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output.txt', "w") as f:
    #     f.write(code)

    # 2️⃣ Validate + retry/updating
    
    # validation_prompt = message.prompt_validation.format(steps = message.steps_list,
    #                                      expected_output = message.expected_outputs,
    #                                      code1=code)
    # for i in range(retries + 1):
    # await rt.publish_message(CodeRequest(prompt= validation_prompt), TopicId(type="validate", source="session"))
    # val = await rt.send_message(CodeRequest(prompt= validation_prompt), AgentId("CodeValidator", 'session'))
    # if val.status.upper() == "OK":
    #     break
    
    # await rt.publish_message(CodeRequest(prompt= f"{code}\n\nFeedback:\n{val.feedback}"), TopicId(type="update", source="session"))
    # code = (await rt.send_message(
    #     CodeRequest(prompt = f"{code}\n\nFeedback:\n{val.feedback}"),
    #     AgentId("CodeUpdater", 'session')
    #         )).code
    # else:
    #     raise RuntimeError(f"Validation failed: {val.feedback}")

    # 3️⃣ Merge final code
    # merging_prompt = message.prompt_merging.format(old_f=message.old_f,new_f=code)
    # await rt.publish_message(CodeRequest(merging_prompt), TopicId(type="merge", source="session"))
    # merged = await rt.send_message(CodeRequest(merging_prompt), AgentId("CodeMerger",'session'))

    # await rt.stop_when_idle()
    # await rt.close()
    # return code
    # return merged.code


# # --- 5. Orchestrator using topics & retries ---
# async def run_pipeline(prompt_data, prompt: str, retries: int = 2):
#     rt = SingleThreadedAgentRuntime()
#     for cls in [CodeGenerator, CodeValidator, CodeUpdater, CodeMerger]:
#         await cls.register(rt, cls.__name__, lambda cls=cls: cls())
#         rt.add_subscription(TypeSubscription(topic_type=cls.__name__.lower(), agent_type=cls.__name__))
#     rt.start()

#     # 1️⃣ Generate
#     await rt.publish_message(prompt_data(prompt), topic_id=TopicId(type="generate", source="session"))
#     gen = await rt.send_message(prompt_data(prompt), AgentId("CodeGenerator", "session"))

#     # 2️⃣ Validate + retry/updating
#     code = gen.code
#     validation_code = prompt_data.prompt_validation.format(steps = prompt_data.steps_list,
#                                          expected_output = prompt_data.expected_outputs,
#                                          code1=code)
#     for i in range(retries + 1):
#         await rt.publish_message(prompt_data(validation_code), TopicId(type="validate", source="session"))
#         val = await rt.send_message(prompt_data(validation_code), AgentId("CodeValidator", "session"))
#         if val.status.upper() == "OK":
#             break
#         code = (await rt.send_message(
#             prompt_data(f"{code}\n\nFeedback:\n{val.feedback}"),
#             AgentId("CodeUpdater", "session")
#         )).code
#     else:
#         raise RuntimeError(f"Validation failed: {val.feedback}")

#     # 3️⃣ Merge final code
#     await rt.publish_message(prompt_data(code), TopicId(type="merge", source="session"))
#     merged = await rt.send_message(prompt_data(code), AgentId("CodeMerger", "session"))

#     await rt.stop_when_idle()
#     await rt.close()
#     return merged.code


