﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Atlasd.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Atlasd.Localization.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to {realm}!
        ///This server is hosted by {host}.
        ///There are currently {gameUsers} users playing {gameAds} games of {game}, and {totalUsers} users playing {totalGameAds} games on {realm}..
        /// </summary>
        public static string ChannelFirstJoinGreeting {
            get {
                return ResourceManager.GetString("ChannelFirstJoinGreeting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This channel does not have chat privileges..
        /// </summary>
        public static string ChannelIsChatRestricted {
            get {
                return ResourceManager.GetString("ChannelIsChatRestricted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That channel is full..
        /// </summary>
        public static string ChannelIsFull {
            get {
                return ResourceManager.GetString("ChannelIsFull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That channel is restricted..
        /// </summary>
        public static string ChannelIsRestricted {
            get {
                return ResourceManager.GetString("ChannelIsRestricted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That channel does not exist.
        ///(If you are trying to search for a user, use the /whois command.).
        /// </summary>
        public static string ChannelNotFound {
            get {
                return ResourceManager.GetString("ChannelNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That command is not available at the moment..
        /// </summary>
        public static string ChatCommandUnavailable {
            get {
                return ResourceManager.GetString("ChatCommandUnavailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed logon attempts since then: {count}.
        /// </summary>
        public static string FailedLogonAttempts {
            get {
                return ResourceManager.GetString("FailedLogonAttempts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chatting with this game is restricted to the channels listed in the channel menu..
        /// </summary>
        public static string GameProductIsChatRestricted {
            get {
                return ResourceManager.GetString("GameProductIsChatRestricted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid channel name..
        /// </summary>
        public static string InvalidChannelName {
            get {
                return ResourceManager.GetString("InvalidChannelName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to That is not a valid command. Type /help or /? for more info..
        /// </summary>
        public static string InvalidChatCommand {
            get {
                return ResourceManager.GetString("InvalidChatCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last logon: {timestamp}.
        /// </summary>
        public static string LastLogonInfo {
            get {
                return ResourceManager.GetString("LastLogonInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No one hears you..
        /// </summary>
        public static string NoOneHearsYou {
            get {
                return ResourceManager.GetString("NoOneHearsYou", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {realm} time: {realmTime}
        ///Your local time: {localTime}.
        /// </summary>
        public static string TimeCommand {
            get {
                return ResourceManager.GetString("TimeCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Users in channel {channel}:
        ///{users}.
        /// </summary>
        public static string WhoCommand {
            get {
                return ResourceManager.GetString("WhoCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are banned from that channel..
        /// </summary>
        public static string YouAreBannedFromThatChannel {
            get {
                return ResourceManager.GetString("YouAreBannedFromThatChannel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are {name}, using {game} in {realm}.
        /// </summary>
        public static string YouAreUsingGameInRealm {
            get {
                return ResourceManager.GetString("YouAreUsingGameInRealm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are {name}, using {game} in the channel {channel}..
        /// </summary>
        public static string YouAreUsingGameInTheChannel {
            get {
                return ResourceManager.GetString("YouAreUsingGameInTheChannel", resourceCulture);
            }
        }
    }
}
