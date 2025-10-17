using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region MAP OLUŞTURMA

            Random rnd = new Random();

            #region Atıcılar

            int shtbluex = rnd.Next(0, 40);
            int shtbluey = rnd.Next(0, 40);

            int shtredx = rnd.Next(80, 120);
            int shtredy = rnd.Next(0, 40);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(shtbluex, shtbluey);
            Console.Write("X");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(shtredx, shtredy);
            Console.Write("X");

            #endregion

            #region Kardan Adamlar

            int snwblue1x, snwblue1y, snwblue2x, snwblue2y, snwred1x, snwred1y, snwred2x, snwred2y;

            // 1. Mavi Kardan Adamı
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
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(snwblue1x, snwblue1y);
                    Console.Write("&");

                    break;
                }
            }

            // 2. Mavi Kardan Adamı
            while (true)
            {
                snwblue2x = rnd.Next(0, 40);
                snwblue2y = rnd.Next(0, 40);

                if (snwblue2x == shtbluex && snwblue2y == shtbluey || snwblue2x == snwblue1x && snwblue2y == snwblue1y)
                {
                    continue;
                }
                else
                {
                    Console.SetCursorPosition(snwblue2x, snwblue2y);
                    Console.Write("&");

                    break;
                }
            }

            // 1. Kırmızı Kardan Adamı
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(snwred1x, snwred1y);
                    Console.Write("&");

                    break;
                }
            }

            // 2. Kırmızı Kardan Adamı
            while (true)
            {
                snwred2x = rnd.Next(80, 120);
                snwred2y = rnd.Next(0, 40);

                if (snwred2x == shtredx && snwred2y == shtredy || snwred2x == snwred1x && snwred2y == snwred1y)
                {
                    continue;
                }
                else
                {
                    Console.SetCursorPosition(snwred2x, snwred2y); //kırmızı takım 2. kardan adamı
                    Console.Write("&");

                    break;
                }
            }

            #endregion

            #region Duvar 1

            int wall1length = rnd.Next(3, 7);
            int wall1x = rnd.Next(40, 80);
            int wall1y = rnd.Next(0, (41 - wall1length));
            int wall1end = wall1y + wall1length - 1;

            // 1. Duvar oluşturma döngüsü
            for (int wall1loop = 0; wall1loop < wall1length; wall1loop++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(wall1x, wall1y + wall1loop);
                Console.Write("#");
            }

            #endregion

            #region Duvar 2

            int wall2length, wall2x, wall2y, wall2end;

            // 2. Duvarın 1. Duvarla Çakışma Kontrolü
            while (true)
            {
                wall2length = rnd.Next(3, 7);
                wall2x = rnd.Next(40, 80);
                wall2y = rnd.Next(0, (41 - wall2length));
                wall2end = wall2y + wall2length - 1;

                if (wall2x == wall1x && wall2y <= wall1end && wall2end >= wall1y)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            // 2. Duvar oluşturma döngüsü
            for (int wall2loop = 0; wall2loop < wall2length; wall2loop++)
            {
                Console.SetCursorPosition(wall2x, wall2y + wall2loop);
                Console.Write("#");
            }

            #endregion

            #endregion

            #region KAR TOPU ATMA

            // Yazı kısmını ayıran çizgi
            for (int lineloop = 0; lineloop < 40; lineloop++)
            {
                Console.ResetColor();
                Console.SetCursorPosition(120, lineloop);
                Console.Write("|");
            }

            double speed, angle, windspeed, radyan, ballx, bally;

            int txty = 0;

            windspeed = rnd.Next(0, 401);
            windspeed = (windspeed - 200) / 100;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(122, txty);
            Console.Write("Round 1: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Mavi ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Takım");
            txty++;

            Console.SetCursorPosition(122, txty);
            Console.Write("Rüzgar Hızı: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(windspeed);
            txty++;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(122, txty);
            Console.Write("Açı değerini gir (5-85): ");
            txty++;

            //açı için 5 ile 85 arası değer kontrolü
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                angle = Convert.ToDouble(Console.ReadLine());
                radyan = angle * Math.PI / 180;

                if (angle >= 5 && angle <= 85)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Hata! ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Lütfen 5 ile 85 arasında bir değer girin: ");
                    txty++;
                    continue;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(122, txty);
            Console.Write("Hız değerini gir (5-25): ");
            txty++;

            //hız için 5 ile 25 arası değer kontrolü
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                speed = Convert.ToDouble(Console.ReadLine());

                if (speed >= 5 && speed <= 25)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Hata! ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Lütfen 5 ile 25 arasında bir değer girin: ");
                    txty++;
                    continue;
                }
            }

            ballx = shtbluex + 1;
            bally = shtbluey;

            double dt = 0.1;
            double g = 1;
            double vx = speed * Math.Cos(radyan);
            double vy = -speed * Math.Sin(radyan);

            int t1x = -1, t1y = -1;
            int t2x = -1, t2y = -1;
            int t3x = -1, t3y = -1;
            int t4x = -1, t4y = -1;

            while (true)
            {
                Thread.Sleep(50);

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                // sınıra çarptın?
                if (ballx > 119 || ballx < 0)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Sınıra Çarptın!");
                    txty++;
                    break;
                }

                // yere düştün?
                if (bally > 39)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Yere Düştün!");
                    txty++;
                    break;
                }

                // kendi atıcını vurdun?
                if ((int)ballx == shtbluex && (int)bally == shtbluey)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Kendini Vurdun!");
                    txty++;
                    break;
                }

                // kendi kardan adamını vurdun?
                if ((int)ballx == snwblue1x && (int)bally == snwblue1y || (int)ballx == snwblue2x && (int)bally == snwblue2y)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Kendi Kardan Adamını Vurdun!");
                    txty++;
                    break;
                }

                // kırmızı atıcıyı vurdun?
                if ((int)ballx == shtredx && (int)bally == shtredy)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Kırmızı Atıcıyı Vurdun!");
                    txty++;
                    break;
                }

                // kırmızı kardan adamını vurdun?
                if ((int)ballx == snwred1x && (int)bally == snwred1y || (int)ballx == snwred2x && (int)bally == snwred2y)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Kırmızı Kardan Adamını Vurdun!");
                    txty++;
                    break;
                }

                // 1. duvara çarptın?
                if ((int)ballx == wall1x && (int)bally >= wall1y && (int)bally <= wall1end)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Duvara Çarptın!");
                    txty++;
                    break;
                }

                // 2. duvara çarptın?
                if ((int)ballx == wall2x && (int)bally >= wall2y && (int)bally <= wall2end)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.Write("Duvara Çarptın!");
                    txty++;
                    break;
                }

                //1. Mor iz
                if (t1y >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition((int)t1x, (int)t1y);
                    Console.Write("O");
                }

                //2. Mor iz
                if (t2y >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition((int)t2x, (int)t2y);
                    Console.Write("O");
                }

                //3. Mor iz
                if (t3y >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition((int)t3x, (int)t3y);
                    Console.Write("O");
                }

                //Gri iz
                if (t4y >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition((int)t4x, (int)t4y);
                    Console.Write("o");
                }

                //Pembe iz
                if (bally >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition((int)ballx, (int)bally);
                    Console.Write("@");
                }

                t4x = t3x;
                t3x = t2x;
                t2x = t1x;
                t1x = (int)ballx;

                t4y = t3y;
                t3y = t2y;
                t2y = t1y;
                t1y = (int)bally;

                vx = vx + windspeed * dt;
                vy = vy + g * dt;
                ballx = ballx + vx * dt;
                bally = bally + vy * dt;
            }

            #endregion

            Console.ReadLine();
        }
    }
}
