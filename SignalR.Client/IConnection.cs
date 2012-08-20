using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SignalR.Client.Http;

namespace SignalR.Client
{
    using SignalR.Client.Transports;

    public interface IConnection
    {
        bool ChangeState(ConnectionState oldState, ConnectionState newState);

        Task<T> Send<T>(string data);

        void OnReceived(JToken data);
        void OnError(Exception ex);
        void OnReconnected();
        void PrepareRequest(IRequest request);

        /// <summary>
        /// Occurs when the <see cref="Connection"/> has received data from the server.
        /// </summary>
        event Action<string> Received;

        /// <summary>
        /// Occurs when the <see cref="Connection"/> has encountered an error.
        /// </summary>
        event Action<Exception> Error;

        /// <summary>
        /// Occurs when the <see cref="Connection"/> is stopped.
        /// </summary>
        event Action Closed;

        /// <summary>
        /// Occurs when the <see cref="Connection"/> successfully reconnects after a timeout.
        /// </summary>
        event Action Reconnected;

        /// <summary>
        /// Occurs when the <see cref="Connection"/> state changes.
        /// </summary>
        event Action<StateChange> StateChanged;

        /// <summary>
        /// Gets or sets the cookies associated with the connection.
        /// </summary>
        CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Gets or sets authentication information for the connection.
        /// </summary>
        ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the groups for the connection.
        /// </summary>
        IEnumerable<string> Groups { get; set; }

        /// <summary>
        /// Gets the url for the connection.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets or sets the last message id for the connection.
        /// </summary>
        string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the connection id for the connection.
        /// </summary>
        string ConnectionId { get; set; }

        /// <summary>
        /// Gets a dictionary for storing state for a the connection.
        /// </summary>
        IDictionary<string, object> Items { get; }

        /// <summary>
        /// Gets the querystring specified in the ctor.
        /// </summary>
        string QueryString { get; }

        /// <summary>
        /// Gets the current <see cref="ConnectionState"/> of the connection.
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// Starts the <see cref="Connection"/>.
        /// </summary>
        /// <returns>A task that represents when the connection has started.</returns>
        Task Start();

        /// <summary>
        /// Starts the <see cref="Connection"/>.
        /// </summary>
        /// <param name="httpClient">The http client</param>
        /// <returns>A task that represents when the connection has started.</returns>
        Task Start(IHttpClient httpClient);

        /// <summary>
        /// Starts the <see cref="Connection"/>.
        /// </summary>
        /// <param name="transport">The transport to use.</param>
        /// <returns>A task that represents when the connection has started.</returns>
        Task Start(IClientTransport transport);

        /// <summary>
        /// Stops the <see cref="Connection"/>.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sends data asynchronously over the connection.
        /// </summary>
        /// <param name="data">The data to send.</param>
        /// <returns>A task that represents when the data has been sent.</returns>
        Task Send(string data);

        /// <summary>
        /// Sends an object that will be JSON serialized asynchronously over the connection.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>A task that represents when the data has been sent.</returns>
        Task Send(object value);
    }
}
