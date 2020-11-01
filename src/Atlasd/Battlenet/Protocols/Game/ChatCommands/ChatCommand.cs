﻿using Atlasd.Battlenet.Protocols.Game.ChatCommands;
using Atlasd.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atlasd.Battlenet.Protocols.Game
{
    class ChatCommand
    {
        public List<string> Arguments { get; protected set; }

        public ChatCommand(List<string> arguments)
        {
            Arguments = arguments;
        }

        public virtual bool CanInvoke(ChatCommandContext context)
        {
            return false;
        }

        public virtual void Invoke(ChatCommandContext context)
        {
            throw new NotSupportedException("Base ChatCommand class does not Invoke()");
        }

        public static ChatCommand FromByteArray(byte[] text)
        {
            return FromString(Encoding.UTF8.GetString(text[..^1]));
        }

        public static ChatCommand FromString(string text)
        {
            var args = new List<string>(text.Split(' '));

            var cmd = args[0];
            args.RemoveAt(0);

            switch (cmd)
            {
                case "admin":
                    return new AdminCommand(args);
                case "away":
                    return new AwayCommand(args);
                case "channel":
                case "join":
                case "j":
                    return new JoinCommand(args);
                case "help":
                case "?":
                    return new HelpCommand(args);
                case "ignore":
                case "squelch":
                    return new SquelchCommand(args);
                case "kick":
                    return new KickCommand(args);
                case "time":
                    return new TimeCommand(args);
                case "unignore":
                case "unsquelch":
                    return new UnsquelchCommand(args);
                case "whereis":
                case "where":
                case "whois":
                    return new WhereIsCommand(args);
                case "who":
                    return new WhoCommand(args);
                case "whoami":
                    return new WhoAmICommand(args);
                default:
                    return new InvalidCommand(args);
            }
        }
    }
}
