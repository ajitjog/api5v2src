using Confluent.Kafka;
using Confluent.Kafka.Admin;
using CoreApi5V2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApi5V2.Utilities
{
    public class KafkaUtility
    {
        private static ClientConfig LoadConfig()
        {
            try
            {

                var clientConfig = new ClientConfig();
                clientConfig.Append(new KeyValuePair<string, string>("bootstrap.servers", "mykeycloak.westus.cloudapp.azure.com:9092"));

                return clientConfig;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured reading  {e.Message}");
                System.Environment.Exit(1);
                return null; // avoid not-all-paths-return-value compiler error.
            }
        }

        static async Task CreateTopicMaybe(string name, int numPartitions, short replicationFactor, ClientConfig cloudConfig)
        {
            using (var adminClient = new AdminClientBuilder(cloudConfig).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                        new TopicSpecification { Name = name, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        Console.WriteLine($"An error occured creating topic {name}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine("Topic already exists");
                    }
                }
            }
        }

        public static void Produce(string topic, City c)
        {
            ClientConfig config = LoadConfig();
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                producer.AddBrokers("mykeycloak.westus.cloudapp.azure.com:9092");
                var key = c.CityName;
                var val = JObject.FromObject(new { operation = "read", density = c.CityDensity }).ToString(Formatting.None);

                Console.WriteLine($"Producing record: {key} {val}");

                producer.Produce(topic, new Message<string, string> { Key = key, Value = val },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        }
                    });
                producer.Flush(TimeSpan.FromSeconds(5));
                Console.WriteLine($"Flush done");
            }


        }
    }

}
