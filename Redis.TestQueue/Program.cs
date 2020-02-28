using Redis.TestQueue.Model;
using System;
using Redis.TestQueue.Service;
using Redis.TestQueue.Resource;
using Newtonsoft.Json;

namespace Redis.TestQueue
{
    class Program
    {
        public static TargetService _TargetService;
        public static RedisService _RedisService;

        static void Main(string[] args)
        {
            Console.WriteLine("Test Redis with queue");

            Initialize();

            do
            {
                Console.WriteLine("What operation you wanna do?");
                Console.WriteLine("1 - Push entities in queue");

                switch (Console.ReadLine())
                {
                    case "1":
                        PushEntities();
                        break;
                    default:
                        Console.WriteLine("Not found operation... \n");
                        continue;
                }

                Console.WriteLine($"Keep working? (y/n)");
            } while (Console.ReadLine() != "n");
        }

        private static void Initialize()
        {
            _TargetService = new TargetService();
            _RedisService = new RedisService();

            // Iniciar serviço do Redis
            _RedisService.Start();

            // Criar diretórios onde estão os objetos
            Storage.StartStorage();
        }

        private static void PushEntities()
        {
            Console.WriteLine($"How much Entities to push in queue: ");
            var targets = _TargetService.InitializeTargets(Int32.Parse(Console.ReadLine()));

            foreach (var target in targets)
            {
                var targetJson = JsonConvert.SerializeObject(target);

                _RedisService.RightPush(RedisKeys.ListTargetsKey(), targetJson);
            }
        }
    }
}
