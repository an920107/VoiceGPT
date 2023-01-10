using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGPT {
    class SpeechSynthesis {
        public static async Task SynthesisToSpeakerAsync() {
            while (true) {
                // Receives a text from console input and synthesize it to speaker.
                Console.WriteLine("Enter some text that you want to speak, or enter empty text to exit.");
                Console.Write("> ");
                string text = Console.ReadLine();
                if (string.IsNullOrEmpty(text)) {
                    break;
                }
                await SynthesisToSpeakerAsync(text, "en-US");
            }
        }

        public static async Task SynthesisToSpeakerAsync(string text) {
            await SynthesisToSpeakerAsync(text, "en-US");
        }

        public static async Task SynthesisToSpeakerAsync(string text, string language) {

            if (string.IsNullOrEmpty(text))
                return;

            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription("c7cbf01e97b141c28ec68e59cac7ab92", "westus");
            config.SpeechSynthesisLanguage = language;
            // Note: the voice setting will not overwrite the voice element in input SSML.
            config.SpeechSynthesisVoiceName = "zh-CN-XiaoshuangNeural";

            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(config)) {
                using (var result = await synthesizer.SpeakTextAsync(text)) {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted) {
                        //Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled) {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error) {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }
            }
        }
    }
}
