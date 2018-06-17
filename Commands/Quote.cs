using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class Quote : ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            List<int> validKeys = bot.Quotes.Select(n => n.Key).ToList();
            int Index;
            string message = "";
            if(bot.Quotes.Count == 0)
            {
                rtn.Successs = false;
                rtn.FailureReason = CommandResult.FailureReasonEnum.InvalidArguments;
                rtn.FailureMessage = "There are no quotes to show";
                return rtn;
            }
            if(e.Command.ArgumentsAsList.Count == 0)
            {
                Index = validKeys[new Random().Next(validKeys.Count)];
            }
            else
            {
                if(!int.TryParse(e.Command.ArgumentsAsList[0],out Index))
                {
                    Index = validKeys[new Random().Next(validKeys.Count)];
                }
            }
            if (validKeys.Contains(Index))
            {
                message = $"Quote {Index}: \"{bot.Quotes.FirstOrDefault(n => n.Key == Index).Value}\"";
            }
            else
            {
                rtn.Successs = false;
                rtn.FailureReason = CommandResult.FailureReasonEnum.InvalidArguments;
                rtn.FailureMessage = $"Unable to locate Quote {e.Command.ArgumentsAsList.FirstOrDefault()}";
            }
            bot.Client.SendMessage(bot.Channel, message);
            return rtn;
        }
    }
}