from config.config import get_model_configuration
# from autogen_ext.models.openai import OpenAIChatCompletionClient
from autogen_ext.models.openai import AzureOpenAIChatCompletionClient

# 1️⃣ Set up LLM client
config = get_model_configuration()
model_client = AzureOpenAIChatCompletionClient(
    azure_deployment=config['config']["azure_deployment"],
    azure_endpoint=config['config']["azure_endpoint"],
    api_key=config['config']["api_key"],
    api_version=config['config']["api_version"],
    model=config['config']['model'],
    temperature=config['temperature'],
    max_tokens=4096,
    top_p=0.9,
    top_k=20
  
)
