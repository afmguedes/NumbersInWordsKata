namespace NumbersInWordsKata
{
    class Currency
    {
        public string SingularUnit { get; }
        public string PluralUnit { get; }
        public string SingularSubUnit { get; }
        public string PluralSubUnit { get; }

        public Currency(string singularUnit, string singularSubUnit)
        {
            SingularUnit = singularUnit;
            PluralUnit = $"{singularUnit}s";
            SingularSubUnit = singularSubUnit;
            PluralSubUnit = $"{singularSubUnit}s";
        }
        public Currency(string singularUnit, string pluralUnit, string singularSubUnit, string pluralSubUnit)
        {
            SingularUnit = singularUnit;
            PluralUnit = pluralUnit;
            SingularSubUnit = singularSubUnit;
            PluralSubUnit = pluralSubUnit;
        }
    }
}