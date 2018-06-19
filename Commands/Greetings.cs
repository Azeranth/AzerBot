using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class Greetings : ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            bot.Client.SendMessage(e.Command.ChatMessage.Channel,$"Greetings {e.Command.ChatMessage.DisplayName} Welcome, I am AzerBot");
            return rtn;
        }
    }
}