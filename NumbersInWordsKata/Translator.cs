﻿using System.Collections.Generic;
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
            {90, "ninety"}
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

            if (Regex.IsMatch(money, @"\d+\.\d+ \\[^\d\.]+"))
            {
                var amountAndCurrency = money.Split(' ', '.');
                var intAmount = int.Parse(amountAndCurrency[0]);
                var decAmount = int.Parse(amountAndCurrency[1]);
                var currency = amountAndCurrency[2];

                string amountTranslated;
                string currencyTranslated;

                if (intAmount > 0)
                {
                    var restIntAmount = intAmount % 10;

                    amountTranslated = GetAmountTranslated(intAmount, restIntAmount);

                    currencyTranslated =
                        intAmount < 2 ? Currencies[currency].SingularUnit : Currencies[currency].PluralUnit;

                    numbersInWords += $"{amountTranslated} {currencyTranslated}";

                    if (decAmount > 0)
                    {
                        numbersInWords += $" {DotInWords} ";
                    }
                }

                if (decAmount > 0)
                {
                    var restDecAmount = decAmount % 10;

                    amountTranslated = GetAmountTranslated(decAmount, restDecAmount);

                    currencyTranslated =
                        decAmount < 2 ? Currencies[currency].SingularSubUnit : Currencies[currency].PluralSubUnit;

                    numbersInWords += $"{amountTranslated} {currencyTranslated}";
                }
            }

            return numbersInWords;
        }

        private static string GetAmountTranslated(int amount, int rest)
        {
            string amountTranslated;

            if (IsOneWordNumber(amount, rest))
            {
                amountTranslated = IntegerTranslations[amount];
            }
            else
            {
                var tens = amount / 10 * 10;
                amountTranslated = $"{IntegerTranslations[tens]} {IntegerTranslations[rest]}";
            }

            return amountTranslated;
        }

        private static bool IsOneWordNumber(double amount, double rest)
        {
            return amount < 21 || rest == 0;
        }
    }
}