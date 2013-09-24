Blog2Github
===========

This project is used to convert the blog entries from MetaWeblog providers to Github Pages Octopress format.



## Usage

Please refer to the below screen shot.

![Blog2Github screenshot](https://raw.github.com/fresky/Blog2Github/master/screenshot.png)

1. F5 to run the project. Or download the [binary file](https://raw.github.com/fresky/Blog2Github/master/Blog2Github.zip).  
2. Select the "cnblogs" if your blog provider is "cnblogs". Select "Other" if not, and input the metaweblog url of your blog.  
3. Input your user name and password.  
4. Input the count of blog posts you want to fetch.
5. Input the output folder (should be the Octopress source posts folder, like **d:\fresky.github.io\source\\_posts**) of the .markdown files.  
6. Click the "Generate" button.
7. Run the rake deploy command of Octopress to publish the blogs to Github Pages.


## Requirements

Require [.NET Framework 4.5](http://msdn.microsoft.com/library/vstudio/5a4x27ek).

## Credits
Blog2Github used the [MetaWeblogSharp](http://metaweblogsharp.codeplex.com/) to communicate with MetaWeblog providers.


## License

Blog2Github is released under the MIT License. See the bundled LICENSE file for details.

## Chang Log

1. 09/24/2013	initial version
