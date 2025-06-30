namespace QuantLab.Modules.Risk.Domain.Model
{
    internal record Position(Guid Id, CryptoPair CryptoPair, decimal Quantity, decimal AverageEntryPrice);


}
