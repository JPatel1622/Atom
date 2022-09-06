using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Atom.Domain.Enum
{

    public enum Roles
    {
        [Description(description: "Standard")]
        Standard = 1,
        [Description(description: "OrgUsr")]
        OrgUsr = 2,
        [Description(description: "OrgCoord")]
        OrgCoord = 3,
        [Description(description: "OrgAdmin")]
        OrgAdmin = 4
    }

    public enum Policies
    {
        [Description(description: "ViewPubEvent")]
        ViewPubEvent,
        [Description(description: "ViewOrgEvent")]
        ViewOrgEvent,
        [Description(description: "SearchEvent")]
        SearchEvent,
        [Description(description: "RSVP")]
        RSVP,
        [Description(description: "CrudEvent")]
        CrudEvent,
        [Description(description: "CrudOrgUsr")]
        CrudOrgUsr,
        [Description(description: "InviteOrg")]
        InviteOrg,
        [Description(description: "AcceptOrgEvent")]
        AcceptOrgEvent,
        [Description(description: "CapEvent")]
        CapEvent
    }
}





