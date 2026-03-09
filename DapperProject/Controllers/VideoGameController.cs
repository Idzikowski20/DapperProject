using DapperProject.Models;
using DapperProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameRepository _videoGameRepository;

        public VideoGameController(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetGamesAsync()
        {
            var videoGames = await _videoGameRepository.GetAllAsync();
            return Ok(videoGames);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public async Task<ActionResult<VideoGame>> GetGameByIdAsync(int id)
        {
            var videoGame = await _videoGameRepository.GetByIdAsync(id);
            if (videoGame == null)
            {
                return NotFound("This Video Game does not exist in database");
            }
            return Ok(videoGame);
        }

        [HttpPost]
        public async Task<ActionResult> AddGameAsync(VideoGame videoGame)
        {
            await _videoGameRepository.AddAsync(videoGame);
            return CreatedAtAction("GetGameById", new { id = videoGame.Id}, videoGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameAsync(int id, VideoGame videoGame)
        {
            var existingGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound("Game with that ID does not exist in database.");
            }
            videoGame.Id = id;
            await _videoGameRepository.UpdateAsync(videoGame);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameAsync(int id)
        {
            var existingGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound("Game with that ID does not exist in database.");
            }
            await _videoGameRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
