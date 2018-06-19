using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class DeleteQuote : Command,ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            string message = "";
            List<int> validKeys = bot.Quotes.Select(n => n.Key).ToList();
            int Index;
            if(e.Command.ArgumentsAsList.Count == 0)
            {
                rtn.Successs = false;
                rtn.FailureReason = CommandResult.FailureReasonEnum.InvalidArguments;
                rtn.FailureMessage = "Please specify a quote to delete";
                return rtn;
            }
            else
            {
                if(!int.TryParse(e.Command.ArgumentsAsList[0], out Index))
                {
                    rtn.Successs = false;
                    rtn.FailureReason = CommandResult.FailureReasonEnum.InvalidArguments;
                    rtn.FailureMessage = $"Could not find specified quote \"{e.Command.ArgumentsAsList[0]}\"";
                    return rtn;
                }
            }
            if(validKeys.Contains(Index))
            {
                message = $"Quote {Index} removed: \"{bot.Quotes.FirstOrDefault(n => n.Key == Index).Value}\"";
                bot.Quotes.RemoveAll(n => n.Key == Index);
                bot.Quotes = bot.Quotes;
            }
            else
            {
                message = bot.Quotes.FirstOrDefault(n => n.Key == Index).Value;
                bot.Quotes.RemoveAll(n => n.Key == Index);
                bot.Quotes = bot.Quotes;
                return rtn;
            }
            bot.Client.SendMessage(bot.Channel, message);
            return rtn;
        }
    }
}