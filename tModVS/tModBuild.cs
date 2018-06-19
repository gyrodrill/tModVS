﻿using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace tModVS
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class tModBuild
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("85cc15e2-457a-4242-b460-66a6f53f9c2c");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="tModBuild"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private tModBuild(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static tModBuild Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in tModBuild's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            whatitis = await package.GetServiceAsync(typeof(DTE)) as DTE;
            Instance = new tModBuild(package, commandService);
        }

        private static DTE whatitis = null;
        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var dte = (Package.GetGlobalService(typeof(SDTE)) as DTE);
            bool cn = dte.LocaleID == 2052;
            var p = dte.ActiveSolutionProjects as Array;
            if (p.Length < 1)
            {
                VsShellUtilities.ShowMessageBox(this.package, cn ? "请打开一个解决方案或项目后使用。" : "Open a solution or project before build tMod.",
                    "tModVS", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            ModCompile.ModProjectFolder = Path.GetDirectoryName((p.GetValue(0) as Project).FullName);
            ModCompile.Build();
            // string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            // string title = "tModBuild";
            // 
            // Show a message box to prove we were here
            // VsShellUtilities.ShowMessageBox(
            //     this.package,
            //     message,
            //     title,
            //     OLEMSGICON.OLEMSGICON_INFO,
            //     OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //     OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
