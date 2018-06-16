using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TwitchLib.Client.Events;
using AzerBot.Commands;

namespace AzerBot
{
    class Bot
    {
        #region Constructors
        public Bot()
        {
            //Loads the path used to find all files
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //Loads in the JSON for stored info
            commandConfigurationsRaw = File.ReadAllText($"{path}\\Data\\CommandConfigurations.json");
            quotesRaw = File.ReadAllText($"{path}\\Data\\Quotes.json");
            eatMeScoresRaw = File.ReadAllText($"{path}\\Data\\EatMeScores.json");
            //Converts the JSON into their proper forms
            commandConfigurations = JsonConvert.DeserializeObject<List<CommandConfiguration>>(commandConfigurationsRaw);
            quotes = JsonConvert.DeserializeObject<List<KeyValuePair<float, string>>>(quotesRaw);
            eatMeScores = JsonConvert.DeserializeObject<List<KeyValuePair<float,string>>>(eatMeScoresRaw);
            //Check if any of the loaded json is null, and instantiates it if it is
            if(commandConfigurations == null) {commandConfigurations = new List<CommandConfiguration>();}
            if(quotes == null) {quotes = new List<KeyValuePair<float, string>>();}
            if(eatMeScores == null) {eatMeScores = new List<KeyValuePair<float, string>>();}
            //Loads the Credentials of the bot
            credentials = new ConnectionCredentials(twitchUsername: "AzerBot", twitchOAuth: "7tlb2gedqia143iguq399ub4rtsvb5");
            //Set The Events for the bot
            client.OnLog += OnLog;
            client.OnConnected += OnConnected;
            client.OnJoinedChannel += OnJoined;
            client.OnMessageReceived += OnMessageReceived;
            client.OnChatCommandReceived += OnChatCommandReceived;
        }
        #endregion

        #region Fields
        private string path = "";
        private TwitchClient client = new TwitchClient();
        private ConnectionCredentials credentials;
        private string channel = "azeranth";
        private List<CommandConfiguration> commandConfigurations;
        private string commandConfigurationsRaw;
        private List<KeyValuePair<float, string>> quotes;
        private string quotesRaw = "";
        private List<KeyValuePair<float, string>> eatMeScores;
        private string eatMeScoresRaw = "";
        #endregion

        #region Properties
        public List<CommandConfiguration> CommandConfigurations { get => commandConfigurations; set => commandConfigurations = value; }
        public List<KeyValuePair<float, string>> Quotes { get => quotes; set => quotes = value; }
        public List<KeyValuePair<float, string>> EatMeScores { get => eatMeScores; set => eatMeScores = value; }
        public TwitchClient Client { get => client; set => client = value; }
        #endregion

        #region Methods
        #region Connection Events
        public void Connect()
        {
            Console.WriteLine("Attempting to connect...");
            Client.Initialize(credentials);
            Client.Connect();
        }

        private void OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Conneciton Successful");
            Console.WriteLine($"Attempting to join {channel}");
            Client.JoinChannel(channel);
        }
        private void OnJoined(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Joined {e.Channel}");
            Client.SendMessage(channel, $"Greetings, I am AzerBot");
        }
        #endregion

        private void OnLog(object sender, OnLogArgs e)
        {
            //Console.WriteLine(e.Data);
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {

        }

        private void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            string CommandPhrase = e.Command.CommandText.ToLower();
            CommandConfiguration config = CommandConfigurations.FirstOrDefault(n=>n.Name.ToLower() == CommandPhrase);
            if(!config.Validate(e))
            {
                return;
            }
            switch (CommandPhrase)
            {
                case "greetings":
                case "hello":
                case "hi":
                    Greetings command = new Greetings();
                    command.Run(client, e, config);
                break;
                default:
                break;
            }
        }
        #endregion
    }
}