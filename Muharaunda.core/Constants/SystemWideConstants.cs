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

        public enum Statuses
        {
            Active,
            Flagged,
            Terminated,
            Unauthorised,
            Deceased,
            Closed
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
