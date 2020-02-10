using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YoutubeSearch.Application.Channels.Queries.GetChannelDetails;
using YoutubeSearch.Application.Channels.Queries.SearchChannels;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChannelsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Channel>> Get([FromQuery]string searchTerm, [FromQuery]bool fromCache = false)
        {
            return await mediator.Send(new SearchChannelsQuery(searchTerm, fromCache));
        }

        [HttpGet("{id}")]
        public async Task<Channel> GetDetails(string id)
        {
            return await mediator.Send(new GetChannelDetailsQuery(id));
        }
    }
}