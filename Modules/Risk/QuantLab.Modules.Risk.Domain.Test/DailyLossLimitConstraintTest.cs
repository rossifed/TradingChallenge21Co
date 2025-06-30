using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Modules.Risk.Domain.Model.Constraints;

namespace QuantLab.Modules.Risk.Domain.Test
{
    public class DailyLossLimitConstraintTests
    {
        [Theory]
        // limitValue, totalDailyPnL, expectedBreachCount
        [InlineData(10000, -12000, 1)] // Breach: loss exceeds limit
        [InlineData(10000, -9000, 0)]  // No breach: loss within limit
        [InlineData(10000, 5000, 0)]   // No breach: positive PnL
        [InlineData(10000, 0, 0)]      // No breach: zero PnL
        [InlineData(0, -1, 1)]         // Breach: any loss with zero limit
        public void Check_Breach_With_PositionPnL(decimal limitValue, decimal totalDailyPnL, int expectedBreachCount)
        {
            // Arrange
            var constraint = new DailyLossLimitConstraint(limitValue);
            var positionPnl = new PositionPnL(
                PositionId: Guid.NewGuid(),
                UnrealizedPnL: 0m,
                RealizedPnL: 0m,
                DailyUnrealizedPnL: 0m,
                DailyRealizedPnL: 0m,
                MarketPrice: 0m,
                TotalPnL: 0m,
                TotalDailyPnL: totalDailyPnL,
                ComputedOnUtc: DateTime.UtcNow
            );


            var breaches = constraint.Check(positionPnl);


            Assert.Equal(expectedBreachCount, breaches.Count());
        }
    }
}


