namespace UsefulParts
{
    internal class EnsureLengthAndUniqueName(string elementName, IEnumerable<string> existingElements, int maxLength)
    {
        private int MaxLength { get; } = maxLength;

        private IEnumerable<string> ExistingElements { get; } = existingElements;

        private string ElementName { get; } = elementName;

        private string EnsuredElementName { set; get; }

        public string Ensure()
        {
            EnsuredElementName = ElementName;

            var counter = 1;

            do EnsuredElementName = EnsureNameLengthLoc(EnsuredElementName);
            while (ExistingElements.Contains(EnsuredElementName));

            return EnsuredElementName;

            string EnsureNameLengthLoc(string nameLoc)
            {
                var numPostfix = "_" + counter;

                var underScoreIndLoc = nameLoc.LastIndexOf("_", StringComparison.Ordinal);
                if (underScoreIndLoc > 0 && underScoreIndLoc <= MaxLength - numPostfix.Length)
                {
                    var tail = nameLoc.Substring(underScoreIndLoc + 1);
                    if (tail.ToCharArray().All(char.IsDigit))
                        counter = int.Parse(tail) + 1;
                    else
                        numPostfix = "_" + counter;
                }
                else
                    numPostfix = "_" + counter;

                if (nameLoc.Length + numPostfix.Length > MaxLength)
                    nameLoc = nameLoc[..(MaxLength - numPostfix.Length)];

                nameLoc += numPostfix;

                counter++;

                return nameLoc;
            }
        }
    }
}