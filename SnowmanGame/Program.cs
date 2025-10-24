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
            #region Değişkenler

            Random rnd = new Random();

            double speed = 0;
            double angle = 0;
            double radyan = 0;
            double windspeed = 0;
            double ballx = 0;
            double bally = 0;
            double vx = 0;
            double vy = 0;

            int txty = 0;
            int turn = 1;
            int round = 0;
            int stay = 0;
            int mapcount = 0;

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

            int snwblue1 = 1;
            int snwblue2 = 1;
            int snwred1 = 1;
            int snwred2 = 1;

            double dt = 0.03;
            double g = 1;

            #endregion

            // Tur döngüsü
            while (true)
            {
                // Sıra değiştirme kontrolü
                if (stay == 0)
                {
                    if (turn == 1)
                    {
                        turn = 0;
                        round++;
                    }
                    else if (turn == 0)
                    {
                        turn = 1;
                        round++;
                    }
                }

                // Mapi temizleme döngüsü
                for (int cx = 0; cx < 120; cx++)
                {
                    for (int cy = 0; cy < 40; cy++)
                    {
                        Console.SetCursorPosition(cx, cy);
                        Console.Write(" ");
                    }
                }

                // Random ve sağ panel kontrolü
                if (round % 6 == 1 && stay == 0)
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

                    mapcount++;

                    // Oyun başladı mesajı
                    if (round == 1)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Oyun Başladı!");
                        txty++;
                    }

                    // Sağ panel temizleme ve map güncellendi mesajı
                    if (round != 1)
                    {
                        for (int t1 = 122; t1 < 180; t1++)
                        {
                            for (int t2 = 0; t2 < 43; t2++)
                            {
                                Console.SetCursorPosition(t1, t2);
                                Console.Write(" ");
                            }
                        }

                        txty = 0;

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.SetCursorPosition(122, txty);
                        Console.WriteLine("Map Güncellendi!");
                        txty++;
                    }
                }

                #region Map Oluşturma  

                // Çerçeve çizgileri
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int lineloop1 = 0; lineloop1 < 40; lineloop1++)
                {
                    Console.SetCursorPosition(120, lineloop1);
                    Console.Write("║");
                }
                for (int lineloop2 = 0; lineloop2 < 120; lineloop2 = lineloop2 + 2)
                {
                    Console.SetCursorPosition(lineloop2, 40);
                    Console.Write("══");
                }
                Console.Write("╝");

                Console.ForegroundColor = ConsoleColor.Blue;
                if (turn == 0)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(shtbluex, shtbluey);
                Console.Write("X");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (turn == 1)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(shtredx, shtredy);
                Console.Write("X");
                Console.ResetColor();

                // 1. Mavi Kardan Adamı
                while (true)
                {
                    if (snwblue1 == 1)
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
                    break;
                }

                // 2. Mavi Kardan Adamı
                while (true)
                {
                    if (snwblue2 == 1)
                    {
                        if (snwblue2x == shtbluex && snwblue2y == shtbluey || snwblue2x == snwblue1x && snwblue2y == snwblue1y)
                        {
                            snwblue2x = rnd.Next(0, 40);
                            snwblue2y = rnd.Next(0, 40);
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.SetCursorPosition(snwblue2x, snwblue2y);
                            Console.Write("&");

                            break;
                        }
                    }
                    break;
                }

                // 1. Kırmızı Kardan Adamı
                while (true)
                {
                    if (snwred1 == 1)
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
                    break;
                }

                // 2. Kırmızı Kardan Adamı
                while (true)
                {
                    if (snwred2 == 1)
                    {
                        if (snwred2x == shtredx && snwred2y == shtredy || snwred2x == snwred1x && snwred2y == snwred1y)
                        {
                            snwred2x = rnd.Next(80, 120);
                            snwred2y = rnd.Next(0, 40);
                            continue;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(snwred2x, snwred2y); //kırmızı takım 2. kardan adamı
                            Console.Write("&");

                            break;
                        }
                    }
                    break;
                }

                // 1. Duvar Oluşturma Döngüsü
                for (int wall1loop = 0; wall1loop < wall1length; wall1loop++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(wall1x, wall1y + wall1loop);
                    Console.Write("|");
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
                    Console.Write("|");
                }

                #endregion

                #region Alt Panel Kontrolleri

                Console.SetCursorPosition(56, 41);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Topun Anlık Koordinatları: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("- -      ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 41);
                Console.Write("Round ");
                Console.Write((round + 1) / 2);
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

                Console.SetCursorPosition(32, 41);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Map " + mapcount + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("(" + ((round - 1) % 6 + 1) + "/6)");

                windspeed = rnd.Next(0, 401);
                windspeed = (windspeed - 200) / 100;
                Console.SetCursorPosition(101, 41);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Rüzgar Hızı: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                if (windspeed > 0)
                    Console.Write($">> {windspeed:F2}    ");
                else if (windspeed < 0)
                    Console.Write($"<< {Math.Abs(windspeed):F2}    ");
                else
                    Console.Write("Yok          ");

                #endregion

                #region Açı ve hız değerleri alma ve oyunu başlatma

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(122, txty);
                Console.Write("                                                  ");
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
                        Console.Write("Lütfen sayı girin                      ");
                        Console.SetCursorPosition(147, txty - 1);
                        Console.Write("                    ");
                        Console.SetCursorPosition(147, txty - 1);
                        continue;
                    }
                    else if (angle < 5 || angle > 85)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen 5 ile 85 arasında bir sayı girin");
                        Console.SetCursorPosition(147, txty - 1);
                        Console.Write("                    ");
                        Console.SetCursorPosition(147, txty - 1);
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
                Console.Write("                                                  ");
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
                        Console.Write("Lütfen sayı girin                      ");
                        Console.SetCursorPosition(147, txty - 1);
                        Console.Write("                    ");
                        Console.SetCursorPosition(147, txty - 1);
                        continue;
                    }
                    else if (speed < 5 || speed > 25)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Hata! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Lütfen 5 ile 25 arasında bir sayı girin");
                        Console.SetCursorPosition(147, txty - 1);
                        Console.Write("                    ");
                        Console.SetCursorPosition(147, txty - 1);
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

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(122, txty + 1);
                Console.Write("(Animasyonu geçmek için önce space'e bas)");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(122, txty);
                Console.Write("                                                    ");
                Console.SetCursorPosition(122, txty);
                Console.Write("Başlamak için enter'e bas");
                string animation = Console.ReadLine();
                Console.SetCursorPosition(122, txty + 1);
                Console.Write("                                          ");
                Console.SetCursorPosition(122, txty);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                if (animation == "")
                {
                    Console.Write("Top atılıyor...          ");
                    Thread.Sleep(1000);
                }

                int bs = 0;
                int rs = 0;

                stay = 0;

                #endregion

                // Topu atma döngüsü
                while (true)
                {
                    if (animation == "")
                        Thread.Sleep(12);

                    int x = (int)Math.Round(ballx);
                    int y = (int)Math.Round(bally);

                    // Topun anlık konumu
                    int virtualy = 40 - y;
                    Console.SetCursorPosition(83, 41);
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
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Top Yere Düştü!");
                        txty++;
                        break;
                    }

                    // Mavi atıcıyı vurma kontrolü
                    if (x == shtbluex && y == shtbluey)
                    {
                        if (bs == 2)
                        {
                            Console.SetCursorPosition(122, txty);
                            Console.Write("                                        ");
                            Console.SetCursorPosition(122, txty);
                            Console.Write("Mavi Atıcıyı Vurdun! ");
                            txty++;

                            if (turn == 1)
                            {
                                stay = 1;
                                Console.Write("Tekrar atış yapabilirsin");
                            }

                            break;
                        }
                        else
                        {
                            bs = 1;
                        }
                    }

                    // Kırmızı atıcıyı vurma kontrolü
                    if (x == shtredx && y == shtredy)
                    {
                        if (rs == 2)
                        {
                            Console.SetCursorPosition(122, txty);
                            Console.Write("                                        ");
                            Console.SetCursorPosition(122, txty);
                            Console.Write("Kırmızı Atıcıyı Vurdun! ");
                            txty++;

                            if (turn == 0)
                            {
                                stay = 1;
                                Console.Write("Tekrar atış yapabilirsin");
                            }

                            break;
                        }
                        else
                        {
                            rs = 1;
                        }
                    }

                    #region Başta kendini vurmama kontrolü

                    if (bs == 1)
                        bs = 0;
                    else if (bs == 0)
                        bs = 2;

                    if (rs == 1)
                        rs = 0;
                    else if (rs == 0)
                        rs = 2;

                    #endregion

                    // 1. Mavi kardan adam kontrolü
                    if (x == snwblue1x && y == snwblue1y && snwblue1 == 1)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Mavi Kardan Adamını Vurdun!");
                        txty++;
                        snwblue1 = 0;
                        break;
                    }

                    // 2. Mavi kardan adam kontrolü
                    if (x == snwblue2x && y == snwblue2y && snwblue2 == 1)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Mavi Kardan Adamını Vurdun!");
                        txty++;
                        snwblue2 = 0;
                        break;
                    }

                    // 1. Kırmızı kardan adam kontrolü
                    if (x == snwred1x && y == snwred1y && snwred1 == 1)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Kırmızı Kardan Adamını Vurdun!");
                        txty++;
                        snwred1 = 0;
                        break;
                    }

                    // 2. Kırmızı kardan adam kontrolü
                    if (x == snwred2x && y == snwred2y && snwred2 == 1)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Kırmızı Kardan Adamını Vurdun!");
                        txty++;
                        snwred2 = 0;
                        break;
                    }

                    // 1. Duvara çarpma kontrolü
                    if (x == wall1x && y >= wall1y && y <= wall1end)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Duvara Çarptın!");
                        txty++;
                        break;
                    }

                    // 2. duvara çarptın?
                    if (x == wall2x && y >= wall2y && y <= wall2end)
                    {
                        Console.SetCursorPosition(122, txty);
                        Console.Write("                                        ");
                        Console.SetCursorPosition(122, txty);
                        Console.Write("Duvara Çarptın!");
                        txty++;
                        break;
                    }

                    // Top izi çizdirme
                    if (y >= 0 && x <= 119 && x >= 0 && (x != shtbluex || y != shtbluey) && (x != shtredx || y != shtredy))
                    {
                        Console.SetCursorPosition(x, y);

                        int tracecolor = rnd.Next(1, 4);

                        if (tracecolor == 1 && turn == 0)
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        if (tracecolor == 2 && turn == 0)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        if (tracecolor == 1 && turn == 1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        if (tracecolor == 2 && turn == 1)
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        if (tracecolor == 3)
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                        int tracesize = rnd.Next(1, 4);

                        if (tracesize == 1)
                            Console.Write("o");
                        if (tracesize == 2)
                            Console.Write("O");
                        if (tracesize == 3)
                            Console.Write("0");
                    }

                    #region Hesaplamaları güncelleme

                    vx = vx + windspeed * dt;
                    vy = vy + g * dt;
                    ballx = ballx + vx * dt;
                    bally = bally + vy * dt;

                    #endregion
                }

                #region Kazanma kontrolü

                if (snwblue1 == 0 && snwblue2 == 0)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Oyunu ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Kırmızı ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Takım Kazandı!");
                    txty++;
                    break;
                }
                if (snwred1 == 0 && snwred2 == 0)
                {
                    Console.SetCursorPosition(122, txty);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Oyunu ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("Mavi ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Takım Kazandı!");
                    txty++;
                    break;
                }

                #endregion

                #region Yeni tura geçme işlemi

                Thread.Sleep(1000);
                Console.SetCursorPosition(122, txty);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Yeni tura geçmek için herhangi bir tuşa bas");
                Console.ReadKey();
                Console.SetCursorPosition(122, txty);
                Console.Write("                                            ");

                #endregion
            }

            #region Oyun Sonu

            Console.SetCursorPosition(122, txty);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Çıkmak için herhangi bir tuşa bas");
            Console.ReadKey();

            #endregion
        }
    }
}
