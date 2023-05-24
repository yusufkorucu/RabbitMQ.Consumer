using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Connection
ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://utmrfjnx:5EAFe_eJPnPfK04-bk1qfDp6YVP51MbT@chimpanzee.rmq.cloudamqp.com/utmrfjnx");

//Connection active Channeş Open

using IConnection connection = connectionFactory.CreateConnection();
using IModel channnel = connection.CreateModel();

//Quee oluşurma
channnel.QueueDeclare(queue: "korucu-test-quee", exclusive: false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);

channnel.BasicConsume(queue: "korucu-test-quee", autoAck: false, consumer: consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    //response message operation
    //e.Body:quue message data
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
}
Console.Read();