using MapperAPI.Database;
using MapperAPI.Models;

namespace MapperAPI.Repositories
{
    public class Mapper : IMapper
    {
        private readonly IDbConnectionFactory _dbFactory;  
        public Mapper(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<IEnumerable<MapperModel>> GetUnMappedEntities(string searchStr)
        {
            string sql = "select * from mapping where isMapped = 0 and isDeleted = 0 and (@str = '' or title like '%'+@str+'%')";
            return await _dbFactory.ExecuteQuery<MapperModel>(sql, new { str = searchStr });
        }
        public async Task<IEnumerable<MapperModel>> GetMappedEntities(string searchStr)
        {
            string sql = "select * from mapping where isMapped = 1 and isDeleted = 0 and (@str = '' or title like '%'+@str+'%')";
            return await _dbFactory.ExecuteQuery<MapperModel>(sql, new {str = searchStr}); 
        }
    }
}
