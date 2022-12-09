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
        public string name { get; set; } // A name for the song
        public string artist { get; set; } // A name for the artist

        public string date { get; set; } // The time of recording of this song

        public Node<Note> melody { get; set; } // The melody of this song

        public Song(string name, string artist, Node<Note> melody, string date) // A constructor for the song
        {
            this.name = name;
            this.artist = artist;
            this.date = date;
            this.melody = melody;
        }

    }
}
