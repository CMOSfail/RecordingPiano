using System; //Standard C# library
using System.Diagnostics; //For "StopWatch" to measure the length of notes during recording
using Unit4.CollectionsLib; //Unit4.dll for Node<T>
using System.Speech.Synthesis; //For TTS (Text to Speech)
using System.IO; //For the save file

namespace Piano
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synth02 = new SpeechSynthesizer(); //Start the TTS for the Main method

            Song song = Rec(); //Create a new song object and record it (calls for Rec method)

            Console.WriteLine($"Now playing {song.name}, by {song.artist}."); //Tell the user what's happening
            synth02.Speak($"Now playing {song.name}, by {song.artist}."); //TTS for the song name
            
            Play(song); //Play the song (after recording)

            Console.Write("Would you like to save this song? (Y/N)"); //Ask the user if they want to save the song
            synth02.Speak("Would you like to save this song?"); //TTS for the question

            char saveFile = Console.ReadKey().KeyChar; //Take input from the user
            Console.WriteLine(); //Enter a new line
            if (saveFile == 'Y' || saveFile == 'y') //If the user wants to save the song
            {
                Save(song); //Save the song

                Console.Write("Finished saving the song!"); //Tell the user of the current save status
                synth02.Speak("Goodbye!"); //TTS for goodbye
            }
            else //If the user doesn't want to save the song
            {
                Console.WriteLine("Okay, goodbye!"); //Tell the user goodbye
                synth02.Speak("Okay, goodbye!"); //TTS for goodbye
            }

            Console.WriteLine("Press 'Enter' to exit.");
            synth02.Speak("Press 'Enter' to exit.");

            char exit = Console.ReadKey(true).KeyChar; //Take input from the user

            while (exit != 13)
            {
                exit = Console.ReadKey(true).KeyChar; //Take input from the user
            }

        }
        

        public static Song Rec() //Record the song
        {
            SpeechSynthesizer synth = new SpeechSynthesizer(); //Start the TTS for the Rec method

            Console.WriteLine("Please input the name of the song, then press the Enter key to continue: "); //Request the name of the song from the user
            synth.Speak("Please input the name of the song, then press the Enter key to continue: "); //TTS the request
            string name = Console.ReadLine(); //Take input from the user
            Console.WriteLine("-----------------------------"); //Separate the input from the recording
            
            Console.WriteLine("Please input the name of the artist, then press the Enter key to continue: "); //Request the artist of the song from the user
            synth.Speak("Please input the name of the artist, then press the Enter key to continue: "); //TTS the request
            string artist = Console.ReadLine(); //Take input from the user
            Console.WriteLine("-----------------------------"); //Separate the input from the recording
            
            string date = Convert.ToString(DateTime.Now); //Get the current date and time

            Node<Note> mel = null; //create a new Node of type Note (from the Note class) called mel to be recorded on
            char key = 'a'; //Create a char variable to store the key pressed by the user

            Console.WriteLine("-----------------------------"); //Separate the instructions from the recording
            Console.Write("Would you like to hear the instructions? (Y/N): "); //Ask the user if they would like to hear the instructions
            synth.Speak("Would you like to hear the instructions? "); //TTS the request
            char instructions = Console.ReadKey().KeyChar; //Take input from the user
            Console.WriteLine(); //Start a new line
            if (instructions == 'Y' || instructions == 'y') //If the user wants to hear the instructions
            {
                Instructions(); //Announce the instructions using the instructions method
            }
            else
            {
                Console.WriteLine("-----------------------------"); //Separate the recording
            }

            Console.WriteLine("Shhhhhh! Now Recording..."); //Tell the user that the recording has started

            while (key != 'r') //Start the recording, as long as 'r' isn't pressed, keep on recording!
            {
                Note note = new Note(1, 1); //Create a base note to be modified
                ConsoleKeyInfo info = Console.ReadKey(true); //Get the pressed key from the user
                key = info.KeyChar; //Check which key was pressed
                Stopwatch stopWatch = new Stopwatch(); //Create a stopwatch to measure the length of the note (to determine his duration later)
                note.c = key; //Set the note's char (to determine its frequency later) to the key pressed by the user
                stopWatch.Start(); //Start the stopwatch
                ConsoleKeyInfo temp = Console.ReadKey(true); //wait for another keypress to stop the stopwatch
                stopWatch.Stop(); //Stop the stopwatch
                note.ts = stopWatch.Elapsed; //save the notes duration to the note's 'ts' (TimeSpan) var
                mel = new Node<Note>(note, mel); //add the recorded note to the melody (Node<Note> inside song)
            }
            Node<Note> melody = Reverse(mel); //beacuse a Node<T> is a LIFO (Last In First Out) structure, we need to reverse the order of the notes to play them in the correct order
            Song song = new Song(name, artist, melody, date); //Create a new song object with the recorded melody

            Console.WriteLine("That was wonderful! The recording was now finished, now lets get on to playing it..."); //Tell the user that the recording has started
            synth.Speak("That was wonderful! The recording was now finished, now lets get on to playing it..."); //TTS the message
            Console.WriteLine("-----------------------------"); //Separate the recording from the playing

            return song; //return the recorded song object
        }

        public static void Play(Song song) //Play the song
        {
            while (song.melody.HasNext()) //Start playing the song
            {
                song.melody.SetValue(Convert2Note(song.melody.GetValue())); //Convert the current note from the recording format to the Console.Beep() format
                Console.Beep(song.melody.GetValue().frequency, song.melody.GetValue().duration); //Beep the note
                song.melody = song.melody.GetNext(); //Get the next note of the song
            }
        }

        public static Note Convert2Note(Note note) //Convert the pressed char into a frequency
        {
            Note n2 = new Note(1, 1); //Create a new note to be returned modified
            switch (note.c) //Check which key was pressed
            {
                case '1': //If the key was '1'
                    n2.frequency = 233; //Set to this frequency
                    n2.name = "Bb"; //Give it this name
                    break;
                case '2': //If the key was '2'
                    n2.frequency = 262; //Set to this frequency
                    n2.name = "C"; //Give it this name
                    break;
                case '3': //If the key was '3'
                    n2.frequency = 294; //Set to this frequency
                    n2.name = "D"; //Give it this name
                    break;
                case '4':  //If the key was '4'
                    n2.frequency = 311; //Set to this frequency
                    n2.name = "Eb"; //Give it this name
                    break;
                case '5': //If the key was '5'
                    n2.frequency = 349; //Set to this frequency
                    n2.name = "F"; //Give it this name
                    break;
                case '6': //If the key was '6'
                    n2.frequency = 392; //Set to this frequency
                    n2.name = "G"; //Give it this name
                    break;
                case '7': //If the key was '7'
                    n2.frequency = 440; //Set to this frequency
                    n2.name = "A"; //Give it this name
                    break;
                case '8': //If the key was '8'
                    n2.frequency = 466; //Set to this frequency
                    n2.name = "Bb4"; //Give it this name
                    break;
                case '9': //If the key was '9' 
                    n2.frequency = 523; //Set to this frequency
                    n2.name = "C5"; //Give it this name
                    break;
                case '0': //If the key was '0'
                    n2.frequency = 587; //Set to this frequency
                    n2.name = "D5"; //Give it this name
                    break;
                case 'r': //If the key was 'r'
                    n2.frequency = 233; //Set to this frequency (so the software wont crash)
                    n2.name = "Recording stopped here."; //Give it this description
                    break;
                default: //If any other key was pressed
                    n2.frequency = 233; //Set to this frequency (so the software wont crash)
                    n2.name = "Wrong key was pressed"; //Give it this description (so the software wont crash)
                    break;
            }

            n2.duration = (note.ts.Milliseconds + (note.ts.Seconds / 1000)); //Set the note's duration to the recorded duration ( Console.Beep() needs the duration in milliseconds, so wee add the recorded seconds converted to ms to the recorded milliseconds)

            return n2; //Return the modified note
        }
    
        public static void Save(Song song) //Save the song to a file
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Set the save file path to the user's Documents folder
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{song.name}.txt"), true)) //Use StreamWriter to create a file and write to it
            {
                outputFile.WriteLine($"{song.name} by {song.artist}  |  {song.date}"); //Write the song name, artist and date to the file's first line
            }
            while (song.melody.HasNext()) //Start playing the song
            {
                song.melody.SetValue(Convert2Note(song.melody.GetValue())); //Convert the current note from the recording format to the Console.Beep() format
                string text = "Frequency: " + (Convert.ToString(song.melody.GetValue().frequency)) + "hz " + $" ({song.melody.GetValue().name}) " + " | Duration: " + (Convert.ToString(song.melody.GetValue().duration)) + "ms"; //Save the note parameters
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, $"{song.name}.txt"), true)) //Use StreamWriter to add the note parameters to the file
                {
                    outputFile.WriteLine(text); //Write the note parameters to the file in a new line
                }
                song.melody = song.melody.GetNext(); //Get the next note of the song
            }
        }
        
        public static void Instructions()
        {
            SpeechSynthesizer synth01 = new SpeechSynthesizer(); //Start the TTS for the Instructions method

            synth01.Speak("The recording is about to start."); //Announce that untill now everything is working well, recording will soon start.
            Console.WriteLine("Use the numbers at the top of your keyboard to input the notes. Apart from the first note, all other notes need to be pressed 2 times (double-press)."); //Tell the user what keys to press to record the notes
            synth01.Speak("Use the numbers at the top of your keyboard to input the notes. Apart from the first note, all other notes need to be pressed 2 times (double-press)."); //TTS the instructions
            Console.WriteLine("The notes are ranging from Bb3 (1), to D5 (0). Any other key may crash the software"); //Tell the user which keys to press to record the notes
            synth01.Speak("The notes are ranging from B-flat at 1, to high D at 0. Please note that any other key may crash the software"); //TTS the instructions
            Console.WriteLine("Press the 'r' key to stop recording (Please notice that you may need to press this key multiple times)"); //Tell the user to press the 'r' key to stop recording
            synth01.Speak("Press the 'r' key to stop recording. Please notice that you may need to press this key multiple times"); //TTS the instructions
            synth01.Speak("The recording has now started, Have fun!"); //Announce that recording has started.
            Console.WriteLine("-----------------------------"); //Separate the instructions from the recording

        }

        public static Node<Note> Reverse(Node<Note> node) //Reverse the order of the Node<Note> list
        {
            Node<Note> node2 = null; //Create a new Node<Note> list to be modified
            while (node.HasNext()) //As long as the original Node<Note> list has a next node
            {
                node2 = new Node<Note>(node.GetValue(), node2); //Put the last node of the original list to the first node of the new list
                node = node.GetNext(); //Set the original list to its next node
            }
            node2 = new Node<Note>(node.GetValue(), node2); //beacuse the loop stops when the original list has no next node, we need to add the last node of the original list to the new list

            return node2; //return the reversed list
        }

    }
}
