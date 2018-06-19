using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class Generic : Command,ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            bot.Client.SendMessage(e.Command.ChatMessage.Channel, "I am a generic command filler, if you see this message, some thing has gone wrong");
            return rtn;
        }
    }
}