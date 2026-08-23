using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
    }
}
