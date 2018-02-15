using DS.Data.Mongo;
using DS.Data.Mongo.Entities;
using DS.Framework.Mongo.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/MongoTest")]
    public class MongoTestController : Controller
    {
        private readonly IMongoRepositoryAsync<Note> _noteRepository;

        public MongoTestController(IMongoRepositoryAsync<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var data = _noteRepository.Get();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _noteRepository.GetAsync(id);
            return Ok(data);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]Note note)
        {
             var a= _noteRepository.Queryable().Find(x => true).FirstOrDefault();



            
            var data = _noteRepository.Insert(note);
            return Ok(data);
        }
    }
}