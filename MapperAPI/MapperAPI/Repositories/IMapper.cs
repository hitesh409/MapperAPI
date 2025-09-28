using MapperAPI.Models;

namespace MapperAPI.Repositories
{
    public interface IMapper
    {
        Task<IEnumerable<MapperModel>> GetUnMappedEntities(string searchStr); 
        Task<IEnumerable<MapperModel>> GetMappedEntities(string searchStr);
        Task<int> UpdateMappingStatus(string unmappedIds, string mappedIds, string userId);
    }
}
