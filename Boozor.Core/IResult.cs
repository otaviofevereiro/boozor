using System.Collections.Generic;

namespace Boozor.Core
{
    public interface IResult
    {
        IReadOnlyCollection<Validation> Errors { get; set; }
        bool HasErrors { get; }
        bool HasInformations { get; }
        bool HasWarnings { get; }
        IReadOnlyCollection<Validation> Informations { get; set; }
        bool IsInvalid { get; }
        bool IsValid { get; }
        IReadOnlyCollection<Validation> Warnings { get; set; }

        void AddAlert(string alert);
        void AddError(string error);
        void AddError(Validation validation);
        void AddInformation(string information);
    }
}