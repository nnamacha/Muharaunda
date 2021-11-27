namespace Munharaunda.Core.Constants
{
    public class ResponseConstants
    {
        public const string R00 = "R00";
        public const string R400 = "R400";
        public const string R02 = "R02";
        public const string R03 = "R03";
        public const string R404 = "R404";
        public const string R01 = "R01"; //Technical Error


        //Unhandled exceptions
        public const string R500 = "R500";

        //Response messages
        public const string R00Message = "Success";
        public const string R01Message = "Failure";
        public const string R02Message = "Validation error";
        public const string CREATE_PROFILE_REQUEST_INVALID = "The Create profile request is invalid";
        public const string RECORD_NOT_FOUND = "Records not found";
        public const string INVALID_PROFILE_STATUS = "Invalid Profile status";
        public const string INACTIVE_PROFILE_FOUND = "Inactive profile found";
        public const string AUTHORISED_PROFILE_FOUND = "Authorised profile found";
        public const string PROFILE_AUTHORISATION_FAILED = "Failed to authorise profile";
        public const string PROFILE_UPDATE_FAILED = "Failed to update profile";
        public const string PROFILE_NOT_UNIQUE = "Profile is not unique";
        public const string FUNERAL_NOT_UNIQUE = "funeral is not unique";
        public const string CREATE_FUNERAL_REQUEST_INVALID = "Create funeral request invalid";
        public const string PROFILE_NOT_FOUND = "Profile Not Found";
        public const string PROFILE_NOT_CREATED = "Failed to create profile record";
        public const string FUNERAL_UPDATE_FAILED = "Failed to update Funeral";
        public const string FAILED_PROFILE_STATUS_UPDATE = "Failed to update Profile status";
    }
}
