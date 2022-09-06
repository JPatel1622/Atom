using Atom.Data.BaseClass;
using Dapper.Contrib.Extensions;

namespace Atom.Data.Entity.Security
{
    [Table("[sec].[UserFavorite]")]
    public class FavoriteEventEntity : EntityBase
    {
        [Key]
        [Computed]
        public int UserFavoriteId { get; set; }
        public int EventTypeId { get; set; }
        public string EventId { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
