/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;
using System.Runtime.Serialization;
using System.Web;

namespace Goldfish.Pages
{
	/// <summary>
	/// The main page entity.
	/// </summary>
	[Serializable]
	[DataContract]
	public class Page : Entities.BaseEntity<Page>, Cache.ICacheEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the optional id of the parent page.
		/// </summary>
		[DataMember]
		public Guid? ParentId { get; set; }

		/// <summary>
		/// Gets/sets the sequence number of the page.
		/// </summary>
		[DataMember]
		public int Seqno { get; set; }

		/// <summary>
		/// Gets/sets the meta keywords.
		/// </summary>
		[DataMember]
		public string Keywords { get; set; }

		/// <summary>
		/// Gets/sets the meta description.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Gets/sets the title.
		/// </summary>
		[DataMember]
		public string Title { get; set; }

		/// <summary>
		/// Gets/sets the unique slug.
		/// </summary>
		[DataMember]
		public string Slug { get; set; }

		/// <summary>
		/// Gets/sets the main body.
		/// </summary>
		[DataMember]
		public string Body { get; set; }

		/// <summary>
		/// Gets the main body as html.
		/// </summary>
		[DataMember]
		public IHtmlString Html { get; internal set; }

		/// <summary>
		/// Gets/sets the optional view that should render the page.
		/// </summary>
		[DataMember]
		public string View { get; set; }

		/// <summary>
		/// Gets/sets the date the page was published.
		/// </summary>
		[DataMember]
		public DateTime? Published { get; set; }
		#endregion

		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		public void RemoveFromCache() {
			// Remove the current page
			PagesModule.GetCache<Page>().Remove(Id);

			// Remove the navigation structure
			PagesModule.Cache.Remove(PagesModule.CACHE_NAVIGATION_KEY);
		}

		#region Events
		/// <summary>
		/// Called when the entity is about to get saved.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The current entity state</param>
		public override void OnSave(DbContext db, EntityState state) {
			if (state == EntityState.Added) {
				MoveSeqno(db, Seqno, true);
			} else {
				var orgParentId = (Guid?)db.Entry<Page>(this).OriginalValues["ParentId"];
				var orgSeqno = (int)db.Entry<Page>(this).OriginalValues["Seqno"];

				if (orgParentId != ParentId || orgSeqno != Seqno) { 
					MoveSeqno(db, orgSeqno + 1, false) ;
					MoveSeqno(db, Seqno, true) ;
				}
			}
			base.OnSave(db, state);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Moves the seqno around so that a page can be inserted into the structure.
		/// </summary>
		/// <param name="db">The current db context</param>
		/// <param name="seqno">The seqno</param>
		/// <param name="inc">Whether to increase or decrease</param>
		private void MoveSeqno(DbContext db, int seqno, bool inc) {
			if (ParentId.HasValue)
				db.Database.ExecuteSqlCommand("UPDATE Pages SET Seqno = Seqno " + (inc ? "+ 1" : "- 1") +
					" WHERE ParentId = {0} AND Seqno >= {1}", ParentId.Value, seqno);
			else db.Database.ExecuteSqlCommand("UPDATE Pages SET Seqno = Seqno " + (inc ? "+ 1" : "- 1") +
				" WHERE ParentId IS NULL AND Seqno >= {0}", seqno);
		}
		#endregion
	}
}