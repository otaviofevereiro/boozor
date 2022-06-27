namespace Boozor.Business
{
    public static class StringValueExtensions
    {
        //public static ValueObject<string> Format(this ValueObject<string> value, string mask)
        //{
        //    value =  string.Format(mask, value.Value);
        //}

        //public static ValueObject<string> IsMaxLength(this ValueObject<string> value, int maxLenght)
        //{
        //    if (value.Value is not null && value.Value.Length > maxLenght)
        //        value.Add(new Error($"Campo deve ter no máximo {maxLenght} caracteres."));

        //    return value;
        //}

        //public static ValueObject<string> IsMinLength(this ValueObject<string> value, int minLenght)
        //{
        //    if (value.Value is not null && value.Value.Length < minLenght)
        //        value.Add(new Error($"Campo deve ter no mínimo {minLenght} caracteres."));

        //    return value;
        //}

        public static ValueObject<string> IsRequired(this ValueObject<string> value)
        {
            value.AddValidation(validate);

            return value;

            Result? validate(ValueObject<string> value)
            {
                if (string.IsNullOrWhiteSpace(value.Value))
                    return new Error("Campo obrigatório preenchimento.");

                return Result.Empty;
            }
        }

        private static ValueObject<string> RemoveChars(this ValueObject<string> value, CharTypes charTypes)
        {
            if (charTypes == CharTypes.Alphanumeric)
                value.Value = value.Value.RemoveSpecialCharacteres();
            else if (charTypes == CharTypes.Numeric)
                value.Value = value.Value.ToNumeric();

            return value;
        }

        public static ValueObject<string> ToAlphaNumeric(this ValueObject<string> value)
        {
            value.Value = value.Value.RemoveSpecialCharacteres();

            return value;
        }
    }
}
