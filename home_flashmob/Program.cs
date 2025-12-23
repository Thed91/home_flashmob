using System;

namespace home_flashmob
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int floors = 0;
            int entrances = 0;
            int apartments = 0;

            floors = inputValues(nameof(floors), floors);
            entrances = inputValues(nameof(entrances), entrances);
            apartments = inputValues(nameof(apartments), apartments);

            int[] config = new int[3] { floors, entrances, apartments };

            Console.WriteLine("Which flag do you want to draw?");
            Console.WriteLine("1 - Belarus");
            Console.WriteLine("2 - Georgia");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine();

            int flagType = 0;
            while (!int.TryParse(choice, out flagType) || (flagType != 1 && flagType != 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                choice = Console.ReadLine();
            }

            printFlashmob(config, flagType);
        }

        static string[] printBelarus(int[] config)
        {
            int floors = config[0];
            int entrances = config[1];
            int apartments = config[2];
            int allApartments = floors * entrances * apartments;

            if (floors < 3)
            {
                Console.WriteLine("Error: the Georgian flag requires at least 3 floors.");
                return null;
            }

            string[] apartmentsLightColor = new string[allApartments];

            for (int e = 0; e < entrances; e++)
            {
                for (int f = 0; f < floors; f++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int index = e * (floors * apartments) + f * apartments + a;

                        if (f == floors - 1)
                        {
                            apartmentsLightColor[index] = "white";
                        }
                        else if (f == floors - 2)
                        {
                            apartmentsLightColor[index] = "red";
                        }
                        else if (f == floors - 3)
                        {
                            apartmentsLightColor[index] = "white";
                        }
                        else
                        {
                            apartmentsLightColor[index] = "black";
                        }
                    }
                }
            }

            return apartmentsLightColor;
        }

        static string[] printGeorgia(int[] config)
        {
            int floors = config[0];
            int entrances = config[1];
            int apartments = config[2];
            int allApartments = floors * entrances * apartments;
            int totalWidth = entrances * apartments;

            if (floors <= 11 || totalWidth <= 11)
            {
                Console.WriteLine("Error: the Georgian flag requires more than 11 floors and more than 11 apartments per floor.");
                return null;
            }

            string[] apartmentsLightColor = new string[allApartments];

            for (int i = 0; i < allApartments; i++)
            {
                apartmentsLightColor[i] = "black";
            }

            for (int f = floors - 11; f < floors; f++)
            {
                for (int e = 0; e < entrances; e++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int horizontalPosition = e * apartments + a;

                        if (horizontalPosition < 11)
                        {
                            int index = e * (floors * apartments) + f * apartments + a;
                            int flagRow = floors - 1 - f;
                            int flagCol = horizontalPosition;

                            bool isBigCross = flagRow == 5 || flagCol == 5;

                            bool isSmallCross = false;

                            if ((flagRow == 2 && (flagCol == 1 || flagCol == 2 || flagCol == 3)) ||
                                (flagCol == 2 && (flagRow == 1 || flagRow == 2 || flagRow == 3)))
                                isSmallCross = true;

                            if ((flagRow == 2 && (flagCol == 7 || flagCol == 8 || flagCol == 9)) ||
                                (flagCol == 8 && (flagRow == 1 || flagRow == 2 || flagRow == 3)))
                                isSmallCross = true;

                            if ((flagRow == 8 && (flagCol == 1 || flagCol == 2 || flagCol == 3)) ||
                                (flagCol == 2 && (flagRow == 7 || flagRow == 8 || flagRow == 9)))
                                isSmallCross = true;

                            // Нижний правый (центр в 8,8)
                            if ((flagRow == 8 && (flagCol == 7 || flagCol == 8 || flagCol == 9)) ||
                                (flagCol == 8 && (flagRow == 7 || flagRow == 8 || flagRow == 9)))
                                isSmallCross = true;

                            if (isBigCross || isSmallCross)
                            {
                                apartmentsLightColor[index] = "red";
                            }
                            else
                            {
                                apartmentsLightColor[index] = "white";
                            }
                        }
                    }
                }
            }

            return apartmentsLightColor;
        }

        static void printFlashmob(int[] config, int flagType)
        {
            int floors = config[0];
            int entrances = config[1];
            int apartments = config[2];

            string[] apartmentsLightColor = null;

            if (flagType == 1)
            {
                apartmentsLightColor = printBelarus(config);
            }
            else if (flagType == 2)
            {
                apartmentsLightColor = printGeorgia(config);
            }

            if (apartmentsLightColor == null)
            {
                return;
            }

            Console.WriteLine();
            for (int f = floors - 1; f >= 0; f--)
            {
                for (int e = 0; e < entrances; e++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int index = e * (floors * apartments) + f * apartments + a;
                        coloredStar(apartmentsLightColor[index]);
                    }
                }
                Console.WriteLine();
            }
        }

        static int inputValues(string strVariable, int variable)
        {
            Console.WriteLine("How many " + strVariable + " are there in the house?");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out variable))
            {
                Console.WriteLine("Enter a valid value");
                input = Console.ReadLine();
            }
            return variable;
        }

        static ConsoleColor getConsoleColor(string colorName)
        {
            if (colorName == "red") return ConsoleColor.Red;
            else if (colorName == "yellow") return ConsoleColor.Yellow;
            else if (colorName == "green") return ConsoleColor.Green;
            else if (colorName == "blue") return ConsoleColor.Blue;
            else if (colorName == "purple") return ConsoleColor.Magenta;
            else if (colorName == "black") return ConsoleColor.Black;
            else if (colorName == "white") return ConsoleColor.White;
            else if (colorName == "gray") return ConsoleColor.Gray;
            else if (colorName == "cyan") return ConsoleColor.Cyan;
            else return ConsoleColor.White;
        }
        static void coloredStar(string colorName)
        {
            ConsoleColor color = getConsoleColor(colorName);
            Console.ForegroundColor = color;
            Console.Write("* ");
            Console.ResetColor();
        }
    }
}
