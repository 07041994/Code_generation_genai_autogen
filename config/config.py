import yaml


def get_configuration(config_file_path='config/config.yaml'):
    # Open and read the contents of the config.json file
    with open(config_file_path, 'r') as file:
        config = yaml.load(file, Loader=yaml.FullLoader)
    return config


def get_model_configuration(config_file_path='config/model_config.yaml'):
    # Open and read the contents of the config.json file
    with open(config_file_path, 'r') as file:
        config = yaml.load(file, Loader=yaml.FullLoader)
    return config