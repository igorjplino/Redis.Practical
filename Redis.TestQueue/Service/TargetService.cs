using Redis.TestQueue.Model;
using System.Collections.Generic;

namespace Redis.TestQueue.Service
{
    public class TargetService
    {
        private int lastId = 1;

        public List<Target> InitializeTargets(int amount)
        {
            var targets = new List<Target>();

            for (int i = 0; i < amount; i++)
            {
                targets.Add(new Target
                {
                    Id = lastId,
                    Name = $"target_{i}",
                    Status = TargetStatus.Discovery
                });

                lastId++;
            }

            return targets;

            //var jsonObj = JsonConvert.SerializeObject(targets, Formatting.Indented);
            //System.IO.File.WriteAllText(Storage.TargetsToSend(), jsonObj);
        }
    }
}
