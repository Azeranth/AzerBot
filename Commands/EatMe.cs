using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class EatMe : ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            List<KeyValuePair<string, int>> scores = bot.EatMeScores;
            string message = "";
            string sender = e.Command.ChatMessage.Username;
            int gain = new Random().Next(1, 5);
            if (DateTime.Now > bot.EatMeTime)
            {
                if (!scores.Any(n => n.Key == sender))
                {
                    scores.Add(new KeyValuePair<string, int>(sender, 0));
                }
                int val = scores.FirstOrDefault(n => n.Key == sender).Value + gain;
                scores[scores.FindIndex(n => n.Key == sender)] = new KeyValuePair<string, int>(sender, val);
                message = $"{sender} has been eaten, gaining {gain} lucky pennies. They now have {scores.FirstOrDefault(n=>n.Key == sender).Value} lucky pennies";                
            }
            else
            {
                if (!scores.Any(n => n.Key == sender))
                {
                    message = $"{sender} wasn't even playing, and they still lost. FeelsBadMan";
                }
                else
                {
                    if (scores.FirstOrDefault(n => n.Key == sender).Value == 0)
                    {
                        message = $"{sender} just keeps losing, better luck next time :(";
                    }
                    else
                    {
                        message = $"{sender} got greedy, and has lost {scores.FirstOrDefault(n=>n.Key == sender).Value} lucky pennies LUL";
                        scores[scores.FindIndex(n => n.Key == sender)] = new KeyValuePair<string, int>(sender, 0);
                    }
                }
            }
            bot.EatMeTime = DateTime.Now + new TimeSpan(hours: 0, minutes: new Random().Next(5, 10), seconds: new Random().Next(60));
            
            bot.EatMeScores = scores;
            bot.Client.SendMessage(bot.Channel, message);
            return rtn;
        }
    }
}