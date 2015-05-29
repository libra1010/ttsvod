using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            Test_Mp3Info();

            Console.Read();
        }

        /// <summary>
        /// 测试获取MP3头文件信息
        /// </summary>
        private static void Test_Mp3Info()
        {
            string file = @"D:\Music\门丽 - 梦中想着你.mp3";

            Mp3FileInfo mp3file = new Mp3FileInfo(file);

            var mp3info = mp3file.GetMp3();

            string name = mp3info.Artist.Trim('\0');

        }


    }//end class
}
