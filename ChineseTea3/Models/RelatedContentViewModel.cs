using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace ChineseTea3.Models
{
    public class RelatedContentViewModel
    {
        public IPagedList<TeaProduct> RelatedContentPagedList { get; set; }

        public IEnumerable<TeaProduct> RelatedContentList { get; set; } 

        public TeaProduct Tea { get; set; }

        public int Id { get; set; }


    }
}