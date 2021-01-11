using System;
using System.Threading.Tasks;
using Grpc.Core;
using Koledeus.API.Data;
using Koledeus.API.Data.Entities;
using Koledeus.Contract;
using Microsoft.Extensions.Logging;

namespace Koledeus.API.Services
{
    public class CPUService : Cpu.CpuBase
    {
        private readonly KoledeusDbContext _dbContext;
        private readonly ILogger<CPUService> _logger;

        public CPUService(ILogger<CPUService> logger, KoledeusDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public override async Task<CPUInfoReply> FeedCPUInfo(IAsyncStreamReader<CPUInfoRequest> requestStream,
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                await _dbContext.ClientMetrics.AddAsync(new ClientMetricEntity()
                {
                    Id = Guid.NewGuid(),
                    Cpu = requestStream.Current.CpuPercentage,
                    MemoryUsage = requestStream.Current.MemoryUsage
                });
                _logger.LogInformation($"CPU Info: {requestStream.Current.CpuPercentage}");
            }

            var cpuInfoReply = new CPUInfoReply();
            try
            {
                await _dbContext.SaveChangesAsync();
                cpuInfoReply.IsSuccess = true;
            }
            catch (Exception e)
            {
                cpuInfoReply.IsSuccess = false;
            }

            return cpuInfoReply;
        }
    }
}