using System;
using EnvDTE;
using EnvDTE80;
using System.IO;
using ProjectObjectModel;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSBuildOptimizer.Commands
{
    internal sealed class IndependentProjectsCommand
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("ee225585-93f2-4f3e-8992-98020f3a2dc4");

        public static IndependentProjectsCommand Instance { get; private set; }

        private readonly Package package;
        private IVsOutputWindowPane outputPane;

        private IServiceProvider ServiceProvider => package;

        private IndependentProjectsCommand(Package package)
        {
            outputPane         = OutputWindow.Pane;
            this.package       = package ?? throw new ArgumentNullException(nameof(package));
            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService == null) return;

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem      = new MenuCommand(MenuItemCallback, menuCommandID);

            commandService.AddCommand(menuItem);
        }

        public static void Initialize(Package package)
        {
            Instance = new IndependentProjectsCommand(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            try
            {
                var dte   = (DTE2) ServiceProvider.GetService(typeof(DTE));
                var model = new Model(Path.GetDirectoryName(dte.Solution.FullName));

                ShowIndependentProjects(model);
            }
            catch (Exception exception)
            {
                MessageWriter.Exception(exception.Message);
            }
        }

        private static void ShowIndependentProjects(Model model)
        {
            foreach (var independentProject in model.GetIndependentProjects())
                MessageWriter.Write(independentProject.ToString());
            MessageWriter.Write("");
        }
    }
}
