namespace Guilds.Models.DTO
{
    public class JoinGuildDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public GuildUser mapToGuildUser()
        {
            return new GuildUser
            {
                Id = UserId,
                Name = Name
            };
        }
    }
}
