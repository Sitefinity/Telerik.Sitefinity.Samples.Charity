Telerik.Sitefinity.Samples.Charity
==================================

The Charity starter kit (Telerik Charity) can be used to quickly create an interactive and engaging non-profit website. Originally inspired by the Microsoft GiveCamp charity events, the Charity starter kit contains a suite of useful custom widgets and helps educate developers about Sitefinity.

Using the Charity starter kit, you can create:

* Social media widgets - including Facebook, Twitter, and Flickr 
* Donation widget with PayPal integration 
* Maps widget - with Bing Maps integration 
* iCal reminder and feeds for events 
* Custom templates and themes 
* Sample contact form 
* Volunteer and job opportunity pages 



### Requirements

* Sitefinity 6.3 license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.


### Installation instructions: SDK Samples from GitHub


1. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.
3. Build the solution.

The project refers to the following NuGet packages:

**Sitefinity.Widgets.Calendar** library

*	DDay.iCal.nupkg – from nugget official package store

* 	Telerik.Sitefinity.Core.nupkg

*	Telerik.Sitefinity.Content.nupkg

*	OpenAccess.Core.nupkg

**Sitefinity.Widgets.Maps** library

* Telerik.Sitefinity.Core.nupkg

**Sitefinity.Widgets.Social** library

*	Telerik.Sitefinity.Core.nupkg

**SitefinityWebApp** library

*	Telerik.Sitefinity.All.nupkg

*	DDay.iCal.nupkg – from nugget official package store

**Telerik.Sitefinity.Samples.Common** library

*	Telerik.Sitefinity.Core.nupkg

*	Telerik.Sitefinity.Content.nupkg

*	OpenAccess.Core.nupkg

**TemplateImporter** library

*	Telerik.Sitefinity.Core.nupkg

*	Telerik.Web.UI.nupkg

*	OpenAccess.Core.nupkg

You can find the packages in the official [Sitefinity Nuget Server](http://nuget.sitefinity.com).

### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password
