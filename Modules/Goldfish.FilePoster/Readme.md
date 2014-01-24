# FilePoster module

The **FilePoster** module enables the blog owner to publish and manager posts by uploading markdown files to the server. The file poster can do the following things:

* **Create** and **publish** new posts
* **Update** existing posts
* **Rename** existing posts
* **Remove** existing posts

## Config

The following configuration is available for the module.

### Default author

	Goldfish.Config.FilePoster.DefaultAuthorId

Gets/sets which author should be used when creating posts.

## How to post

When the module is activated the folder `App_Data/FilePoster` is created which posts should be uploaded to. In it's simplest form, publishing a new post is just a matter of uploading or saving a new **markdown** file to this folder. The name of the post is determinied by the filename.

Let's for example say we add a new file called `My first post.md`, this will result in a new post named **My first post** with the slug **my-first-post**.

Please note that since the title is determined from the the filename, leading title should not be included in the body of the post.

## Parameters

You can specify more parameters by adding some lines in the beginning of your mardown. The available parameters are `Keywords`, `Description`, `Categories`, `Tags` and `Publish`. This could for example look like this for our first post:

	keywords: Blog, Post, First
	description: This is my first post ever
	categories: General, Newbie stuff
	tags: Rambling
	
	Welcome to my first post! It's not the longest but it's sooo good.

### Keywords

The keywords are used for rendering meta-tags for search engine optimization. 

### Description

The description is used for rendering meta-tags as well as **open graph** tags for the post.

### Categories

A comma separated list of category names. If the categories doesn't exist they are created with the post.

### Tags

A comma separated list of tag names. If the tags doesn't exist they are created with the post.

### Publish

Specifies the date that the post should be available for others to read. If this parameter is omitted the post is published right away.