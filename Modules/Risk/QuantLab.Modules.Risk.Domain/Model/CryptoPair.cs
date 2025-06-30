namespace QuantLab.Modules.Risk.Domain.Model
{
    internal record CryptoPair(Crypto Base, Crypto Quote)
    {

        public override string ToString() => $"{Base}{Quote}";

        private static readonly HashSet<string> Symbols = new HashSet<string>
    {
        "BTC", "ETH", "SOL", "USDC", "MNT", "XRP", "ARB", "PEPE",
        "SUI", "WIF", "PENGU", "ADA", "VIRTUAL", "LINK", "SPX",
        "SEI", "LTC", "BCH", "TIA", "AFG", "USDT"
    };

        internal bool IsBase(Crypto crypto)
            => Base == crypto;

        internal bool IQuote(Crypto crypto)
            => Quote == crypto;
        internal bool Contains(Crypto crypto)
         => IsBase(crypto) || IQuote(crypto);
        internal static CryptoPair Parse(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

            symbol = symbol.Trim().ToUpperInvariant();

            for (int i = 1; i < symbol.Length; i++)
            {
                var left = symbol.Substring(0, i);
                var right = symbol.Substring(i);

                if (Symbols.Contains(left) && Symbols.Contains(right))
                {
                    return new CryptoPair(new Crypto(left), new Crypto(right));
                }
            }

            throw new FormatException($"Invalid trading pair symbol: '{symbol}'");
        }
    }
}
