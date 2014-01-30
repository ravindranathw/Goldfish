# Goldfish

Goldfish is a **lightweight**, **modular** and **high performance** blogging platform for ASP.NET MVC. The source code has been designed ground-up to be easy, robust and to follow best pratices and patterns. It is built using the following packages:

* ASP.NET MVC 5.1
* ASP.NET Identity 1.0
* Entity Framework 6.0.2
* Microsoft Extensibility Framework
* WebActivatorEx 2.0.4
* MarkdownSharp 1.13
* RazorGenerator.Mvc 2.2.1
* AutoMapper 3.1.1
* TinyIoC 1.2

Goldfish was initially created as a sandbox project to test new concepts for the 3.0 release of Piranha CMS, however, after a while I realized that the Piranha wouldn't be the only fish in the pond.

## License

Goldfish is released under the **MIT** license giving you permission to do pretty much **anything** you want with the source code as long as you include the copyright notices.

## Current status

The project is still in early development and lacks a lot of features to be considered a full release. To check out the current status, post feature requests or report bugs, please refer to the [issue list](http://github.com/goldfish/issues).

If you like the looks of the project **please** contribute to it by **forking** it, adding new functionality or modules and sending me a **pull request**.

## Features

### Extreme page speed

With the provided default theme Goldfish renders a single post, with comments in less than **10ms** for consecutive requests. This is a result of using all of the **async** features of .NET 4.5 together with smart **caching** that minimizes roundtrips to the database.

### Posts

* Posts are written in Markdown
* Automatic SEO optimized friendly URL:s
* Keywords, description and open graph tags
* Publish right away or at a future date
* Categorize and tag posts
* Browser cache with ETags and LastModified

### Comments

* Anti-forgery token support
* Stripping of HTML-tags in submitted comments
* Link-generation with `rel="nofollow"`
* Configurable moderation
* Gravatar support
* Access to pending comments for the authors

### Archives

* Standard archive of all blog pots with paging and date filter
* Category archives with paging
* Tag archives with paging

### Syndication

* RSS 2.0 & Atom 1.0 feeds for all blog posts

### Themes

* Themes control **all** aspects of the UI, no more shady default layouts to override
* Theme views based on C# Razor syntax `.cshtml`

### Security

* OAuth integration with ASP.NET Identity
* Multiple admin accounts
* Multiple authors


## Architecture

* Clean repository layer for manipulating all entities
* Asynchronous support for all get methods
* Integration tests of all repositories
* Replacable & mockable api
* Entity Framework Code First data model & migrations
* Hooks for injecting code into all significant events
* No unnecessary dependencies or packages

## Deployment

* Can be hosted in any environment that delivers .NET 4.5
* Works will all databases supported by Entity Framework