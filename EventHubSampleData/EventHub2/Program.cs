using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace SendSampleData
{
    class Program
    {
        const string eventHubName = "test-hub";
        // Copy the connection string ("Connection string-primary key") from your Event Hub namespace.
        const string connectionString = @"<YourConnectionString>";

        static void Main(string[] args)
        {
            EventHubIngestion();
        }

        static void EventHubIngestion()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            int counter = 0;
            for (int i = 0; i < 100; i++)
            {
                int recordsPerMessage = 3;
                try
                {
                    List<string> records = Enumerable
                        .Range(0, recordsPerMessage)
                        .Select(recordNumber => $"{{\"timeStamp\": \"{DateTime.UtcNow.AddSeconds(100 * counter)}\", \"name\": \"{$"name {counter}"}\", \"metric\": {counter + recordNumber}, \"source\": \"EventHubMessage\"}}")
                        .ToList();
                    string recordString = string.Join(Environment.NewLine, records);

                    EventData eventData = new EventData(Encoding.UTF8.GetBytes(recordString));
                    Console.WriteLine($"sending message {counter}");
                    // Optional "dynamic routing" properties for the database, table, and mapping you created. 
                    // eventData.Properties.Add("Table", "TestTable");
                    // eventData.Properties.Add("IngestionMappingReference", "TestMapping");
                    // eventData.Properties.Add("Format", "json");
                    eventHubClient.Send(eventData);
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                counter += recordsPerMessage;

                Thread.Sleep(10000);
            }
        }
    }
}
