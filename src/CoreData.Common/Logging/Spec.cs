namespace CoreData.Common.Logging
{
    public static class Spec
    {
        static readonly (char open, char close) ActEnclosure = ('<', '>');

        public static readonly string Event = Act("EVENT");
        public static readonly string Fail = Act("FAIL");
        public static readonly string Skip = Act("SKIP");
        /// <summary>Marks string as spec action .</summary>
        public static string Act(string value) => $"{ActEnclosure.open}{value}{ActEnclosure.close}";

        /// <summary>Tags string for better log management</summary>
        /// <seealso cref="https://stackify.com/get-smarter-log-management-with-log-tags/"/>
        public static string Tag(string value) => $"#{value}";
    }
}
