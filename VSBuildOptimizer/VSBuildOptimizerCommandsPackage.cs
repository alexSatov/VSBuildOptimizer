using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace VSBuildOptimizer.Commands
{
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class IndependentProjectsCommandPackage : Package
    {
        public const string PackageGuidString = "236f2095-cf8f-42bf-ae31-8040cbba5134";

        #region Package Members

        protected override void Initialize()
        {
            IndependentProjectsCommand.Initialize(this);
            base.Initialize();
            CheckAllReferencesCommand.Initialize(this);
            OptimizedBuildCommand.Initialize(this);
        }

        #endregion
    }
}
