using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolrNet.Attributes;

namespace AdMonitorSolr.Models
{
    public class Ads
    {
        [SolrUniqueKey("AdId")]
        public string AdId { get; set; }

        [SolrField("Title")]
        public string Title { get; set; }

        [SolrField("Description")]
        public string Description { get; set; }

        [SolrField("Brand")]
        public ICollection<string> Brands { get; set; }

        [SolrField("DateModified")]
        public DateTime DateModified { get; set; }

        [SolrField("pubDetails")]
        public ICollection<string> adPubsXML { get; set; }

        public ICollection<Publication> adPubs { get;set; }
    }

    public class Publication
    {
        public string publicid { get; set; }
        public string publicationId { get; set; }
        public string publicationName { get; set; }
        public string tags { get; set; }
        public string pages { get; set; }
        public string locator { get; set; }
        public string regionIds { get; set; }
        public string mediaType { get; set; }
        public string frequency { get; set; }
        public string circulation { get; set; }
        public string status { get; set; }
        public string dateModified { get; set; }

    }
}
