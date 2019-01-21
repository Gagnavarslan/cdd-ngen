using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;

namespace CoreData.Common.Collaboration
{
    //todo: check official page https://github.com/SignalR/SignalR/blob/master/samples/Common/CommonClient.cs
    /// <summary>Local env messages hub, acting as a service for higher level communication abstractions:
    /// <para> - both CDDS services: standard user session and shared across terminal sessions</para>
    /// <para> - single local app instance validation</para>
    /// <para> - collaboration services with remote cdd clients</para></summary>
    public interface IMessageHub //Collaboration
    {
        /// <summary>Messages communication channel</summary>
        Uri ChannelAddress { get; }
    }

    public class MessageHub : Hub, IMessageHub
    {
        private readonly IDisposable _channel;

        public MessageHub(Uri channel) // http://localhost:8118
        {
            ChannelAddress = channel ?? throw new ArgumentNullException();
            _channel = WebApp.Start(channel.AbsoluteUri);
        }

        public Uri ChannelAddress { get; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _channel.Dispose();
        }
    }
}
