using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceOne;

namespace GrpcClientOne
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5020");
            Fisher.FisherClient client = new Fisher.FisherClient(channel);

            FishResponse  fishResponse = await client.FishAsync(new FishRequest { Number = 777 });

            Console.WriteLine($"fishResponse: {fishResponse}");

            AsyncServerStreamingCall<FishOverTimeResponse> call = client.FishOverTime(new FishOverTimeRequest( ));

            IAsyncStreamReader<FishOverTimeResponse> streamReader = call.ResponseStream;

            while (await streamReader.MoveNext())
            {
                string message = streamReader.Current.Message;
                Console.WriteLine(message);
            }
        }
    }
}