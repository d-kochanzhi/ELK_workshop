using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;



namespace elastic_dotnet_client
{
    internal class Program
    {
        public class Person { 
        
            public int id { get; set; }

            public string personname { get; set; }    

            public string personlastname { get; set; }    

            public string adress { get; set; }  

        }

        static void Main(string[] args)
        {

            Console.WriteLine("Start searching [*етр*]..");

            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            var response = client.Search<Person>(s => s
                .Index("i_sql_ts") 
                .From(0)
                .Size(10)
                .Query(q => q
                    .QueryString(qs=>qs
                        .Fields(f=>f.Fields("personname^3", "personlastname^2", "adress"))
                        .Query("*етр*"))
                    
                )
            );

            Console.WriteLine($"found records {response.Total}");

            foreach (var e in response.Hits)
            {
                Console.WriteLine($"found id={e.Source.id}; name={e.Source.personname}; lastname={e.Source.personlastname}; adress={e.Source.adress}");
            }

            Console.ReadLine();
        }
    }
}
