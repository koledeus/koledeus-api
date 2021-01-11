using System;

namespace Koledeus.API.Data.Entities
{
    public class ClientMetricEntity : Entity<Guid>
    {
        public double Cpu { get; set; }
        public long MemoryUsage { get; set; }
    }
}