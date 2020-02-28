using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Redis.TestQueue.Resource
{
    public static class Storage
    {
        private const string Base = "Objects";

        public static string TargetsToSend() => Base + "\\Targets_ToSend.json";

        public static void StartStorage()
        {
            if (!Directory.Exists(Base))
                Directory.CreateDirectory(Base);
        }
    }
}
