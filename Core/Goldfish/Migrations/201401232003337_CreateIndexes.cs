/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

namespace Goldfish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateIndexes : DbMigration
    {
		/// <summary>
		/// Apply migration
		/// </summary>
        public override void Up() {
			CreateIndex("Categories", "Slug", true, "IDX_CategorySlug");
			CreateIndex("Params", "InternalId", true, "IDX_ParamInternalId");
			CreateIndex("Posts", "Slug", true, "IDX_PostSlug");
			CreateIndex("Tags", "Slug", true, "IDX_Tagslug");
        }
        
		/// <summary>
		/// Revert migration
		/// </summary>
        public override void Down() {
			DropIndex("Categories", "IDX_CategorySlug");
			DropIndex("Params", "IDX_ParamInternalId");
			DropIndex("Posts", "IDX_PostSlug");
			DropIndex("Tags", "IDX_Tagslug");
        }
    }
}
