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
channnel.QueueDeclare(queue: "korucu-test-quee", exclusive: false,durable:true);

EventingBasicConsumer consumer = new EventingBasicConsumer(channnel);
// autoack true value kuyruktan siler false ise consumerdan onay bekelenecektir

channnel.BasicConsume(queue: "korucu-test-quee", autoAck: false, consumer: consumer);
channnel.BasicQos(0, 1, false);
consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    //response message operation
    //e.Body:quue message data
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    //e.tag uniqe multiple true denirse oncekileride başarılı der false olursa sadece o mesaj
    channnel.BasicAck(e.DeliveryTag, multiple: false);
}
Console.Read();