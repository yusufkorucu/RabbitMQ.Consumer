using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active Channeş Open

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

#region P2P (Point to Point)

//string queueName = "example-p2p-queue";

//channnel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);


//EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
//channnel.BasicConsume(queue: queueName,
//   autoAck: false,
//   consumer: consumer);

//consumer.Received += Consumer_Received;

//void Consumer_Received(object? sender, BasicDeliverEventArgs e)
//{
//   Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//}

#endregion

#region Publish/Subscribe (pub/sub)

//string exchangeName = "example-pub-sub-exchange";
//channnel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//string queueName = channnel.QueueDeclare().QueueName;


//channnel.QueueBind(
//    queue: queueName,
//    exchange: exchangeName,
//    routingKey: string.Empty);

////channnel.BasicQos(prefetchCount: 1,
////    prefetchSize: 0,
////    global: false);


//EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
//channnel.BasicConsume(queue: queueName,
//    autoAck: false, consumer);
//consumer.Received += Consumer_Received;

//void Consumer_Received(object? sender, BasicDeliverEventArgs e)
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//}

#endregion

#region Work Quee(İş kuyruğu)
//string queueName = "example-work-queue";

//channnel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false
//    );


//EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
//channnel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer: consumer
//    );

//channnel.BasicQos(
//    prefetchCount: 1,
//    prefetchSize: 0,
//    global: false
//    );

//consumer.Received += Consumer_Received;

//void Consumer_Received(object? sender, BasicDeliverEventArgs e)
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//}

#endregion

#region Request Response 

string requestQueueName = "example-request-response-queue";

channnel.QueueDeclare(queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);

channnel.BasicConsume(queue: requestQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);

    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem Tamamlandı {message}");

    IBasicProperties properties = channnel.CreateBasicProperties();

    properties.CorrelationId = e.BasicProperties.CorrelationId;

    channnel.BasicPublish(
        exchange: string.Empty,
        routingKey: e.BasicProperties.ReplyTo,
        basicProperties: properties,
        body: responseMessage
        );

}

#endregion

Console.Read();