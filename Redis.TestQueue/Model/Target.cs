using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.TestQueue.Model
{
    public class Target
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TargetStatus Status { get; set; }
    }
}
