using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGPT {
    class CommandDetection {

        public static Command command { get; private set; }

        public static async Task Post(string text) {
            await Task.Run(() => { });
            if (text.Contains("冷氣"))
                command = Command.AirConditioner;
            else if (text.Contains("除濕機"))
                command = Command.Dehumidifier;
            else if (text.Contains("開燈"))
                command = Command.Light;
            else
                command = Command.None;

        }
    }
}
