using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;


namespace WJMIOT
{

    //微雪1.3寸，sh1106
    public class OLED_sh1106
    {
  
        private const UInt32 SCREEN_WIDTH_PX = 132;                         /* Number of horizontal pixels on the display */
        private const UInt32 SCREEN_HEIGHT_PX = 64;                         /* Number of vertical pixels on the display   */
        private const UInt32 SCREEN_HEIGHT_PAGES = SCREEN_HEIGHT_PX / 8;    /* The vertical pixels on this display are arranged into 'pages' of 8 pixels each */
        static byte[] SerializedDisplayBuffer = new byte[SCREEN_WIDTH_PX * SCREEN_HEIGHT_PAGES];                /* A temporary buffer used to prepare graphics data for sending over SPI          */
        static byte[] tempUpBuffer = new byte[SCREEN_WIDTH_PX];
        static byte[] tempDownBuffer = new byte[SCREEN_WIDTH_PX];
        public SPI spiDevice;

        OutputPort dataCommandPin;
        OutputPort resetOutputPort;

        public OLED_sh1106(Cpu.Pin cePin, OutputPort dcPin, OutputPort rsPin)
        {
            spiDevice = new SPI(new SPI.Configuration(cePin, false, 0, 0, false, true, 10000, SPI.SPI_module.SPI1));
            dataCommandPin = dcPin;
            resetOutputPort = rsPin;
            
        }
        public void Inital()
        {
            ResetDisplayDevice();
            ClearScreen();
            WriteCommand(new byte[] { 0xAF });
            DisplayString("Microsoft.SPOT.Debugger.CorDebug.14.dll'");

        }



         public void ResetDisplayDevice()
         {
            resetOutputPort.Write(false);
            Thread.Sleep(10);
            resetOutputPort.Write(true);
         }

        public void WriteCommand(byte[] commandByte)
         {
            dataCommandPin.Write(false);
            spiDevice.Write(commandByte);
         }

        public void WriteData(byte[] dataByte)
        {
            dataCommandPin.Write(true);
            spiDevice.Write(dataByte);
        }


        public  void ClearScreen()
        {
            Array.Clear(SerializedDisplayBuffer, 0, SerializedDisplayBuffer.Length);

            for (int i = 0; i < SCREEN_HEIGHT_PAGES; i++)
            {
                //0x0B0 page adress
                int page = i + 176;
                dataCommandPin.Write(false);
                spiDevice.Write(BitConverter.GetBytes(page));
                dataCommandPin.Write(true);
                spiDevice.Write(SerializedDisplayBuffer);
            }

            spiDevice.Write(new byte[] { 0x10 });//输入返回第一行
            spiDevice.Write(new byte[] { 0x00 });//输入返回第一行

        }

        void DisplayWriteLine(ref byte[] buffer, int lineCount, int offset)
        {

            dataCommandPin.Write(false);
            spiDevice.Write(new byte[] { 0x10 });//输入返回第一行
            spiDevice.Write(new byte[] { 0x00 });//输入返回第一行
            spiDevice.Write(BitConverter.GetBytes(176 + lineCount * 2 + offset));
            dataCommandPin.Write(true);
            spiDevice.Write(new byte[] { 0x00, 0x00 });
            spiDevice.Write(buffer);

            Array.Clear(buffer, 0, buffer.Length);//清空缓存

        }

        public void DisplayString(string str)
        {
            ClearScreen();
            int lineCount = 0;//两个page拼成一行
            int currentPixeWidth = 0;

            Array.Clear(tempUpBuffer, 0, tempUpBuffer.Length);//清空缓存
            Array.Clear(tempDownBuffer, 0, tempDownBuffer.Length);//清空缓存

            FontCharacterDescriptor[] fontDesGroup = new FontCharacterDescriptor[str.Length];

            for (int i = 0; i < str.Length; i++)
            {


                fontDesGroup[i] = DisplayFontTable.GetCharacterDescriptor(str[i]);

                if (currentPixeWidth < 110)
                {
                    Array.Copy(fontDesGroup[i].characterDataUp, 0, tempUpBuffer, currentPixeWidth, fontDesGroup[i].characterWidthPx);
                    Array.Copy(fontDesGroup[i].characterDataDown, 0, tempDownBuffer, currentPixeWidth, fontDesGroup[i].characterWidthPx);
                    currentPixeWidth += fontDesGroup[i].characterWidthPx;
                }
                else
                {
                    Array.Copy(fontDesGroup[i].characterDataUp, 0, tempUpBuffer, currentPixeWidth, fontDesGroup[i].characterWidthPx);
                    Array.Copy(fontDesGroup[i].characterDataDown, 0, tempDownBuffer, currentPixeWidth, fontDesGroup[i].characterWidthPx);
                    DisplayWriteLine(ref tempUpBuffer, lineCount, 0);
                    DisplayWriteLine(ref tempDownBuffer, lineCount, 1);

                    lineCount++;

                    if (lineCount > 3)
                    {
                        break;
                    }

                    currentPixeWidth = 0;

                }

                if (i == str.Length - 1)
                {
                    DisplayWriteLine(ref tempUpBuffer, lineCount, 0);
                    DisplayWriteLine(ref tempDownBuffer, lineCount, 1);
                }

            }

        }

    }
}
