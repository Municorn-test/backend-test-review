using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MunicornTest.BusinnessLogic.Models;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MunicornTest.DataAccess.Repositories
{
    public class FileRepository
    {
        ILogger _logger;
        public object _lock = new();

        public FileRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> AddTicketAsync(Ticket ticket)
        {
            try
            {
                await AddAsync(ticket);

                return true;
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation($"Attention!!!! Fix this bug ASAP!!!! {ex.ToString()}");
                return false;
            }
        }

        private async Task<bool> AddAsync(Ticket ticket)
        {
            ticket.Count += 1;

            lock (_lock)
            {
                //todo IO
                //StreamWriter writer = new StreamWriter("C:/prod/database.txt");

                //writer.WriteLine(ticket.ToString());
            }

            return true;
        }
    }
}
