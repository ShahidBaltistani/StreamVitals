jQuery.Finder.js
================
Finder is a jQuery plugin for web apps and hybrid mobile apps to replicate the "Find" feature available in browsers and other software via Ctrl+F. This is perfect for implementing in hybrid mobile apps, where the browser's features are not available to the user, and for web apps where you need to constrain the "Find" function to a particular porton of the page (e.g., to exclude UI elements from the search).

- [Installation and Setup](#installation-and-setup)
- [Usage](#usage)
- [Example](#example)
- [Attribution](#attribution)
- [License](#license)

## Installation and Setup

This plugin requires jQuery, [jQuery.Highlight](https://github.com/knownasilya/jquery-highlight), and [jQuery.ScrollTo](https://github.com/flesler/jquery.scrollTo) to be included in the project above this plugin. Simply download all three plugins to your project directory, and include them in the `<head>` or just before the closing `</body>` tag of your page like so:

```html
<script src="jquery.highlight.js"></script>
<script src="jquery.scrollto.js"></script>
<script src="jquery.finder.js"></script>
```

The included `style.css` includes some very barebones CSS which you can easily add to via some helpful and descriptive classes:

* `.finder`
* `.finder-input`
* `.finder-count`
* `.btn-finder` + `.btn-finder-prev`, `.btn-finder-next`, and `.btn-finder-close`

You'll also need to include FontAwesome 5 to make the button icons work.

## Usage

1. Define the wrapper by adding the `data-finder-wrapper` attribute to the element. (The attribute doesn't need a value. E.g., `<div class="my-wrapper" data-finder-wrapper></div>` will work.)

	This is the element to which the Finder UI will be attached, and which should contain the content to be searched.
	Note that for the `scrollTo` feature to work correctly, this must be a scrolling element — i.e., it must have a fixed height of some sort with `overflow-y` set to `scroll` or `auto`. For very simple pages, the `<body>` will work just fine.

	A scroll offset can be set with `data-finder-scroll-offset="200"`, where "200" is the number of pixels from the top of the wrapper where the active highlighted element will appear. This will likely require some tweaking depending on your project's needs.

2. Define the content by adding the `data-finder-content` attribute.

	There's no reason this can't be the same element as the wrapper, but you can separate them if you'd like.

3. Define the button or link which should activate the finder by adding the `data-finder-activator` attribute.

	If you'd like to activate the Finder UI by any other means, you can do so simply by calling `finder.activate()` in your own JavaScript code.

## Example

You can see a live example of the plugin at work [here](http://dev.dannymcgee.net/jquery-finder/example.html).

## Attribution

This plugin was created by [Danny McGee](https://github.com/dannymcgee).

## License

MIT