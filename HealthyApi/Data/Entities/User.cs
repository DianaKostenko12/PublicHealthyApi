namespace HealthyApi.Data.Entities
{
    public class User
    {
        public long Id { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public string Sex { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
