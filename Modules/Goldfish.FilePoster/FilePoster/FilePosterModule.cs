﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Web;
using Goldfish.Extend;

namespace Goldfish.FilePoster
{
	/// <summary>
	/// Starts the file poster module.
	/// </summary>
	[Module(Name="File poster", Config=typeof(Config.FilePoster))]
	[Export(typeof(IModule))]
	public class FilePosterModule : Module
	{
		/// <summary>
		/// Initializes the file poster module.
		/// </summary>
		public override void Init() {
			var context = HttpContext.Current;
			var path = context.Server.MapPath("~/App_Data/FilePoster");
			
			// Ensure upload directory
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			// Start the file system watcher
			var watcher = new FileSystemWatcher(path);
			context.Application.Add("FilePoster", watcher);
			context.ApplicationInstance.Disposed += ApplicationDisposed;

			// Add events
			watcher.Created += Poster.FileCreated;
			watcher.Changed += Poster.FileChanged;
			watcher.Renamed += Poster.FileRenamed;
			watcher.Deleted += Poster.FileDeleted;

			watcher.EnableRaisingEvents = true;
		}

		#region Event handlers
		/// <summary>
		/// Dispose the file system watcher when the application disposes
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		static void ApplicationDisposed(object sender, EventArgs e) {
			var application = ((HttpApplication)sender).Application;
			var watcher = (FileSystemWatcher)application["FilePoster"];

			application.Remove("FilePoster");
			watcher.Dispose();
		}
		#endregion
	}
}