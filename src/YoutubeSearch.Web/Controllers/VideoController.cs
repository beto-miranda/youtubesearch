using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YoutubeSearch.Application.Videos.Queries.GetVideoDetails;
using YoutubeSearch.Application.Videos.Queries.SearchVideos;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IMediator mediator;

        public VideosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Video>> Get([FromQuery]string searchTerm, [FromQuery]bool fromCache = false)
        {
            return await mediator.Send(new SearchVideosQuery(searchTerm, fromCache));
        }

        [HttpGet("{id}")]
        public async Task<Video> GetDetails(string id)
        {
            return await mediator.Send(new GetVideoDetailsQuery(id));
        }
    }
}