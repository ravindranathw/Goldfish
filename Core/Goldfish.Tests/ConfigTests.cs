/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goldfish.Tests
{
	/// <summary>
	/// Integration tests for the application config.
	/// </summary>
	[TestClass]
	public class ConfigTests
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ConfigTests() { 
			App.Init();
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config"), WorkItem(2)]
		public void ArchivePageSize() { 
			Assert.AreEqual(Config.Blog.ArchivePageSize, 5);
			Config.Blog.ArchivePageSize = 4;
			Assert.AreEqual(Config.Blog.ArchivePageSize, 4);
			Config.Blog.ArchivePageSize = 5;
			Assert.AreEqual(Config.Blog.ArchivePageSize, 5);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config"), WorkItem(1)]
		public void BlogTitle() { 
			Assert.AreEqual(Config.Blog.Title, "Goldfish");
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void CacheEnabled() {
			Assert.AreEqual(Config.App.Cache.IsEnabled, true);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void CacheExpiration() {
			Assert.AreEqual(Config.App.Cache.Expires, 30);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void CommentType() {
			Assert.AreEqual(Config.Blog.Comments.Type, Models.CommentType.Enabled);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void CommentModerateAnonymous() {
			Assert.AreEqual(Config.Blog.Comments.ModerateAnonymous, false);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void CommentModerateAuthorized() {
			Assert.AreEqual(Config.Blog.Comments.ModerateAuthorized, false);
		}

		/// <summary>
		/// Tests that the config can be read with its default values.
		/// </summary>
		[TestMethod, TestCategory("Config")]
		public void Theme() { 
			Assert.AreEqual(Config.App.Theme, "Casper");
		}
	}
}
