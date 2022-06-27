namespace Boozor.Business
{
    public record Text : ValueObject<string>
    {
        public Text()
        {
        }

        public string? FormatedValue { get; private set; }

        public override string ToString()
        {
            return FormatedValue ?? string.Empty;
        }

        private void Format(string? value)
        {
            if (string.IsNullOrEmpty(value))
                FormatedValue = value;
            else if (!string.IsNullOrEmpty(Mask))
                StringValueExtensions.Format(this, Mask);
        }

        private string? RemoveChars(string? value)
        {
            switch (AcceptChars)
            {
                case CharTypes.Alphanumeric:
                    return value.RemoveSpecialCharacteres();
                case CharTypes.Numeric:
                    return value.ToNumeric();
                default:
                    return value;
            }
        }

        private string? Text_BeforeValueChange(string? value)
        {
            return RemoveChars(value);
        }

        private void Text_ValueChanged(string? value)
        {
            Format(value);
        }
    }
}