using System;
using ProjectObjectModel;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSBuildOptimizer
{
    public static class OutputWindow
    {
        public static IVsOutputWindowPane Pane { get; }

        static OutputWindow()
        {
            var outWindow  = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var customGuid = new Guid("8320df0c-39d2-4591-b686-82fb6005f5e5");

            outWindow.CreatePane(ref customGuid, "VSBuildOptimizer", 1, 1);
            outWindow.GetPane(ref customGuid, out IVsOutputWindowPane pane);

            Pane = pane;

            MessageWriter.OnErrorMessage     += args => Pane.OutputString($"Error: {args.Text}\r\n");
            MessageWriter.OnWarningMessage   += args => Pane.OutputString($"Warning: {args.Text}\r\n");
            MessageWriter.OnExceptionMessage += args => Pane.OutputString($"Exception: {args.Text}\r\n");
            MessageWriter.OnSimpleMessage    += args => Pane.OutputString($"{args.Text}\r\n");
        }
        
    }
}