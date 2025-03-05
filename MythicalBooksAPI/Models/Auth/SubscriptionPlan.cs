namespace MythicalBooksAPI.Models.Auth
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double PriceMonth { get; set; }
        public double PriceYear { get; set; }
        public string? Delivery {  get; set; }
        public string? Discounts { get; set; }
        public string? Chest { get; set; }
        public string? MythicalCoins {  get; set; }
        public string? UserSupport { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    }
}
