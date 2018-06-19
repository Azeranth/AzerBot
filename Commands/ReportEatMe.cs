using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class ReportEatMe : Command,ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            string message = "";
            List<KeyValuePair<string, int>> scores = bot.EatMeScores.Where(n=>n.Value > 0).ToList();
            scores = scores.OrderBy(n => n.Value).ToList();
            switch (scores.Count)
            {
                case 0:
                    message = "No one has any lucky pennies. The supreme devito rules all";
                    break;
                case 1:
                    message = $"Only {scores[0].Key} has any pennies, with {scores[0].Value} pennies";
                    break;
                case 2:
                    message = $"{scores[0].Key} is the richest with {scores[0].Value} pennies, and {scores[1].Key} is right behind with {scores[1].Value} pennies";
                    break;
                case 3:
                    message = $"The only three who have been eaten are {scores[0].Key} with {scores[0].Value} pennies, {scores[1].Key} with {scores[1].Value}, and {scores[2].Key} with {scores[2].Value}";
                    break;
                default:
                    message = $"The three richest people are {scores[0].Key} with {scores[0].Value} pennies, {scores[1].Key} with {scores[1].Value}, and {scores[2].Key} with {scores[2].Value}";
                    break;
            }
            bot.Client.SendMessage(bot.Channel, message);
            return rtn;
        }
    }
}