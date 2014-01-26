# Feed module

The **Feed** module adds support for syndication feeds in RSS 2.0 and Atom 1.0. When installed it automatically generates link tags to the feeds in the following format:

	<link rel="alternate" type="application/rss+xml" title="Blog title" 
		href="http://yourdomain/feed/rss/" />
	<link rel="alternate" type="application/atom+xml" title="Blog title" 
		href="http://yourdomain/feed/atom/" />
		
It also adds a controller listening to requests to the two above URL:s