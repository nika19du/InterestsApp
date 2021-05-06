namespace InYourInterest.Common
{
    public class ErrorMessages
    {
        public const string UserUsernameLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const string UserPasswordLengthErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";
        public const string UserPasswordsDoNotMatchErrorMessage = "The password and confirmation password do not match.";
        public const string UserChangePasswordDoNotMatchErrorMessage = "The new password and confirmation password do not match.";
        public const string UserAgeRestrictionErrorMessage = "You must be at least {0} years of age.";
        public const string UserProfilePictureUploadErrorMessage = "Incorrect file format.";
        public const string UserInvalidGenderType = "Not valid gender.";
    }
}
