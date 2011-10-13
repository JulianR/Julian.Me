using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Julian.Me.Web.Security;
using Julian.Me.Core.Security;
using Julian.Me.Core.Data;
using Julian.Me.Core.Models;
using System.Data.Entity;

namespace Julian.Me.Web
{
  public class MvcApplication : System.Web.HttpApplication
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
         "PortfolioProject",
         "portfolio/{project}",
         new { controller = "portfolio", action = "index", project = UrlParameter.Optional }
      );

      routes.MapRoute(
         "BlogMonthPostID",
         "blog/{date}/{postID}",
         new { controller = "blog", action = "index" }
      );

      routes.MapRoute(
        "Blog",
        "blog/{date}",
        new { controller = "blog", action = "index", date = UrlParameter.Optional }
     );

      routes.MapRoute(
        "Default", // Route name
        "{controller}/{action}/{id}", // URL with parameters
        new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
      );
    }

    public override void Init()
    {
      //this.PostAcquireRequestState += MvcApplication_PostAcquireRequestState;
      base.Init();
    }


    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();

      RegisterGlobalFilters(GlobalFilters.Filters);
      RegisterRoutes(RouteTable.Routes);

      Database.SetInitializer(new DbInitializer());

    }

    void MvcApplication_PostAcquireRequestState(object sender, EventArgs e)
    {
      if (Request == null)
      {
        return;
      }

      if (Request.IsAuthenticated)
      {
        var username = HttpContext.Current.User.Identity.Name;

        var sessionPrincipal = Session["Principal"] as ClientPrincipal;

        if (sessionPrincipal == null)
        {
          var identity = new ClientIdentity(username, null, UserAuthenticationMode.Username);

          sessionPrincipal = new ClientPrincipal(identity);

          Session["Principal"] = sessionPrincipal;
        }

        HttpContext.Current.User = sessionPrincipal;
      }
      else
      {
        Session["Principal"] = null;
      }
    }
  }



  public class DbInitializer : System.Data.Entity.CreateDatabaseIfNotExists<DbDataContext>
  {

    private static string[] _lipsum = new[]
    {
      "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam eu augue nisl. Nulla facilisi. Nulla est velit, auctor non ullamcorper vel, semper eu felis. Curabitur id odio lectus. Praesent luctus lectus ut eros vestibulum sodales. Nullam dictum gravida nisi id congue. Mauris hendrerit consectetur orci, a rhoncus purus imperdiet sed. Sed lectus nibh, mattis nec pellentesque nec, consequat nec nisi. Duis at nulla massa. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aenean a sapien justo.</p>",

      "<p>Phasellus convallis lacus ac tellus porta laoreet. Praesent sagittis, est sed feugiat venenatis, dolor arcu blandit odio, vel interdum mi tellus ut lorem. Aenean euismod varius dolor, ut posuere augue hendrerit sit amet. Ut eros erat, faucibus eget rhoncus sed, sodales sed nulla. Integer nec massa sem, at lobortis tellus. Maecenas et diam eros. Nunc arcu leo, mattis vel rhoncus at, aliquam sit amet augue. Duis tincidunt dictum est nec laoreet. Quisque metus erat, adipiscing non blandit ut, dictum sed dolor. Suspendisse pellentesque velit eget orci porta convallis cursus nibh faucibus. Nam et tincidunt diam. Nam tincidunt dolor sit amet lacus cursus imperdiet aliquet quis diam. Ut ultricies, ligula et porta tincidunt, arcu nisl tempor libero, eget pellentesque orci sem quis diam.</p>",

      "<p>Duis nulla enim, semper at blandit non, sagittis at nisl. Quisque nec leo posuere neque consequat vulputate quis et lectus. Donec elementum erat ut augue vulputate et porttitor risus laoreet. Suspendisse ornare ligula at urna fermentum eget lobortis felis tristique. Vestibulum pulvinar posuere viverra. Phasellus vestibulum dignissim vulputate. Aliquam nec placerat neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc velit eros, semper vel porttitor ac, faucibus nec est. Aenean et eros neque.</p>",

      "<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Pellentesque sit amet blandit risus. Pellentesque pulvinar, quam sit amet mattis ornare, urna eros dignissim massa, ac posuere turpis sapien a ante. Maecenas ligula ante, scelerisque eget gravida sit amet, scelerisque at massa. Proin vel elit id diam dictum tempus. Aliquam mattis fermentum pharetra. Etiam eu egestas metus. Nullam vel mollis nisi. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce at dignissim turpis. Phasellus varius porta facilisis. Curabitur vel mi sit amet lectus imperdiet eleifend. Integer ut neque ut nisl sodales rhoncus. Nam a eros sit amet erat convallis fringilla. Nullam tempor ipsum vitae mauris rutrum convallis. Nullam eget pharetra urna. Sed nec velit magna, id elementum enim.</p>",

      "<p>Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Integer accumsan blandit purus in hendrerit. Aliquam dictum ullamcorper hendrerit. Nunc elementum interdum dapibus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec iaculis pellentesque bibendum. Curabitur iaculis est et felis ultrices pellentesque. Duis in lorem sed lorem tincidunt dignissim. Pellentesque mi dolor, vehicula sit amet pharetra et, vestibulum at elit. Vivamus molestie elit id metus suscipit eu laoreet lectus cursus. Phasellus mattis faucibus orci, nec porttitor lorem auctor sed.</p>",

      "<p>Vivamus tempus elementum dictum. Proin a consectetur quam. Nunc nec gravida nibh. In tristique consectetur velit, nec commodo dolor eleifend a. Sed hendrerit vehicula tempus. Donec bibendum urna id urna euismod placerat. In gravida gravida massa, in semper urna dapibus vitae. Phasellus dictum luctus elementum. Suspendisse potenti. Vestibulum odio nibh, venenatis sit amet accumsan sed, sollicitudin id nisi. Sed sit amet placerat erat. Nullam tincidunt tempor erat sit amet posuere.</p>",

      "<p>Donec consectetur facilisis metus, eget sollicitudin risus tempor id. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum lobortis sagittis sapien, nec placerat nulla aliquam ut. Pellentesque non ligula metus. Nam risus dolor, consequat vitae dapibus at, fringilla a nibh. Nam at metus eu tortor tempus blandit. Suspendisse fringilla, sapien nec pulvinar interdum, sapien risus facilisis orci, vitae sodales justo sem quis lacus. Sed nibh lacus, placerat ut commodo sed, consequat non diam. Vestibulum vitae augue in tellus rutrum pretium. Etiam tempor tortor ut urna dignissim accumsan. Integer quis purus justo, sit amet ornare metus. Nulla a orci risus.</p>",

      "<p>Maecenas id nunc sed urna pulvinar dapibus eget vel orci. Maecenas tempor nunc in nulla placerat ut fringilla velit mattis. Sed rutrum convallis est, eu iaculis sem viverra in. Nullam a dictum dui. Maecenas luctus ultrices felis, et malesuada leo pharetra vitae. Morbi at scelerisque tellus. Curabitur eu viverra enim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer porttitor dignissim ultricies. Nunc sed diam et massa viverra facilisis. Ut consequat tellus sed risus faucibus ullamcorper. Proin lobortis massa ut leo porta aliquet.</p>",

      "<p>Fusce sollicitudin nibh et ante fermentum euismod. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum congue mi sed mauris hendrerit consectetur. Nullam eget cursus nisl. Etiam est nibh, pretium vitae ultrices sit amet, sodales vel diam. Pellentesque viverra tincidunt diam, sed commodo elit posuere nec. Aenean imperdiet neque sit amet lacus iaculis lacinia. Donec a tellus est. Nam quis lectus est. Proin at dui lorem. Nulla facilisi. In sed leo nec elit bibendum congue. Mauris nisi lacus, condimentum at tincidunt quis, commodo et est. In tristique scelerisque ipsum non laoreet. Curabitur porta interdum molestie. Aenean quis malesuada tortor. Praesent sollicitudin odio eu nibh dictum non ullamcorper purus faucibus. Fusce in ornare quam.</p>",

      "<p>Quisque sit amet dolor vel est volutpat pharetra. Integer consectetur augue in lacus scelerisque sit amet rhoncus nisi venenatis. Aenean velit ipsum, tristique at fermentum vitae, feugiat ut magna. Integer libero libero, sodales sed sodales quis, scelerisque non augue. Aliquam non elit justo, sagittis venenatis tortor. Donec pretium tristique dignissim. Aenean mattis, mauris in sodales lobortis, ipsum lectus consectetur lorem, ut pellentesque dui neque eget diam. Morbi tincidunt, lectus vulputate sagittis fermentum, metus tellus scelerisque dui, et fermentum massa enim eu turpis. Morbi eu odio at tortor mollis ultrices. Ut sollicitudin turpis vel nunc ornare quis malesuada tellus molestie. Sed venenatis, quam eu dignissim convallis, magna velit sagittis tortor, in posuere lorem felis quis ligula. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.</p>",

      "<p>Suspendisse in justo sed elit facilisis dictum. Nam elementum, dui vitae porttitor ornare, arcu nisi consequat eros, non faucibus mi dui ac felis. Maecenas vestibulum condimentum auctor. Donec id libero nec arcu dignissim tempus sit amet vel urna. Nam et mauris vitae leo vulputate imperdiet. Sed non erat in magna dictum volutpat id quis metus. Donec a nunc erat, et accumsan massa. Etiam tincidunt sodales volutpat. Vestibulum scelerisque enim quis nisl eleifend id eleifend nisl ornare. Vestibulum rhoncus lacus ut ante placerat sit amet consequat elit volutpat. Aenean justo lectus, pulvinar ut faucibus quis, facilisis non metus. Vestibulum adipiscing, diam ac euismod tincidunt, est risus tincidunt quam, a convallis odio purus a justo. Fusce cursus est at enim egestas aliquam. Donec nulla nulla, vulputate eget aliquam tristique, adipiscing sit amet libero.</p>",

      "<p>Aliquam lectus mauris, ultricies sit amet vulputate vitae, imperdiet a quam. In id lacus dui. Fusce ac ante dolor, ut mattis sapien. Suspendisse potenti. Aenean vestibulum malesuada mi non iaculis. Suspendisse porttitor ante et elit pulvinar ultrices. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam in felis vel nulla venenatis tempus nec ut mauris. Nulla quis libero ut libero consequat dignissim. Nullam sit amet mi tortor, eget hendrerit urna. Aliquam ipsum ante, iaculis at pharetra ut, vestibulum vitae magna. Nulla vitae felis quis nulla ultricies cursus. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>",

      "<p>Praesent sed dignissim eros. Aenean tincidunt rhoncus dolor sit amet rutrum. Aenean vulputate rhoncus volutpat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc sit amet sapien dolor, id adipiscing tortor. Pellentesque tincidunt magna at elit sodales tristique. Etiam sit amet massa sapien, et pulvinar velit. Etiam facilisis varius ligula, eu pellentesque neque imperdiet porttitor. Nunc dapibus justo et erat sollicitudin placerat. Morbi orci odio, bibendum sed posuere eget, viverra quis velit. Donec congue vulputate enim vitae ornare. Nam arcu nibh, bibendum vel tincidunt nec, adipiscing vel magna. Fusce odio enim, ultrices interdum egestas id, condimentum non magna. Maecenas tincidunt, mi sed tincidunt iaculis, dui ante vestibulum eros, vel volutpat erat nibh porttitor nisi. Vivamus purus metus, volutpat ac tempus cursus, mollis id massa. Proin malesuada, dolor mollis condimentum gravida, metus quam euismod erat, nec dictum tortor sem aliquam arcu. Vivamus porttitor vulputate turpis vitae cursus. Donec ornare, nulla a condimentum aliquet, sem nulla tincidunt massa, ut posuere eros purus vitae tortor.</p>",

      "<p>Nunc et scelerisque ante. Phasellus sed risus odio. In nec orci est, id dictum nulla. Nunc sed elit nisl, id tincidunt justo. Vestibulum lobortis facilisis risus, non aliquet erat dapibus sed. Quisque sollicitudin pharetra tempor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Quisque justo urna, auctor a tempus eleifend, malesuada at magna. Pellentesque malesuada orci consequat dolor dapibus at consequat lacus lobortis. Aenean eleifend, urna nec egestas facilisis, turpis nunc suscipit lacus, sed bibendum diam lorem vel risus. Aliquam et purus elit. Phasellus id sapien vitae lectus tempus varius et eget arcu. Quisque venenatis metus id neque viverra vehicula. Sed mollis dolor in nisl sagittis dignissim. Aenean malesuada tempus neque, at luctus arcu aliquam eu. Pellentesque pretium euismod ipsum in imperdiet. Sed vel felis in ante lacinia dignissim.</p>",

      "<p>Fusce in arcu in neque congue aliquam. Curabitur et orci quis arcu vestibulum varius rutrum a nisl. Curabitur consequat libero a quam sollicitudin vitae auctor quam ornare. Integer risus enim, fermentum vitae lacinia nec, mollis eu justo. Praesent ac arcu eu tellus fermentum gravida. Proin eleifend, urna sit amet ultricies placerat, leo orci pellentesque mi, vitae posuere eros quam ut nisl. Suspendisse vel nisl eu purus fermentum vulputate ac ut sem. Suspendisse potenti. Fusce scelerisque eros mi, sed blandit lectus. Nulla facilisi. Maecenas a enim augue, a pretium velit. Mauris erat nunc, bibendum a commodo vitae, ornare et est. Aliquam eget feugiat lectus. </p>",
    };

    protected override void Seed(DbDataContext context)
    {
      var securityProvider = new DefaultSecurityProvider();

      var salt = securityProvider.GetSalt();

      var user = new User
      {
        EmailAddress = "julian.rooze@gmail.com",
        PasswordHash = securityProvider.Hash("test" + salt),
        PasswordSalt = salt,
        IsAdmin = true,
        FirstName = "Julian",
        LastName = "Rooze"
      };

      context.Users.Add(user);

      var random = new Random();

      var tag = new Tag
      {
        Name = "lipsum"
      };

      for (var i = 0; i < 20; i++)
      {
        var creationTime = new DateTime(random.Next(2010, 2012), random.Next(1, 13), random.Next(1, 29));
        var post = new BlogPost
        {
          CreatedBy = user,
          CreationTime = creationTime,
          Title = "Title " + i,
          Content = string.Join("", from x in Enumerable.Range(1, random.Next(1, 5))
                                    select _lipsum[random.Next(0, _lipsum.Length)]),
          Tags = new List<Tag>
          {
            tag
          },
          Year = creationTime.Year,
          Month = creationTime.Month

        };
        context.BlogPosts.Add(post);
      }

      context.SaveChanges();

      base.Seed(context);
    }
  }
}