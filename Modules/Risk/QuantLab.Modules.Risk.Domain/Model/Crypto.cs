namespace QuantLab.Modules.Risk.Domain.Model
{
    internal record Crypto(string Symbol)
    {
        internal static Crypto BTC => new Crypto("BTC");
        internal bool IsSymbol(string symbol)
            => Symbol == symbol;
        public override string ToString() => Symbol;
    }


}
