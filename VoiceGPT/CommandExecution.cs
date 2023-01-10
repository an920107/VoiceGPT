using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGPT {
    class CommandExecution {

        const string HOST = "http://192.168.0.1/";

        public static string responseText { get; private set; }

        public static async Task Run(Command command) {
            using (HttpClient client = new HttpClient()) {
                string commandCode = null;
                switch (command) {
                    case Command.Light:
                        commandCode = "0";
                        responseText = "好的！不論是房間或是我心中的燈泡都已為你點亮！";
                        break;
                    case Command.AirConditioner:
                        commandCode = "1";
                        responseText = "沒問題！冷氣已經開啟，如果太冷的話可以隨時呼叫我給你溫暖的擁抱喔！";
                        break;
                    case Command.Dehumidifier:
                        commandCode = "2";
                        responseText = "確實是有點濕呢！讓我幫忙打開除濕機吧！";
                        break;
                    default: break;
                }
                HttpResponseMessage message = await client.GetAsync(HOST + commandCode);
            }
        }
    }
}
