namespace Guilds.Models.DTO
{
    public class JoinedChannelDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid GuildId { get; set; }
    }
}
