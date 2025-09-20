using MapperAPI.Models;

namespace MapperAPI.Repositories
{
    public interface IMapper
    {
        Task<IEnumerable<MapperModel>> GetUnMappedEntities(string searchStr); 
        Task<IEnumerable<MapperModel>> GetMappedEntities(string searchStr); 
    }
}
