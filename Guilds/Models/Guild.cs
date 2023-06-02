using Cassandra;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;
using GenericTools.Database;
using Guilds.Models.DTO;

namespace Guilds.Models
{
    [Table("guild")]
    public class Guild : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("owner")]
        public GuildUser Owner { get; set; }
        [Column("guildchannel")]
        public List<GuildChannel> Channels { get; set; }
        [Column("guilduser")]
        public List<GuildUser> Users { get; set; }
        public JoinedGuildDto mapToJoinedGuildDto()
        {
            var joinedGuild = new JoinedGuildDto
            {
                Name = Name,
                Id = Id,
                Channels = Channels
            };
            return joinedGuild;
        }
    }

}
