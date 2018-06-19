using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class RemoveQueue :Command, ICommand
    {
        public CommandResult Run(Bot bot, OnChatCommandReceivedArgs e, CommandConfiguration config)
        {
            CommandResult rtn = new CommandResult();
            string target = e.Command.ChatMessage.Username;
            string message = "";
            List<KeyValuePair<string, int>> queue = bot.Queue;
            if (e.Command.ChatMessage.IsBroadcaster && e.Command.ArgumentsAsList.Any())
            {
                target = e.Command.ArgumentsAsString;
            }
            if (!queue.Any(n => n.Key == target))
            {
                message = $"{target} is not in the queue";
            }
            else
            {
                int index = queue.FirstOrDefault(n => n.Key == target).Value;
                queue.RemoveAll(n => n.Key == target);
                message = $"{target} was at position: {index} and has now been removed";
            }
            queue = queue.OrderBy(n => n.Value).ToList();
            for (int i = 0; i < queue.Count(); i++)
            {
                queue[i] = new KeyValuePair<string, int>(queue[i].Key, i);
            }
            bot.Client.SendMessage(bot.Channel, message);
            bot.Queue = queue;
            return rtn;
        }
    }
}