﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public GameMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
