using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit4.CollectionsLib;

namespace Piano
{
    internal class Song
    {
        public string name { get; set; }
        public Node<Note> melody { get; set; }

        public Song(string name, Node<Note> melody)
        {
            this.name = name;
            this.melody = melody;
        }

        


    }
}
