using System.Threading.Channels;

namespace Guilds.Models.DTO
{
    public class CreateChannelDto
    {
        public string Name { get; set; }
        public Guid GuildId { get; set; }

        public GuildChannel mapToGuildChannel()
        {
            return new GuildChannel
            {
                Id = Guid.NewGuid(),
                Name = Name,
                GuildId = GuildId
            };
        }
    }
}
