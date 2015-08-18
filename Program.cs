using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using WJMIOT;


namespace NetduinoOLED
{
    public class Program
    {

        static OutputPort dataCommandPin = new OutputPort(Pins.GPIO_PIN_D7, true);
        static OutputPort resetPin = new OutputPort(Pins.GPIO_PIN_D6, true);
//      static OutputPort csPin = new OutputPort(Pins.GPIO_PIN_D10, true);

        static OLED_sh1106 oled = new OLED_sh1106(Pins.GPIO_PIN_D10,dataCommandPin, resetPin);
        

        public static void Main()
        {
            oled.Inital();



        }

    }



}
