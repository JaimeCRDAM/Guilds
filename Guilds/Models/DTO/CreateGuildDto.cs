namespace Guilds.Models.DTO
{
    public class CreateGuildDto
    {
        public string Name { get; set; }
        public GuildUser Owner { get; set; }

        public Guild mapToGuild()
        {
            var newGuild = new Guild
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Owner = Owner,
                Channels = new List<GuildChannel>(),
                Users = new List<GuildUser>()
            };

            var newChannel = new GuildChannel
            {
                Id = Guid.NewGuid(),
                Name = "Default",
                GuildId = newGuild.Id
            };
            var newGuildChannelList = (List<GuildChannel>)newGuild.Channels;
            var newGuildUserList = (List<GuildUser>)newGuild.Users;

            newGuildChannelList.Add(newChannel);
            newGuildUserList.Add(Owner);

            return newGuild;
        }
    }
}
