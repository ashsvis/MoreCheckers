using Checkers;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CheckersAppServer
{
    [DataContract]
    public struct GameStatus
    {
        [DataMember]
        public bool Exists { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string PlayerName { get; set; }
        [DataMember]
        public WinPlayer WinPlayer { get; set; }
        [DataMember]
        public Player Player { get; set; }
        [DataMember]
        public bool Direction { get; set; }
        [DataMember]
        public int BlackScore { get; set; }
        [DataMember]
        public int WhiteScore { get; set; }
        [DataMember]
        public List<LogItem> Log { get; set; }
    }
}
