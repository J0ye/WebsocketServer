using System;
using System.Collections.Generic;
using System.Text;

namespace WebsocketServer
{
    public sealed class global
    {
        // Singelton
        private static readonly global instance = new global();

        static global() { }

        public global() { }

        public static global Instance()
        {
            return instance;
        }

        // global data
        public int g_ChatMember = 0;
    }
}
