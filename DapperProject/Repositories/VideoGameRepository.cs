using Dapper;
using DapperProject.Factory;
using DapperProject.Models;
using Microsoft.Data.SqlClient;

namespace DapperProject.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {

        private readonly IDbConnectionFactory _connectionFactory;
        public VideoGameRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IReadOnlyList<VideoGame>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = @"SELECT Id, Title, Publisher, Developer, Platform, ReleaseDate
                         FROM VIDEOGAMES";

            var videoGames = await connection
                .QueryAsync<VideoGame>(query);

            return videoGames.ToList();
        }

        public async Task<VideoGame?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = "SELECT Id, Title, Publisher, Developer, Platform, ReleaseDate" +
                " FROM VIDEOGAMES WHERE Id = @Id";

            var videoGame = await connection
                .QueryFirstOrDefaultAsync<VideoGame>
                (query,new {Id = id});

                return videoGame; 
        }

        public async Task AddAsync(VideoGame videoGame)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var query = @"INSERT INTO VIDEOGAMES 
                     (Title, Publisher, Developer, Platform, ReleaseDate)
                     VALUES (@Title, @Publisher, @Developer, @Platform, @ReleaseDate);
                     SELECT CAST(SCOPE_IDENTITY() as int);";

                var newId = await connection.QuerySingleAsync<int>
                (
                query,
                videoGame,
                transaction
                );

                videoGame.Id = newId;

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding new game:" + ex.Message);
                transaction.Rollback();
                throw;
            }
            
        }

        public async Task UpdateAsync(VideoGame videoGame)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            var query = @"UPDATE VideoGames
                SET Title = @Title,
                    Publisher = @Publisher,
                    Developer = @Developer,
                    Platform = @Platform,
                    ReleaseDate = @ReleaseDate
                WHERE Id = @Id";

            try
            {
                await connection.ExecuteAsync(query, videoGame, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating game:" + ex.Message);
                transaction.Rollback();
                throw;
            }
            
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection
                .ExecuteAsync("DELETE FROM VIDEOGAMES WHERE Id = @id",
                new { Id = id});
        }

    }
}
