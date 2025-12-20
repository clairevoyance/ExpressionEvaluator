namespace ExpressionEvaluator.Api.Models
{
    public class ExpressionRecord
    {
        public int Id { get; set; }                         // Primary Key
        public string Expression { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;  // Stored as string
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}