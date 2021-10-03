namespace Muharaunda.Core.Contracts
{
    public interface IAppSettings
    {
        public int MinAgeInMonths { get; }
        public int LengthForMobileNumber { get; }
        public int NumberOfDaysToActivateProfile { get; }
        public int MaximumDependentAge { get; }
        public bool ProfileCreationAutoAuthorisation { get; }
    }
}
