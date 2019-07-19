using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PacMan
{
    /// <summary>
    /// Logging interface that is testable
    /// </summary>
    interface ILoggable
    {
        bool _debug { get; }
        void Log(string s);
    }
}
