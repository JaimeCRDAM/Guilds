namespace Guilds.Models.DTO
{
    public class JoinedGuildDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<GuildChannel> Channels { get; set; }
    }
}
