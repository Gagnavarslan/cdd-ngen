using FlubuCore.Context;
using FlubuCore.Scripting;

namespace CoreData.Desktop.Build
{
    public class BuildScript : DefaultBuildScript
    {
        protected override void ConfigureBuildProperties(IBuildPropertiesContext context)
        {
            context.Properties.Set(BuildProps.CompanyName, CommonAssemblyInfo.Company);
            context.Properties.Set(BuildProps.CompanyCopyright, CommonAssemblyInfo.Copyright);
            context.Properties.Set(BuildProps.CompanyTrademark, CommonAssemblyInfo.Trademark);
        }

        protected override void ConfigureTargets(ITaskContext context)
        {
        }

        private void ConfigureClientBuildProperties(IBuildPropertiesContext context)
        {
            context.Properties.Set(BuildProps.ProductName, "FlubuExample");
            context.Properties.Set(BuildProps.SolutionFileName, "cdd.v5.client.sln");
        }
    }

    struct CommonAssemblyInfo
    {
        internal const string Company = "Gagnavarslan";
        internal static readonly string Copyright = $"Copyright © {Company} 2011-2019";
        internal const string Trademark = Company + " ™";
    }
}
