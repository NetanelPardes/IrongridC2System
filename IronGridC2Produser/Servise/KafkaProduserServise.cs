using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IronGridC2Produser.Servise
{
    public class KafkaProduserServise
    {

        private readonly string _bootstrapServvers;
        private readonly IProducer<Null, string> _producer;

        public KafkaProduserServise(string bootstrapServvers)
        {
            _bootstrapServvers = bootstrapServvers;
            var config = new ProducerConfig { BootstrapServers = _bootstrapServvers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task EnsureTopicEistsAsync(string topic, int numPartitions = 1, short replicationFactor = 1)
        {
            var config = new AdminClientConfig { BootstrapServers = _bootstrapServvers };
            using var adminClient = new AdminClientBuilder(config).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new[] { new TopicSpecification { Name = topic, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
            }
            catch (CreateTopicsException e)
            {
                if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
                {
                    Console.WriteLine("this topic already exist");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<DeliveryResult<Null, string>> SendMessageToTopicAsync<T>(string topic, T message)
        {
            string json = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<Null, string>
            {
                Value = json
            };
            var result = await _producer.ProduceAsync(topic, kafkaMessage);
            Console.WriteLine($"The message sent to the topic {topic}");
            return result;
        }
    }
}
