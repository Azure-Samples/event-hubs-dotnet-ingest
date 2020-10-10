---
page_type: sample
languages:
- csharp
products:
- azure
description: "This sample shows you how to send basic messages to an event hub."
urlFragment: event-hubs-dotnet-ingest
---

# Event hub message sample

This sample shows you how to send basic messages to an event hub, which allows you to test consumption of those messages by other applications that connect to the event hub.

# How to run this sample

If you don't have an Azure subscription, create a [free account] before you begin.

1.  Go to the [Azure Portal] and log in using your Azure account.

2. Clone the Repo.
    ```bash
    git clone https://github.com/Azure-Samples/event-hubs-dotnet-ingest.git
    ```

3. Get to SendSampleData.csproj in EventHubSampleSendData.
    ```bash
    cd event-hubs-dotnet-ingest\EventHubSampleData\EventHubSampleSendData
    ```

4. Open the solution in Visual Studio, filled in the eventHub's connection string. You can get help with [how to get eventHub's connection string].

5. Press F5 to run the project.


<!-- LINKS -->
[free account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[Azure Portal]: https://portal.azure.com
[how to get eventHub's connection string]: https://docs.microsoft.com/azure/event-hubs/event-hubs-get-connection-string#get-connection-string-from-the-portal