using Confluent.Kafka;
using IronGridC2Consumer.Data;
using IronGridC2Consumer.Servise;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IronGridC2Consumer
{
    public class Program
    {
        public static async Task Main(string[] main)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var service = new ServiceCollection();
            var conn = configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<IronGridC2DbContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));
            service.AddScoped<KafkaConsumerServise>();
            var serviceProvider = service.BuildServiceProvider();


            using (var scope = serviceProvider.CreateScope())
            {
                var database = scope.ServiceProvider.GetRequiredService<IronGridC2DbContext>();
                database.Database.EnsureCreated();
            }

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
                GroupId = configuration["Kafka:GroupId"] ?? "IronGridC2-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

            string UAVTopic = configuration["kafka:Topics:UAV"] ?? "uav-events";
            string PerimeterSensorTopic = configuration["kafka:Topics:PerimeterSensor"] ?? "perimeterSensor-events";

            consumer.Subscribe(UAVTopic);
            //consumer.Subscribe(PerimeterSensorTopic);

            while (true)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(10));
                if (result == null || result.Message.Value == null)
                {
                    Console.WriteLine("no more messages");
                    break;
                }
                using var scope = serviceProvider.CreateScope();
                var processingService = scope.ServiceProvider.GetRequiredService<KafkaConsumerServise>();
                //if(result.Message.Value == "UAV")
                //{
                if (await processingService.ProcessAssetLiveStatusUAVEventAsync(result.Message.Value))
                {
                    consumer.Commit(result);
                    Console.WriteLine("Message received successfully from topic UAV");
                }
                else
                {
                    Console.WriteLine("Message not received successfully.");
                }
                //}
                
            }
            consumer.Unsubscribe();
            consumer.Subscribe(PerimeterSensorTopic);
            while (true)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(10));
                if (result == null || result.Message.Value == null)
                {
                    Console.WriteLine("no more messages");
                    break;
                }
                using var scope = serviceProvider.CreateScope();
                var processingService = scope.ServiceProvider.GetRequiredService<KafkaConsumerServise>();
                //else if (result.Message.Value == "PerimeterSensor")
                //{
                if (await processingService.ProcessAssetLiveStatusPerimeterSensorEventAsync(result.Message.Value))
                {
                    consumer.Commit(result);
                    Console.WriteLine("Message received successfully from topic PerimeterSensor");
                }
                else
                {
                    Console.WriteLine("Message not received successfully.");
                }
                //}
            }
            consumer.Unsubscribe();
        }
    }
}