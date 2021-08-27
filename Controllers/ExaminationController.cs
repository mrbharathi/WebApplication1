using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Exam> Get()
        {
            var rng = new Random();
            
            return Enumerable.Range(1, 30).Select(index => new Exam
            {
                Name = Names[index % (Names.Length - 1)],
                Id = index.ToString(),
                Gender = (index % 3).ToString()
            })
            .ToArray();
        }
    }
}
