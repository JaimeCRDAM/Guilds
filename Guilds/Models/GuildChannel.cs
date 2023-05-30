using GenericTools.Database;
using Cassandra.Mapping.Attributes;
using Cassandra.Mapping;
using Cassandra;
using System.Net;
using Guilds.Models.DTO;

namespace Guilds.Models
{
    [Table("guildchannel")]
    public class GuildChannel : BaseEntity
    {
        [Column("name")]
        [Frozen]
        public string Name { get; set; }
        [Column("guildid")]
        [Frozen]
        public Guid GuildId { get; set; }

        public JoinedChannelDto toJoinedChannelDto()
        {
            return new JoinedChannelDto
            {
                Name = Name,
                Id = Id,
                GuildId = GuildId
    };
        }
    }
    public class GuildChannelMapping : Mappings
    {
        public static void GuildChannelMap(Cassandra.ISession session)
        {
            try 
            { 
                session.Execute("CREATE TYPE guildchannel (\r\n  name text,\r\n  guildid uuid, id uuid);");
            }
            catch (Exception e)
            {

            }
            session.UserDefinedTypes.Define(
               UdtMap.For<GuildChannel>()
            );
/*            session.UserDefinedTypes.Define(
               UdtMap.For<GuildChannel>()
                .Map(a => a.id, "id")
                .Map(a => a.Name, "name")
                .Map(a => a.GuildId, "guildid")
            );*/
/*            MappingConfiguration.Global.Define(new Map<GuildChannel>()
            .ExplicitColumns()
            .Column(g => g.id, cm => cm.WithName("id"))
            .Column(g => g.Name, cm => cm.WithName("name"))
            .Column(g => g.GuildId, cm => cm.WithName("guildid")));*/
        }
    }
}
