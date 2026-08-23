using IronGridC2Produser.Servise;
using Microsoft.Extensions.Configuration;
namespace IronGridC2Produser
{
    public class Program
    {
        public static async Task Main(string[] main)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string Connection = configuration["kafka:BootstrapServers"] ?? "localhost:9092";
            string UAVTopic = configuration["kafka:Topics:UAV"] ?? "uav-events";
            string PerimeterSensorTopic = configuration["kafka:Topics:PerimeterSensor"] ?? "perimeterSensor-events";


            var loadDataFromJson = new LoadFromJson();
            var field_reportsList = loadDataFromJson.LoadAssetLiveStatusData("Data/field_reports.json");


            var service = new KafkaProduserServise(Connection);

            await service.EnsureTopicEistsAsync(UAVTopic);
            await service.EnsureTopicEistsAsync(PerimeterSensorTopic);


            foreach (var item in field_reportsList)
            {
                if (item.AssetType == "UAV")
                {
                    await service.SendMessageToTopicAsync(UAVTopic, item);
                }
                else if(item.AssetType == "PerimeterSensor")
                {
                    await service.SendMessageToTopicAsync(PerimeterSensorTopic, item);
                }
            }
        }
    }
}
