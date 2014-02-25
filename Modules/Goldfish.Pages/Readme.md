# Pages module

The **Pages** module adds support for adding pages to your blog. The module can host any amount of pages in a hierarchical navigation structure. Different views can be set to render different pages so you can create any number of unique layouts for you content.

## Adding a page

Pages are easily added through the page Api.

    using (var api = new Goldfish.Pages.Api()) {
        var page = new Page() {
            Seqno = 1,
            Title = "My new page",
            Body = "Welcome to my new page",
            View = "Startpage",
            Published = DateTime.Now
        };
        api.Pages.Add(page);
        api.SaveChanges();
    }
    
This will add a page at the first root level of the navigation structure.

## Routing

Pages are one of the modules that adds routing without the mandatory module prefix. This means that the page with the slug `my-page`will have the url `http://mysite/my-page` and not `http://mysite/pages/my-page`.

## The page entity

The page entity has the following structure:

### Id

The unique id of the page. The id is generated automatically when the page is created if it is empty.

### ParentId

The optional id of the page in the current navigation structure.

### Seqno

The sequence number in the current level of the page. This is the property that orders page with the same parent id.

### Keywords

The keywords used to generate meta, and open graph tags. The keywords has a maximum length of **128** characters.

### Description

The description used to generate meta, and open graph tags. The description has a maximum length of **255** characters.

### Title

The page title. The title is **mandatory** and has a maximum length of **128** characters.

### Slug

The unique slug used to access the page. The slug is **mandatory** and has maximum length of **128** characters.

### Body

The main markdown body of the pages.

### Html

The body transformed to html. This property is **read-only**.

### View

The optional view used to render the page. If the view is not set the view `Pages/Default.cshtml`is used to render the page. The view has a maximum length of **128** characters.

### Created

The date the page was initially created. The created date is stored in **UCT** in the database.

### Updated

The date the page was last updated. The updated date is stored in **UCT** in the database.

### Published

The date the page was published. The published date is stored in **UCT** in the database.

## Navigation

The navigation structure for the current set of pages can be retrieved through the **Navigation** repository of the Pages Api.

    using (var api = new Goldfish.Pages.Api()) {
      var nav = api.Navigation.Get();
    }

The navigation can also be accessed from the views with the helper method **Navigation** when for example sending the structure to a partial view to render the site structure.

    @Html.Partial("Partial/Menu", Pages.Navigation)