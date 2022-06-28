namespace Boozor.Business
{

    public class Person
    {
        public Name Name { get; }

        public void Rules()
        {
            Name.IsRequired();
        }
    }

    public record Name : Text
    {
        public Name(string? value) : base(value)
        {
        }

        protected override void Rules()
        {
            this.IsMinLength(60);
        }
    }

    public abstract record Text : ValueObject<string>
    {
        public Text(string? value) : base(value)
        {
        }

        public string? FormatedValue { get; private set; }

        public override string ToString()
        {
            return FormatedValue ?? string.Empty;
        }

        //private void Format(string? value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        FormatedValue = value;
        //    else if (!string.IsNullOrEmpty(Mask))
        //        StringValueExtensions.Format(this, Mask);
        //}

        //private string? RemoveChars(string? value)
        //{
        //    switch (AcceptChars)
        //    {
        //        case CharTypes.Alphanumeric:
        //            return value.RemoveSpecialCharacteres();
        //        case CharTypes.Numeric:
        //            return value.ToNumeric();
        //        default:
        //            return value;
        //    }
        //}
    }
}