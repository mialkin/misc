using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcServiceOne.Services
{
    public class FisherService : Fisher.FisherBase
    {
        public override Task<FishResponse> Fish(FishRequest request, ServerCallContext context)
        {
            return Task.FromResult(new FishResponse
            {
                Message = "Hello " + request.Number
            });
        }

        public override async Task FishOverTime(FishOverTimeRequest request, IServerStreamWriter<FishOverTimeResponse> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new FishOverTimeResponse
            {
                Message = "Start"
            });
            
            await responseStream.WriteAsync(new FishOverTimeResponse
            {
                Message = "Two seconds delay"
            });
            await Task.Delay(2000);
            
            await responseStream.WriteAsync(new FishOverTimeResponse
            {
                Message = "Five seconds delay"
            });
            await Task.Delay(5000);
            
            await responseStream.WriteAsync(new FishOverTimeResponse
            {
                Message = "Finish"
            });
        }
    }
}