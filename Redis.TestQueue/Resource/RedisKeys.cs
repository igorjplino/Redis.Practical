using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.TestQueue.Resource
{
    public static class RedisKeys
    {
        public static string ListTargetsKey() => "queue:target:discovery";
    }
}
