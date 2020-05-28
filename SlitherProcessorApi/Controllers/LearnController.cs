using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SlitherModel.Source;

namespace SlitherProcessorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        private GameManager GameManager;

        public LearnController(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        [HttpGet]
        [Route("StartGame/{source}")]    
        public string StartGame(string source)
        {
            return GameManager.StartGame(source);
        }

        [HttpPut]
        [Route("UpdateGame/{id}")]
        public bool UpdateGame(string id, [FromBody]SlitherFrame slitherFrame)
        {
            if (ActiveGameDatabase.ActiveGames.ContainsKey(id))
            {
                ActiveGameDatabase.ActiveGames[id].Frames.Add(slitherFrame);
                return true;
            }
            return false;        
        }

        [HttpGet]
        [Route("EndGame/{id}")]
        public bool EndGame(string id)
        {
            return GameManager.EndGame(id);
        }
    }
}