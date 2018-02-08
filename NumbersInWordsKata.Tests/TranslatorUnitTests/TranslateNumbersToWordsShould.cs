using FluentAssertions;
using NUnit.Framework;

namespace NumbersInWordsKata.Tests.TranslatorUnitTests
{
    [TestFixture]
    public class TranslateNumbersToWordsShould
    {
        [TestCase("1.00 \\$", "one dollar")]
        [TestCase("1.00 \\£", "one pound")]
        [TestCase("1.00 \\€", "one euro")]
        [TestCase("2.00 \\$", "two dollars")]
        [TestCase("2.00 \\£", "two pounds")]
        [TestCase("2.00 \\€", "two euros")]
        public void ReturnExpectedResult_WhenCalledWithIntegerLessThanTwentyOne(string integerLessThanTwentyOne,
            string expectedResult)
        {
            var actual = Translator.TranslateNumbersToWords(integerLessThanTwentyOne);

            actual.Should().Be(expectedResult);
        }

        [TestCase("21.00 \\$", "twenty one dollars")]
        [TestCase("22.00 \\£", "twenty two pounds")]
        [TestCase("35.00 \\€", "thirty five euros")]
        [TestCase("100.00 \\€", "one hundred euros")]
        [TestCase("230.00 \\$", "two hundred thirty dollars")]
        [TestCase("345.00 \\£", "three hundred forty five pounds")]
        [TestCase("1000.00 \\$", "one thousand dollars")]
        [TestCase("12345.00 \\£", "twelve thousand three hundred forty five pounds")]
        public void ReturnExpectedResult_WhenCalledWithIntegerGreaterThanTwenty(
            string integerGreaterThanTwenty, string expectedResult)
        {
            var actual = Translator.TranslateNumbersToWords(integerGreaterThanTwenty);

            actual.Should().Be(expectedResult);
        }

        [TestCase("0.00 \\$", "")]
        [TestCase("0.01 \\$", "one cent")]
        [TestCase("0.02 \\£", "two pence")]
        [TestCase("0.21 \\£", "twenty one pence")]
        [TestCase("1.01 \\€", "one euro and one cent")]
        [TestCase("1.02 \\€", "one euro and two cents")]
        [TestCase("1.21 \\$", "one dollar and twenty one cents")]
        [TestCase("2.01 \\$", "two dollars and one cent")]
        [TestCase("2.02 \\£", "two pounds and two pence")]
        [TestCase("2.21 \\£", "two pounds and twenty one pence")]
        [TestCase("21.01 \\€", "twenty one euros and one cent")]
        [TestCase("21.02 \\€", "twenty one euros and two cents")]
        [TestCase("21.21 \\$", "twenty one dollars and twenty one cents")]
        public void ReturnExpectedResult_WhenCalledWithDecimalNumber(string decimalNumber, string expectedResult)
        {
            var actual = Translator.TranslateNumbersToWords(decimalNumber);

            actual.Should().Be(expectedResult);
        }
    }
}