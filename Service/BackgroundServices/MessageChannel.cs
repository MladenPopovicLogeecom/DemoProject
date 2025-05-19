using System.Threading.Channels;

namespace Service.BackgroundServices;

public class MessageChannel
{
    private readonly Channel<string> channel = Channel.CreateUnbounded<string>();

    public void AddMessage(string message)
    {
        channel.Writer.TryWrite(message);
    }

    public ChannelReader<string> Reader => channel.Reader;
}
