using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoOLED
{
    public class Program
    {
        public static void Main()
        {
            // 8 chipselect low active
            // 7 reset low active
            // 10 data/command =volt.high/volt.low


            //OutputPort csOutputPort = new OutputPort(Cpu.Pin.GPIO_Pin6, true);

            Debug.Print(Cpu.Pin.GPIO_Pin8.ToString()+"--------"+ Pins.GPIO_PIN_D10);
            Debug.Print(Cpu.Pin.GPIO_Pin4.ToString() + "--------" + Pins.GPIO_PIN_D4);
            Debug.Print(Cpu.Pin.GPIO_Pin7.ToString() + "--------" + Pins.GPIO_PIN_D7);


            OutputPort resetOutputPort = new OutputPort(Pins.GPIO_PIN_D4, false);
            OutputPort commandDataOutputPort = new OutputPort(Pins.GPIO_PIN_D7, false);



            SPI.Configuration spiConfig = new SPI.Configuration(Pins.GPIO_PIN_D10, false, 0, 0, false, true, 10000, SPI.SPI_module.SPI1);
   //      SPI.Configuration spiConfig = new SPI.Configuration(Pins.GPIO_PIN_D10, false, 0, 0, true, true, 10000, SPI.SPI_module.SPI1);
    //      SPI.Configuration spiConfig = new SPI.Configuration(Pins.GPIO_PIN_D10, false, 0, 0, false, false, 10000, SPI.SPI_module.SPI1);
   //       SPI.Configuration spiConfig = new SPI.Configuration(Pins.GPIO_PIN_D10, false, 0, 0, true, false, 10000, SPI.SPI_module.SPI1);


            SPI spiDisplayDevice = new SPI(spiConfig);



            resetOutputPort.Write(false);
            Thread.Sleep(100);
            resetOutputPort.Write(true);
            Thread.Sleep(500);


            commandDataOutputPort.Write(false);
            spiDisplayDevice.Write(new byte[] { 0xAF });
            spiDisplayDevice.Write(new byte[] { 0xA5 });


            Debug.Print("End");


        }

    }
}
