using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Unit4.CollectionsLib;
using System.Speech.Synthesis;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace Piano
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Start Recording");

            Song song = Rec();
            Console.Beep();
            Console.WriteLine(song);
            Play(song);
        }

        public static Song Rec()
        {
            string name = Convert.ToString(DateTime.Now);
            Node<Note> mel = null;
            char key = 'a';

            while (key != 'r')
            {
                Note note = new Note(1, 1);
                ConsoleKeyInfo info = Console.ReadKey(true);
                key = info.KeyChar;
                Stopwatch stopWatch = new Stopwatch();
                note.c = key;
                stopWatch.Start();
                ConsoleKeyInfo temp = Console.ReadKey(true);
                stopWatch.Stop();
                note.ts = stopWatch.Elapsed;
                mel = new Node<Note>(note, mel);
            }
            Node<Note> melody = Reverse(mel);
            Song song = new Song(name, melody);
            return song;
        }

        public static Node<Note> Reverse(Node<Note> node)
        { 
            Node<Note> node2 = null;
            while(node.HasNext())
            {
                node2 = new Node<Note>(node.GetValue(), node2);
                node = node.GetNext();
            }
            node2 = new Node<Note>(node.GetValue(), node2);

            return node2;
        }

        public static void Play(Song song)
        {
            while (song.melody.HasNext())
            {
                song.melody.SetValue(Convert2(song.melody.GetValue()));
                Console.WriteLine(song.melody.GetValue().duration);
                Console.Beep(song.melody.GetValue().frequency, song.melody.GetValue().duration);
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string text = "Frequency (hz)" + (Convert.ToString(song.melody.GetValue().frequency)) + " | Duration: " + (Convert.ToString(song.melody.GetValue().duration)) + "ms";
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Recording.txt"), true))
                {
                    outputFile.WriteLine(text);
                }
                song.melody = song.melody.GetNext();
            }
        }

        public static Note Convert2(Note note)
        {
            Note n2 = new Note(1, 1);
            switch (note.c)
            {
                case '1':
                    n2.frequency = 233;
                    n2.name = "Bb";
                    break;
                case '2':
                    n2.frequency = 262;
                    n2.name = "C";
                    break;
                case '3':
                    n2.frequency = 294;
                    n2.name = "D";
                    break;
                case '4':
                    n2.frequency = 311;
                    n2.name = "Eb";
                    break;
                case '5':
                    n2.frequency = 349;
                    n2.name = "F";
                    break;
                case '6':
                    n2.frequency = 392;
                    n2.name = "G";
                    break;
                case '7':
                    n2.frequency = 440;
                    n2.name = "A";
                    break;
                case '8':
                    n2.frequency = 466;
                    n2.name = "Bb4";
                    break;
                case '9':
                    n2.frequency = 523;
                    n2.name = "C5";
                    break;
                case '0':
                    n2.frequency = 587;
                    n2.name = "D5";
                    break;
                case 'r':
                    n2.frequency = 233;
                    n2.name = "Bb";
                    break;
            }

            n2.duration = (note.ts.Milliseconds + (note.ts.Seconds / 1000));

            return n2;
        }
    }
}
