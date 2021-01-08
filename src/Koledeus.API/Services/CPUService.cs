using System.Threading.Tasks;
using Grpc.Core;
using Koledeus.Contract;
using Microsoft.Extensions.Logging;

namespace Koledeus.API.Services
{
    public class CPUService : Cpu.CpuBase
    {
        private readonly ILogger<CPUService> _logger;

        public CPUService(ILogger<CPUService> logger)
        {
            _logger = logger;
        }

        public override async Task<CPUInfoReply> FeedCPUInfo(IAsyncStreamReader<CPUInfoRequest> requestStream,
            ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                _logger.LogInformation($"CPU Info: {requestStream.Current.CpuPercentage}");
            }

            return new CPUInfoReply
            {
                IsSuccess = true
            };
        }
    }
}