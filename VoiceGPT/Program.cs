using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace VoiceGPT {

    class Program {

        const string LANGUAGE = "zh-TW";
        const string GPT_PREFIX = "請用情侶的口吻、親密的語氣回覆我。";

        static void Main(string[] args) {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true) {
                Console.Write("\nPress ENTER to start speaking...");
                Console.ReadLine();
                Console.Write("Say something: ");
                SpeechRecognition.RecognitionWithMicrophoneAsync(LANGUAGE).Wait();
                if (SpeechRecognition.text == null)
                    continue;
                Console.WriteLine(SpeechRecognition.text);
                CommandDetection.Post(SpeechRecognition.text).Wait();
                Command command = CommandDetection.command;
                string responseText;
                if (command == Command.None) {
                    ChatGPT.Post(GPT_PREFIX + SpeechRecognition.text).Wait();
                    responseText = ChatGPT.responseText;
                }
                else {
                    CommandExecution.Run(command).Wait();
                    responseText = CommandExecution.responseText;
                }
                Console.WriteLine(responseText);
                SpeechSynthesis.SynthesisToSpeakerAsync(responseText, LANGUAGE).Wait();
            }
        }
    }
}
