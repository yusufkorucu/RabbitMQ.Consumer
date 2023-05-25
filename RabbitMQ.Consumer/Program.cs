using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active Channeş Open

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

channnel.ExchangeDeclare(
    exchange: "topic-exchange-example",
    type: ExchangeType.Topic
    );


Console.Write("Dinlenecek topic formatınız belirtiniz");
string topic = Console.ReadLine();
var queuName = channnel.QueueDeclare().QueueName;

channnel.QueueBind(queue: queuName,
    exchange: "topic-exchange-example",
    routingKey: topic);

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
// autoack true value kuyruktan siler false ise consumerdan onay bekelenecektir

channnel.BasicConsume(queue: queuName, autoAck: true, consumer: consumer);
consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    //response message operation
    //e.Body:quue message data
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    //e.tag uniqe multiple true denirse oncekileride başarılı der false olursa sadece o mesaj
}
Console.Read();