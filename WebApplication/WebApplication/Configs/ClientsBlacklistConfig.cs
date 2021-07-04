using System.Collections.Generic;

namespace WebApplication.Configs
{
    public class ClientsBlacklistConfig
    {
        public IReadOnlyCollection<long> Clients { get; set; }
    }
}