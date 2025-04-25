from azure.eventhub import EventData

# Load raw AMQP message bytes saved by .NET
with open("amqp_message.bin", "rb") as f:
    raw_amqp_bytes = f.read()

# Recreate EventData from raw bytes
# Enqueued time will fail to load here:
event_data = EventData.from_bytes(raw_amqp_bytes)

print("Body:", event_data.body_as_str(encoding='UTF-8'))

if event_data.properties:
    print("Properties:", event_data.properties)


if event_data.system_properties:
    print("System Properties:", event_data.system_properties)
