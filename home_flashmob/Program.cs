using System;
using System.Collections.Generic;

namespace home_flashmob
{
    class Ornament
    {
        public int Floor { get; set; }
        public int HorizontalPosition { get; set; }
        public string Color { get; set; }

        public Ornament(int floor, int horizontalPosition, string color)
        {
            Floor = floor;
            HorizontalPosition = horizontalPosition;
            Color = color;
        }
    }

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
            Console.WriteLine("3 - Christmas Tree");
            Console.Write("Enter your choice (1, 2 or 3): ");
            string choice = Console.ReadLine();

            int flagType = 0;
            while (!int.TryParse(choice, out flagType) || (flagType < 1 || flagType > 3))
            {
                Console.WriteLine("Invalid choice. Please enter 1, 2 or 3.");
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
                Console.WriteLine("Error: the Belarusian flag requires at least 3 floors.");
                return null;
            }

            string[] apartmentsLightColor = new string[allApartments];

            int stripeHeight = floors / 3;

            for (int e = 0; e < entrances; e++)
            {
                for (int f = 0; f < floors; f++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int index = e * (floors * apartments) + f * apartments + a;

                        int floorFromTop = floors - 1 - f;

                        int stripeIndex = floorFromTop / stripeHeight;

                        if (stripeIndex == 0)
                        {
                            apartmentsLightColor[index] = "white";
                        }
                        else if (stripeIndex == 1)
                        {
                            apartmentsLightColor[index] = "red";
                        }
                        else if (stripeIndex == 2)
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

        static string[] printChristmasTree(int[] config)
        {
            int floors = config[0];
            int entrances = config[1];
            int apartments = config[2];
            int allApartments = floors * entrances * apartments;
            int totalWidth = entrances * apartments;

            if (floors < 1)
            {
                Console.WriteLine("Error: the Christmas tree requires at least 1 floor.");
                return null;
            }

            string[] apartmentsLightColor = new string[allApartments];

            for (int i = 0; i < allApartments; i++)
            {
                apartmentsLightColor[i] = "black";
            }

            int effectiveWidth = totalWidth;
            int excludedColumn = -1;

            if (totalWidth % 2 == 0)
            {
                effectiveWidth = totalWidth - 1;
                excludedColumn = totalWidth - 1;
            }

            int maxStars = effectiveWidth;
            int starsPerFloor = (maxStars - 1) / 2;

            if (floors - 1 > 0)
            {
                starsPerFloor = (maxStars - 1) / (floors - 1);
            }

            for (int e = 0; e < entrances; e++)
            {
                for (int f = 0; f < floors; f++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int index = e * (floors * apartments) + f * apartments + a;
                        int horizontalPosition = e * apartments + a;

                        if (horizontalPosition == excludedColumn)
                        {
                            continue;
                        }

                        int floorFromTop = floors - 1 - f;

                        int starsOnThisFloor;
                        if (floors == 1)
                        {
                            starsOnThisFloor = maxStars;
                        }
                        else
                        {
                            starsOnThisFloor = 1 + (floorFromTop * 2 * (maxStars - 1)) / (2 * (floors - 1));
                            if (starsOnThisFloor % 2 == 0)
                            {
                                starsOnThisFloor++;
                            }
                        }

                        if (starsOnThisFloor > maxStars)
                        {
                            starsOnThisFloor = maxStars;
                        }

                        int adjustedPosition = horizontalPosition;
                        if (excludedColumn >= 0 && horizontalPosition > excludedColumn)
                        {
                            adjustedPosition = horizontalPosition - 1;
                        }

                        int startPosition = (effectiveWidth - starsOnThisFloor) / 2;
                        int endPosition = startPosition + starsOnThisFloor - 1;

                        if (adjustedPosition >= startPosition && adjustedPosition <= endPosition)
                        {
                            apartmentsLightColor[index] = "green";
                        }
                    }
                }
            }

            List<Ornament> ornaments = GenerateOrnaments(floors, totalWidth, excludedColumn, effectiveWidth);

            foreach (Ornament ornament in ornaments)
            {
                int f = ornament.Floor;
                int horizontalPosition = ornament.HorizontalPosition;

                for (int e = 0; e < entrances; e++)
                {
                    for (int a = 0; a < apartments; a++)
                    {
                        int currentHorizontalPosition = e * apartments + a;
                        if (currentHorizontalPosition == horizontalPosition)
                        {
                            int index = e * (floors * apartments) + f * apartments + a;
                            if (apartmentsLightColor[index] == "green")
                            {
                                apartmentsLightColor[index] = ornament.Color;
                            }
                            break;
                        }
                    }
                }
            }

            return apartmentsLightColor;
        }

        static List<Ornament> GenerateOrnaments(int floors, int totalWidth, int excludedColumn, int effectiveWidth)
        {
            List<Ornament> ornaments = new List<Ornament>();
            Random random = new Random();

            string[] colors = { "red", "yellow", "blue", "purple", "cyan", "white" };

            int ornamentsPerLevel = Math.Max(2, effectiveWidth / 4);
            int levelCount = Math.Max(3, floors / 3);

            for (int level = 0; level < levelCount; level++)
            {
                int floorStart = (floors * level) / levelCount;
                int floorEnd = (floors * (level + 1)) / levelCount;
                int floor = (floorStart + floorEnd) / 2;

                if (floor >= floors) floor = floors - 1;
                if (floor < 1) floor = 1;

                int floorFromTop = floors - 1 - floor;
                int starsOnThisFloor;

                if (floors == 1)
                {
                    starsOnThisFloor = effectiveWidth;
                }
                else
                {
                    starsOnThisFloor = 1 + (floorFromTop * 2 * (effectiveWidth - 1)) / (2 * (floors - 1));
                    if (starsOnThisFloor % 2 == 0)
                    {
                        starsOnThisFloor++;
                    }
                }

                if (starsOnThisFloor > effectiveWidth)
                {
                    starsOnThisFloor = effectiveWidth;
                }

                int startPosition = (effectiveWidth - starsOnThisFloor) / 2;
                int endPosition = startPosition + starsOnThisFloor - 1;

                int availablePositions = starsOnThisFloor;
                int ornamentsOnThisLevel = Math.Min(ornamentsPerLevel, availablePositions / 2);

                if (ornamentsOnThisLevel < 1) ornamentsOnThisLevel = 1;

                for (int i = 0; i < ornamentsOnThisLevel; i++)
                {
                    int positionIndex = (starsOnThisFloor * (i + 1)) / (ornamentsOnThisLevel + 1);
                    int adjustedPosition = startPosition + positionIndex;

                    if (adjustedPosition > endPosition) adjustedPosition = endPosition;

                    int horizontalPosition = adjustedPosition;
                    if (excludedColumn >= 0 && adjustedPosition >= excludedColumn)
                    {
                        horizontalPosition = adjustedPosition + 1;
                    }

                    string color = colors[(level * ornamentsPerLevel + i) % colors.Length];

                    ornaments.Add(new Ornament(floor, horizontalPosition, color));
                }
            }

            return ornaments;
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
            else if (flagType == 3)
            {
                apartmentsLightColor = printChristmasTree(config);
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
