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
        [Column("ownerid")]
        public Guid OwnerId { get; set; }
        [Column("guildchannel")]
        public IEnumerable<GuildChannel> Channels { get; set; }
        [Column("guilduser")]
        public IEnumerable<GuildUser> Users { get; set; }
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
