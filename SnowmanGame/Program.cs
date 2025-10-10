using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

            int snwblue1x, snwblue1y, snwblue2x, snwblue2y, snwred1x, snwred1y, snwred2x, snwred2y;

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
            } // mavinin 1. kardanadamıyla mavinin atıcısı 

            while (true)
            {
                snwblue2x = rnd.Next(0, 40);
                snwblue2y = rnd.Next(0, 40);

                if (snwblue2x == shtbluex && snwblue2y == shtbluey)
                {
                    continue;
                }
                else
                {
                    break;
                }
            } // mavinin 2. kardanadamıyla mavinin atıcısı 

            while (true) 
            {
                if (snwblue1x == snwblue2x && snwblue1y == snwblue2y)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }  // mavinin kardanadamlarının takışması

            while (true)
            {
                snwred1x = rnd.Next(80, 120);
                snwred1y = rnd.Next(0, 40);

                if (snwred1x == shtredx && snwred1y == shtredy)
                {
                    continue;
                }
                else
                {
                    break;
                }
            } // kırmızının 1. kardanadamıyla atıcısı

            while (true)
            {
                snwred2x = rnd.Next(80, 120);
                snwred2y = rnd.Next(0, 40);

                if (snwred2x == shtredx && snwred2y == shtredy)
                {
                    continue;
                }
                else
                {
                    break;
                }
            } // kırmızının 2. kardanadamıyla atıcısı

            while (true) 
            {
                if (snwred1x == snwred2x && snwred2x == snwred2y)
                {
                    continue;
                }
                else
                {
                    break;
                }
            } // kırmızının her iki kardanadamı            

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
            int wall1end = wall1y + wall1length - 1;

            Console.SetCursorPosition(wall1x, wall1y);
            Console.Write("#");

            Console.SetCursorPosition(wall1x, (wall1y + 1));
            Console.Write("#");

            Console.SetCursorPosition(wall1x, (wall1y + 2));
            Console.Write("#");

            if (wall1length > 3 && wall1end < 40)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 3));
                Console.Write("#");
            }

            if (wall1length > 4 && wall1end < 40)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 4));
                Console.Write("#");
            }

            if (wall1length > 5 && wall1end < 40)
            {
                Console.SetCursorPosition(wall1x, (wall1y + 5));
                Console.Write("#");
            }

            #endregion

            #region Duvar 2

            int wall2x = rnd.Next(40, 80);
            int wall2y = rnd.Next(0, 40);
            int wall2length = rnd.Next(3, 7);
            int wall2end = wall2y + wall2length - 1;
            
            while (true)
            {
                wall2x = rnd.Next(40, 80);
                wall2y = rnd.Next(0, 38);

                if (wall2x == wall1x && wall2y >= wall1y && wall2y <= wall1end)
                {
                    continue;
                }
                else
                {
                    break;
                }
            } //2. Duvar 1. duvarla çakışıyor mu

            Console.SetCursorPosition(wall2x, wall2y);
            Console.Write("#");

            Console.SetCursorPosition(wall2x, (wall2y + 1));
            Console.Write("#");

            Console.SetCursorPosition(wall2x, (wall2y + 2));
            Console.Write("#");

            if (wall2length > 3 && wall2end < 40)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 3));
                Console.Write("#");
            }

            if (wall2length > 4 && wall2end < 40)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 4));
                Console.Write("#");
            }

            if (wall2length > 5 && wall2end < 40)
            {
                Console.SetCursorPosition(wall2x, (wall2y + 5));
                Console.Write("#");
            }

            #endregion

            #endregion

            #region KARTOPU

            Console.SetCursorPosition(shtbluex + 1, shtbluey);
            Console.WriteLine("O");

            #endregion

            Console.ReadLine();
        }
    }
}
