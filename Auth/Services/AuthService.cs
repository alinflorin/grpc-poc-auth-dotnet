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
            return await base.Login(request, context);
        }
    }
}
