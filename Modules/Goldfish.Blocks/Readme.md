# Blocks module

The **Blocks** module add support for adding reusable blocks of markdown content to your blog. These blocks can then be used in the different views.

## Adding a block

Blocks are easily added through the blocks Api.

	using (var api = new Goldfish.Blocks.Api()) {
		var block = new Goldfish.Blocks.Block() {
		  InternalId = "start",
		  Name = "Start",
		  Body = "This is my startpage content"
		}
		api.Add(block);
		api.SaveChanges();
	}

## Using a block

The blocks can be accessed in the views by using the blocks helper. For example, the **start** block created in the earlier example could be displayed by adding the following line to  **start.cshtml**.

    @Blocks.Get("start")
    
The helper will convert the markdown into HTML before rendering it to the view.

## The block entity

The block entity has the following properties.

### Id

The unique identifier used as id in the database.

### InternalId

The unique textual id used to access the blocks from the views.

### Name

The name of the block that should be displayed to content managers.

### Body

The main markdown content of the block.
