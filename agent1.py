import asyncio
from autogen_agentchat.agents import AssistantAgent
from autogen_agentchat.teams import RoundRobinGroupChat
from autogen_agentchat.messages import TextMessage
from autogen_ext.models.openai import OpenAIChatCompletionClient

# Model client setup
client = OpenAIChatCompletionClient(model="gpt-4o")

# Coder: generates code based on the test plan and expected outputs
coder = AssistantAgent(
    name="coder",
    model_client=client,
    system_message=(
        "You are a coder. "
        "You receive test steps and expected outputs—"
        "generate clean Python code that satisfies them."
    )
)

# Reviewer: custom prompt that includes placeholders for test plan, expected outputs, and code
reviewer = AssistantAgent(
    name="reviewer",
    model_client=client,
    system_message=(
        "You are a reviewer. Here are your inputs:\n"
        "TEST PLAN:\n{test_plan}\n\n"
        "EXPECTED OUTPUTS:\n{expected}\n\n"
        "GENERATED CODE:\n{code}\n\n"
        "Your goal: Review the code. "
        "Answer in the format:\n"
        "1. List issues (if any).\n"
        "2. If everything looks correct, write APPROVE."
    )
)

# Build simple two-agent RoundRobin flow
team = RoundRobinGroupChat(agents=[coder, reviewer], max_rounds=2)

async def run_workflow(test_plan: str, expected: str):
    combined_prompt = (
        f"Test steps:\n{test_plan}\n\n"
        f"Expected outputs:\n{expected}"
    )
    initial = TextMessage(source="user", content=combined_prompt)
    result = await team.run(task=initial)

    # Extract the two phases
    coder_msg = result.messages[1].content  # coder's code
    reviewer_prompt = reviewer.system_message.format(
        test_plan=test_plan,
        expected=expected,
        code=coder_msg
    )
    # Manually send to reviewer with custom prompt
    review = await reviewer.run([TextMessage(source="system", content=reviewer_prompt)])
    return coder_msg, review.messages[-1].content

async def main():
    test_plan = "1. Reverse 'abc'\n2. Reverse ''"
    expected = "'cba'\n''"
    code, review = await run_workflow(test_plan, expected)
    print("Generated Code:\n", code)
    print("Reviewer Feedback:\n", review)

if __name__ == "__main__":
    asyncio.run(main())
