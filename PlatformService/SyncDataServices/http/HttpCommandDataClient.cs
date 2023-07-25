using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            // StringContent: used to encapsulate the PlatformReadDto object in a format 
            // that can be sent over HTTP. The JsonSerializer.Serialize(plat) 
            // call is used to convert the plat object to a JSON string, and 
            // Encoding.UTF8 and "application/json" indicate that the content 
            // is JSON and UTF-8 encoded.

            // In an HTTP communication, data must be sent as a stream of bytes. 
            // This means we can't just send an object "as it is" over the network. 
            // Serialization is the process of converting an object into a format that can be transported.
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );

            var responds = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            
            if(responds.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService with OK!");
            }
            else
            {
                Console.WriteLine("--> Sync Post to CommandService was not OK!");
            }
        }

    }
}