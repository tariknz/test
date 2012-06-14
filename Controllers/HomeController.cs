using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using AdMonitorSolr.Models;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;


namespace AdMonitorSolr.Controllers
{

    [HandleError]
    public class HomeController : Controller
    {
      
        //[HttpGet]
        public ActionResult Index(string q)
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            Ads[] ads = null;

            if (q != null)
            {
                Debug.WriteLine("Get!!!");
                Debug.WriteLine(q);
                ads = initSearch(q);
            }
            else
            {
                ads = initSearch();
            }

            

            //this.ads = ads;

            parsePublications(ads);
            ViewData["DataSet"] = ads;

            return View();
        }

        private static void parsePublications(Ads[] ads)
        {
            for (int i = 0; i < ads.Count(); i++)
            {
                if (ads[i].adPubsXML.Count > 0 && ads[i].adPubsXML != null)
                {
                    ads[i].adPubs = new Collection<Publication>();

                    foreach (string xml in ads[i].adPubsXML)
                    {

                        Publication pub = new Publication();
                        string fixedxml = xml.Replace("&", "&amp;");
                        Debug.Write(fixedxml);

                        pub.publicationName = "hello";

                        using (XmlReader reader = XmlReader.Create(new StringReader(fixedxml)))
                        {
                            reader.ReadToFollowing("adpub");

                            reader.MoveToAttribute("publicid");
                            pub.publicid = reader.Value;

                            reader.MoveToAttribute("publicationId");
                            pub.publicationId = reader.Value;

                            reader.MoveToAttribute("publicationName");
                            pub.publicationName = reader.Value;

                            reader.MoveToAttribute("tags");
                            pub.tags = reader.Value;

                            reader.MoveToAttribute("pages");
                            pub.pages = reader.Value;

                            reader.MoveToAttribute("locator");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("regionIds");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("mediaType");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("frequency");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("circulation");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("status");
                            pub.locator = reader.Value;

                            reader.MoveToAttribute("dateModified");
                            pub.locator = reader.Value;

                        }

                        Debug.WriteLine(pub.publicationId);
                        Debug.WriteLine(pub.publicationName);

                        ads[i].adPubs.Add(pub);



                    }
                }
            }
        }

        private Ads[] initSearch(string q)
        {
            if (q.Equals(""))
                q = "*:*";

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Ads>>();
            var results = solr.Query(new SolrQuery(q));

            if(results != null && results.Count > 0)
                Debug.WriteLine(results[0].Title);

            return results.ToArray();

        }

        private Ads[] initSearch()
        {
            return initSearch("*:*");
        }



        public ActionResult About()
        {
            return View();
        }
    }
}
