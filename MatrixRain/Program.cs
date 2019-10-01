using System;

namespace MatrixRain
{
    class Program
    {

        
        //    
        static int Counter;
        static Random randomPosition = new Random();

        // TEKSTIN RULLAUS VAUHTI
        static int flowSpeed = 120;             // ALKUPERÄINEN VAUTI 100
        static int fastFlow = flowSpeed + 40;   // ALKUPERÄINEN VAUHTI + 30
        static int textFlow = fastFlow + 60;    // ALKUPERÄINEN VAUHTI + 50

        // KIRJAINTEN JA NUMEROIDEN VÄRI
        static ConsoleColor baseColor = ConsoleColor.DarkGreen;     // ALKUPERÄINEN ConsoleColor.DarkGreen
        static ConsoleColor greenColor = ConsoleColor.Green;        // ALKUPERÄINEN "static ConsoleColor greenColor = ConsoleColor.Green"
        static ConsoleColor fadedColor = ConsoleColor.White;        // ALKUPERÄINEN ConsoleColor.White

        // TEKSTI MIKÄ NÄKYY KOODIN KESKELLÄ
        static String endText = "VIISTO CODE";

        // ASCII TAULUKKO JA RANDOM ARPOMINEN (http://www.asciitable.com/)
        static char AsciiCharacters
        {
            get
            {
                int t = randomPosition.Next(10);

                if          (t <= 2) return (char)('0' + randomPosition.Next(10));    // TÄMÄ ARPOO RANDOM JÄRJESTYKSESSÄ NUMEROITA 0-9
                else if     (t <= 4) return (char)('a' + randomPosition.Next(27));    // TÄMÄ ARPOO RANDOM JÄRJESTYKSESSÄ PINIÄ KIRJAIMIA 27 KIRJAIMEEN ASTI
                else if     (t <= 6) return (char)('A' + randomPosition.Next(27));    // TÄMÄ ARPOO RANDOM JÄRJESTYKSESSÄ ISOJA KIRJAIMIA 27 KIRJAIMEEN ASTI
                else return (char)(randomPosition.Next(32, 255));                     // 
            }

        }

        static void Main()
        {

            Console.ForegroundColor = baseColor;
            Console.WindowLeft = Console.WindowTop = 0;
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            int width, height;
            int[] y;
            Initialize(out width, out height, out y);

            while (true)
            {
                Counter++;
                ColumnUptade(width, height, y);
                if (Counter > (3 * flowSpeed))
                    Counter = 0;
            }

        }

        // MITÄ TÄMÄ TEKEE? IF ELSE LAUSEET?
        public static int YPositionFields(int yPosition, int height)
        {
            if (yPosition < 0)              return yPosition + height;
            else if (yPosition < height)    return yPosition;
            else                            return 0;

        }
        // Initialize = ALÙSTAA 
        private static void Initialize(out int width, out int height, out int[] y)
        {
            height = Console.WindowHeight;
            width = Console.WindowWidth - 1;
            y = new int[width];
            Console.Clear();

            for (int x = 0; x < width; ++x) { y[x] = randomPosition.Next(height); }
        }

            private static void ColumnUptade(int width, int height, int[] y)
            {
                int X;                      //!!HUOM!! OHJEIDEN MUKAAN PIKKU x MUTTA MULLA VSTUDIO TEKEE ISON X !!HUOM!!
            if (Counter > flowSpeed)
            {
                for (X = 0; X < width; ++X)
                {
                    if (X % 10 == 1) Console.ForegroundColor = fadedColor;  //KONSOLIN ETUALA = HAALISTUNUT VÄRI
                    else Console.ForegroundColor = baseColor;   //--> = PERUSVÄRI

                    Console.SetCursorPosition(X, y[X]); //TÄSSÄ ASETETAAN KOHDISTIMEN SIJAINTI
                    Console.Write(AsciiCharacters);

                    if (X % 10 == 9) Console.ForegroundColor = fadedColor;
                    else             Console.ForegroundColor = baseColor;

                    int temp = y[X] - 2;
                    Console.SetCursorPosition(X, YPositionFields(temp, height));
                    Console.Write(AsciiCharacters);

                    int temp1 = y[X] - 20;  //int temp2 vaihto int temp1
                    Console.SetCursorPosition(X, YPositionFields(temp1, height));
                    Console.Write(' ');       // TÄSSÄ OLI BUGI KUN OLIN KIRJOITTANUT Console.Write(''); Miksi näin? (' ')
                    y[X] = YPositionFields(y[X] + 1, height);
                }
            }

            else if (Counter > flowSpeed && Counter < fastFlow)
            {
                for (X = 0; X < width; ++X)
                {
                    Console.SetCursorPosition(X, y[X]);
                    if (X % 10 == 9) Console.ForegroundColor = fadedColor;
                    else Console.ForegroundColor = baseColor;

                    Console.Write(AsciiCharacters);

                    y[X] = YPositionFields(y[X] + 1, height);
                }
            }

            else if (Counter > fastFlow);
            {
                for (X = 0; X < width; ++X)
                {
                    Console.SetCursorPosition(X, y[X]);
                    Console.Write(' ');

                    int temp1 = y[X] - 20;
                    Console.SetCursorPosition(X, YPositionFields(temp1, height));
                    Console.Write(' ');

                    if (Counter > fastFlow && Counter < textFlow)
                    {
                        if (X % 10 == 9)    Console.ForegroundColor = fadedColor;
                        else                Console.ForegroundColor = baseColor;

                        int temp = y[X] - 2;
                        Console.SetCursorPosition(X, YPositionFields(temp, height));
                        Console.Write(AsciiCharacters);
                    }

                    Console.SetCursorPosition(width / 2, height / 2);
                    Console.Write(endText);
                    y[X] = YPositionFields(y[X] + 1, height); 
                }
            }
            }
        }
    }

