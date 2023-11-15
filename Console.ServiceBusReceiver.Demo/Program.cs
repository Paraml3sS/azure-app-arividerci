using Azure.Messaging.ServiceBus;

const string serviceBusConnectionString = "Endpoint=sb://az-course-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kxnUNhOVYIVsgpd4J9cApOzTyxoVpVyjT+ASbFGk/4o=";

const string queueName = "az-course-bus-queue-1";

var client = new ServiceBusClient(serviceBusConnectionString);
var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ExceptionHandler;

    await processor.StartProcessingAsync();
    Console.WriteLine("Press any key to end the processing");
    Console.ReadKey();

    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");

    Console.WriteLine("Message sent");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    await processor.DisposeAsync();
    await client.DisposeAsync();
}





async Task MessageHandler(ProcessMessageEventArgs processMessageEventArgs)
{
    var body = processMessageEventArgs.Message.Body.ToString();
    Console.WriteLine(body);
    await processMessageEventArgs.CompleteMessageAsync(processMessageEventArgs.Message);
}

Task ExceptionHandler(ProcessErrorEventArgs processMessageEventArgs)
{
    Console.WriteLine(processMessageEventArgs.Exception.ToString());
    return Task.CompletedTask;
}