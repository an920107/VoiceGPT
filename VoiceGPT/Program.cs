using System;

namespace VoiceGPT {

    class Program {

        const string LANGUAGE = "zh-TW";

        static void Main(string[] args) {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true) {
                Console.Write("\nPress ENTER to start speaking...");
                Console.ReadLine();
                Console.Write("Say something: ");
                SpeechRecognition.RecognitionWithMicrophoneAsync(LANGUAGE).Wait();
                Console.WriteLine(SpeechRecognition.text);
                ChatGPT.Post(SpeechRecognition.text).Wait();
                Console.WriteLine(ChatGPT.responseText);
                SpeechSynthesis.SynthesisToSpeakerAsync(ChatGPT.responseText, LANGUAGE).Wait();
            }
        }
    }
}
