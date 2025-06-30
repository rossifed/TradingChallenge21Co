namespace QuantLab.Modules.Risk.Domain.Model
{
    internal record Order(Guid Id, CryptoPair CryptoPair, decimal Quantity);
}
