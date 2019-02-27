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
        public WinPlayer WinPlayer { get; set; }
    }
}
