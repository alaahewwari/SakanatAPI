using Microsoft.AspNetCore.SignalR;
namespace BusinessLogic.Infrastructure.SignalR
{
    public class SignalServer : Hub
    {
        private readonly Dictionary<string, string> userConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            AddUserConnection(userId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            RemoveUserConnection(userId);

            return base.OnDisconnectedAsync(exception);
        }
        public async Task FollowUser(string ownerId, string senderId)
        {
            var ownerConnectionId = GetOwnerConnectionId(ownerId);
            if (ownerConnectionId != null)
            {
                await Clients.User(ownerConnectionId).SendAsync("FollowNotification", senderId);
                await Clients.Caller.SendAsync("FollowNotification", "hi");
            }
            await Clients.Caller.SendAsync("FollowNotification", "hi");
        }
        public void RemoveUserConnection(string userId)
        {
            lock (userConnections)
            {
                if (userId is not null)
                {
                    userConnections.Remove(userId);
                }
            }
        }
        public void AddUserConnection(string userId, string connectionId)
        {
            lock (userConnections)
            {
                if (userId is not null)
                {
                    userConnections[userId] = connectionId;
                }
            }
        }
        public string GetOwnerConnectionId(string userId)
        {
            lock (userConnections)
            {
                return userConnections.TryGetValue(userId, out string connectionId) ? connectionId : null;
            }
        }
    }
}

