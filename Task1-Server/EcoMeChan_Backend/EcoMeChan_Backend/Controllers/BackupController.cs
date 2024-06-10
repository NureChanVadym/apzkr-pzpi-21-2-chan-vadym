using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Npgsql;
using EcoMeChan.Database;

namespace EcoMeChan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public BackupController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> CreateBackup()
        {
            try
            {
                var backupFilePath = "backup.sql";
                await BackupDatabase(_configuration.GetConnectionString("DefaultConnection"), backupFilePath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(backupFilePath);
                return File(fileBytes, "application/sql", "backup.sql");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating backup: {ex.Message}");
            }
        }

        private async Task BackupDatabase(string connectionString, string backupFilePath)
        {
            var pgDumpPath = @"C:\Program Files\PostgreSQL\16\bin\pg_dump.exe"; 

            // Parse connection string
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var host = builder.Host;
            var port = builder.Port;
            var username = builder.Username;
            var password = builder.Password;
            var database = builder.Database;

            var arguments = $"-h {host} -p {port} -U {username} -F c -b -v -f \"{backupFilePath}\" {database}";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = pgDumpPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Environment = { ["PGPASSWORD"] = password } 
            };

            using (var process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new System.Exception($"pg_dump failed: {error}");
                }
            }
        }
    }
}
