using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private const string connectionString = "<connection string>";
    private const string eventHubName = "<hub>";
    private const string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

    static async Task Main()
    {
        await using var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

        await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync())
        {
            EventData eventData = partitionEvent.Data;

            ReadOnlyMemory<byte> rawAmqpBytes = eventData.GetRawAmqpMessage().ToBytes();
            
            await File.WriteAllBytesAsync("amqp_message.bin", rawAmqpBytes.ToArray());

            Console.WriteLine("Raw AMQP message bytes saved.");
            break; // Exit after first message here for the repro...
        }
    }
}
