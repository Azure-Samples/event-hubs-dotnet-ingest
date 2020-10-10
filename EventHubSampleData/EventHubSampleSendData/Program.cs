using Azure.Messaging.EventHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Producer;

namespace SendSampleData
{
    class Program
    {
        const string eventHubName = "test-hub";
        const string connectionString = @"<YourConnectionString>";

        public static async Task Main(string[] args)
        {
            await EventHubIngestionAsync();
        }

        public static async Task EventHubIngestionAsync()
        {
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                int counter = 0;
                for (int i = 0; i < 100; i++)
                {
                    int recordsPerMessage = 3;
                    try
                    {
                        var records = Enumerable
                            .Range(0, recordsPerMessage)
                            .Select(recordNumber => $"{{\"timeStamp\": \"{DateTime.UtcNow.AddSeconds(100 * counter)}\", \"name\": \"{$"name {counter}"}\", \"metric\": {counter + recordNumber}, \"source\": \"EventHubMessage\"}}");
                        
                        string recordString = string.Join(Environment.NewLine, records);

                        EventData eventData = new EventData(Encoding.UTF8.GetBytes(recordString));
                        Console.WriteLine($"sending message {counter}");
                        // Optional "dynamic routing" properties for the database, table, and mapping you created. 
                        eventData.Properties.Add("Table", "TestTable");
                        eventData.Properties.Add("IngestionMappingReference", "TestMapping");
                        eventData.Properties.Add("Format", "json");

                        using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                        eventBatch.TryAdd(eventData);

                        await producerClient.SendAsync(eventBatch);
                    }
                    catch (Exception exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                        Console.ResetColor();
                    }

                    counter += recordsPerMessage;
                }
            }
        }
    }
}
