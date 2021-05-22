using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muharaunda.Core.Constants
{
    public static class SystemWideConstants
    {
        public enum ProfileTypes
        {
            Admin,
            Member,
            Dependent
        }

        public enum ProfileStatuses
        {
            Active,
            Flagged,
            Terminated,
            Unauthorised
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
    }
}
