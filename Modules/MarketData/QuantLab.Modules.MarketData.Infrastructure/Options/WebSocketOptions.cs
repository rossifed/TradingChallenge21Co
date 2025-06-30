namespace QuantLab.Modules.MarketData.Infrastructure.Options
{
    public class WebSocketOptions
    {
        public int ReconnectTimeoutSec { get; }

        public string Uri { get; }

        public WebSocketOptions(string uri, int reconnectTimeoutSec)
        {
            Uri = uri;
            ReconnectTimeoutSec = reconnectTimeoutSec;

        }
    }
}
