﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace homework01
{
    class CommentException : Exception
    {
        public CommentException(string message)
            : base(message)
        {
        }
    }
}
