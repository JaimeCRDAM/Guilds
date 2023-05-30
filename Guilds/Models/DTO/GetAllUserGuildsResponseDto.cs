namespace Guilds.Models.DTO
{
    public class GetAllUserGuildsResponseDto
    {
        public Guid Id { get; set; }
        public List<Guild> Guilds { get; set; }

    }
}
