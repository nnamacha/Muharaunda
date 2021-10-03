namespace Muharaunda.Core.Constants
{
    public static class SystemWideConstants
    {
        public enum ProfileTypes
        {
            Admin,
            Member,
            Dependent,
            NextKin,

        }

        public enum ProfileStatuses
        {
            Active,
            Flagged,
            Terminated,
            Unauthorised,
            Deceased
        }
        public enum IdentificationTypes
        {
            ID,
            Passport
        }
        public enum TransactionTypes
        {
            FuneralPayment,
            Income,
            Expense
        }

        // Responses

    }
}
