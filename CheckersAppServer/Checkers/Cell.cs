using System;
using System.Runtime.Serialization;

namespace Checkers
{
    /// <summary>
    /// Клетка доски
    /// </summary>
    [Serializable]
    [DataContract]
    public class Cell
    {
        /// <summary>
        /// Адрес ячейки
        /// </summary>
        [DataMember]
        public Address Address { get; set; }

        /// <summary>
        /// Состояние ячейки
        /// </summary>
        [DataMember]
        public State State { get; set; }

        /// <summary>
        /// Признак "дамки"
        /// </summary>
        [DataMember]
        public bool King { get; set; }
    }

}
