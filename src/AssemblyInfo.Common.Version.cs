using System.Reflection;

[assembly: AssemblyVersion(AssemblyInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyInfo.FileVersion)]
[assembly: AssemblyInformationalVersion(AssemblyInfo.InformationalVersion)]

partial struct AssemblyInfo
{
    const string Major = "5";
    const string Minor = "0";

    internal const string Version = Major + "." + Minor;
    internal const string FileVersion = Version + ".111"; //todo: autoinc
    internal const string InformationalVersion = Version + "-rc1";
}