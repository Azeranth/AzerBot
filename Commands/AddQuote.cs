using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class AddQuote :Command, ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult { Successs = true};
            int FirstAvailableIndex = Enumerable.Range(0, int.MaxValue).Except(bot.Quotes.Select(n => n.Key)).FirstOrDefault();
            if (e.Command.ArgumentsAsString == "")
            {
                rtn = new CommandResult { Successs = false, FailureReason = CommandResult.FailureReasonEnum.InvalidArguments, FailureMessage = "Please include a phrase to quote" };
            }
            else
            {
                bot.Quotes.Add(new KeyValuePair<int, string>(FirstAvailableIndex,e.Command.ArgumentsAsString));
                bot.Quotes = bot.Quotes;
            }
            return rtn;
        }
    }
}