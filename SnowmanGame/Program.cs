using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region RANDOM

            Random rnd = new Random();

            #endregion

            #region ATICILAR

            int shtbluex = rnd.Next(0, 40);
            int shtbluey = rnd.Next(0, 40);

            int shtredx = rnd.Next(80, 120);
            int shtredy = rnd.Next(0, 40);

            Console.SetCursorPosition(shtbluex, shtbluey); //mavi takım atıcısı
            Console.Write("*");

            Console.SetCursorPosition(shtredx, shtredy); //kırmızı takım atıcısı
            Console.Write("*");

            #endregion

            #region KARDAN ADAMLAR

            int snwblue1x, snwblue1y;

            while (true)
            {
                snwblue1x = rnd.Next(0, 40);
                snwblue1y = rnd.Next(0, 40);

                if (snwblue1x == shtbluex && snwblue1y == shtbluey)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }


            int snwred1x = rnd.Next(80, 120);
            int snwred1y = rnd.Next(0, 40);

            int snwblue2x = rnd.Next(0, 40);
            int snwblue2y = rnd.Next(0, 40);

            int snwred2x = rnd.Next(80, 120);
            int snwred2y = rnd.Next(0, 40);

            Console.SetCursorPosition(snwblue1x, snwblue1y); //mavi takım 1. kardan adamı
            Console.Write("B");

            Console.SetCursorPosition(snwblue2x, snwblue2y); //mavi takım 2. kardan adamı
            Console.Write("B");

            Console.SetCursorPosition(snwred1x, snwred1y); //kırmızı takım 1. kardan adamı
            Console.Write("R");

            Console.SetCursorPosition(snwred2x, snwred2y); //kırmızı takım 2. kardan adamı
            Console.Write("R");

            #endregion

            #region DUVARLAR

            #region Duvar 1

            int wall1x = rnd.Next(40, 80);
            int wall1y = rnd.Next(0, 40);

            int wall1length = rnd.Next(3, 7);

            Console.SetCursorPosition(wall1x, wall1y);
            Console.Write("#");

            Console.SetCursorPosition(wall1x, (wall1y + 1));
            Console.Write("#");

            Console.SetCursorPosition(wall1x, (wall1y + 2));
            Console.Write("#");

            if (wall1length > 3)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 3));
                Console.Write("#");
            }

            if (wall1length > 4)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 4));
                Console.Write("#");
            }

            if (wall1length > 5)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 5));
                Console.Write("#");
            }

            #endregion

            #region Duvar 2

            int wall2x = rnd.Next(40, 80);
            int wall2y = rnd.Next(0, 40);

            int wall2length = rnd.Next(3, 7);

            Console.SetCursorPosition(wall2x, wall2y);
            Console.Write("#");

            Console.SetCursorPosition(wall2x, (wall2y + 1));
            Console.Write("#");

            Console.SetCursorPosition(wall2x, (wall2y + 2));
            Console.Write("#");

            if (wall2length > 3)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 3));
                Console.Write("#");
            }

            if (wall2length > 4)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 4));
                Console.Write("#");
            }

            if (wall2length > 5)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 5));
                Console.Write("#");
            }

            #endregion

            #endregion

            Console.ReadLine();
        }
    }
}
