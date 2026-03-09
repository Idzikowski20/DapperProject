using DapperProject.Models;

namespace DapperProject.Repositories
{
    public interface IVideoGameRepository
    {
        Task<IReadOnlyList<VideoGame>> GetAllAsync();
        Task<VideoGame> GetByIdAsync(int id);
        Task AddAsync(VideoGame videoGame);
        Task UpdateAsync(VideoGame videoGame);
        Task DeleteAsync(int id);
    }
}
