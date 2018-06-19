using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class DelayQueue :Command, ICommand
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
                queue.RemoveAll(n => n.Key == target);
                queue.Add(new KeyValuePair<string, int>(target, queue.Count));
                for (int i = 0; i < queue.Count(); i++)
                {
                    queue[i] = new KeyValuePair<string, int>(queue[i].Key, i);
                }
                message = $"{target} has been pushed to the back of the queue, at position {queue.FirstOrDefault(n => n.Key == target).Value}";
            }
            bot.Client.SendMessage(bot.Channel, message);
            bot.Queue = queue;
            return rtn;
        }
    }
}