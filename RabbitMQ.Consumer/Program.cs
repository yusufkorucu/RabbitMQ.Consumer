using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active Channeş Open

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

channnel.ExchangeDeclare(exchange: "fanout-excahnge-example", type: ExchangeType.Fanout);

Console.Write("Kuyruk adı gir");
string queeName = Console.ReadLine();

channnel.QueueDeclare(
    queue: queeName,
    exclusive: false
    );

channnel.QueueBind(

    queue: queeName,
    exchange: "fanout-excahnge-example",
    routingKey:string.Empty
    );

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);

channnel.BasicConsume
    (queue:queeName,
    autoAck:true,
    consumer:consumer);

consumer.Received += (sernder, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();