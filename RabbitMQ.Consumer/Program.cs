using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

channnel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);


string queeName = channnel.QueueDeclare().QueueName;

channnel.QueueBind(queue: queeName
    , exchange: "direct-exchange-example"
    , routingKey: "direct-quee-example");

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
channnel.BasicConsume(queeName, true, consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    string messeage = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(messeage);
}

Console.Read();