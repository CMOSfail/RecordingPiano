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
        public string name { get; set; }
        public int frequency { get; set; }
        public int duration { get; set; }
        public char c { get; set; }
        public TimeSpan ts { get; set; }

        public Note(int frequency, int duration)
        {
            this.frequency = frequency;
            this.duration = duration;
        }

        public static void Beep(Note note)
        {
            Console.Beep(note.frequency, note.duration);
        }

        
    }
}
