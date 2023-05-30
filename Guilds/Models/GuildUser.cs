
using Cassandra;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;
using GenericTools.Database;
using System.Net;

namespace Guilds.Models
{
    [Table("guilduser")]
    public class GuildUser : BaseEntity
    {
        [Column("name")]
        
        public string Name { get; set; }

    }

    public class GuildUserMapping : Mappings
    {
        public static void GuildUserMap(Cassandra.ISession session)
        {
            try
            {
                session.Execute("CREATE TYPE guilduser (\r\n  name text, id uuid);");
            }
            catch (Exception e)
            {

            }
            session.UserDefinedTypes.Define(UdtMap.For<GuildUser>());
/*            session.UserDefinedTypes.Define(
               UdtMap.For<GuildUser>()
                  .Map(a => a.id, "id")
                  .Map(a => a.Name, "name")
            );*//*
            MappingConfiguration.Global.Define(new Map<GuildUser>()
            .ExplicitColumns()
            .Column(g => g.id, cm => cm.WithName("id"))
            .Column(g => g.Name, cm => cm.WithName("name")));*/
        }
    }
}
