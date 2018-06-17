using System.Collections.Generic;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace AzerBot.Commands
{
    public class CommandConfiguration
    {
        public enum PermissionLevelEnum {Everyone, Moderator, Streamer, Follower, Subscriber}
        private string name = "";
        private bool isEnabled = true;
        private List<PermissionLevelEnum> permissionLevel = new List<PermissionLevelEnum>{PermissionLevelEnum.Everyone};

        public string Name { get => name; set => name = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public List<PermissionLevelEnum> PermissionLevel { get => permissionLevel; set => permissionLevel = value; }
    
        public bool Validate(OnChatCommandReceivedArgs e)
        {
            if(!PermissionLevel.Contains(PermissionLevelEnum.Everyone))
            {
                if(!((PermissionLevel.Contains(PermissionLevelEnum.Streamer)&&e.Command.ChatMessage.IsBroadcaster)||(PermissionLevel.Contains(PermissionLevelEnum.Moderator)&&e.Command.ChatMessage.IsModerator)||(PermissionLevel.Contains(PermissionLevelEnum.Subscriber)&&e.Command.ChatMessage.IsSubscriber)))
                {
                    return false;
                }
            }
            if(!IsEnabled)
            {
                return false;
            }
            return true;
        }
    }
}