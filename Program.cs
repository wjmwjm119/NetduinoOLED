using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Text;

using NETMF.Nordic;
using WJMIOT;


namespace NetduinoOLED
{
    public class Program
    {
        //Global globalSPIDevice
        static SPI.Configuration oledSpiConfig=new SPI.Configuration(Pins.GPIO_PIN_D10, false, 0, 0, false, true, 10000, SPI.SPI_module.SPI1);
        static SPI.Configuration nrf24L01PConfig = new SPI.Configuration(Pins.GPIO_PIN_D9, false, 0, 0, false, true, 2000, SPI.SPI_module.SPI1);

        static SPI globalSPIDevice=new SPI(oledSpiConfig);

        //LED
        static OutputPort dataCommandPin = new OutputPort(Pins.GPIO_PIN_D7, true);
        static OutputPort resetPin = new OutputPort(Pins.GPIO_PIN_D6, true);
        static OLED_sh1106 oled = new OLED_sh1106(ref globalSPIDevice, dataCommandPin, resetPin);

        //nrf24L01Plus
        static NRF24L01Plus _radio=new NRF24L01Plus(ref globalSPIDevice, Pins.GPIO_PIN_D3, Pins.GPIO_PIN_D2);
        static Timer _timer;



        public static void Main()
        {
            
            oled.Inital();
            Thread.Sleep(100);
            oled.DisplayString("***");
            globalSPIDevice.Config = nrf24L01PConfig;


            _radio.Initialize();


    //      _radio.Configure(new byte[] { 0xF5, 0xF0, 0xF0, 0xF0, 0xF2 }, 76, NRFDataRate.DR1Mbps);

            _radio.Configure(new byte[] { 0xF0, 0xF0, 0xF0, 0xF0, 0xE1 }, 76, NRFDataRate.DR1Mbps);
            _radio.OnDataReceived += _radio_OnDataReceived;
            _radio.Enable();


            string outputinfo = "Listening on: " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.Zero, 5)) + " | " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.One, 3)) + " | " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.Two, 3)) + " | " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.Three, 3)) + " | " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.Four, 3)) + " | " +
                        ByteArrayToHexString(_radio.GetAddress(AddressSlot.Five, 3));

            Debug.Print(outputinfo);

//            _timer = new Timer(TimerFire, null, new TimeSpan(0, 0, 0, 2), new TimeSpan(0, 0, 0, 3));


            globalSPIDevice.Config = oledSpiConfig;
            oled.DisplayString(outputinfo);
            Thread.Sleep(100);
            globalSPIDevice.Config = nrf24L01PConfig;

            Thread.Sleep(Timeout.Infinite);

/*

            globalSPIDevice.Config = oledSpiConfig;
            oled.DisplayString("Receiving ---");
            Thread.Sleep(100);
            globalSPIDevice.Config = nrf24L01PConfig;
*/

         

        }


        static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        static void _radio_OnDataReceived(byte[] data)
        {
            Debug.Print("Received: " + new string(Encoding.UTF8.GetChars(data)));

            Thread.Sleep(100);
            globalSPIDevice.Config = oledSpiConfig;
            oled.DisplayString("Received: " + new string(Encoding.UTF8.GetChars(data)));
            Thread.Sleep(1000);
            globalSPIDevice.Config = nrf24L01PConfig;

        }

        static void TimerFire(object state)
        {
            _radio.SendTo(new byte[] { 0xF0, 0xF0, 0xF0, 0xF0, 0xE1 }, Encoding.UTF8.GetBytes("Hello"));
            Debug.Print("Sent: Hello");

            Thread.Sleep(100);
            globalSPIDevice.Config = oledSpiConfig;
            oled.DisplayString("Sent: Hello");
            Thread.Sleep(100);
            globalSPIDevice.Config = nrf24L01PConfig;

        }
       



    }



}
