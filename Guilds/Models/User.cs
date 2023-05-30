using GenericTools.Database;

namespace Guilds.Models
{
    public class User : BaseEntity
    {
        public List<Guid> Guilds { get; set; } = new List<Guid>();
    }
}
