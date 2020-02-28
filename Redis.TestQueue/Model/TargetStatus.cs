using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.TestQueue.Model
{
    public enum TargetStatus
    {
        Discovery = 0,
        Sent = 1,
        Received = 2,
    }
}
