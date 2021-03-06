﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSQL.App.Events
{
    public class RefreshConnectionListEvent: PubSubEvent<RefreshConnectionListEventArgument>
    {
    }

    public class RefreshConnectionListEventArgument
    {
        public string SelectedConnectionId { get; set; }
    }
}
