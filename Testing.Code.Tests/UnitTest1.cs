using Xunit;

namespace Testing.Code.Tests
{
    public class CurrencyConverterTests
    {
        [Theory]
        [InlineData("Ethereum", -1, true)]
        [InlineData("   ", 1, true)]
        [InlineData("Ethereum", 0, false)]
        [InlineData("Dogecoin", 1, false)]
        public void TestSetPrice(string currency, double price, bool throws)
        {
            var systemUnderTest = new Converter();

            var setPrice = () => systemUnderTest.SetPricePerUnit(currency, price);

            if (throws)
            {
                Assert.Throws<ArgumentException>(setPrice);
            }
            else
            {
                setPrice();
                Assert.Equal(price, systemUnderTest.GetPriceForCurrency(currency));
            }
        }

        [Theory]
        [InlineData("Doge", "ETH", 0, 0)]
        [InlineData("Doge", "ETH", 1, 2)]
        public void ConvertCurrenciesCorrectly(string from, string to, double amount, double expected)
        {
            var systemUnderTest = new Converter();

            systemUnderTest.SetPricePerUnit(from, 2);
            systemUnderTest.SetPricePerUnit(to, 1);
            var result = systemUnderTest.Convert(from, to, amount);

            Assert.Equal(expected, result);
        }
    }
}