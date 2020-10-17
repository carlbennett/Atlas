﻿using Atlasd.Battlenet.Exceptions;
using Atlasd.Daemon;
using System;
using System.IO;
using System.Text;

namespace Atlasd.Battlenet.Protocols.Game.Messages
{
    class SID_ENTERCHAT : Message
    {

        public SID_ENTERCHAT()
        {
            Id = (byte)MessageIds.SID_ENTERCHAT;
            Buffer = new byte[0];
        }

        public SID_ENTERCHAT(byte[] buffer)
        {
            Id = (byte)MessageIds.SID_ENTERCHAT;
            Buffer = buffer;
        }

        public override bool Invoke(MessageContext context)
        {
            switch (context.Direction)
            {
                case MessageDirection.ClientToServer:
                    {
                        Logging.WriteLine(Logging.LogLevel.Debug, Logging.LogType.Client_Game, context.Client.RemoteEndPoint, "[" + Common.DirectionToString(context.Direction) + "] SID_ENTERCHAT (" + (4 + Buffer.Length) + " bytes)");

                        if (Buffer.Length < 2)
                            throw new GameProtocolViolationException(context.Client, "SID_ENTERCHAT buffer must be at least 2 bytes");

                        /**
                         * (STRING) Username
                         * (STRING) Statstring
                         */

                        var m = new MemoryStream(Buffer);
                        var r = new BinaryReader(m);

                        context.Client.GameState.OnlineName = r.ReadString();
                        context.Client.GameState.Statstring = Encoding.ASCII.GetBytes(r.ReadString());

                        var productId = (UInt32)context.Client.GameState.Product;

                        if (context.Client.GameState.Statstring.Length == 0)
                        {
                            context.Client.GameState.Statstring = new byte[4];

                            var _m = new MemoryStream(context.Client.GameState.Statstring);
                            var _w = new BinaryWriter(_m);

                            _w.Write(productId);

                            _w.Close();
                            _m.Close();
                        }

                        // Statstring length is between 4-128 bytes, not including the null-terminator.
                        if (context.Client.GameState.Statstring.Length < 4 || context.Client.GameState.Statstring.Length > 128)
                            throw new GameProtocolViolationException(context.Client, "Client sent invalid statstring size in SID_ENTERCHAT");

                        if (context.Client.GameState.Statstring.Length >= 4)
                        {
                            var _m = new MemoryStream(context.Client.GameState.Statstring);
                            var _r = new BinaryReader(_m);

                            if (_r.ReadUInt32() != productId)
                                throw new GameProtocolViolationException(context.Client, "Client attempted to set different product id in statstring");

                            _m.Close();
                            _r.Close();
                        }

                        r.Close();
                        m.Close();

                        return new SID_ENTERCHAT().Invoke(new MessageContext(context.Client, MessageDirection.ServerToClient));
                    }
                case MessageDirection.ServerToClient:
                    {
                        /**
                         * (STRING) Unique name
                         * (STRING) Statstring
                         * (STRING) Account name
                         */

                        Buffer = new byte[3 + context.Client.GameState.OnlineName.Length + context.Client.GameState.Statstring.Length + context.Client.GameState.Username.Length];

                        var m = new MemoryStream(Buffer);
                        var w = new BinaryWriter(m);

                        w.Write((string)context.Client.GameState.OnlineName);
                        w.Write((string)Encoding.ASCII.GetString(context.Client.GameState.Statstring));
                        w.Write((string)context.Client.GameState.Username);

                        w.Close();
                        m.Close();

                        lock (context.Client.GameState)
                        {
                            context.Client.GameState.ChannelFlags = (Account.Flags)context.Client.GameState.ActiveAccount.Get(Account.FlagsKey);
                        }

                        Logging.WriteLine(Logging.LogLevel.Debug, Logging.LogType.Client_Game, context.Client.RemoteEndPoint, "[" + Common.DirectionToString(context.Direction) + "] SID_ENTERCHAT (" + (4 + Buffer.Length) + " bytes)");
                        context.Client.Send(ToByteArray());
                        return true;
                    }
            }

            return false;
        }
    }
}
