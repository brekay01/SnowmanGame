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
            Random rnd = new Random();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            // Çerçeve Çizgileri
            for (int lineloop1 = 0; lineloop1 < 40; lineloop1++)
            {
                Console.SetCursorPosition(120, lineloop1);
                Console.Write("║");
            }
            for (int lineloop2 = 0; lineloop2 < 120; lineloop2++)
            {
                Console.SetCursorPosition(lineloop2, 40);
                Console.Write("═");
            }
            Console.Write("╝");

            #region Değişkenler

            double speed = 0;
            double angle = 0;
            double radyan = 0;
            double windspeed = 0;
            double ballx = 0;
            double bally = 0;
            double vx = 0;
            double vy = 0;

            int txty = 0;
            int turn = 1; // Sıranın kimde olduğunu belirleyen değişken ( 0 = Mavi , 1 = Kırmızı )
            int round = 0;

            int shtbluex = 0;
            int shtbluey = 0;
            int shtredx = 0;
            int shtredy = 0;
            int snwblue1x = 0;
            int snwblue1y = 0;
            int snwblue2x = 0;
            int snwblue2y = 0;
            int snwred1x = 0;
            int snwred1y = 0;
            int snwred2x = 0;
            int snwred2y = 0;
            int wall1length = 0;
            int wall1x = 0;
            int wall1y = 0;
            int wall1end = 0;
            int wall2length = 0;
            int wall2x = 0;
            int wall2y = 0;
            int wall2end = 0;

            double dt = 0.04;
            double g = 1;

            #endregion

            while (true) // Tur Döngüsü
            {
                round++;

                // Mapi temizleme döngüsü
                for (int cx = 0; cx < 120; cx++)
                {
                    for (int cy = 0; cy < 40; cy++)
                    {
                        Console.SetCursorPosition(cx, cy);
                        Console.Write(" ");
                    }
                }

                // 3 Turda Bir Random Değişme Kontrolü
                if (round % 3 == 1)
                {
                    shtbluex = rnd.Next(0, 40);
                    shtbluey = rnd.Next(0, 40);

                    shtredx = rnd.Next(80, 120);
                    shtredy = rnd.Next(0, 40);

                    snwblue1x = rnd.Next(0, 40);
                    snwblue1y = rnd.Next(0, 40);

                    snwblue2x = rnd.Next(0, 40);
                    snwblue2y = rnd.Next(0, 40);

                    snwred1x = rnd.Next(80, 120);
                    snwred1y = rnd.Next(0, 40);

                    snwred2x = rnd.Next(80, 120);
                    snwred2y = rnd.Next(0, 40);

                    wall1length = rnd.Next(3, 7);
                    wall1x = rnd.Next(40, 80);
                    wall1y = rnd.Next(0, (41 - wall1length));
                    wall1end = wall1y + wall1length - 1;

                    wall2length = rnd.Next(3, 7);
                    wall2x = rnd.Next(40, 80);
                    wall2y = rnd.Next(0, (41 - wall2length));
                    wall2end = wall2y + wall2length - 1;

                    if (round != 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(122, txty);
                        Console.WriteLine("Map Güncellendi!");
                        txty++;
                    }
                }

                #region Map Oluşturma

                Console.SetCursorPosition(47, 41);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Topun Anlık Koordinatları: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("- -      ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(shtbluex, shtbluey);
                Console.Write("X");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(shtredx, shtredy);
                Console.Write("X");

                // 1. Mavi Kardan Adamı
                while (true)
                {
                    if (snwblue1x == shtbluex && snwblue1y == shtbluey)
                    {
                        snwblue1x = rnd.Next(0, 40);
                        snwblue1y = rnd.Next(0, 40);
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
                    if (snwblue2x == shtbluex && snwblue2y == shtbluey || snwblue2x == snwblue1x && snwblue2y == snwblue1y)
                    {
                        snwblue2x = rnd.Next(0, 40);
                        snwblue2y = rnd.Next(0, 40);
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
                    if (snwred1x == shtredx && snwred1y == shtredy)
                    {
                        snwred1x = rnd.Next(80, 120);
                        snwred1y = rnd.Next(0, 40);
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
                    if (snwred2x == shtredx && snwred2y == shtredy || snwred2x == snwred1x && snwred2y == snwred1y)
                    {
                        snwred2x = rnd.Next(80, 120);
                        snwred2y = rnd.Next(0, 40);
                        continue;
                    }
                    else
                    {
                        Console.SetCursorPosition(snwred2x, snwred2y); //kırmızı takım 2. kardan adamı
                        Console.Write("&");

                        break;
                    }
                }

                // 1. Duvar Oluşturma Döngüsü
                for (int wall1loop = 0; wall1loop < wall1length; wall1loop++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(wall1x, wall1y + wall1loop);
                    Console.Write("#");
                }

                // 2. Duvarın 1. Duvarla Çakışma Kontrolü
                while (true)
                {
                    if (wall2x == wall1x && wall2y <= wall1end && wall2end >= wall1y)
                    {
                        wall2length = rnd.Next(3, 7);
                        wall2x = rnd.Next(40, 80);
                        wall2y = rnd.Next(0, (41 - wall2length));
                        wall2end = wall2y + wall2length - 1;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                // 2. Duvar Oluşturma Döngüsü
                for (int wall2loop = 0; wall2loop < wall2length; wall2loop++)
                {
                    Console.SetCursorPosition(wall2x, wall2y + wall2loop);
                    Console.Write("#");
                }

                #endregion

                #region Round ve Rüzgar Hızı Kontrolü

                // Sıra Değiştirme Kontrolü
                if (turn == 1)
                {
                    turn = 0;
                }
                else
                {
                    turn = 1;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 41);
                Console.Write("Round ");
                Console.Write(round);
                Console.Write(": ");
                if (turn == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Mavi ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Kırmızı ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Takım");

                windspeed = rnd.Next(0, 401);
                windspeed = (windspeed - 200) / 100;
                Console.SetCursorPosition(103, 41);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Rüzgar Hızı: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(windspeed + "    ");

                #endregion

                #region Açı ve hız değerleri alma ve oyunu başlatma

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(122, txty);
                Console.Write("Açı Değeri Girin");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" (5-85): ");
                txty++;

                // Açı için girilen değer kontrolü
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string angleinput = Console.ReadLine();
                    bool anglecheck = double.TryParse(angleinput, out angle);

                    if (!anglecheck)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen sayı girin: ");
                        txty++;
                        continue;
                    }
                    else if (angle < 5 || angle > 85)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen 5 ile 85 arasında bir sayı girin: ");
                        txty++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                radyan = angle * Math.PI / 180;

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(122, txty);
                Console.Write("Hız Değeri Girin");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" (5-25): ");
                txty++;

                // Hız için girilen değer kontrolü
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string speedinput = Console.ReadLine();
                    bool speedcheck = double.TryParse(speedinput, out speed);

                    if (!speedcheck)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen sayı girin: ");
                        txty++;
                        continue;
                    }
                    else if (angle < 5 || angle > 85)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen 5 ile 25 arasında bir sayı girin: ");
                        txty++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                // Sıraya göre hesaplamalar
                if (turn == 0)
                {
                    ballx = shtbluex;
                    bally = shtbluey;
                    vx = speed * Math.Cos(radyan);
                    vy = -speed * Math.Sin(radyan);
                }
                else
                {
                    ballx = shtredx;
                    bally = shtredy;
                    vx = -speed * Math.Cos(radyan);
                    vy = -speed * Math.Sin(radyan);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(122, txty);
                Console.Write("Atışı başlatmak için herhangi bir tuşa bas!");
                Console.ReadKey();
                Console.SetCursorPosition(122, txty);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Top atıldı!                                ");

                #endregion

                // Topu atma döngüsü
                while (true)
                {
                    Thread.Sleep(10);

                    int x = (int)ballx;
                    int y = (int)bally;

                    // Topun anlık konumu
                    int virtualy = 40 - y;
                    Console.SetCursorPosition(74, 41);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(x + " " + virtualy + " ");

                    // Sıraya göre renk ayarlama
                    if (turn == 0)
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    if (turn == 1)
                        Console.ForegroundColor = ConsoleColor.Red;

                    // Yere düşme kontrolü
                    if (y >= 40)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Yere Düştün!");
                        txty++;
                        break;
                    }

                    // 1. Mavi kardan adam kontrolü
                    if (x == snwblue1x && y == snwblue1y)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Mavi Kardan Adamını Vurdun!");
                        txty++;
                        break;
                    }

                    // 2. Mavi kardan adam kontrolü
                    if (x == snwblue2x && y == snwblue2y)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Mavi Kardan Adamını Vurdun!");
                        txty++;
                        break;
                    }

                    // 1. Kırmızı kardan adam kontrolü
                    if (x == snwred1x && y == snwred1y)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Kırmızı Kardan Adamını Vurdun!");
                        txty++;
                        break;
                    }

                    // 2. Mavi kardan adam kontrolü
                    if (x == snwred2x && y == snwred2y)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Kırmızı Kardan Adamını Vurdun!");
                        txty++;
                        break;
                    }

                    // 1. Duvara çarpma kontrolü
                    if (x == wall1x && y >= wall1y && y <= wall1end)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Duvara Çarptın!");
                        txty++;
                        break;
                    }

                    // 2. duvara çarptın?
                    if (x == wall2x && y >= wall2y && y <= wall2end)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Duvara Çarptın!");
                        txty++;
                        break;
                    }

                    // Top izi çizdirme
                    if (y >= 0 && x <= 119 && x >= 0)
                    {
                        int balltrace = rnd.Next(1, 3);

                        if (balltrace == 1 && turn == 0)
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (balltrace == 2 && turn == 0)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        if (balltrace == 1 && turn == 1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (balltrace == 2 && turn == 1)
                            Console.ForegroundColor = ConsoleColor.DarkRed;

                        Console.SetCursorPosition(x, y);
                        Console.Write("o");
                    }

                    vx = vx + windspeed * dt;
                    vy = vy + g * dt;
                    ballx = ballx + vx * dt;
                    bally = bally + vy * dt;
                }

                #region Yeni tura geçme işlemi

                Console.SetCursorPosition(122, txty);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Yeni tura geçmek için herhangi bir tuşa bas!");
                Console.ReadKey();
                Console.SetCursorPosition(122, txty);
                Console.Write("                                            ");

                #endregion
            }
        }
    }
}
