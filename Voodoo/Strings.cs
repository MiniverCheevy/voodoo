namespace Voodoo
{
    public struct Strings
    {
        public struct SortDirection
        {
            public static readonly string Descending = "DESC";
            public static readonly string Ascending = "ASC";
        }

        public struct Validation
        {
            public static string requestIsRequired = "Request is required.";
            public static string validationErrorsOccurred = "Please correct errors and try again.";
            public static string enumerationMustBeAnEnum = "enumeration must be an enum";
        }
    }
}