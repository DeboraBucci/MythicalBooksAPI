namespace MythicalBooksAPI.Models.Auth
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public int SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        
    }
}
