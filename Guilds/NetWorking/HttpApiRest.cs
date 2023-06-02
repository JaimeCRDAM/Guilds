using GenericTools;
using Guilds.Models;
using Guilds.Models.DTO;
using System.Text;
using System.Text.Json;

namespace Guilds.NetWorking
{
    public class HttpApiRest : IHttpApiRest
    {
        private readonly HttpClient _httpClient;

        public HttpApiRest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddUserToGuild(Guid userId, Guid guildId)
        {
            var response = await _httpClient.PostAsync($"{IpsEnum.LogUpIn}api/addusertoguild/{userId}/{guildId}", null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<List<Guid>> GetAllGuilds(Guid id)
        {
            var response = await _httpClient.GetAsync($"{IpsEnum.LogUpIn}api/getallguilds/{id}");
            List<Guid> responseBody = await response.Content.ReadFromJsonAsync<List<Guid>>();
            return responseBody;
        }

        public async Task<bool> DeleteUserFromGuild(Guid userId, Guid guildId)
        {
            var response = await _httpClient.DeleteAsync($"{IpsEnum.LogUpIn}api/deleteuserfromguild/{userId}/{guildId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateGuildTextChannel(Guid channelId, IEnumerable<Guid> usersId)
        {
            var body = new CreateGuildTextChannelDto
            {
                ChannelId = channelId,
                RecipientId = usersId,
            };
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{IpsEnum.DirectMessage}createguildchannel", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
