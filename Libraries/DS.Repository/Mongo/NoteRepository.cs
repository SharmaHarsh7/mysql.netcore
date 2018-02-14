using DS.Data.Mongo.Entities;
using DS.Framework.Mongo.Repository;

namespace DS.Repository.Mongo
{
    public class NoteRepository
    {

        private readonly IMongoRepository<Note> _noteRepository;

        public NoteRepository(IMongoRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Note Get(string id)
        {
            return _noteRepository.Get(id);
        }
    }
}
