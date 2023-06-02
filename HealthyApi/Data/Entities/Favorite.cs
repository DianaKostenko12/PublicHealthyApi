namespace HealthyApi.Data.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string FavoriteDiet { get; set; }

        public virtual User User { get; set; }
    }
}
