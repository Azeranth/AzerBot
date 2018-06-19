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
    public class Bot
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
            quotes = JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(quotesRaw);
            eatMeScores = JsonConvert.DeserializeObject<List<KeyValuePair<string,int>>>(eatMeScoresRaw);
            //Check if any of the loaded json is null, and instantiates it if it is
            if(commandConfigurations == null) {commandConfigurations = new List<CommandConfiguration>();}
            if(quotes == null) {quotes = new List<KeyValuePair<int, string>>();}
            if(eatMeScores == null) {eatMeScores = new List<KeyValuePair<string, int>>();}
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
        private string channel = "azeranth" +
            "";
        private List<CommandConfiguration> commandConfigurations;
        private string commandConfigurationsRaw;
        private List<KeyValuePair<int, string>> quotes;
        private string quotesRaw = "";
        private List<KeyValuePair<string, int>> eatMeScores;
        private string eatMeScoresRaw = "";
        private DateTime eatMeTime = DateTime.Now;
        private List<KeyValuePair<string ,int>> queue= new List<KeyValuePair<string, int>>();
        #endregion

        #region Properties
        public List<CommandConfiguration> CommandConfigurations
        {
            get
            {
                return commandConfigurations;
            }
            set
            {
                commandConfigurations = value;
                commandConfigurations.SaveObject($"{path}\\Data\\CommandConfigurations.json");
            }
        }
        public List<KeyValuePair<int, string>> Quotes
        {
            get
            {
                return quotes;
            }
            set
            {
                quotes = value;
                quotes.SaveObject($"{path}\\Data\\Quotes.json");
            }
        }
        public List<KeyValuePair<string, int>> EatMeScores
        {
            get
            {
                return eatMeScores;
            }
            set
            {
                eatMeScores = value;
                eatMeScores.SaveObject($"{path}\\Data\\EatMeScores.json");
            }
        }
        public DateTime EatMeTime { get => eatMeTime; set => eatMeTime = value; }
        public List<KeyValuePair<string, int>> Queue
        {
            get { return queue; }
            set
            {
                queue = value;
                Console.Clear();
                Console.WriteLine(ReportQueue.PrintQueue(queue));
            }
        }
        public TwitchClient Client { get => client; set => client = value; }
        public string Channel { get => channel; set => channel = value; }
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
            Console.WriteLine($"Attempting to join {Channel}");
            Client.JoinChannel(Channel);
        }
        private void OnJoined(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Joined {e.Channel}");
            Client.SendMessage(Channel, $"Greetings, I am AzerBot");
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
            ICommand command;
            string CommandPhrase = e.Command.CommandText.ToLower();
            CommandConfiguration config = new CommandConfiguration();
            CommandResult result;
            if (CommandConfigurations.Any(n => n.Name == CommandPhrase))
            {
                config = CommandConfigurations.FirstOrDefault(n => n.Name.ToLower() == CommandPhrase);
            }
            switch (CommandPhrase)
            {
                case "greetings":
                case "hello":
                case "hi":
                    command = new Greetings();
                    result = command.Run(this, e, config);
                    break;
                case "addquote":
                    command = new AddQuote();
                    result = command.Run(this, e, config);
                    break;
                case "quote":
                    command = new ReportQuote();
                    result = command.Run(this, e, config);
                    break;
                case "removequote":
                case "deletequote":
                    command = new DeleteQuote();
                    result = command.Run(this, e, config);
                    break;
                case "eatme":
                    command = new EatMe();
                    result = command.Run(this, e, config);
                    break;
                case "eatmescores":
                case "reporteatme":
                    command = new ReportEatMe();
                    result = command.Run(this, e, config);
                    break;
                case "queueme":
                case "joinqueue":
                case "addqueue":
                    command = new AddQueue();
                    result = command.Run(this, e, config);
                    break;
                case "leavequeue":
                case "removequeue":
                    command = new RemoveQueue();
                    result = command.Run(this, e, config);
                    break;
                case "queue":
                case "reportqueue":
                    command = new ReportQueue();
                    result = command.Run(this, e, config);
                    break;
                case "delayme":
                case "delayqueue":
                    command = new DelayQueue();
                    result = command.Run(this, e, config);
                    break;
                default:
                    Client.SendMessage(Channel, $"Unrecognized command \"{e.Command.CommandText}\" type !commands to see a list of commands");
                return;
            }
            if(!result.Successs)
            {
                Client.SendMessage(Channel, result.FailureMessage);
                Console.WriteLine($"Failure {result.FailureReason.ToString()} occured while executing {e.Command.CommandText}. Failure Message: \n {result.FailureMessage}");
            }
        }
        #endregion
    }
}