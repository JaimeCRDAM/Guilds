namespace Guilds.NetWorking
{
    public interface IHttpApiRest
    {
        Task<List<Guid>> GetAllGuilds(Guid id);
        Task<bool> AddUserToGuild(Guid userId, Guid guildId);

        Task<bool> DeleteUserFromGuild(Guid userId, Guid guildId);

        Task<bool> CreateGuildTextChannel(Guid channelId, IEnumerable<Guid> usersId);
    }
}
