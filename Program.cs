using System;
using System.Collections.Generic;
using System.Linq;
using AbstractMovieAssignment.FileManagers;
using NLog;

namespace AbstractMovieAssignment
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly CsvFileHelper CsvFileHelper = new();
        private static readonly Menu Menu = new();

        private static void Main(string[] args)
        {
            var option = 0;
            var choice = 0;
            while (option != 4)
            {
                Menu.Display();
                option = Menu.ValueGetter();
                switch (option)
                {
                    //movies
                    case 1:
                        Console.WriteLine("Do you want to\n1.)Display\n2.)Add");
                        logger.Trace("User chose option 1: Movies");
                        choice = Menu.ValueGetter();
                        CsvFileHelper.Movies();
                        switch (choice)
                        {
                            case 1:
                            {
                                logger.Trace("User chose to search movies");
                                Console.WriteLine("What would you like to search?(Enter for all)");
                                var search = Console.ReadLine();
                                CsvFileHelper.SearchMedia("Movie", search);
                                break;
                            }
                            case 2:
                            {
                                logger.Trace("User chose to add movie");
                                var genresPicked = new List<string>();
                                var id = CsvFileHelper.MovieList.Last().Id + 1;
                                Console.WriteLine("What is the title of the film?");
                                var title = Console.ReadLine();
                                Console.WriteLine("What year was the movie made in?");
                                title = title + " (" + Console.ReadLine() + ")";
                                while (DuplicateChecker(title, "Movie"))
                                {
                                    Console.WriteLine("The film you picked already exists in enter a new one");
                                    title = Console.ReadLine();
                                    Console.WriteLine("Enter the year of the film");
                                    title = title + " (" + Console.ReadLine() + ")";
                                }

                                Console.WriteLine("How many genres do you want to add?");
                                var genreAmount = Menu.ValueGetter();
                                for (var i = 0; i < genreAmount; i++)
                                {
                                    Console.WriteLine($"What is the {i + 1} genre?");
                                    genresPicked.Add(Console.ReadLine());
                                }

                                CsvFileHelper.MovieAdd(id, title, string.Join("|", genresPicked));
                                break;
                            }
                            default:
                                logger.Trace("User made invalid input");
                                Console.WriteLine("Sorry not a choice!");
                                break;
                        }

                        break;
                    //Shows
                    case 2:
                        logger.Trace("User chose option 2: Shows");
                        Console.WriteLine("Do you want to\n1.)Display \n2.)Add");
                        choice = Menu.ValueGetter();
                        CsvFileHelper.Shows(); //reads the videos file and makes the list for it in the CSVFile Class
                        switch (choice)
                        {
                            case 1:
                            {
                                logger.Trace("User chose to search shows");
                                Console.WriteLine("What would you like to search?(Enter for all)");
                                var search = Console.ReadLine();
                                CsvFileHelper.SearchMedia("Show", search);
                                break;
                            }
                            case 2:
                            {
                                logger.Trace("User chose to add show");
                                var writers = new List<string>();
                                var id = CsvFileHelper.ShowsList.Last().Id + 1;
                                Console.WriteLine("What is the title of the show?");
                                var title = Console.ReadLine();
                                while (DuplicateChecker(title, "Show"))
                                {
                                    Console.WriteLine("That show already exists try another");
                                    title = Console.ReadLine();
                                }

                                Console.WriteLine("How many seasons are in the show?");
                                var seasons = Menu.ValueGetter();
                                Console.WriteLine("How many episodes?");
                                var episodes = Menu.ValueGetter();
                                Console.WriteLine("How many writers are there?");
                                var writerCount = Menu.ValueGetter();
                                for (var i = 0; i < writerCount; i++)
                                {
                                    Console.WriteLine($"What is the name of writer #{i + 1}");
                                    writers.Add(Console.ReadLine());
                                }

                                CsvFileHelper.ShowAdd(id, title, seasons, episodes, string.Join('|', writers));
                                break;
                            }
                            default:
                                logger.Trace("User made invalid input");
                                Console.WriteLine("Sorry not a choice!");
                                break;
                        }

                        break;
                    //Videos
                    case 3:
                        logger.Trace("User chose option 3:Videos");
                        Console.WriteLine("Do you want to \n1.)Display\n2.)Add");
                        choice = Menu.ValueGetter();
                        CsvFileHelper.Videos(); //reads the videos file and makes the list for it in the CSVFile Class
                        switch (choice)
                        {
                            case 1:
                            {
                                logger.Trace("User chose to search videos ");
                                Console.WriteLine("What would you like to search?(Enter for all)");
                                var search = Console.ReadLine();
                                CsvFileHelper.SearchMedia("Video", search);
                                break;
                            }
                            case 2:
                            {
                                logger.Trace("User chose to add a video");
                                var regions = new List<string>();
                                var formats = new List<string>();
                                var id = CsvFileHelper.VideoList.Last().Id + 1;
                                Console.WriteLine("What is the title of the video?");
                                var title = Console.ReadLine();
                                while (DuplicateChecker(title, "Video"))
                                {
                                    Console.WriteLine("This Video already exists enter a new title");
                                    title = Console.ReadLine();
                                }

                                Console.WriteLine("How many formats is the video on?");
                                var formatTotals = Menu.ValueGetter();
                                for (var i = 0; i < formatTotals; i++)
                                {
                                    Console.WriteLine($"Format #{i + 1} Ex.DVD,Bluray,VHS");
                                    formats.Add(Console.ReadLine());
                                }
                                Console.WriteLine("How many minutes long is the video?");
                                var length = Menu.ValueGetter();
                                Console.WriteLine("How many regions is it in?");
                                var regionsCount = Menu.ValueGetter();
                                for (var i = 0;
                                    i < regionsCount;
                                    i++) //I don't know the region codes so I made them on the spot
                                {
                                    Console.WriteLine($"Region #{i + 1}?");
                                    Console.WriteLine(
                                        "0.)North America\n1.)South America\n2.)Europe\n3.)Asia\n4.)Australia\n5.)Antarctica");
                                    regions.Add(Console.ReadLine());
                                }

                                CsvFileHelper.VideoAdd(id, title, string.Join('|', formats), length,
                                    string.Join('|', regions));
                                break;
                            }
                            default:
                                logger.Trace("User made an invalid input");
                                Console.WriteLine("Sorry not a choice!");
                                break;
                        }

                        break;
                    //Exit
                    case 4:
                        logger.Debug("User exited the program");
                        Console.WriteLine("Goodbye!");
                        break;
                    //wrong input
                    default:
                        logger.Debug($"User chose {option} not valid");
                        Console.WriteLine("That isn't an option sorry!");
                        break;
                }
            }
        }

        public static bool DuplicateChecker(string chosenMedia, string type)
        {
            var contained = false;
            switch (type)
            {
                case "Movie":
                    CsvFileHelper.Movies();
                    foreach (var media in CsvFileHelper.MovieList)
                        if (media.title.ToLower().Equals(chosenMedia.ToLower()))
                        {
                            contained = true;
                            break;
                        }

                    break;
                case "Show":
                    CsvFileHelper.Movies();
                    foreach (var media in CsvFileHelper.MovieList)
                        if (media.title == chosenMedia)
                        {
                            contained = true;
                            break;
                        }

                    break;
                case "Video":
                    CsvFileHelper.Videos();
                    foreach (var media in CsvFileHelper.VideoList)
                        if (media.title == chosenMedia)
                        {
                            contained = true;
                            break;
                        }

                    break;
            }

            return contained;
        }
    }
}