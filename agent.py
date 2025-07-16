from autogen_agentchat.agents import AssistantAgent, UserProxyAgent
from autogen_agentchat.teams import RoundRobinGroupChat
from config.config import get_model_configuration
from autogen_core.models import ChatCompletionClient, SystemMessage, UserMessage
from prompt_model import Message
from autogen.prompt.prompt import result_format
from autogen_ext.models.openai import AzureOpenAIChatCompletionClient
from prompt_model import AgentResponse
from autogen_core import (
    RoutedAgent, message_handler,
    SingleThreadedAgentRuntime, AgentId, message_handler,MessageContext,TypeSubscription,
    type_subscription, TopicId,
    EVENT_LOGGER_NAME
)
from utils.logger import logging
from dataclasses import dataclass


config = get_model_configuration()


model_client = AzureOpenAIChatCompletionClient(
    azure_deployment=config['config']["azure_deployment"],
    azure_endpoint=config['config']["azure_endpoint"],
    api_key=config['config']["api_key"],
    api_version=config['config']["api_version"],
    model=config['config']['model'],
    temperature=config['temperature'],
    max_tokens=4096
    # response_format=AgentResponse
)


@dataclass
class CodeRequest:
    prompt: str

class BaseAgent(RoutedAgent):
    def __init__(self, name: str,sys_prompt: str,model_client: ChatCompletionClient):
        super().__init__(name)
        self.system_prompt = sys_prompt
        # self.logger = __import__('logging').getLogger(EVENT_LOGGER_NAME)
        # self.logger.setLevel(__import__('logging').INFO)
        self.model_client = model_client
        # self.name =name

    @message_handler
    async def handle(self, message:CodeRequest, ctx: MessageContext) -> AgentResponse:
        sys_msg = SystemMessage(content=self.system_prompt, source="system")
        usr_msg = UserMessage(content=message.prompt, source="user")
        resp = await self.model_client.create([sys_msg,usr_msg],
                                         cancellation_token=ctx.cancellation_token,
                                            
                                         json_output=AgentResponse)
        # logging.info(f'{self.id.type}: {resp.content}')
        return AgentResponse.model_validate_json(resp.content)


# --- 4. Agents with type-based subscriptions ---
@type_subscription(topic_type="generate")
class CodeGenerator(BaseAgent):
    def __init__(self): 
        super().__init__("code_generator","You are generating long, complete C# automation scripts based on detailed test plans. Always generate the full code, never partial.",model_client)

@type_subscription(topic_type="validate")
class CodeValidator(BaseAgent):
    def __init__(self): 
        super().__init__("code_validator", "You are an Expert Software Engineer. Validate code and return JSON."+result_format,model_client)

@type_subscription(topic_type="update")
class CodeUpdater(BaseAgent):
    def __init__(self): 
        super().__init__("code_updater", "You are an Expert Software Engineer. Update code based on feedback JSON."+result_format,model_client)

@type_subscription(topic_type="merge")
class CodeMerger(BaseAgent):
    def __init__(self): 
        super().__init__("code_merger", "You are an Expert Software Engineer. Merge dependencies and return JSON."+result_format,model_client)


# --- 5. Orchestrator using topics & retries ---
async def run_pipeline( message:Message, prompt: str, retries: int = 1):
    # TOPIC = TopicId(type="session", source="session")
    rt = SingleThreadedAgentRuntime()
    for cls in [CodeGenerator,CodeValidator,CodeUpdater, CodeMerger]:
        await cls.register(rt, cls.__name__, lambda cls=cls: cls())
        await rt.add_subscription(TypeSubscription(topic_type=cls.__name__.lower(),
                                                    agent_type=cls.__name__))
    rt.start()

    # 2. Publish initial prompt
    # await rt.publish_message(CodeRequest(prompt), TopicId('codegenerator','session'))
    # await rt.stop_when_idle()
    # 1️⃣ Generate
    await rt.publish_message(CodeRequest(prompt+'generate code for all steps  strictly. do not give any excuse.'), topic_id=TopicId(type="generate", source="session"))
    gen = await rt.send_message(CodeRequest(prompt+'generate code for all steps  strictly. do not give any excuse.'), AgentId("CodeGenerator", 'session'))
    code = gen.code
    logging.info(f'code_generator {code}')
    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/dubug_propmt.txt', "w", encoding="utf-8") as f:
        f.write(prompt)
    with open('C:/Users/AmanKumar/Documents/GitHub/chetan-genai/autogen/output/output.txt', "w") as f:
        f.write(code)

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

    await rt.stop_when_idle()
    await rt.close()
    return code
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


