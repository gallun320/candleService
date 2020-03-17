using System.Reflection;

namespace GraphCandleApp.Utils
{
    internal static class VersionController
    {
        static VersionController()
        {
            var obj = Assembly.GetExecutingAssembly().GetName().Version;
            Major = obj.Major;
            Minor = obj.Minor;
            Build = obj.Build;
        }

        public static int Major { get; }
        public static int Minor { get; }
        public static int Build { get; }

        public static string GetVersion()
        {
            return $"{Major.ToString()}.{Minor.ToString()}.{Build.ToString()}";
        }
    }
}