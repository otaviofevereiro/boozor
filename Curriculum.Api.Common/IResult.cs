using System.Collections.Generic;

namespace Curriculum.Api.Common
{
    public interface IResult
    {
        IReadOnlyCollection<string> Errors { get; set; }
        bool HasErrors { get; }
        bool HasInformations { get; }
        bool HasWarnings { get; }
        IReadOnlyCollection<string> Informations { get; set; }
        bool IsInvalid { get; }
        bool IsValid { get; }
        IReadOnlyCollection<string> Warnings { get; set; }

        void AddAlert(string alert);
        void AddError(string error);
        void AddInformation(string information);
    }
}