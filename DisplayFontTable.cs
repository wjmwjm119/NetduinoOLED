﻿using System;


namespace WJMIOT
{

    public class FontCharacterDescriptor
    {
        public int characterWidthPx;
        public byte[] characterDataUp;
        public byte[] characterDataDown;

        public FontCharacterDescriptor(int inWidthPx, byte[] charData)
        {

            characterWidthPx = inWidthPx+ 1;
            characterDataUp = new byte[characterWidthPx];
            characterDataDown = new byte[characterWidthPx];
            Array.Copy(charData, 0, characterDataUp, 0, characterWidthPx - 1);
            Array.Copy(charData, characterWidthPx - 1, characterDataDown, 0, characterWidthPx - 1);
        }

    }

    public static class DisplayFontTable
    {

        public static FontCharacterDescriptor GetCharacterDescriptor(char inChar)
        {
            byte[] byteSource;
            int fontWidth;
            int charAsciiNo = (int)inChar;

   //       Debug.WriteLine("Ascii "+charAsciiNo);

            //ascii 空格开始 32   '~'  126
            if (charAsciiNo >= 32 && charAsciiNo <= 126)
            {
                fontWidth = engFontDescriptorsWidth [charAsciiNo-32];

   //           Debug.WriteLine(fontWidth);

                byteSource = new byte[fontWidth * 2];
                
                Array.Copy(engFontBitmaps, engFontDescriptorsStartPosition[charAsciiNo-32], byteSource, 0, fontWidth * 2);

                return new FontCharacterDescriptor(fontWidth, byteSource);
            }
            else
            {
                byteSource=new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};
                return new FontCharacterDescriptor(4, byteSource);
            }
         }



        public static readonly int[] engFontDescriptorsWidth = new int[]
        {
             2,2,3,10,6,11,11,1,4,4,7,7,2,4,2,6,7,3,6,6,9,6,7,8,7,7,2,2,7,7,7,5,13,11,6,8,9,6,6,9,9,1,4,8,7,12,9,10,7,10,8,7,7,8,10,15,10,9,9,3,6,3,7,7,2,6,8,6,7,7,4,7,6,2,5,6,1,11,6,8,8,7,5,5,5,6,8,12,8,8,7,3,1,3,7
        };

        public static readonly int[] engFontDescriptorsStartPosition = new int[]
        {
            0,6,12,21,51,69,102,135,138,150,162,183,204,210,222,228,246,267,276,294,312,339,357,378,402,423,444,450,456,477,498,519,534,573,606,624,648,675,693,711,738,765,768,780,804,825,861,888,918,939,969,993,1014,1035,1059,1089,1134,1164,1191,1218,1227,1245,1254,1275,1296,1302,1320,1344,1362,1383,1404,1416,1437,1455,1461,1476,1494,1497,1530,1548,1572,1596,1617,1632,1647,1662,1680,1704,1740,1764,1788,1809,1818,1821,1830
        };



    public static readonly byte[] engFontBitmaps = new byte[]
    {
     0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x00, 0x19, 0x18, 0x00, 0x00,     0x3C, 0x00, 0x3C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x00, 0x20, 0xE0, 0x3C, 0x20, 0x20, 0xE0,
0x3C, 0x20, 0x20, 0x01, 0x0D, 0x03, 0x01, 0x01, 0x0D, 0x03, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x38, 0x48, 0xC4, 0xFE, 0x84, 0x08,
0x08, 0x10, 0x10, 0x7F, 0x11, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x78, 0x84, 0x84, 0x84, 0x78, 0x80, 0x60, 0x98, 0x84, 0x80, 0x00, 0x00, 0x00, 0x10, 0x0C, 0x03, 0x00,
0x0F, 0x10, 0x10, 0x10, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0xB8, 0xC4, 0xC4, 0xC4, 0xA4, 0x38, 0x00, 0x00, 0xE0, 0x00, 0x0F, 0x18,
0x10, 0x10, 0x10, 0x19, 0x0B, 0x0E, 0x1B, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x3C, 0x00, 0x00,   0xC0, 0x30, 0x08, 0x04, 0x0F, 0x30,
0x40, 0x80, 0x00, 0x00, 0x00, 0x00,     0x04, 0x08, 0x30, 0xC0, 0x80, 0x40, 0x30, 0x0F, 0x00, 0x00, 0x00, 0x00,     0x10, 0x90, 0x50, 0x3C, 0x50, 0x90, 0x10, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x00, 0x00, 0x00, 0xE0, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x0F, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00,   0x00, 0x00, 0x60, 0x18, 0x00, 0x00,     0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00, 0x18, 0x18, 0x00, 0x00,
0x00, 0x00, 0x00, 0xC0, 0x38, 0x04, 0x40, 0x38, 0x07, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xF0, 0x08, 0x04, 0x04, 0x04, 0x08, 0xF0, 0x07, 0x08, 0x10, 0x10,
0x10, 0x08, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x10, 0x08, 0xFC, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00,   0x08, 0x04, 0x04, 0x04, 0x8C, 0x78, 0x1C, 0x1E, 0x13, 0x11,
0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x08, 0x84, 0x84, 0x84, 0x4C, 0x38, 0x08, 0x10, 0x10, 0x10, 0x09, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x00, 0x00,
0x80, 0x60, 0x30, 0x18, 0xFC, 0x00, 0x00, 0x02, 0x03, 0x02, 0x02, 0x02, 0x02, 0x1F, 0x02, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x7C, 0x44, 0x44, 0x44,
0x84, 0x80, 0x08, 0x10, 0x10, 0x10, 0x08, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xE0, 0x98, 0x44, 0x44, 0x44, 0xC4, 0x80, 0x07, 0x08, 0x10, 0x10, 0x10, 0x08, 0x07, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x04, 0x04, 0x04, 0x04, 0xC4, 0x74, 0x1C, 0x04, 0x00, 0x00, 0x1C, 0x1F, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0x00, 0x38, 0xCC, 0x84, 0x84, 0xCC, 0x38, 0x0F, 0x09, 0x10, 0x10, 0x10, 0x09, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x70, 0x88, 0x04, 0x04, 0x04, 0x88,
0xF0, 0x00, 0x08, 0x11, 0x11, 0x11, 0x0C, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x60, 0x60, 0x18, 0x18, 0x00, 0x00,     0x60, 0x60, 0x60, 0x18, 0x00, 0x00,     0x00,
0x80, 0x80, 0xC0, 0x40, 0x40, 0x20, 0x01, 0x03, 0x02, 0x06, 0x04, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x02, 0x02,
0x02, 0x02, 0x02, 0x02, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x20, 0x40, 0x40, 0xC0, 0x80, 0x80, 0x00, 0x08, 0x04, 0x04, 0x06, 0x02, 0x03, 0x01, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00,     0x08, 0x04, 0x84, 0x44, 0x38, 0x00, 0x1B, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xC0, 0x30, 0x18, 0x88, 0x44, 0x24, 0x24, 0x24, 0xE4, 0x08,
0x08, 0x30, 0xE0, 0x07, 0x18, 0x20, 0x27, 0x48, 0x48, 0x48, 0x44, 0x4F, 0x28, 0x08, 0x04, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x80, 0xE0, 0x38, 0x04, 0x38, 0xE0, 0x80, 0x00, 0x00, 0x10, 0x1C, 0x0F, 0x03, 0x02, 0x02, 0x02, 0x03, 0x0F, 0x1C, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00, 0x00,     0xFC, 0x84, 0x84, 0x84, 0xCC, 0x78, 0x1F, 0x10, 0x10, 0x10, 0x19, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xE0, 0x10, 0x08, 0x04, 0x04, 0x04,
0x04, 0x08, 0x03, 0x0C, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x04, 0x04, 0x04, 0x04, 0x04, 0x08, 0x10, 0xE0, 0x1F, 0x10,
0x10, 0x10, 0x10, 0x10, 0x08, 0x04, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x84, 0x84, 0x84, 0x84, 0x04, 0x1F, 0x10, 0x10, 0x10, 0x10, 0x10, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00,   0xFC, 0x84, 0x84, 0x84, 0x84, 0x84, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xE0, 0x10, 0x08, 0x04, 0x04,
0x84, 0x84, 0x84, 0x88, 0x03, 0x0C, 0x08, 0x10, 0x10, 0x10, 0x10, 0x10, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80,
0x80, 0xFC, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x1F, 0x00,   0x00, 0x00, 0x00, 0xFC, 0x10, 0x10,
0x18, 0x07, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x80, 0xC0, 0x60, 0x30, 0x18, 0x0C, 0x04, 0x1F, 0x00, 0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0xFC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xFC, 0x0C, 0x38, 0xE0, 0x80, 0x00,
0x00, 0x00, 0x80, 0x60, 0x18, 0xFC, 0x1F, 0x00, 0x00, 0x00, 0x03, 0x0E, 0x18, 0x0E, 0x01, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0xFC, 0x1C, 0x38, 0x70, 0xC0, 0x80, 0x00, 0x00, 0xFC, 0x1F, 0x00, 0x00, 0x00, 0x01, 0x03, 0x06, 0x1C, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0xE0, 0x18, 0x08, 0x04, 0x04, 0x04, 0x04, 0x08, 0x18, 0xE0, 0x03, 0x0C, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x0C, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0xFC, 0x04, 0x04, 0x04, 0x04, 0x88, 0x78, 0x1F, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xE0, 0x18, 0x08, 0x04, 0x04, 0x04,
0x04, 0x08, 0x18, 0xE0, 0x03, 0x0C, 0x08, 0x10, 0x10, 0x10, 0x10, 0x38, 0x6C, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xFC, 0x84, 0x84, 0x84, 0x84,
0x4C, 0x38, 0x00, 0x1F, 0x00, 0x00, 0x01, 0x03, 0x0E, 0x18, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x70, 0x48, 0x84, 0x84, 0x04, 0x04, 0x08, 0x08, 0x10, 0x10,
0x10, 0x11, 0x11, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x04, 0x04, 0x04, 0xFC, 0x04, 0x04, 0x04, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00,   0xFC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFC, 0x07, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x04,
0x3C, 0xF0, 0x80, 0x00, 0x00, 0x80, 0xF0, 0x3C, 0x04, 0x00, 0x00, 0x01, 0x0F, 0x1C, 0x1C, 0x0F, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
0x0C, 0xFC, 0xF0, 0x00, 0x00, 0xC0, 0x30, 0x3C, 0xF0, 0x80, 0x00, 0x00, 0xF0, 0xFC, 0x0C, 0x00, 0x00, 0x07, 0x1E, 0x1C, 0x07, 0x00, 0x00, 0x01, 0x0F, 0x1C, 0x1E, 0x07, 0x00,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x04, 0x0C, 0x18, 0x70, 0xC0, 0xC0, 0x70, 0x18, 0x0C, 0x04, 0x10, 0x18, 0x0C,
0x07, 0x01, 0x01, 0x07, 0x0C, 0x18, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x04, 0x1C, 0x78, 0xC0, 0x00, 0xC0, 0x78, 0x1C, 0x04, 0x00, 0x00, 0x00,
0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x00, 0x04, 0x04, 0x04, 0xC4, 0x64, 0x34, 0x0C, 0x04, 0x10, 0x18, 0x16, 0x13, 0x10,
0x10, 0x10, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xFC, 0x04, 0x04, 0xFF, 0x80, 0x80, 0x00, 0x00, 0x00,   0x04, 0x38, 0xC0, 0x00, 0x00, 0x00, 0x00,
0x00, 0x01, 0x07, 0x38, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x04, 0x04, 0xFC, 0x80, 0x80, 0xFF, 0x00, 0x00, 0x00,   0x00, 0xE0, 0x38, 0x0C, 0x38, 0xE0, 0x00, 0x01,
0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01,
0x01, 0x01, 0x01, 0x01, 0x01,   0x03, 0x06, 0x00, 0x00, 0x00, 0x00,     0x00, 0x40, 0x20, 0x20, 0x20, 0xC0, 0x0E, 0x12, 0x11, 0x11, 0x09, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0xFE, 0x40, 0x20, 0x20, 0x20, 0x20, 0x40, 0x80, 0x1F, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x80, 0x40, 0x20,
0x20, 0x20, 0x60, 0x07, 0x08, 0x10, 0x10, 0x10, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x80, 0x40, 0x20, 0x20, 0x20, 0x40, 0xFE, 0x07, 0x18, 0x10, 0x10, 0x10, 0x08, 0x1F,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x80, 0x40, 0x20, 0x20, 0x20, 0x60, 0xC0, 0x07, 0x09, 0x11, 0x11, 0x11, 0x11, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x20,
0xFC, 0x22, 0x22, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x80, 0x40, 0x20, 0x20, 0x20, 0x40, 0xE0, 0x07, 0x88, 0x10, 0x10, 0x10, 0x88, 0x7F, 0x00, 0x00, 0x01, 0x01,
0x01, 0x00, 0x00,   0xFE, 0x40, 0x20, 0x20, 0x60, 0xC0, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xE6, 0x06, 0x1F, 0x00, 0x00, 0x00,     0x00,
0x00, 0x00, 0xE6, 0x06, 0x00, 0x00, 0x80, 0x7F, 0x00, 0x01, 0x01, 0x01, 0x00, 0x00,     0xFE, 0x00, 0x80, 0xC0, 0x60, 0x20, 0x1F, 0x03, 0x07, 0x0C, 0x18, 0x10, 0x00, 0x00, 0x00,
0x00, 0x00, 0x00,   0xFE, 0x1F, 0x00,   0xE0, 0x40, 0x20, 0x20, 0x60, 0xC0, 0x40, 0x20, 0x20, 0x20, 0xC0, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x1F,
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xE0, 0x40, 0x20, 0x20, 0x60, 0xC0, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00,   0x80, 0x40, 0x20, 0x20, 0x20, 0x20, 0x40, 0x80, 0x07, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0xE0, 0x40, 0x20,
0x20, 0x20, 0x20, 0x40, 0x80, 0xFF, 0x08, 0x10, 0x10, 0x10, 0x10, 0x08, 0x07, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x80, 0x40, 0x20, 0x20, 0x20, 0x40, 0xE0, 0x07,
0x18, 0x10, 0x10, 0x10, 0x08, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,   0xE0, 0x40, 0x20, 0x20, 0x20, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0xC0,
0x20, 0x20, 0x20, 0x40, 0x09, 0x11, 0x13, 0x12, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00,     0x20, 0xF8, 0x20, 0x20, 0x20, 0x00, 0x0F, 0x10, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00,
0xE0, 0x00, 0x00, 0x00, 0x00, 0xE0, 0x0F, 0x18, 0x10, 0x10, 0x08, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x20, 0xE0, 0x80, 0x00, 0x00, 0xC0, 0xE0, 0x20, 0x00, 0x01, 0x0F,
0x10, 0x0E, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x20, 0xE0, 0x80, 0x00, 0x00, 0x80, 0xE0, 0x80, 0x00, 0x00, 0x80, 0x60, 0x00, 0x01, 0x0F, 0x1C,
0x0E, 0x01, 0x00, 0x07, 0x1C, 0x1C, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,     0x20, 0x60, 0xC0, 0x80, 0x00, 0xC0, 0x60, 0x20, 0x10,
0x18, 0x0C, 0x03, 0x07, 0x0C, 0x18, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,   0x20, 0xE0, 0x80, 0x00, 0x00, 0x00, 0xC0, 0x20, 0x00, 0x01, 0x87, 0xFC, 0x38, 0x06,
0x01, 0x00, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00,     0x20, 0x20, 0x20, 0x20, 0xA0, 0x60, 0x20, 0x10, 0x18, 0x16, 0x13, 0x11, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00,
0x00, 0x00,     0x00, 0xF8, 0x04, 0x01, 0x7E, 0x80, 0x00, 0x00, 0x00,   0xFE, 0xFF, 0x01,   0x04, 0xF8, 0x00, 0x80, 0x7E, 0x01, 0x00, 0x00, 0x00,   0x00, 0x80, 0x80, 0x80, 0x00,
0x00, 0x80, 0x01, 0x00, 0x00, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
    };

    }


    }
















