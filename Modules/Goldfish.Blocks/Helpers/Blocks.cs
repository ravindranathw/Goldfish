/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Web;

namespace Goldfish.Helpers
{
	/// <summary>
	/// The view helper for the blocks module.
	/// </summary>
	public static class Blocks
	{
		/// <summary>
		/// Gets the block with the given internal id.
		/// </summary>
		/// <param name="internalId">The internal id</param>
		/// <returns>The block content as html</returns>
		public static IHtmlString Get(string internalId) {
			using (var rep = new Goldfish.Blocks.BlockRepository()) {
				var block = rep.GetByInternalId(internalId);

				if (block != null) {
					var converter = new MarkdownSharp.Markdown();
					return new HtmlString(converter.Transform(block.Body));
				}
				return new HtmlString("");
			}
		}
	}
}