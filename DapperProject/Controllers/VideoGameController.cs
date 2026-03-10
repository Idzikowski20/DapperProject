using AutoMapper;
using DapperProject.Dtos;
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
        private readonly IMapper _mapper;

        public VideoGameController(IVideoGameRepository videoGameRepository, IMapper mapper)
        {
            _videoGameRepository = videoGameRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VideoGameDto>>> GetGamesAsync()
        {
            var videoGames = await _videoGameRepository.GetAllAsync();
            var dto = _mapper.Map<IReadOnlyList<VideoGameDto>>(videoGames);

            return Ok(dto);
        }

        [HttpGet("{id}", Name = "GetGameById")]
        public async Task<IActionResult> GetGameByIdAsync(int id)
        {
            var videoGame = await _videoGameRepository.GetByIdAsync(id);
            if (videoGame == null)
            {
                return NotFound("This Video Game does not exist in database");
            }
            var dto = _mapper.Map<VideoGameDto>(videoGame);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddGameAsync(CreateVideoGameDto dto)
        {
            var videoGame = _mapper.Map<VideoGame>(dto);

            await _videoGameRepository.AddAsync(videoGame);
            var result = _mapper.Map<VideoGameDto>(videoGame);
            return CreatedAtAction("GetGameById", new { id = videoGame.Id}, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameAsync(int id, UpdateVideoGameDto dto)
        {
            var existingGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound("Game with that ID does not exist in database.");
            }
            var videoGame = _mapper.Map<VideoGame>(dto);
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
