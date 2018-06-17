using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public interface ICommand
    {
        CommandResult Run(Bot bot, OnChatCommandReceivedArgs e,CommandConfiguration config);
    }
}