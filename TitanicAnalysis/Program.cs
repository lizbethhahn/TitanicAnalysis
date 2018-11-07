﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TitanicAnalysis
{
    class Program
    {
        private static string female;
        private static string male;

        static void Main(string[] args)
        {
           
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "TitanicData.csv");
            var passengers = ReadTitanicData(fileName);

            StringBuilder menu = new StringBuilder();
            menu.Append("----------------------------");
            menu.Append("\nTo return a list of passengers, enter 1.");
            menu.Append("\nTo sort surviors by gender, press 2 for female, press 3 for male");
            menu.Append("\n----------------------------");
            menu.Append("\nEnter Q to quit");

            Console.WriteLine(menu.ToString());

            
            var fileContents = passengers;
            var input = Console.ReadLine();
            do
            {
             
                switch (input)
                {
                    case "1":
                        PrintList(fileContents);
                        break;
                    case "2":
                        fileContents = FemaleSurvivor(passengers, female);
                        Console.WriteLine("Found " + passengers.ToString() + " female passengers.");
                        break;
                    case "3":
                        fileContents = MaleSurvivor(passengers, male);
                        Console.WriteLine("Found " + fileContents.ToString() + " male passengers.");
                        break;

                }
                
            } 
            while (input.ToUpper() != "Q");           
        }


        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<Passenger> ReadTitanicData(string fileName)
        {
            var titanicData = new List<Passenger>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var passenger = new Passenger();
                    string[] value = line.Split(',');

                    int parseInt;
                    //if (int.TryParse(value[0], out parseInt))
                    //{
                    //    passenger.PassClass = parseInt;
                    //}
                    if (int.TryParse(value[5], out parseInt))
                    {
                        passenger.Age = parseInt;
                    }
                    passenger.Survived = value[1];
                    passenger.LastName = value[2];
                    passenger.FirstName = value[3];
                    passenger.Sex = value[4];



                    titanicData.Add(passenger);
                }
            }
            return titanicData;
        }
        private static void PrintList(List<Passenger> passengers)
        {
            foreach (var passenger in passengers)
            {
                Console.WriteLine(passenger.ToString());
            }
        }

        public static List<Passenger> FemaleSurvivor(List<Passenger> passengers, string Sex)
        {
            List<Passenger> femaleSurvivor = new List<Passenger>();
            foreach (Passenger passenger in passengers)
            {
                if (passenger.Sex == "female" && passenger.Survived == "1")
                {
                    femaleSurvivor.Add(passenger);
                }
            }
            return femaleSurvivor;
        }

        public static List<Passenger> MaleSurvivor(List<Passenger> passengers, string Sex)
        {
            List<Passenger> maleSurvivor = new List<Passenger>();
            foreach (Passenger passenger in passengers)
            {
                if (passenger.Sex == "male" && passenger.Survived == "1")
                {
                    maleSurvivor.Add(passenger);
                }
            }
            return maleSurvivor;
        }
    }
}