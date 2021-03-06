﻿using Atlasd.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlasd.Battlenet.Protocols.Game.ChatCommands
{
    class AdminCommand : ChatCommand
    {
        public AdminCommand(byte[] rawBuffer, List<string> arguments) : base(rawBuffer, arguments) { }

        public override bool CanInvoke(ChatCommandContext context)
        {
            return context != null && context.GameState != null && context.GameState.ActiveAccount != null;
        }

        public override void Invoke(ChatCommandContext context)
        {
            var hasSudo = false;

            lock (context.GameState)
            {
                hasSudo = context.GameState.ChannelFlags.HasFlag(Account.Flags.Admin)
                    || context.GameState.ChannelFlags.HasFlag(Account.Flags.Employee);
            }

            if (!hasSudo)
            {
                new InvalidCommand(RawBuffer, Arguments).Invoke(context);
                return;
            }

            string cmd;

            if (Arguments.Count == 0)
            {
                cmd = "";
            }
            else
            {
                cmd = Arguments[0];
                Arguments.RemoveAt(0);
            }

            // Calculates and removes (cmd+' ') from (raw) which prints into (newRaw):
            RawBuffer = RawBuffer[(Encoding.UTF8.GetByteCount(cmd) + (Arguments.Count > 0 ? 1 : 0))..];

            string r;

            switch (cmd.ToLower())
            {
                case "announce":
                case "broadcast":
                    new AdminBroadcastCommand(RawBuffer, Arguments).Invoke(context); return;
                case "disconnect":
                case "dc":
                    new AdminDisconnectCommand(RawBuffer, Arguments).Invoke(context); return;
                case "help":
                case "?":
                    r = string.Join(Battlenet.Common.NewLine, new List<string>() {
                        { "/admin ? (alias: /admin help)" },
                        { "/admin announce (alias: /admin broadcast)" },
                        { "/admin broadcast <message>" },
                        { "/admin dc (alias: /admin disconnect)" },
                        { "/admin disconnect <user> [reason]" },
                        { "/admin help (this text)" },
                        { "/admin move (alias: /admin moveuser)" },
                        { "/admin moveuser <user> <channel>" },
                        { "/admin shutdown [seconds] [message]" },
                        { "/admin spoofuserflag (alias: /admin spoofuserflags)" },
                        { "/admin spoofuserflags <user> <flags>" },
                        { "/admin spoofusergame <user> <game>" },
                        { "/admin spoofusername <oldname> <newname>" },
                        { "/admin spoofuserping <user> <ping>" },
                        { "" },
                    });
                    break;
                case "moveuser":
                case "move":
                    new AdminMoveUserCommand(RawBuffer, Arguments).Invoke(context); return;
                case "reload":
                    new AdminReloadCommand(RawBuffer, Arguments).Invoke(context); return;
                case "shutdown":
                    new AdminShutdownCommand(RawBuffer, Arguments).Invoke(context); return;
                case "spoofuserflag":
                case "spoofuserflags":
                    new AdminSpoofUserFlagsCommand(RawBuffer, Arguments).Invoke(context); return;
                default:
                    r = "That is not a valid admin command. Type /admin help or /admin ? for more info.";
                    break;
            }

            foreach (var kv in context.Environment)
            {
                r = r.Replace("{" + kv.Key + "}", kv.Value);
            }

            foreach (var line in r.Split(Battlenet.Common.NewLine))
                new ChatEvent(ChatEvent.EventIds.EID_INFO, context.GameState.ChannelFlags, context.GameState.Ping, context.GameState.OnlineName, line).WriteTo(context.GameState.Client);
        }
    }
}
