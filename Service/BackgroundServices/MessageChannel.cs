using System.Threading.Channels;

namespace Service.BackgroundServices;

public class MessageChannel
{
    private readonly Channel<string> channel = Channel.CreateUnbounded<string>();
    public ChannelReader<string> Reader => channel.Reader;

    public void AddMessage(string message)
    {
        channel.Writer.TryWrite(message);
    }
}