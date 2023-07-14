using Accord.Math.Random;
using Newtonsoft.Json;
using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.QuantumGenerators
{
    public class QuantumRandomNumberGenerator : NumberGenerator
    {
        private const int BatchSize = 1024;

        public override async Task<List<double>> GetRandomNumbers(int count)
        {
            List<double> randomNumbers = new List<double>();

            int batches = (int)Math.Ceiling((double)count / BatchSize);

            for (int i = 0; i < batches; i++)
            {
                int numbersInBatch = Math.Min(count - i * BatchSize, BatchSize);
                int[] batch = await GetQuantumRandomNumbers(numbersInBatch);
                randomNumbers.AddRange(batch.Select(x => x / 255.0));
            }

            return randomNumbers;
        }

        public async Task<int[]> GetQuantumRandomNumbers(int count)
        {
            using (var httpClient = new HttpClient())
            {
                string url = $"https://qrng.anu.edu.au/API/jsonI.php?length={count}&type=uint8";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(content);
                    if (jsonResponse.data != null)
                    {
                        int[] randomNumbers = jsonResponse.data.ToObject<int[]>();
                        return randomNumbers;
                    }
                }
            }

            return new int[0];
        }
    }
}
