using Atom.Data.BaseClass;
using Atom.Domain.Enum;

namespace Atom.Data.Model.Security
{
    public class FavoriteEventModel : ModelBase
    {
        public int UserFavoriteId { get; set; }
        public EventType EventTypeId { get; set; }
        public string EventId { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
