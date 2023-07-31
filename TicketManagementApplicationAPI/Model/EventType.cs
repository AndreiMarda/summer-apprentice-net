using System;
using System.Collections.Generic;

namespace TicketManagementApplicationAPI.Model;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public override string ToString()
    {
        return Name;
    }
}