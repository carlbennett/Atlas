﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Atlasd.Daemon
{
    class Common
    {
        public static Dictionary<string, dynamic> Settings { get; private set; }

        public static void Initialize()
        {
            Settings = new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);

            Settings.Add("account.auto_admin", true);
            Settings.Add("account.disallow_words", new List<string>()
            {
                "ass",
                "battle.net",
                "blizzard",
                "chink",
                "cracker",
                "cunt",
                "fag",
                "faggot",
                "fuck",
                "idiot",
                "nigga",
                "nigger",
                "niglet",
                "twat",
                "wetback",
            });
            Settings.Add("account.max_adjacent_punctuation", 0);
            Settings.Add("account.max_length", 15);
            Settings.Add("account.max_punctuation", 7);
            Settings.Add("account.min_alphanumeric", 1);
            Settings.Add("account.min_length", 3);
            Settings.Add("battlenet.listener.interface", "0.0.0.0");
            Settings.Add("battlenet.listener.port", 6112);
            Settings.Add("channel.auto_op", true);
            Settings.Add("channel.max_length", 31);
            Settings.Add("channel.max_users", 40);
        }
    }
}