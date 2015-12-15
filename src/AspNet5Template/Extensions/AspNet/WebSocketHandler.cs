﻿using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace AspNet5Template.Extensions.AspNet{
    public abstract class WebSocketHandler{
        /// <summary>
        /// WebSocket連線狀態改變事件
        /// </summary>
        /// <param name="Context">Http通訊內容</param>
        /// <param name="Socket">WebSocket物件</param>
        public delegate void WebSocketConnectionEvent(HttpContext Context, WebSocket Socket);

        /// <summary>
        /// WebSocket接收訊息事件
        /// </summary>
        /// <param name="Socket">WebSocket物件</param>
        /// <param name="Type">訊息類型</param>
        /// <param name="ReceiveMessage">接收到的訊息</param>
        public delegate void WebsocketReceiveEvent(WebSocket Socket, WebSocketMessageType Type, byte[] ReceiveMessage);

        /// <summary>
        /// 表示Request路徑
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// Handler建構式，您必須至少聲明Request路徑
        /// </summary>
        /// <param name="RequestPath">Request路徑</param>
        /// <param name="SubProtocol">子通訊協定</param>
        public WebSocketHandler(string RequestPath,string SubProtocol = null) {
            this.RequestPath = RequestPath;
        }

        /// <summary>
        /// Handler進入點
        /// </summary>
        /// <param name="Context">Http通訊內容</param>
        /// <param name="Next">下一個連線工作</param>
        /// <returns></returns>
        internal async Task Start(HttpContext Context, Func<Task> Next) {
            //如果不是WebSocket發出的請求，則由其他路由處理
            if (!Context.WebSockets.IsWebSocketRequest) {
                await Next(); return;
            }

            //根據檢查決定是否允許連線
            if (AcceptConditions(Context)) {
                //轉發監聽
                Listen(Context,await Context.WebSockets.AcceptWebSocketAsync());
            } else {
                
            }
        }

        /// <summary>
        /// 允許WebSocket連線之條件
        /// </summary>
        /// <param name="Context">Http通訊內容</param>
        /// <returns></returns>
        protected abstract bool AcceptConditions(HttpContext Context);

        /// <summary>
        /// 當WebSocket開啟連線時
        /// </summary>
        protected event WebSocketConnectionEvent OnConnected;

        /// <summary>
        /// 當WebSocket關閉連線時
        /// </summary>
        protected event WebSocketConnectionEvent OnDisconnected;

        /// <summary>
        /// 當WebSocket接收到訊息
        /// </summary>
        protected event WebsocketReceiveEvent OnReceive;

        /// <summary>
        /// WebSocket監聽主程序
        /// </summary>
        /// <param name="Context">Http通訊內容</param>
        /// <param name="Socket">WebSocket物件</param>
        protected async void Listen(HttpContext Context, WebSocket Socket) {
            OnConnected?.Invoke(Context, Socket);
            
            while(true) {
                bool ReceiveComplete = true;
                WebSocketReceiveResult ReceiveResult;
                List<byte> ReceiveData = new List<byte>();

                //循環接收資料以防資料大於緩衝區大小時分段傳輸
                do {
                    //建立緩衝區
                    byte[] Buffer = new byte[4 * 1024];

                    //接收資料
                    ReceiveResult = await Socket.ReceiveAsync(new ArraySegment<byte>(Buffer),CancellationToken.None);

                    //乾淨資料
                    byte[] ClearData = new byte[ReceiveResult.Count];

                    //複製本次傳輸範圍資料
                    Array.Copy(Buffer, ClearData, ReceiveResult.Count);

                    //存入接收資料集合
                    ReceiveData.AddRange(ClearData);

                    //檢查是否接收完成
                    ReceiveComplete = !ReceiveResult.EndOfMessage;
                } while (ReceiveComplete);

                //檢查是否關閉連線，如關閉則跳脫循環監聽
                if (ReceiveResult.CloseStatus.HasValue) break;

                OnReceive?.Invoke(Socket,ReceiveResult.MessageType,ReceiveData.ToArray());
            };

            OnDisconnected.Invoke(Context, Socket);
        }        
    }
}