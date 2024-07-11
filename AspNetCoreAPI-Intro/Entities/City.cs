namespace AspNetCoreAPI_Intro.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Code { get; set; }  //Plaka No
        public int CountryId { get; set; }

        //Navigation Property
        public Country Country { get; set; }

    }
}
