using EnvDTE;
using System;
using EnvDTE80;
using System.IO;
using System.Linq;
using BuildOptimizer;
using ProjectObjectModel;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;

namespace VSBuildOptimizer.Commands
{
    internal sealed class OptimizedBuildCommand
    {
        public const int CommandId = 0x0111;

        public static readonly Guid CommandSet = new Guid("4b065022-605d-42e4-bc25-fe6765555063");

        public static OptimizedBuildCommand Instance { get; private set; }

        private readonly Package package;
        private IServiceProvider ServiceProvider => package;

        private OptimizedBuildCommand(Package package)
        {
            this.package       = package ?? throw new ArgumentNullException(nameof(package));
            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService == null) return;

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem      = new MenuCommand(MenuItemCallback, menuCommandID);

            commandService.AddCommand(menuItem);
        }

        public static void Initialize(Package package)
        {
            Instance = new OptimizedBuildCommand(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            try
            {
                var dte   = (DTE2) ServiceProvider.GetService(typeof(DTE));
                var model = new Model(Path.GetDirectoryName(dte.Solution.FullName));

                OptimizeAndBuild(dte, model);
            }
            catch (Exception exception)
            {
                MessageWriter.Exception(exception.Message);
            }
        }

        private static void OptimizeAndBuild(DTE2 dte, Model model)
        {
            var projectsToOptimize = GetSelectedProjects(dte, model);
            var buildConfig        = dte.Solution.SolutionBuild.ActiveConfiguration.Name;

            model.Optimize(projectsToOptimize);

            switch (buildConfig)
            {
                case "Debug":
                    ModelBuilder.BuildDebug(model);
                    break;
                case "Release":
                    ModelBuilder.BuildRelease(model);
                    break;
                default:
                    MessageWriter.Exception("Unknown build configuration");
                    break;
            }
        }

        private static IEnumerable<ProjectObjectModel.Project> GetSelectedProjects(DTE2 dte, Model model)
        {
            var selectedItems = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;

            return from selectedItem in selectedItems.Cast<UIHierarchyItem>()
                let projFileInfo = new FileInfo($"{selectedItem.Name}{ProjectObjectModel.Project.Extension}")
                select model.Solution.GetProjectBy(projFileInfo);
        }
    }
}
