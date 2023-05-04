using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SendUpdatesHub
{
    public class ProgressHub : Hub
    {
        public async Task SendProgressUpdate(int progress)
        {
            await Clients.All.SendAsync("ReceiveProgressUpdate", progress);
        }
    }
}
