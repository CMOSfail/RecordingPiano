using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Piano
{
    internal class Note
    {
        public string name { get; set; } // A name for the note
        public int frequency { get; set; } // The frequency of the note
        public int duration { get; set; } // The duration of the note
        public char c { get; set; } // The character recorded of the note (to be converted later)
        public TimeSpan ts { get; set; } // The time span of the note (to be converted later)

        public Note(int frequency, int duration) // A constructor for the note
        {
            this.frequency = frequency;
            this.duration = duration;
        }

        
    }
}
