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

            int allApartments = floors * entrances * apartments;
            Console.WriteLine(allApartments);

            string[] apartmentsLightColor = new string[allApartments];
            //string[] apartmentsLightColor = new string[8] { "red", "yellow", "green", "blue", "purple", "black", "www", "rrr" };
            for (int i = 0; i < apartmentsLightColor.Length; i++)
            {
                Console.WriteLine("Color for " + (i + 1) + " apartment");
                apartmentsLightColor[i] = Console.ReadLine();
            }
            for (int f = floors - 1; f >= 0; f--)
            {
                string result = "";
                for (int e = entrances - 1; e >= 0; e--)
                {
                    string aptInEnt = "";
                    for (int a = 0; a < apartments; a++)
                    {
                        int index = e * (floors * apartments) + f * apartments + a;
                        aptInEnt = aptInEnt + apartmentsLightColor[index] + " ";
                    }
                    result = aptInEnt + result;
                }
                Console.WriteLine(result);
            }
        }

        private static int inputValues(string strVariable, int variable)
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
    }
}
