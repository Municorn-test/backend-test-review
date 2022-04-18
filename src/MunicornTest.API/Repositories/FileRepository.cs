using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MunicornTest.BusinnessLogic.Models;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

namespace MunicornTest.DataAccess.Repositories
{
    public class FileRepository
    {
        private const string StorageFileName = "tickets.txt";

        ILogger _logger;
        public object _storageLock = new();


        public FileRepository(ILogger logger)
        {
            _logger = logger;
        }

        public long TicketCount { get; private set; }

        public long StorageSize
        {
            get
            {
                lock (_storageLock)
                {
                    return new FileInfo(StorageFileName).Length;
                }
            }
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
            lock (_storageLock)
            {
                var jsonString = JsonSerializer.Serialize(ticket);
                var storageFile = new StreamWriter(StorageFileName, true);
                storageFile.WriteLine(jsonString);
                storageFile.Close();
            }

            TicketCount++;
            _logger.LogInformation("Ticket added");

            return true;
        }
    }
}
