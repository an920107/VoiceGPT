using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGPT {
    public class SpeechRecognition {

        public static string text { get; private set; }

        public static async Task RecognitionWithMicrophoneAsync() {
            await RecognitionWithMicrophoneAsync("en-US");
        }

        // Speech recognition from microphone.
        public static async Task RecognitionWithMicrophoneAsync(string language) {
            // <recognitionWithMicrophone>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription("98a147ddcd8847ed80aa85fac2f85801", "eastasia");

            // Ask for detailed recognition result
            config.OutputFormat = OutputFormat.Detailed;

            // If you also want word-level timing in the detailed recognition results, set the following.
            // Note that if you set the following, you can omit the previous line
            //      "config.OutputFormat = OutputFormat.Detailed",
            // since word-level timing implies detailed recognition results.
            config.RequestWordLevelTimestamps();

            // Creates a speech recognizer using microphone as audio input.
            using (var recognizer = new SpeechRecognizer(config, language)) {
                // Starts recognizing.

                //Console.WriteLine("Say something...");

                // Starts speech recognition, and returns after a single utterance is recognized. The end of a
                // single utterance is determined by listening for silence at the end or until a maximum of 15
                // seconds of audio is processed.  The task returns the recognition text as result.
                // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
                // shot recognition like command or query.
                // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech) {
                    //Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                    text = result.Text;
                }
                else if (result.Reason == ResultReason.NoMatch) {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    text = null;
                }
                else if (result.Reason == ResultReason.Canceled) {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error) {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                    text = null;
                }
            }
            // </recognitionWithMicrophone>
        }
    }
}
