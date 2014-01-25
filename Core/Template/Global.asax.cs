using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Goldfish.Models;

namespace Template
{
    public class MvcApplication : System.Web.HttpApplication
    {
		protected void Session_Start(Object sender, EventArgs e) {
			// This line is just to make sure that even unauthorized used will
			// get a presistent session. The reason for this is so that people
			// commenting can preview their comments before they are approved.
			Session["Started"] = true;
		}

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			Goldfish.Hooks.App.Db.Seed = db => {
				// Add some data
				using (var api = new Goldfish.Api()) {

					var author = api.Authors.Get(a => a.Email == "john@doe.com").SingleOrDefault();
					if (author == null) {
						author = new Author() {
							Name = "John Doe",
							Email = "john@doe.com"
						};
						api.Authors.Add(author);
					}
					api.SaveChanges();

					var post = api.Posts.GetBySlug("my-first-post");
					if (post == null) {
						post = new Post() {
							Author = author,
							Title = "My first post",
							Body =
								"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ullamcorper nulla non metus auctor fringilla. Etiam porta sem malesuada magna mollis euismod. Nullam quis risus eget urna mollis ornare vel eu leo. Nulla vitae elit libero, a pharetra augue. Donec sed odio dui.\n\n" +
								"## Header 2\n\n" +
								"Cras justo odio, dapibus ac facilisis in, egestas eget quam. Nulla vitae elit libero, a pharetra augue. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur blandit tempus porttitor. Curabitur blandit tempus porttitor. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus."
						};
						post.Categories.Add(new Category() {
							Name = "Introduction"
						});
						api.Posts.Add(post);
						api.SaveChanges();

						api.Posts.Publish(post.Id.Value);
						api.SaveChanges();
					}

					if (api.Comments.GetByPostId(post.Id.Value).Count == 0) {
						var comment = new Comment() { 
							PostId = post.Id.Value,
							Author = "Håkan",
							Email = "hakan@tidyui.com",
							Body = 
								"For more information, please visit Goldfish at Github\n\n" +
								"http://github.com/tidyui/goldfish\n\n" + 
								"Happy blogging!",
							IsApproved = true
						};
						api.Comments.Add(comment);
						api.SaveChanges();
					}
				}
			};
        }
    }
}
