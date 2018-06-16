using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class Greetings : ICommand
    {
        public CommandResult Run(TwitchClient client, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            client.SendMessage(e.Command.ChatMessage.Channel,$"Greetings {e.Command.ChatMessage.DisplayName} Welcome, I am AzerBot");
            return rtn;

        }
    }
}