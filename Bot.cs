using System;
using System.Runtime.InteropServices;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using WindowsInput.Native;
using WindowsInput;
using System.Threading;

namespace TBot
{
    internal class Bot
    {
        ConnectionCredentials cred = new ConnectionCredentials(TwitchInfo.ChannelName, TwitchInfo.BotToken);
        TwitchClient client;
 
        char nl = Convert.ToChar(11);

        private string[] _bannedWords = new string[3] { "big follow", "retard", "js is best" };
        internal void Connect(bool isLogging)
        {
            client = new TwitchClient();
            client.Initialize(cred, TwitchInfo.ChannelName);
            client.OnConnected += Client_OnConnected;

            Console.WriteLine("[Bot]: Connecting...");

            if (isLogging)
                client.OnLog += Client_OnLog;

            client.OnError += Client_OnError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.AddChatCommandIdentifier('!');

            client.Connect();
            
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("[Bot]: Connected");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            
                
            

            Console.WriteLine($"[{e.ChatMessage.DisplayName}]: {e.ChatMessage.Message}");
        }

        private void Client_OnError(object sender, OnErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        InputSimulator sim = new InputSimulator();

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            
            switch (e.Command.CommandText.ToLower())
            {
                case "1к20":
                    string msg = $"{e.Command.ChatMessage.DisplayName} на кубике выпало {RndInt(1, 20)} ";
                    client.SendMessage(TwitchInfo.ChannelName, msg);
                    Console.WriteLine($"[Bot]: {msg}");
                    break;
                case "ссылки":
                    client.SendMessage(TwitchInfo.ChannelName, "Ссылки на мои соц сети! ");
                    break;
                    /* накатие клавиши на кравиатуре по команде в чате
                case "нажать":
                    sim.Keyboard.KeyPress(VirtualKeyCode.VK_Q);
                    break;*/
            }

            if (e.Command.ChatMessage.DisplayName == TwitchInfo.ChannelName)
            {
                switch (e.Command.CommandText.ToLower())
                {
                    case "Привет":
                        client.SendMessage(TwitchInfo.ChannelName, "Здравствуй");
                        
                        break;
                }
            }
        }

        private int RndInt(int min, int max)
        {
            int value;

            Random rnd = new Random();

            value = rnd.Next(min, max);

            return value;
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }

        internal void Disconnect()
        {
            client.Disconnect();
        }
    }
}