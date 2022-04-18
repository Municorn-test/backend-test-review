using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MunicornTest.BusinnessLogic.Models;
using MunicornTest.DataAccess.Repositories;

namespace MunicornTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicornTetsController : ControllerBase
    {
        private FileRepository FileRepository { get; set; }

        public MunicornTetsController(ILogger logger)
        {
            FileRepository = new FileRepository(logger);
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket(string t, Ticket.State s, string a, int c, List<string> co)
        {
            //insert empty ticket if title is null
            if (t == null)
                return Ok(await FileRepository.AddTicketAsync(null));

            return Ok(await FileRepository.AddTicketAsync(new Ticket() { Title = t, CurrentState = s, AssignedToUser = a, Count = c, Comments = co }));
        }
    }
}
