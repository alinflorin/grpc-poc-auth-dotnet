using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class AuthService : Auth.AuthBase
    {
        public override async Task<LoginReply> Login(LoginRequest request, ServerCallContext context)
        {
            var reply = new LoginReply();
            if (request.Username.ToLower() != "admin" || request.Password.ToLower() != "grpc-poc")
            {
                reply.Succesful = false;
                reply.Errors.Add("Invalid login credentials");
                return reply;
            }
            reply.Succesful = true;
            reply.User = new User { 
                Email = "admin@admin.com",
                FullName = "Admin Admin",
                IsAdmin = true,
                Username = "admin"
            };
            return await Task.FromResult(reply);
        }

        public override async Task SubscribeToNotifications(SubscribeToNotificationsRequest request, IServerStreamWriter<Notification> responseStream, ServerCallContext context)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    var notif = new Notification
                    {
                        Title = new NLipsum.Core.Word().ToString(),
                        Message = new NLipsum.Core.Paragraph().ToString()
                    };
                    await responseStream.WriteAsync(notif);
                    await Task.Delay(5000);
                }
            });
        }
    }
}
