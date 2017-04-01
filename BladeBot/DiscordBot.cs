using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Discord.Audio;

namespace BladeBot
{
    public class DiscordBot
    {
        DiscordClient client;

        CommandService commands;

        public DiscordBot()
        {


            client = new DiscordClient(
                input =>
                {
                    input.LogLevel = LogSeverity.Info;
                    input.LogHandler = Log;
                }
            );

            client.UsingCommands(input =>
            {
                input.PrefixChar = '*' ;
                input.AllowMentionPrefix = true;
            });
            client.UsingAudio(input =>
            {
                input.Mode = AudioMode.Outgoing;
            });
            commands = client.GetService<CommandService>();

            commands.CreateCommand("Hello").Do(async (e) =>
            {
                await e.Channel.SendMessage("Hey whats good my man!");
            });

            commands.CreateCommand("Status").Do(async (e) =>
            {
                Status item = null;
                using (var client = new WebClient())
                {
                    var json = client.DownloadString("https://mcapi.ca/query/104.218.101.25:25565/players");
                    item = JsonConvert.DeserializeObject<Status>(json);
                }
                await e.Channel.SendMessage("Ping: " + item.ping.ToString());
                await e.Channel.SendMessage("Players online: " + item.players.online.ToString());
            });


            commands.CreateCommand("play").Parameter("link", ParameterType.Optional).Do(async (e) =>
            { 
                var voicechannel = client.FindServers(e.Server.Name).FirstOrDefault().VoiceChannels.FirstOrDefault();
                var test = await client.GetService<AudioService>().Join(voicechannel);
                await test.Join(voicechannel);
                await e.Channel.SendMessage(e.Args[0]);
            });

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("Mjk3NzQ5NTkwNTc5NjA5NjAx.C8Fe_w.EK4bmE5J4Ifbps8l1XwjS29cBcY", TokenType.Bot);
                client.SetGame(new Game("Use *Status"));
            });
            
        }

        private void Log(object sneder, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
