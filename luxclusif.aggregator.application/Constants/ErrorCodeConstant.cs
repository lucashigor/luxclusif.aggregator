using luxclusif.aggregator.application.Models;

namespace luxclusif.aggregator.application.Constants
{
    public static class ErrorCodeConstant
    {
        public static readonly ErrorModel Generic = new ("0001", "Unfortunately an error occurred during the processing.");
        public static readonly ErrorModel Validation = new ("0002", "Unfortunately your request do not pass in our validation process.");
        public static readonly ErrorModel ErrorOnSavingNewUser = new ("0003", "Unfortunately an error occorred when saving the Order.");
        public static readonly ErrorModel NotificationValuesError = new ("0004", "Error on creating a notification.");
    }
}
