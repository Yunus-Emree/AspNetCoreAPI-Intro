namespace AspNetCoreAPI_Intro.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Property
        public List<City> Cities { get; set; }
    }
}
