using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public static class WebSocketExtension{
        public static async Task<Tuple<string, WebSocketReceiveResult>> ReceiveTextAsync(this WebSocket Obj, CancellationToken Token, int BufferSize = 1024 * 4) {
            byte[] Buffer = new byte[BufferSize];
            WebSocketReceiveResult ReceiveResult = await Obj.ReceiveAsync(new ArraySegment<byte>(Buffer), Token);
            string Result = Encoding.UTF8.GetString(Buffer);
            int End = Result.IndexOf("\0");
            if (End == -1) End = Result.Length;
            Result = Result.Substring(0, End);
            return new Tuple<string, WebSocketReceiveResult>(Result, ReceiveResult);
        }

        public static async Task SendTextAsync(this WebSocket Obj, string Data) {
            await Obj.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(Data)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
