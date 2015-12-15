using Microsoft.AspNet.Builder;
using Microsoft.AspNet.WebSockets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class IApplicationBuilderExtension{
        public static IApplicationBuilder UseWebSockets<Handler>(this IApplicationBuilder builder, WebSocketOptions options = null) where Handler : WebSocketHandler , new (){
            if(options == null)
                builder.UseWebSockets();
            else
                builder.UseWebSockets(options);

            Handler handler = (Handler)Activator.CreateInstance(typeof(Handler));

            builder.Map(handler.RequestPath, WebSocketApi => {
                builder.Use(handler.Start);
            });

            return builder;
        }
    }
}
