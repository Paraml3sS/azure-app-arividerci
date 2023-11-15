using Azure.Messaging.ServiceBus;

const string serviceBusConnectionString = "Endpoint=sb://az-course-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kxnUNhOVYIVsgpd4J9cApOzTyxoVpVyjT+ASbFGk/4o=";

const string queueName = "az-course-bus-queue-1";
const int maxNumOfMessages = 3;

var client = new ServiceBusClient(serviceBusConnectionString);
var sender = client.CreateSender(queueName);

using var batch = await sender.CreateMessageBatchAsync();

for (int i = 0; i < maxNumOfMessages; i++)
{
    if (batch.TryAddMessage(new ServiceBusMessage($"This is a message - {i}")) == false)
    {
        Console.WriteLine($"Message - {i} was not added to the batch");
    }
}

try
{
    await sender.SendMessagesAsync(batch);
    Console.WriteLine("Message sent");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString() );
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}

