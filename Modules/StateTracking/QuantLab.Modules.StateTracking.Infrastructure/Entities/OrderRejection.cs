using System.ComponentModel.DataAnnotations.Schema;

namespace QuantLab.Modules.StateTracking.Infrastructure.Entities
{
    public class OrderRejection
    {
        public Guid Id { get; set; } // Primary key for EF Core

        public Guid OrderId { get; set; } // Foreign key
        public virtual Order Order { get; set; } // Navigation property

        // Store reasons as a serialized string (simple approach)
        public string ReasonsSerialized { get; set; }

        [NotMapped]
        public IEnumerable<string> Reasons
        {
            get => ReasonsSerialized?.Split(';') ?? Enumerable.Empty<string>();
            set => ReasonsSerialized = string.Join(';', value);
        }

        protected OrderRejection() { } // For EF Core

        public OrderRejection(Order order, IEnumerable<string> reasons)
        {
            Order = order;
            Reasons = reasons;
        }
    }
}