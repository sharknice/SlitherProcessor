using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SlitherBrain;
using SlitherModel.Source;

namespace SlitherProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private readonly GameManager GameManager;
        private readonly SlitherPlayer SlitherPlayer;

        public PlayController(GameManager gameManager, SlitherPlayer slitherPlayer)
        {
            GameManager = gameManager;
            SlitherPlayer = slitherPlayer;
        }

        [HttpGet]
        [Route("StartGame/{source}")]
        public string StartGame(string source)
        {
            return GameManager.StartGame(source);
        }

        [HttpPut]
        [Route("UpdateGame/{id}/{millisecondsToAction}")]
        public GameDecision PlayGame(string id, int millisecondsToAction, [FromBody]SlitherFrame slitherFrame)
        {
            ActiveGameDatabase.ActiveGames.First(game => game.Id == id).Frames.Add(slitherFrame);

            return SlitherPlayer.PlayGame(id, slitherFrame, millisecondsToAction);
        }

        [HttpGet]
        [Route("EndGame/{id}")]
        public bool EndGame(string id)
        {
            return GameManager.EndGame(id);
        }
    }
}