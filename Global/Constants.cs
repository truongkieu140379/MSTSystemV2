using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Global
{
    public static class ConstSwaggerUrl
    {
        public const string HTTPS = "https://";
        public const string TERMS_OF_SERVICE = HTTPS + "www.youtube.com/";
        public const string CONTRACT = HTTPS + "www.youtube.com/";
        public const string LICENSE = HTTPS + "www.youtube.com/";
    }

    public static class GlobalConstants
    {
        //common statuses const
        public const string ACTIVE_STATUS = "Active";
        public const string INACTIVE_STATUS = "Inactive";
        public const string PENDING_STATUS = "Pending";
        public const string DENIED_STATUS = "Denied";
        public const string ONGOING_STATUS = "Ongoing";
        public const string UNPAID_STATUS = "Unpaid";
        public const string ACCEPTED_STATUS = "Accepted";
        public const string SUCCESSFUL_STATUS = "Successful"; 
        public const string FAILED_STATUS = "Failed";
    }

    public static class ConstFirebaseToken
    {
        public const string CLAIM_EMAIL = "email";
        public const string CLAIM_NAME = "name";
        public const string CLAIM_PICTURE = "picture";
    }

    public static class ConstRole
    {
        public const string ROLE_ADMIN = "1";
        public const string ROLE_USER = "2";
        //public const string ROLE_TUTOR = "3";
        //public const string ROLE_TUTEE = "4";
    }

    public static class ConstProjectToken
    {
        public const string ISSUER = "tutor-search-company";
        public const string AUDIENCE_USER = "user";
    }

    public static class ConstApiErrorMessage
    {
        public const string TOKEN_FIREBASE_INVALID_NULL = "firebase token is null or empty";
        public const string TOKEN_FIREBASE_INVALID = "invalid firebase token";
    }
}
