using System;

namespace CheckersAppServer
{
    public partial class CheckersService : ICheckersService
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
