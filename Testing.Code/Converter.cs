namespace Testing.Code
{
    public class Converter
    {
        private Dictionary<string, double> CryptoCurrencies { get; } = new Dictionary<string, double>();

        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(string currencyName, double price)
        {
            if(price < 0)
            {
                throw new ArgumentException("Price was negative", nameof(price));
            }

            if (string.IsNullOrWhiteSpace(currencyName))
            {
                throw new ArgumentException("Currency was invalid", nameof(currencyName));
            }

            _ = CryptoCurrencies.Remove(currencyName);
            CryptoCurrencies.Add(currencyName, price);
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(string fromCurrencyName, string toCurrencyName, double amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Amount cant be negative");
            }

            if(amount == 0)
            {
                return 0;
            }

            var fromCurrencyUsd = GetPriceForCurrency(fromCurrencyName);
            var toCurrencyUsd = GetPriceForCurrency(toCurrencyName);

            return (fromCurrencyUsd / toCurrencyUsd) * amount;
        }

        public double GetPriceForCurrency(string currencyName)
        {
            if(!CryptoCurrencies.TryGetValue(currencyName, out var price))
            {
                throw new ArgumentException("Price for currency not set!");
            }

            return price;
        }
    }
}