using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class Exam
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Gender { get; set; }
    }
    [ApiController]
    [Route("[Controller]")]
    public class ExaminationController : ControllerBase
    {
        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=testfilestorage4;AccountKey=2ApAdNKqKUb6jOg06CTFKl5PqhuMscY5tf3XJQ/bu+WtnoNxBGxnqYQXd+bM5T4pXWjlZNjvF2Ek3CQkHRJCBw==;EndpointSuffix=core.windows.net";
        private static string containerName = "testcontainer";

        private static readonly string[] Names = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExaminationController> _logger;

        public ExaminationController(ILogger<ExaminationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return DownloadExams();

            //var rng = new Random();
            
            //return Enumerable.Range(1, 30).Select(index => new Exam
            //{
            //    Name = Names[index % (Names.Length - 1)],
            //    Id = index.ToString(),
            //    Gender = (index % 3).ToString()
            //})
            //.ToArray();
        }

        [Route("exams/{id}")]
        public Exam GetExam(string id)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            string filePath = $"exam{id}.json";

            //container.GetBlobClient($"exam{id}.json").DownloadTo(filePath);
            //return JsonSerializer.Deserialize<Exam>(System.IO.File.ReadAllText(filePath));
            var memorystream = new MemoryStream();
            container.GetBlobClient($"exam{id}.json").DownloadTo(memorystream);
            string s = System.Text.UTF8Encoding.UTF8.GetString(memorystream.ToArray());
            return JsonSerializer.Deserialize<Exam>(s);

        }
        private List<string> DownloadExams()
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            var blobItems = container.GetBlobs();

            return blobItems.Select(b => b.Name).ToList();
        }
    }
}
