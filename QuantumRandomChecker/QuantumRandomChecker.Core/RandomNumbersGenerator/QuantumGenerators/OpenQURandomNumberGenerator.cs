using QuantumRandomChecker.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuantumRandomChecker.Core.RandomNumbersGenerator.QuantumGenerators
{
    public class OpenQURandomNumberGenerator : NumberGenerator
    {
        private readonly HttpClient _httpClient;

        public OpenQURandomNumberGenerator()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://random.openqu.org/api/")
            };
        }

        public override async Task<List<double>> GetRandomNumbers(int count)
        {
            List<double> randomNumbers = new List<double>();

            int batchSize = 1024;
            int batches = (int)Math.Ceiling((double)count / batchSize);

            for (int i = 0; i < batches; i++)
            {
                int numbersInBatch = Math.Min(count - i * batchSize, batchSize);
                int[] batch = await GetIntegers(0, 255, numbersInBatch);
                randomNumbers.AddRange(batch.Select(x => x / 255.0));
            }

            return randomNumbers;
        }

        public async Task<int[]> GetIntegers(int min, int max, int size)
        {
            var url = $"randint?size={size}&min={min}&max={max}";
            var stream = await _httpClient.GetStreamAsync(url);

            using (var document = await JsonDocument.ParseAsync(stream))
            {
                return document
                    .RootElement
                    .GetProperty("result")
                    .EnumerateArray()
                    .Select(x => x.GetInt32())
                    .ToArray();
            }
        }
    }
}
