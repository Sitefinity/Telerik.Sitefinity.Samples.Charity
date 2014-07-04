using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using SitefinityWebApp.App_Custom.iCal;
using SitefinityWebApp.Widgets.SampleLayoutControl;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Modules.Blogs.Web.UI;
using Telerik.Sitefinity.Modules.Blogs.Web.UI.Public;
using Telerik.Sitefinity.Modules.Events.Web.UI;
using Telerik.Sitefinity.Modules.Events.Web.UI.Public;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.News.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Samples.Common;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        private const string BaseTemplateName = "Base Template";
        private const string CharityBlueThemeName = "CharityBlueTheme";
        private const string BaseTemplateId = "4166a221-7131-442a-8f19-000000000002";
        private const string OneColumnTemplateId = "4166a221-7131-442a-8f19-000000000003";
        private const string TwoColumnsTemplateId = "4166a221-7131-442a-8f19-000000000004";
        private const string CharityBlueThemePath = "~/App_Data/Sitefinity/WebsiteTemplates/CharityBlue/App_Themes/CharityBlue";
        private const string BaseTemplateMasterPage = "~/App_Data/Sitefinity/WebsiteTemplates/CharityBlue/App_Master/Base.master";
        private const string HomePageId = "A6F9DB68-70CB-422B-BE43-000000000002";
        private const string AboutUsPageId = "A6F9DB68-70CB-422B-BE43-000000000003";
        private const string NewsPageId = "A6F9DB68-70CB-422B-BE43-000000000004";
        private const string BlogPageId = "A6F9DB68-70CB-422B-BE43-000000000005";
        private const string EventsPageId = "A6F9DB68-70CB-422B-BE43-000000000006";
        private const string DonatePageId = "A6F9DB68-70CB-422B-BE43-000000000007";
        private const string VolunteerPageId = "A6F9DB68-70CB-422B-BE43-000000000008";
        private const string ContactUsPageId = "A6F9DB68-70CB-422B-BE43-000000000009";
        private const string CharityBlogId = "f0e51514-aee5-429a-af66-000000000001";
        private const string ContactFormId = "c3e7abbd-5978-4de2-824a-000000000001";

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup       
            SystemManager.ApplicationStart += this.SystemManager_ApplicationStart;
        }

        private void SystemManager_ApplicationStart(object sender, EventArgs e)
        {
            SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(this.CreateSample);
            SystemManager.RunWithElevatedPrivilege(worker);
        }

        private void CreateSample(object[] args)
        {            
            var availableLanguages = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages;
            if (availableLanguages.Length <= 1)
            {
                SampleUtilities.CreateUsersAndRoles();

                SampleUtilities.UploadImages(HttpRuntime.AppDomainAppPath + "CharityImages", "SiteImages");

                ConfigManager.Executed += this.ConfigManager_Executed;

                this.CreateNewsItems();
                this.CreateBlogPosts();
                this.CreateEvents();
                this.RegisterControlTemplates();
                this.CreateCharityBlueThemeAndTemplates();
                this.CreateHomePage();
                this.CreateAboutUsPage();
                this.CreateNewsPage();
                this.CreateBlogPage();
                this.CreateEventsPage();
                this.CreateDonatePage();
                this.CreateContactUsPage();
                this.CreateFeeds();
                this.AddContactUsPhonesToBackend();
            }
        }

        private void AddContactUsPhonesToBackend()
        {
            Guid templateID = SiteInitializer.DefaultBackendTemplateId;

            PageManager pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Id == templateID).FirstOrDefault();
            var templateControls = template.Controls.Where(t => t.ObjectType == typeof(ContentBlockBase).FullName);
            bool containsPhones = false;

            foreach (var control in templateControls)
            {
                var block = pageManager.LoadControl(control) as ContentBlockBase;
                if (block.Html.Contains("Contact us"))
                {
                    containsPhones = true;
                }
            }

            if (!containsPhones)
            {
                var phonesContentBlock = new ContentBlockBase();
                phonesContentBlock.Html = @"<span class=""phone""><strong>Contact us:</strong>
                                        <ul>
                                            <li class=""phone-list""><strong>US</strong> +1-888-365-2779</li>
                                            <li class=""phone-list""><strong>UK</strong> +44-20-7291-0580</li>
                                            <li class=""phone-list""><strong>AU</strong> +61-2-8090-1465</li>
                                            <li class=""phone-list""><strong>DE</strong> +49-89-2441642-70</li>
                                        </ul>
                                        </span>";
                SampleUtilities.AddControlToTemplate(templateID, phonesContentBlock, "Header", "Contact Us");

                var cssControl = new CssEmbedControl();
                cssControl.CustomCssCode =
                    @".phone
                {
                font-size: 11px;
                position: absolute;
                right: 20px;
                float: right;
                width: 200px;
                padding-top: 5px;
                color: white;
                margin-right: 220px;
                }
                li.phone-list
                {
                line-height: 14px;
                font-size: 11px;
                }
                a.sfMoreDetails, a.sfMoreDetails:link, a.sfMoreDetails:visited, a.sfMoreDetails:hover, a.sfMoreDetails:active
                {
                height: 15px;
                padding: 3px;
                background-color: #0889DD;
                color: #ffffff;
                border-bottom-left-radius: 5px;
                border-bottom-right-radius: 5px;
                border-top-left-radius: 5px;
                border-top-right-radius: 5px;
                }";

                SampleUtilities.AddControlToTemplate(templateID, cssControl, "Header", "CSS");
            }
        }

        private void CreateNewsItems()
        {
            var newsCreated = App.WorkWith().NewsItems().Get().Count() > 0;

            if (!newsCreated)
            {
                var author = "admin";
                var newsTitle = "Charity News Item A";
                var newsContent = @"<div id=""lipsum"">
<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris sit amet nisi ac risus scelerisque fermentum non sit amet purus. Morbi dolor sapien, luctus ac commodo posuere, scelerisque eu arcu. Mauris a dolor risus, quis mattis mauris. Mauris tellus lacus, auctor quis hendrerit ut, vulputate quis est. Sed massa est, gravida eu cursus et, laoreet non risus. Praesent bibendum condimentum tellus, ut blandit lorem vehicula mattis. Nam at nisl quis lacus accumsan tempor. Mauris viverra aliquam lorem, et molestie odio mattis in. Integer non ipsum lacus. Morbi sed justo arcu. Pellentesque volutpat scelerisque ligula nec ultrices. Mauris quam ipsum, fringilla ac molestie ac, interdum ut eros. Etiam sit amet lacus orci, quis eleifend nibh. </p>
<p> Praesent mauris odio, venenatis eu suscipit vel, rhoncus eu est. Nam semper diam a massa aliquam bibendum. Mauris eget quam orci, eu gravida odio. Curabitur tellus enim, rutrum sed venenatis nec, aliquet vitae nibh. Aenean eget nisi non libero cursus sagittis. Sed pellentesque eleifend libero, sed eleifend ipsum interdum at. Mauris eget ultricies odio. Donec eget fringilla lectus. Curabitur elit lacus, aliquam in facilisis eu, laoreet faucibus sem. Fusce varius dolor at nulla facilisis id semper massa sodales. </p>
<p> Aenean placerat viverra risus hendrerit dictum. Quisque mattis, magna id egestas lobortis, dui lorem egestas orci, sit amet pulvinar felis odio quis ligula. Vivamus erat nisi, lobortis vitae tincidunt a, blandit eu lectus. Fusce vitae purus vitae sapien adipiscing molestie fringilla ut tortor. Quisque ipsum tellus, hendrerit in lobortis vitae, placerat sed dui. Maecenas vehicula vehicula nibh non ullamcorper. Nam pulvinar, dolor id mollis pretium, sem urna posuere ligula, quis facilisis dui eros non lectus. Nulla sodales venenatis quam, at molestie risus tincidunt quis. Curabitur et justo vel arcu condimentum suscipit. Aenean a odio massa. Praesent aliquam mauris sit amet lectus venenatis ac vestibulum massa egestas. Nunc in velit quam. Fusce malesuada dolor eu erat rutrum cursus in eu erat. Maecenas mi justo, interdum eget ultricies non, viverra sed libero. </p>
<p> Duis neque odio, pellentesque vitae fermentum vel, condimentum sed nisi. Nulla interdum, tortor nec ultrices congue, lectus mi tincidunt ante, et egestas massa erat vitae enim. Integer sagittis interdum elit. Suspendisse potenti. Integer a ligula nibh, ac consequat felis. Cras a magna nunc, a semper odio. Donec nulla mauris, luctus quis egestas nec, malesuada eu ligula. Vivamus malesuada lorem id massa sollicitudin in sagittis felis pellentesque. Cras vel nulla eget nunc tristique hendrerit. Ut porttitor mattis cursus. </p>
<p> Integer in mauris in eros malesuada ullamcorper. Phasellus vel nunc vel nunc malesuada tristique. Sed in felis quis nisl vehicula ultrices. Cras nec nunc eu massa imperdiet aliquam. Ut interdum, leo ut adipiscing egestas, lectus purus commodo felis, ac condimentum purus massa ut nibh. Vestibulum non nisi leo, eget faucibus nunc. In hac habitasse platea dictumst. Maecenas mauris urna, sollicitudin ac rhoncus quis, adipiscing ac massa. Duis et nisl augue. Vestibulum ullamcorper lectus egestas diam euismod ultrices. Quisque quis luctus mi. </p>
</div>";
                var summary = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris sit amet nisi ac risus scelerisque fermentum non sit amet purus. Morbi dolor sapien, luctus ac commodo posuere, scelerisque eu arcu. Mauris a dolor risus, quis mattis mauris. Mauris tellus lacus, auctor quis hendrerit ut, vulputate quis est.";

                SampleUtilities.CreateNewsItem(newsTitle, newsContent, summary, author);

                newsTitle = "Charity News Item B";

                SampleUtilities.CreateNewsItem(newsTitle, newsContent, summary, author);

                newsTitle = "Charity News Item C";

                SampleUtilities.CreateNewsItem(newsTitle, newsContent, summary, author);
            }
        }

        private void CreateBlogPosts()
        {
            SampleUtilities.CreateBlog(new Guid(CharityBlogId), "Charity Blog", "Charity Blog");

            var postsCreated = App.WorkWith().Blog(new Guid(CharityBlogId)).BlogPosts().Get().Count() > 0;

            if (!postsCreated)
            {
                var title = "Charity Starter Kit now available!";
                var content = @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus non lectus at sem consequat suscipit. Etiam nunc sem, condimentum a feugiat sit amet, consectetur sed justo. Nullam purus erat, tincidunt sed commodo ac, mollis vel neque. Vestibulum volutpat nulla in enim aliquet vitae ornare nunc gravida. </p>
<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Etiam ipsum nisl, tempor at tincidunt quis, congue vel purus. Duis sodales, est quis mollis posuere, mi mauris mollis sapien, ut rhoncus leo justo sed dolor. Etiam blandit tincidunt velit ac pellentesque. Cras nec leo lacus. Maecenas vel ipsum vitae dolor ultrices tempus. </p>
<p>Nam a erat felis, a pellentesque diam. Phasellus nibh lectus, adipiscing nec pharetra in, semper eu ipsum. Nam venenatis dignissim risus nec fringilla. Quisque placerat leo non ligula dictum sed auctor neque bibendum. Proin arcu risus, ullamcorper in dictum sed, tincidunt et odio. Praesent sed orci eget metus commodo egestas.</p>
<p>Quisque sollicitudin tellus quis est egestas nec pretium leo volutpat. Aliquam euismod viverra sollicitudin. Morbi quis leo velit, eget tincidunt nisi. Phasellus non convallis nunc. Ut ligula lorem, hendrerit eget eleifend quis, ullamcorper vel quam. Quisque ullamcorper dolor quis nunc volutpat eget elementum nunc lobortis. </p>
<p>Aenean laoreet sodales libero eget feugiat. Praesent purus nibh, blandit eu fermentum ac, pulvinar sed mi. Sed lobortis euismod tortor, vitae mattis nulla pulvinar quis. Quisque a augue a felis consectetur aliquam ac non metus. Fusce sapien erat, ultrices vitae scelerisque sed, gravida ut elit. In hac habitasse platea dictumst. Aliquam quis lectus sit amet lectus porta imperdiet. Pellentesque sed libero vitae nulla elementum venenatis nec vitae dolor. Quisque egestas purus id nulla fringilla sed malesuada ligula venenatis. In sit amet purus velit. Quisque ac aliquam urna.</p>";
                var author = "admin";

                SampleUtilities.CreateBlogPost(new Guid(CharityBlogId), title, content, author, string.Empty);

                title = "Introduction";

                SampleUtilities.CreateBlogPost(new Guid(CharityBlogId), title, content, author, string.Empty);
            }
        }

        private void CreateEvents()
        {
            var eventsCreated = App.WorkWith().Events().Get().Count() > 0;

            if (!eventsCreated)
            {
                var title = "Charity Event";
                var content = @"<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu justo vehicula sapien mollis eleifend quis quis nisi. In erat est, consectetur ut tristique eu, aliquam vitae felis. Suspendisse ut velit dolor. Morbi ac mollis diam. Integer malesuada, dui in tincidunt congue, sapien odio semper justo, egestas tincidunt nulla lorem et nibh. </p>
<p>Donec congue pellentesque dictum. Cras ullamcorper rhoncus ullamcorper. Donec in dolor lacus, et luctus ante. In iaculis, odio ac dictum feugiat, metus risus laoreet elit, ac faucibus dui lacus a ligula. Vivamus sapien quam, pulvinar id sagittis a, rhoncus placerat enim. Mauris condimentum posuere volutpat. Integer eget interdum justo. Quisque in tellus ipsum. Praesent vitae erat nec mauris eleifend varius vitae at augue. </p>
<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis imperdiet, sem nec iaculis tincidunt, nisi quam fringilla dolor, id venenatis nulla tortor a neque. Nam id massa sit amet purus aliquam tempor. </p>";
                var startDate = new DateTime(2011, 12, 01, 18, 0, 0);
                var endDate = new DateTime(2011, 12, 02, 4, 0, 0);
                var street = "Organization";
                var city = "123 Fake Str";
                var state = "TX";
                var country = "USA";
                var contactEmail = "no-reply@telerik.com";
                var contactName = "John Doe";
                var contactWeb = "http://www.sitefinity.com";

                SampleUtilities.CreateEvent(title, content, startDate, endDate, street, city, state, country, contactEmail, contactName, contactWeb);

                title = "Fundraiser";
                content = @"<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu justo vehicula sapien mollis eleifend quis quis nisi. In erat est, consectetur ut tristique eu, aliquam vitae felis. Suspendisse ut velit dolor. Morbi ac mollis diam. Integer malesuada, dui in tincidunt congue, sapien odio semper justo, egestas tincidunt nulla lorem et nibh. Donec congue pellentesque dictum. Cras ullamcorper rhoncus ullamcorper. Donec in dolor lacus, et luctus ante. In iaculis, odio ac dictum feugiat, metus risus laoreet elit, ac faucibus dui lacus a ligula. Vivamus sapien quam, pulvinar id sagittis a, rhoncus placerat enim. Mauris condimentum posuere volutpat. Integer eget interdum justo. Quisque in tellus ipsum. Praesent vitae erat nec mauris eleifend varius vitae at augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis imperdiet, sem nec iaculis tincidunt, nisi quam fringilla dolor, id venenatis nulla tortor a neque. Nam id massa sit amet purus aliquam tempor. </p>";
                startDate = new DateTime(2011, 12, 12, 3, 0, 0);
                endDate = new DateTime(2011, 12, 12, 7, 0, 0);

                SampleUtilities.CreateEvent(title, content, startDate, endDate, street, city, state, country);

                title = "January Board Meeting";
                content = @"<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu justo vehicula sapien mollis eleifend quis quis nisi. In erat est, consectetur ut tristique eu, aliquam vitae felis. Suspendisse ut velit dolor. Morbi ac mollis diam. Integer malesuada, dui in tincidunt congue, sapien odio semper justo, egestas tincidunt nulla lorem et nibh. Donec congue pellentesque dictum. Cras ullamcorper rhoncus ullamcorper. Donec in dolor lacus, et luctus ante. In iaculis, odio ac dictum feugiat, metus risus laoreet elit, ac faucibus dui lacus a ligula. Vivamus sapien quam, pulvinar id sagittis a, rhoncus placerat enim. Mauris condimentum posuere volutpat. Integer eget interdum justo. Quisque in tellus ipsum. Praesent vitae erat nec mauris eleifend varius vitae at augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis imperdiet, sem nec iaculis tincidunt, nisi quam fringilla dolor, id venenatis nulla tortor a neque. Nam id massa sit amet purus aliquam tempor. </p> <p> Donec cursus tristique leo, id sodales ante pharetra a. Sed gravida, massa ut scelerisque malesuada, massa nisl tincidunt libero, at pharetra ipsum lacus id arcu. Ut non sem non arcu placerat malesuada ac at lectus. Sed quis odio ac felis tristique auctor. Cras aliquam egestas lorem sed varius. Duis viverra vulputate malesuada. Quisque id arcu nunc. Donec blandit, orci consequat interdum pretium, mauris orci iaculis lectus, vel vestibulum tortor risus vitae quam. Nulla felis tellus, gravida lobortis sodales et, varius quis lacus. Fusce risus mi, bibendum ac rhoncus non, gravida eu velit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent molestie nibh at nunc mollis faucibus. Praesent fermentum tempor lorem in egestas. Duis at sapien et nisi ornare tristique. Pellentesque fringilla, ipsum id tincidunt porta, massa metus rutrum diam, sit amet faucibus ante enim vitae mi. Nullam dignissim sem magna. Etiam molestie dapibus ligula tincidunt imperdiet. Suspendisse id mi eu nulla condimentum interdum sed a massa. Maecenas justo enim, accumsan sit amet commodo sit amet, fermentum posuere odio. </p>";
                startDate = new DateTime(2011, 1, 4, 21, 0, 0);
                endDate = new DateTime(2011, 1, 4, 23, 0, 0);

                SampleUtilities.CreateEvent(title, content, startDate, endDate);

                title = "New Years Eve Celebration";
                content = @"<p> Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin eu justo vehicula sapien mollis eleifend quis quis nisi. In erat est, consectetur ut tristique eu, aliquam vitae felis. Suspendisse ut velit dolor. Morbi ac mollis diam. Integer malesuada, dui in tincidunt congue, sapien odio semper justo, egestas tincidunt nulla lorem et nibh. Donec congue pellentesque dictum. Cras ullamcorper rhoncus ullamcorper. Donec in dolor lacus, et luctus ante. In iaculis, odio ac dictum feugiat, metus risus laoreet elit, ac faucibus dui lacus a ligula. Vivamus sapien quam, pulvinar id sagittis a, rhoncus placerat enim. Mauris condimentum posuere volutpat. Integer eget interdum justo. Quisque in tellus ipsum. Praesent vitae erat nec mauris eleifend varius vitae at augue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Duis imperdiet, sem nec iaculis tincidunt, nisi quam fringilla dolor, id venenatis nulla tortor a neque. Nam id massa sit amet purus aliquam tempor. </p> <p> Donec cursus tristique leo, id sodales ante pharetra a. Sed gravida, massa ut scelerisque malesuada, massa nisl tincidunt libero, at pharetra ipsum lacus id arcu. Ut non sem non arcu placerat malesuada ac at lectus. Sed quis odio ac felis tristique auctor. Cras aliquam egestas lorem sed varius. Duis viverra vulputate malesuada. Quisque id arcu nunc. Donec blandit, orci consequat interdum pretium, mauris orci iaculis lectus, vel vestibulum tortor risus vitae quam. Nulla felis tellus, gravida lobortis sodales et, varius quis lacus. Fusce risus mi, bibendum ac rhoncus non, gravida eu velit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent molestie nibh at nunc mollis faucibus. Praesent fermentum tempor lorem in egestas. Duis at sapien et nisi ornare tristique. Pellentesque fringilla, ipsum id tincidunt porta, massa metus rutrum diam, sit amet faucibus ante enim vitae mi. Nullam dignissim sem magna. Etiam molestie dapibus ligula tincidunt imperdiet. Suspendisse id mi eu nulla condimentum interdum sed a massa. Maecenas justo enim, accumsan sit amet commodo sit amet, fermentum posuere odio. </p>";
                startDate = new DateTime(2011, 1, 1, 5, 0, 0);
                endDate = new DateTime(2011, 1, 1, 10, 0, 0);

                SampleUtilities.CreateEvent(title, content, startDate, endDate);
            }
        }

        private void RegisterControlTemplates()
        {
            var templatePath = "SitefinityWebApp.Widgets.Templates.BlogPostsListTemplate.ascx";
            var assemblyName = "SitefinityWebApp";

            SampleUtilities.RegisterAspNetControlTemplate("Blog List", templatePath, assemblyName, typeof(MasterPostsView));

            templatePath = "SitefinityWebApp.Widgets.Templates.EventsDateListTemplate.ascx";

            SampleUtilities.RegisterAspNetControlTemplate("Events Date List", templatePath, assemblyName, typeof(MasterView));

            templatePath = "SitefinityWebApp.Widgets.Templates.EventsSidebarTemplate.ascx";

            SampleUtilities.RegisterAspNetControlTemplate("Events Sidebar", templatePath, assemblyName, typeof(MasterView));

            templatePath = "SitefinityWebApp.Widgets.Templates.EventsCalendarTemplate.ascx";

            SampleUtilities.RegisterAspNetControlTemplate("Events Calendar", templatePath, assemblyName, typeof(MasterView));

            templatePath = "SitefinityWebApp.Widgets.Templates.EventsDetailsiCalTemplate.ascx";

            SampleUtilities.RegisterAspNetControlTemplate("Event Details with iCal", templatePath, assemblyName, typeof(Telerik.Sitefinity.Modules.Events.Web.UI.Public.DetailsView));
        }

        private void CreateCharityBlueThemeAndTemplates()
        {
            SampleUtilities.RegisterTheme(CharityBlueThemeName, CharityBlueThemePath);
            this.CreateBaseTemplate();
            this.CreateOneColumnTemplate();
            this.CreateTwoColumnsTemplate();
        }

        private void CreateBaseTemplate()
        {
            var result = SampleUtilities.RegisterTemplate(new Guid(BaseTemplateId), BaseTemplateName, BaseTemplateName, BaseTemplateMasterPage, CharityBlueThemeName);

            if (result)
            {
                var headerLayoutControl = new SampleLayoutControl();
                headerLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                headerLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.HeaderLayoutTemplate.ascx";
                headerLayoutControl.ID = "Header";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), headerLayoutControl, "Body", "50% + 50% (custom)");

                var navigationControl = new NavigationControl();
                navigationControl.NavigationMode = NavigationModes.HorizontalDropDownMenu;
                navigationControl.NavigationAction = NavigationAction.OnMouseOver;
                navigationControl.Skin = "menu";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), navigationControl, "Body", "Navigation");

                var bannerLayoutControl = new SampleLayoutControl();
                bannerLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                bannerLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.BannerLayoutTemplate.ascx";
                bannerLayoutControl.ID = "Banner";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), bannerLayoutControl, "Body", "50% + 50% (custom)");

                var mainLayoutControl = new SampleLayoutControl();
                mainLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                mainLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.MainLayoutTemplate.ascx";
                mainLayoutControl.ID = "Base";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), mainLayoutControl, "Body", "100% (custom)");

                var footerLayoutControl = new SampleLayoutControl();
                footerLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                footerLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.FooterLayoutTemplate.ascx";
                footerLayoutControl.ID = "Footer";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), footerLayoutControl, "Body", "100% (custom)");

                var templateLogoBlock = new ContentBlockBase();
                templateLogoBlock.Html = string.Format(@"<a sfref=""[pages]{0}"" href=""/home"">
                                        <h1>Telerik Charity Kit</h1>
                                        </a>
                                        <h2>givecamp</h2>", HomePageId);

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), templateLogoBlock, "Header_Logo", "Content Block");

                ImageControl leftBannerImage = new ImageControl();
                leftBannerImage.ImageId = SampleUtilities.GetImageId("leftBanner.jpg");

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), leftBannerImage, "Banner_Left", "Image");

                var rightBannerBlock = new ContentBlockBase();
                rightBannerBlock.Html = string.Format(@"<h3>Some slogan here&nbsp;</h3>
                                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.</p>
                                        <p><a sfref=""[pages]{0}"" href=""/donate"">Make a donation</a></p>", DonatePageId);

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), rightBannerBlock, "Banner_Right", "Content Block");

                var footerNavigationControl = new NavigationControl();
                footerNavigationControl.Skin = "footer_menu";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), footerNavigationControl, "Footer_Content", "Navigation");

                var footerBlock = new ContentBlockBase();
                footerBlock.Html = @"<p>All rights reserved | <a target=""_blank"" href=""http://www.telerik.com"">Telerik</a> Corp.&reg; 2011</p>";

                SampleUtilities.AddControlToTemplate(new Guid(BaseTemplateId), footerBlock, "Footer_Content", "Content Block");
            }
        }

        private void CreateOneColumnTemplate()
        {
            SampleUtilities.CreateBasedOnTemplate(new Guid(BaseTemplateId), new Guid(OneColumnTemplateId), "OneColumn", "One Column", CharityBlueThemeName);
        }

        private void CreateTwoColumnsTemplate()
        {
            var result = SampleUtilities.CreateBasedOnTemplate(new Guid(BaseTemplateId), new Guid(TwoColumnsTemplateId), "TwoColumns", "Two Columns", CharityBlueThemeName);

            if (result)
            {
                var twoColumnsLayoutControl = new SampleLayoutControl();
                twoColumnsLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                twoColumnsLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.TwoColumnsLayoutTemplate.ascx";
                twoColumnsLayoutControl.ID = "Main_TwoColumns";

                SampleUtilities.AddControlToTemplate(new Guid(TwoColumnsTemplateId), twoColumnsLayoutControl, "Base_Content", "67% + 33% (custom)");
            }
        }

        private void CreateHomePage()
        {
            var result = SampleUtilities.CreatePage(new Guid(HomePageId), "Home");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(HomePageId), new Guid(BaseTemplateId));

                var mainLayoutControl = new SampleLayoutControl();
                mainLayoutControl.AssemblyInfo = typeof(SampleLayoutControl).FullName;
                mainLayoutControl.Layout = "SitefinityWebApp.Widgets.SampleLayoutControl.ThreeColumnsCustomLayoutTemplate.ascx";
                mainLayoutControl.ID = "Main";

                SampleUtilities.AddControlToPage(new Guid(HomePageId), mainLayoutControl, "Base_Content", "33% + 34% +33% (custom)");

                var mainBottomContentBlock = new ContentBlockBase();
                var signatureImageId = SampleUtilities.GetImageId("signature.jpg");
                var signatureImageUrl = SampleUtilities.GetImageDefaultUrl("signature.jpg");

                mainBottomContentBlock.Html = string.Format(@"<blockquote>
                                                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem 
                                                        accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae 
                                                        ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt 
                                                        explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut 
                                                        odit aut fugit</p>
                                                        <p><img sfref=""[images]{0}"" src=""{1}"" /></p>
                                                        </blockquote>", signatureImageId, signatureImageUrl);

                SampleUtilities.AddControlToPage(new Guid(HomePageId), mainBottomContentBlock, "Base_Content", "Content Block");

                ImageControl leftImage = new ImageControl();
                leftImage.ImageId = SampleUtilities.GetImageId("people.jpg");

                SampleUtilities.AddControlToPage(new Guid(HomePageId), leftImage, "Main_Left", "Image");

                var leftColumnContentBlock = new ContentBlockBase();

                leftColumnContentBlock.Html = string.Format(@"<a sfref=""[pages]{0}"" href=""/about-us"">
                                                        <h2>Making a Difference</h2></a>
                                                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem 
                                                        accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae 
                                                        ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt 
                                                        explicabo.</p>", AboutUsPageId);

                SampleUtilities.AddControlToPage(new Guid(HomePageId), leftColumnContentBlock, "Main_Left", "Content Block");

                ImageControl centerImage = new ImageControl();
                centerImage.ImageId = SampleUtilities.GetImageId("globe.jpg");

                SampleUtilities.AddControlToPage(new Guid(HomePageId), centerImage, "Main_Center", "Image");

                var centerColumnContentBlock = new ContentBlockBase();

                centerColumnContentBlock.Html = string.Format(@"<a sfref=""[pages]{0}"" href=""/blog"">
                                                            <h2>Blog</h2></a>
                                                            <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem 
                                                            accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae 
                                                            ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt 
                                                            explicabo.</p>", VolunteerPageId);

                SampleUtilities.AddControlToPage(new Guid(HomePageId), centerColumnContentBlock, "Main_Center", "Content Block");

                ImageControl rightImage = new ImageControl();
                rightImage.ImageId = SampleUtilities.GetImageId("writing.jpg");

                SampleUtilities.AddControlToPage(new Guid(HomePageId), rightImage, "Main_Right", "Image");

                var rightColumnContentBlock = new ContentBlockBase();

                rightColumnContentBlock.Html = string.Format(@"<a sfref=""[pages]{0}"" href=""/events"">
                                                        <h2>Events</h2></a>
                                                        <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem 
                                                        accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab 
                                                        illo inventore veritatis et quasi architecto beatae vitae dicta sunt 
                                                        explicabo.</p>", EventsPageId);

                SampleUtilities.AddControlToPage(new Guid(HomePageId), rightColumnContentBlock, "Main_Right", "Content Block");
            }
        }

        private void CreateAboutUsPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(AboutUsPageId), "About Us");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(AboutUsPageId), new Guid(OneColumnTemplateId));

                var titleContentBlock = new ContentBlockBase();
                titleContentBlock.Html = "<h1>About Us</h1>";

                SampleUtilities.AddControlToPage(new Guid(AboutUsPageId), titleContentBlock, "Base_Content", "Content Block");

                var overviewContentBlock = new ContentBlockBase();
                overviewContentBlock.Html = @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed ligula 
                                        vitae est posuere congue sed pretium quam. Pellentesque at velit in massa ornare 
                                        sagittis at in lacus. Fusce placerat leo eu sem sagittis posuere. Cras a arcu nec 
                                        turpis pulvinar vehicula. Proin sed metus nec leo rutrum mattis vel vel est. Pellentesque 
                                        lacinia nulla id lorem cursus fringilla.</p>
                                        <p>Proin non commodo velit. Vestibulum ante ipsum primis in faucibus orci luctus et 
                                        ultrices posuere cubilia Curae; Donec elementum imperdiet volutpat. Integer sit amet felis 
                                        in magna iaculis molestie vel in velit. Nulla facilisi. Sed iaculis imperdiet turpis, in 
                                        vulputate lorem mattis non. In condimentum, neque vitae elementum sodales, metus mi congue 
                                        urna, a imperdiet sem massa at felis. Aliquam erat volutpat. Nullam eget lorem enim, at 
                                        venenatis felis. Maecenas et euismod diam.</p>
                                        <p>Pellentesque tortor tellus, cursus dignissim hendrerit non, ornare a tellus. Donec ut 
                                        consequat massa. Phasellus fermentum, justo vel congue tincidunt, metus diam sagittis eros, 
                                        sed ornare tellus enim sit amet sem. Duis gravida tortor eget risus varius vel ullamcorper 
                                        risus semper. </p>";

                SampleUtilities.AddControlToPage(new Guid(AboutUsPageId), overviewContentBlock, "Base_Content", "Content Block");

                var ourMissionContentBlock = new ContentBlockBase();
                ourMissionContentBlock.Html = @"<h2>Our mission</h2>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vel massa eu magna 
                                            egestas blandit. Pellentesque habitant morbi tristique senectus et netus et malesuada 
                                            fames ac turpis egestas.</p>
                                            <ol>
                                                <li>Suspendisse sed ultrices mi.&nbsp;</li>
                                                <li>Aliquam ac ipsum dolor.</li>
                                                <li>Maecenas vitae magna&nbsp; </li>
                                            </ol>";

                SampleUtilities.AddControlToPage(new Guid(AboutUsPageId), ourMissionContentBlock, "Base_Content", "Content Block");

                FacebookLike facebookLike = new FacebookLike();
                facebookLike.UrlToLike = "http://www.facebook.com/sitefinity";
                facebookLike.Width = "280";
                SampleUtilities.AddControlToPage(new Guid(AboutUsPageId), facebookLike, "Base_Content", "Facebook LikeBox");
            }
        }

        private void CreateNewsPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(NewsPageId), "News");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(NewsPageId), new Guid(TwoColumnsTemplateId));

                var templateKey = SampleUtilities.GetControlTemplateKey(typeof(MasterListView), "Titles, dates and summaries");
                SampleUtilities.RegisterNewsFrontendView("NewsFrontend", templateKey, typeof(MasterListView), "NewsFrontendTitleDatesAndSummariesList");

                var newsControl = new NewsView();
                newsControl.MasterViewName = "NewsFrontendTitleDatesAndSummariesList";

                SampleUtilities.AddControlToPage(new Guid(NewsPageId), newsControl, "Main_TwoColumns_Content", "News");

                TwitterFeed twitterFeed = new TwitterFeed();
                twitterFeed.Username = "Sitefinity";
                twitterFeed.ShowTimestamps = true;

                SampleUtilities.AddControlToPage(new Guid(NewsPageId), twitterFeed, "Main_TwoColumns_Sidebar", "Twitter Feed");
            }
        }

        private void CreateBlogPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(BlogPageId), "Blog");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(BlogPageId), new Guid(OneColumnTemplateId));

                var templateKey = SampleUtilities.GetControlTemplateKey(typeof(MasterPostsView), "Blog List");
                SampleUtilities.RegisterBlogPostsFrontendView("BlogPostsFrontend", templateKey, typeof(MasterPostsView), "BlogPostsFrontendBlogList");

                var blogsControl = new BlogPostView();
                blogsControl.MasterViewName = "BlogPostsFrontendBlogList";

                SampleUtilities.AddControlToPage(new Guid(BlogPageId), blogsControl, "Base_Content", "Blog posts");

                var subscribeTitleBlock = new ContentBlockBase();
                subscribeTitleBlock.Html = "<h2>Subscribe - <a href=\"/Feeds/charity-blog\" class=\"rssfeed\">RSS Feed</a></h2>";

                SampleUtilities.AddControlToPage(new Guid(BlogPageId), subscribeTitleBlock, "Base_Content", "Content Block");
            }
        }

        private void CreateEventsPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(EventsPageId), "Events");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(EventsPageId), new Guid(OneColumnTemplateId));

                var templateKey = SampleUtilities.GetControlTemplateKey(typeof(MasterView), "Events Date List");
                SampleUtilities.RegisterEventsFrontendView("EventsFrontend", templateKey, typeof(MasterView), "EventsFrontendDateList");

                var eventsControl = new EventsView();
                eventsControl.MasterViewName = "EventsFrontendDateList";
                eventsControl.ControlDefinition.GetDefaultDetailView().TemplateKey = SampleUtilities.GetControlTemplateKey(typeof(Telerik.Sitefinity.Modules.Events.Web.UI.Public.DetailsView), "Event Details with iCal");

                SampleUtilities.AddControlToPage(new Guid(EventsPageId), eventsControl, "Base_Content", "Events");
            }
        }

        private void CreateDonatePage()
        {
            var result = SampleUtilities.CreatePage(new Guid(DonatePageId), "Donate");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(DonatePageId), new Guid(OneColumnTemplateId));

                var titleContentBlock = new ContentBlockBase();
                titleContentBlock.Html = "<h1>Make a donation</h1>";

                SampleUtilities.AddControlToPage(new Guid(DonatePageId), titleContentBlock, "Base_Content", "Content Block");

                var overviewContentBlock = new ContentBlockBase();
                overviewContentBlock.Html = "<p>Sed in leo dui. Etiam quis justo lectus. Quisque sit amet rhoncus ligula. Mauris fermentum odio sed turpis lobortis ullamcorper. Donec sit amet neque vel turpis commodo interdum. Donec pellentesque nulla purus. Proin semper viverra sapien, ut vehicula tortor viverra ac.</p>";

                SampleUtilities.AddControlToPage(new Guid(DonatePageId), overviewContentBlock, "Base_Content", "Content Block");

                var contributionsContentBlock = new ContentBlockBase();
                contributionsContentBlock.Html = @"<h2>Putting your contributions to work</h2>
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer non turpis interdum sem pellentesque pulvinar quis sit amet ligula. Proin sed enim turpis. Aenean commodo mollis tempor. Vivamus et ipsum est. Morbi dapibus, magna id condimentum fermentum, nulla ipsum adipiscing felis, in hendrerit lacus eros vel mi. Nulla tempus lectus ut sapien tristique ultrices.
<ul>
    <li>In auctor sollicitudin lacus pellentesque laoreet. </li>
    <li>Pellentesque vel orci in est elementum viverra. </li>
    <li>Quisque non lacus orci. </li>
    <li>Nunc ac nulla a orci pellentesque adipiscing eget non ipsum. </li>
    <li>Suspendisse placerat lacus sed dolor ornare consequat. </li>
    <li>Fusce scelerisque tortor lacinia lorem consequat sit amet sollicitudin lorem tincidunt. </li>
    <li>Etiam eu massa et ligula porta ullamcorper. </li>
</ul>
In condimentum, quam eget bibendum ultrices, quam dolor mollis lorem, at consectetur turpis dolor ac nunc. Nulla facilisi. Suspendisse nec massa sit amet elit porta vulputate.
";

                SampleUtilities.AddControlToPage(new Guid(DonatePageId), contributionsContentBlock, "Base_Content", "Content Block");
}
        }

        private void CreateVolunteerPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(VolunteerPageId), "Volunteer");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(VolunteerPageId), new Guid(TwoColumnsTemplateId));

                var titleContentBlock = new ContentBlockBase();
                titleContentBlock.Html = "<h1>Volunteer</h1>";

                var contentContentBlock = new ContentBlockBase();
                contentContentBlock.Html = @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed ligula vitae est posuere congue sed pretium quam. Pellentesque at velit in massa ornare sagittis at in lacus. Fusce placerat leo eu sem sagittis posuere. Cras a arcu nec turpis pulvinar vehicula. Proin sed metus nec leo rutrum mattis vel vel est. Pellentesque lacinia nulla id lorem cursus fringilla.</p>
<p>Proin non commodo velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec elementum imperdiet volutpat. Integer sit amet felis in magna iaculis molestie vel in velit. Nulla facilisi. Sed iaculis imperdiet turpis, in vulputate lorem mattis non. In condimentum, neque vitae elementum sodales, metus mi congue urna, a imperdiet sem massa at felis. Aliquam erat volutpat. Nullam eget lorem enim, at venenatis felis. Maecenas et euismod diam.</p>
<p>Pellentesque tortor tellus, cursus dignissim hendrerit non, ornare a tellus. Donec ut consequat massa. Phasellus fermentum, justo vel congue tincidunt, metus diam sagittis eros, sed ornare tellus enim sit amet sem. Duis gravida tortor eget risus varius vel ullamcorper risus semper. </p>";

                SampleUtilities.AddControlToPage(new Guid(VolunteerPageId), contentContentBlock, "Main_TwoColumns_Content", "Content Block");

                var sidebarTitleContentBlock = new ContentBlockBase();
                sidebarTitleContentBlock.Html = "<h2>Upcoming Events</h2>";

                SampleUtilities.AddControlToPage(new Guid(VolunteerPageId), sidebarTitleContentBlock, "Main_TwoColumns_Sidebar", "Content Block");

                var templateKey = SampleUtilities.GetControlTemplateKey(typeof(MasterView), "Events Sidebar");
                SampleUtilities.RegisterEventsFrontendView("EventsFrontend", templateKey, typeof(MasterView), "EventsFrontendSidebarList", new Guid(EventsPageId));

                var eventsControl = new EventsView();

                eventsControl.MasterViewName = "EventsFrontendSidebarList";

                SampleUtilities.AddControlToPage(new Guid(VolunteerPageId), eventsControl, "Main_TwoColumns_Sidebar", "Events");

                var moreEventsLinkBlock = new ContentBlockBase();
                moreEventsLinkBlock.Html = string.Format(@"<a sfref=""[pages]{0}"" href=""/events"">View more events.</a>", EventsPageId);

                SampleUtilities.AddControlToPage(new Guid(VolunteerPageId), moreEventsLinkBlock, "Main_TwoColumns_Sidebar", "Content Block");
            }
        }

        private void CreateContactUsPage()
        {
            var result = SampleUtilities.CreatePage(new Guid(ContactUsPageId), "Contact Us");

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(ContactUsPageId), new Guid(TwoColumnsTemplateId));

                var titleContentBlock = new ContentBlockBase();
                titleContentBlock.Html = @"<h1>Contact Us</h1>
<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sed diam lectus, vel facilisis odio. Donec ac rutrum felis. Curabitur bibendum est vel elit iaculis a scelerisque massa rhoncus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>";

                SampleUtilities.AddControlToPage(new Guid(ContactUsPageId), titleContentBlock, "Main_TwoColumns_Content", "Content Block");

                var controls = new Dictionary<Control, string>();

                FormTextBox nameBox = new FormTextBox();
                nameBox.Title = "Your Name:";
                nameBox.ValidatorDefinition.Required = true;
                nameBox.ValidatorDefinition.RequiredViolationMessage = "Your name is required!";
                nameBox.TextBoxSize = FormControlSize.Medium;

                controls.Add(nameBox, "Body");

                FormTextBox emailBox = new FormTextBox();
                emailBox.Title = "Your E-mail:";
                emailBox.TextBoxSize = FormControlSize.Medium;

                controls.Add(emailBox, "Body");

                FormTextBox phoneBox = new FormTextBox();
                phoneBox.Title = "Your Phone:";
                phoneBox.TextBoxSize = FormControlSize.Medium;

                controls.Add(phoneBox, "Body");

                FormParagraphTextBox commentBox = new FormParagraphTextBox();
                commentBox.Title = "Your Comments:";
                commentBox.ValidatorDefinition.Required = true;
                commentBox.ValidatorDefinition.RequiredViolationMessage = "Your comment is required!";
                commentBox.ParagraphTextBoxSize = FormControlSize.Medium;

                controls.Add(commentBox, "Body");

                FormSubmitButton submitButton = new FormSubmitButton();
                submitButton.Text = "Submit";

                controls.Add(submitButton, "Body");

                SampleUtilities.CreateForm(new Guid(ContactFormId), "ContactUs", "Contact Us", "The form was submitted successfully!", controls);

                FormsControl form = new FormsControl();
                form.FormId = new Guid(ContactFormId);

                SampleUtilities.AddControlToPage(new Guid(ContactUsPageId), form, "Main_TwoColumns_Content", "Forms Control");

                var address1ContentBlock = new ContentBlockBase();
                address1ContentBlock.Html = @"<h2>Address</h2>
                                            <address>
                                            <p><strong></strong></p>
                                            <strong>Telerik</strong><br />
                                            10200 Grogans Mill Rd, Suite 130<br />
                                            The Woodlands, TX 77380
                                            <p>
                                            phone: +1-888-365-2779<br />
                                            fax: +1-617-249-2116
                                            </p>
                                            </address>";

                SampleUtilities.AddControlToPage(new Guid(ContactUsPageId), address1ContentBlock, "Main_TwoColumns_Sidebar", "Content Block");

                var address1BingMap = new ContentBlockBase();
                address1BingMap.Html = @"<div id=""mapviewer""><iframe id=""map"" Name=""mapFrame"" scrolling=""no"" width=""275"" height=""300"" frameborder=""0"" 
                                        src=""http://www.bing.com/maps/embed/?lvl=16&amp;cp=30.15605578126997~-95.47054726927884&amp;sty=r&amp;draggable=true&amp;v=2&amp;dir=0&amp;where1=10200+Grogans+Mill+Rd%2C+Spring%2C+TX+77380&amp;form=LMLTEW&amp;pp=30.15667~-95.471008&amp;mkt=en-us&amp;emid=12e63ea9-96a2-5d51-8729-d655e78665b7&amp;w=275&amp;h=300""></iframe></div>";
                SampleUtilities.AddControlToPage(new Guid(ContactUsPageId), address1BingMap, "Main_TwoColumns_Sidebar", "Bing Map Widget");
            }
        }

        private void CreateFeeds()
        {
            SampleUtilities.CreateContentFeed("Charity Blog", new Guid(BlogPageId), typeof(BlogPost), 25, RssContentOutputSetting.TitleAndContent, RssFormatOutputSettings.RssOnly);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        private void ConfigManager_Executed(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs args)
        {
            if (args.CommandName == "SaveSection")
            {
                var section = args.CommandArguments as VirtualPathSettingsConfig;
                if (section != null)
                {
                    // Reset the Virtual path manager, whenever the section of the VirtualPathSettingsConfig is saved.
                    // This is needed so that the prefixes for templates in our module assembly are taken into account.
                    VirtualPathManager.Reset();
                }
            }
        }
    }
}