using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NumbersInWordsKata
{
    public class Translator
    {
        private static readonly Dictionary<int, string> IntegerTranslations = new Dictionary<int, string>
        {
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "seventeen"},
            {18, "eighteen"},
            {19, "nineteen"},
            {20, "twenty"},
            {30, "thirty"},
            {40, "forty"},
            {50, "fifty"},
            {60, "sixty"},
            {70, "seventy"},
            {80, "eighty"},
            {90, "ninety"},
            {100, "hundred"}
        };

        private static readonly Dictionary<string, Currency> Currencies = new Dictionary<string, Currency>
        {
            {"\\$", new Currency("dollar", "cent")},
            {"\\€", new Currency("euro", "cent")},
            {"\\£", new Currency("pound", "pounds", "penny", "pence")}
        };

        private static readonly string DotInWords = "and";

        public static string TranslateNumbersToWords(string money)
        {
            var numbersInWords = string.Empty;

            if (!Regex.IsMatch(money, @"\d+\.\d+ \\[^\d\.]+")) return numbersInWords;

            var amountAndCurrency = money.Split(' ', '.');
            var intAmount = int.Parse(amountAndCurrency[0]);
            var decAmount = int.Parse(amountAndCurrency[1]);
            var currency = amountAndCurrency[2];

            string amountTranslated;
            string currencyTranslated;

            if (intAmount > 0)
            {
                amountTranslated = GetAmountTranslated(intAmount);
                currencyTranslated = GetCurrencyTranslated(intAmount, currency, UnitType.Unit);
                numbersInWords += $"{amountTranslated} {currencyTranslated}";
            }

            if (decAmount > 0)
            {
                amountTranslated = GetAmountTranslated(decAmount);
                currencyTranslated = GetCurrencyTranslated(decAmount, currency, UnitType.SubUnit);

                var hasIntegerPart = intAmount > 0;

                if (hasIntegerPart)
                    numbersInWords += $" {DotInWords} {amountTranslated} {currencyTranslated}";
                else
                    numbersInWords += $"{amountTranslated} {currencyTranslated}";
            }

            return numbersInWords;
        }

        private static string GetAmountTranslated(int amount)
        {
            var amountTranslated = string.Empty;

            var hundreds = amount / 100;

            if (hundreds > 0)
            {
                amountTranslated = SetOrAppendAmountTranslated(amountTranslated, hundreds) + " hundred";
                amount -= hundreds * 100;
            }

            var tens = amount / 10;

            if (tens > 0)
            {
                amountTranslated = SetOrAppendAmountTranslated(amountTranslated, tens * 10);
                amount -= tens * 10;
            }
                
            if (amount > 0)
                amountTranslated = SetOrAppendAmountTranslated(amountTranslated, amount);

            return amountTranslated;
        }

        private static string SetOrAppendAmountTranslated(string amountTranslated, int place)
        {
            var translation = IntegerTranslations[place];

            amountTranslated += string.IsNullOrEmpty(amountTranslated)
                ? $"{translation}"
                : $" {translation}";
            return amountTranslated;
        }

        private static string GetCurrencyTranslated(int amount, string currency, UnitType unitType)
        {
            if (unitType == UnitType.Unit)
                return amount < 2 ? Currencies[currency].SingularUnit : Currencies[currency].PluralUnit;

            return amount < 2 ? Currencies[currency].SingularSubUnit : Currencies[currency].PluralSubUnit;
        }
    }

    internal enum UnitType
    {
        Unit,
        SubUnit
    }
}