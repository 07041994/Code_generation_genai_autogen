import logging
import os.path
import sys
from autogen_core import EVENT_LOGGER_NAME
# from config.config import get_configuration
from logging.handlers import TimedRotatingFileHandler
# from opentelemetry.sdk.resources import Resource
# from opentelemetry.sdk.trace import TracerProvider
# from opentelemetry.sdk.trace.export import BatchSpanProcessor
# from opentelemetry.exporter.otlp.proto.http.trace_exporter import OTLPSpanExporter
# from traceai_autogen import AutogenInstrumentor
# # from opentelemetry import trace

# # --- 1. Setup OpenTelemetry ---
# resource = Resource.create({"service.name": "autogen-code-pipeline", "service.version": "1.0.0"})
# provider = TracerProvider(resource=resource)
# provider.add_span_processor(BatchSpanProcessor(OTLPSpanExporter()))
# trace.set_tracer_provider(provider)
# AutogenInstrumentor().instrument(tracer_provider=provider)  # Trace all Autogen internals :contentReference[oaicite:4]{index=4}



def custom_logger():
    # create a logger named fLogger
    logger = logging.getLogger(EVENT_LOGGER_NAME)
    # set logging level
    logger.setLevel(logging.DEBUG)
    log_path = "./application_logs/"
    if not os.path.exists(log_path):
        os.makedirs(log_path)
    log_filename = "fast_app_log.log"

    # create a log handler
    # backupCount=100 means, only latest 100 log files will be retained and older log files will be deleted
    # interval=1 means the log rotation interval is 1
    # when='d' means the rotating interval will be in terms of days
    # so logs will be rotated every 24 hours(1 day) in this example
    # Following are the options for 'when' parameter
    # S - Seconds, M - Minutes, H - Hours, D - Days,
    # midnight - roll over at midnight, W{0-6} - roll over on a certain day; 0 - Monday
    fileHandler = TimedRotatingFileHandler(
        log_path + log_filename, backupCount=10, when='midnight', interval=1)

    # use namer function of the handler to keep the .log extension at the end of the file name
    fileHandler.namer = lambda name: name.replace(".log", "") + ".log"

    # create a log formatter object and assign to the log handler
    logFormatter = logging.Formatter("%(asctime)s - %(levelname)s - %(filename)s:%(funcName)s:%(lineno)d - %(message)s")
    fileHandler.setFormatter(logFormatter)

    # add log handler to logger object
    logger.addHandler(fileHandler)

    stream_handler = logging.StreamHandler(sys.stdout)
    stream_handler.setFormatter(logFormatter)
    logger.addHandler(stream_handler)

    return logger


logging = custom_logger()
