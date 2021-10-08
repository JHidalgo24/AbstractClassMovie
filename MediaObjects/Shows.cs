using CsvHelper.Configuration.Attributes;

namespace AbstractMovieAssignment.MediaObjects
{
    public class Shows : Media
    {
        [Name("season")] public int season { get; set; }
        [Name("episode")] public int episode { get; set; }
        [Name("writers")] public string writersString { get; set; }
        private string[] writersArray{get;set;}

        public string[] WritersList()
        {
            writersArray = writersString.Split('|');
            return writersArray;
        }
        

        public override string Display()
        {
            return $"ShowId:{Id} Title:{title} Seasons:{season} Episodes:{episode} Writers:{writersString} ";
        }
    }
}