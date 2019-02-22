using System;
using System.Drawing;

namespace Checkers
{
    /// <summary>
    /// Адрес ячейки
    /// </summary>
    [Serializable]
    public struct Address
    {
        const string chars = "abcdefgh";

        public char Column { get; set; }
        public int Row { get; set; }

        public Address(int x = -1, int y = -1)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                Row = 7 - y + 1;
                Column = chars.ToCharArray()[x];
            }
            else
            {
                Column = '\0';
                Row = 0;
            }
        }

        public Address(string addr)
        {
            int row;
            if (!string.IsNullOrWhiteSpace(addr) && addr.Length == 2 &&
                chars.IndexOf(addr[0]) >= 0 && int.TryParse(addr[1].ToString(), out row) &&
                row > 0 && row <= 8)
            {
                Column = addr[0];
                Row = row;
            }
            else
            {
                Column = '\0';
                Row = 0;
            }
        }

        public bool IsEmpty()
        {
            return Column == '\0' && Row == 0;
        }

        public Point Coords
        {
            get { return new Point(chars.IndexOf(Column), 7 - (Row - 1)); }
        }

        public override string ToString()
        {
            return IsEmpty() ? "empty" : string.Format("{0}{1}", Column, Row);
        }
    }

}
