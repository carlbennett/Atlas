﻿using System;
using System.Collections.Generic;
using System.Net;

namespace Atlasd.Battlenet.Protocols.Game
{
    class GameState : IDisposable
    {
        public enum LogonTypes : UInt32
        {
            OLS = 0,
            NLSBeta = 1,
            NLS = 2,
        };

        public ClientState Client { get; protected set; }

        public Account ActiveAccount;
        public Channel ActiveChannel;
        public Account.Flags ChannelFlags;
        public DateTime ConnectedTimestamp;
        public List<GameKey> GameKeys;
        public IPAddress LocalIPAddress;
        public LocaleInfo Locale;
        public LogonTypes LogonType;
        public DateTime PingDelta;
        public Platform.PlatformCode Platform;
        public Product.ProductCode Product;
        public VersionInfo Version;

        public UInt32 ClientToken;
        public string KeyOwner;
        public string OnlineName;
        public Int32 Ping;
        public UInt32 PingToken;
        public UInt32 ProtocolId;
        public UInt32 ServerToken;
        public bool SpawnKey;
        public byte[] Statstring;
        public Int32 TimezoneBias;
        public bool UDPSupported;
        public UInt32 UDPToken;
        public string Username;

        public GameState(ClientState client)
        {
            var r = new Random();

            Client = client;

            ActiveAccount = null;
            ActiveChannel = null;
            ChannelFlags = Account.Flags.None;
            ConnectedTimestamp = DateTime.Now;
            GameKeys = new List<GameKey>();
            LocalIPAddress = null;
            Locale = new LocaleInfo();
            LogonType = LogonTypes.OLS;
            PingDelta = DateTime.Now;
            Platform = Battlenet.Platform.PlatformCode.None;
            Product = Battlenet.Product.ProductCode.None;
            Version = new VersionInfo();

            ClientToken = 0;
            KeyOwner = null;
            OnlineName = null;
            Ping = -1;
            PingToken = (uint)r.Next(0, 0x7FFFFFFF);
            ProtocolId = 0;
            ServerToken = (uint)r.Next(0, 0x7FFFFFFF);
            SpawnKey = false;
            Statstring = new byte[4];
            TimezoneBias = 0;
            UDPSupported = false;
            UDPToken = (uint)r.Next(0, 0x7FFFFFFF);
            Username = null;
        }

        public void Dispose() /* part of IDisposable */
        {
            if (ActiveAccount != null)
            {
                lock (ActiveAccount)
                {
                    ActiveAccount.Set(Account.LastLogoffKey, DateTime.Now);

                    var timeLogged = (UInt32)ActiveAccount.Get(Account.TimeLoggedKey);
                    var diff = DateTime.Now - ConnectedTimestamp;
                    timeLogged += (UInt32)Math.Round(diff.TotalSeconds);
                    ActiveAccount.Set(Account.TimeLoggedKey, timeLogged);

                    var username = (string)ActiveAccount.Get(Account.UsernameKey);
                    if (Battlenet.Common.ActiveAccounts.ContainsKey(username))
                        Battlenet.Common.ActiveAccounts.Remove(username);
                }
            }

            if (ActiveChannel != null)
            {
                lock (ActiveChannel) ActiveChannel.RemoveUser(this);
                ActiveChannel = null;
            }
        }
    }
}
