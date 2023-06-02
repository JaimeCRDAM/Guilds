using Cassandra.Mapping;
using Guilds.Models;

namespace Guilds
{
    public class MyMappings: Mappings
    {
        public static void mapClasses(Cassandra.ISession session) 
        {
            GuildChannelMapping.GuildChannelMap(session);
            GuildUserMapping.GuildUserMap(session);
        }
        public MyMappings()
        {
            // Define mappings in the constructor of your class
            // that inherits from Mappings
            For<GuildUser>()
               .TableName("users")
               .PartitionKey(u => u.Id)
               .Column(u => u.Id, cm => cm.WithName("id"))
               .Column(u => u.Name, cm => cm.WithName("name"));
            For<GuildChannel>()
                .TableName("channels")
                .PartitionKey(c => c.Id)
                .Column(c => c.Id, cm => cm.WithName("id"))
                .Column(c => c.Name, cm => cm.WithName("name"))
                .Column(c => c.GuildId, cm => cm.WithName("guildid"));
            For<Guild>()
                .TableName("guilds")
                .PartitionKey(g => g.Id)
                .Column(g => g.Id, cm => cm.WithName("id"))
                .Column(g => g.Name, cm => cm.WithName("name"))
                .Column(g => g.Owner, cm => cm.WithName("owner"))
                .Column(g => g.Channels, cm => cm.WithName("channel").AsFrozen())
                .Column(g => g.Users, cm => cm.WithName("user").AsFrozen());
        }
    }
}
