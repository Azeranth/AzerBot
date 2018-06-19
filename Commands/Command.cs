using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzerBot.Commands
{
    public abstract class Command
    {
        public bool Validate()
        {
            return true;
        }
    }
}
