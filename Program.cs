using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class TermSearch
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("startIndex")]
        public int StartIndex { get; set; }
    }

    class Program
    {
        //the http client allows us to send http requests and
        // receive http responses from a resource identified by a url
        private static readonly HttpClient client = new HttpClient();


        //making this async because it will call a new private static async method that returns a task called
        // ProcessRepositories.
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            // this while loop asks for user pokemon until they dont, then break; 
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter term");
                    var Term = Console.ReadLine();

                    if (string.IsNullOrEmpty(Term))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://chroniclingamerica.loc.gov/search/titles/results/?terms=" + Term + "&format=json&page=5");
                    //+ pokemonName.ToLower());
                    // this serializes the HTTP content to a string as an asynchronous operation 
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var term_result = JsonConvert.DeserializeObject<TermSearch>(resultRead);
                    Console.WriteLine("----");
                    Console.WriteLine("Total items " + term_result.TotalItems);
                    Console.WriteLine("Items per page " + term_result.ItemsPerPage);
                    Console.WriteLine("Start index " + term_result.StartIndex);
                    Console.WriteLine("\n----");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error, you put something wrong do it again");
                }

            }



        }
    }
}