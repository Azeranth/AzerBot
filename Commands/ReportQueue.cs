using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class ReportQueue : Command,ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            string sender = e.Command.ChatMessage.Username;
            string message = "";
            List<KeyValuePair<string, int>> queue = bot.Queue;
            if(!queue.Any())
            {
                message = $"{sender} the queue is currently empty";
            }
            else
            {
                message = $"{sender} the current queue is: ";
                foreach (var member in queue.OrderBy(n=>n.Value))
                {
                    message += $"{member.Key}, ";
                }
                message = message.Remove(message.Length - 2);
            }
            bot.Client.SendMessage(bot.Channel, message);
            return rtn;
        }
        public static string PrintQueue(List<KeyValuePair<string, int>> input)
        {
            string rtn = "";
            StringBuilder builder = new StringBuilder();
            foreach (var member in input.OrderBy(n=>n.Value))
            {
                builder.Append(member.Key);
                builder.Append("".PadRight(32 - member.Key.Length, ' '));
                builder.Append($"|   {member.Value}\n");
            }
            rtn = builder.ToString();
            return rtn;
        }
    }
}