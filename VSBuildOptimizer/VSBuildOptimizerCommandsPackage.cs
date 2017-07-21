using VSBuildOptimizer.Commands;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace VSBuildOptimizer
{
    [Guid                        (PackageGuidString)]
    [ProvideMenuResource         ("Menus.ctmenu", 1)]
    [PackageRegistration         (UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0.1", IconResourceID = 400)]
    [SuppressMessage             ("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", 
                                  Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class IndependentProjectsCommandPackage : Package
    {
        public const string PackageGuidString = "236f2095-cf8f-42bf-ae31-8040cbba5134";
        
        protected override void Initialize()
        {
            base                      .Initialize();
            OptimizedBuildCommand     .Initialize(this);
            CheckAllReferencesCommand .Initialize(this);
            NonReferencedProjectsCommand.Initialize(this);
        }
    }
}
