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
        private long _ticketCount;
        private FileRepository FileRepository { get; set; }

        public MunicornTetsController(ILogger logger)
        {
            FileRepository = new FileRepository(logger);
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket(string t, Ticket.State s, string a, List<string> co)
        {
            var isTicketAdded = await FileRepository.AddTicketAsync(
                ValidateAndCreateTicket(t, s, a, co)
                );
            
            if(isTicketAdded) 
                _ticketCount++;

            return Ok(isTicketAdded);
        }

        [HttpGet("StorageSize")]
        public async Task<IActionResult> GetStorageSize()
        {
            return Ok(FileRepository.StorageSize);
        }

        [HttpGet("TicketCount")]
        public async Task<ActionResult> GetTicketCount()
        {
            return Ok(_ticketCount);
        }

        private static Ticket ValidateAndCreateTicket(string title, Ticket.State state, string assignedToUser, List<string> comments)
        {
            if (title == null)
            {
                Console.WriteLine("Title is required");
                return null;
            }

            return new Ticket() { Title = title, CurrentState = state, AssignedToUser = assignedToUser, Comments = comments };
        }
    }
}
