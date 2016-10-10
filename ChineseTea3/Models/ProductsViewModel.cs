using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace ChineseTea3.Models
{
    public class ProductsViewModel
    {
        public ProductsViewModel()
        {
            this.PriceCondition = new List<string> {"Sort...","Lowest to highest", "Highest to lowest"};
            //this.KindCondition = new List<string> {"Sort by kind", "Green", "Oolong", "Red"};
            //this.RangeCondition = new List<string>{"Sort by price range", "$0 ~ 50", "$50 ~ 100", "$100 ~"};
        }

        public List<TeaProduct> TeaList { get; set; }

        public IPagedList<TeaProduct> TeaPagedList { get; set; }

        public List<string> KindList { get; set; }

        private string kind;

        public string Kind
        {
            get { return kind ?? "All kinds"; }
            set { kind = value; }
        }

        public List<string> PriceCondition { get; set; }

        public List<string> KindCondition { get; set; }

        public List<string> RangeCondition { get; set; } 

        public string SortByPrice { get; set; }

        public string SortByPriceRange { get; set; }

        public string SortByKind { get; set; }

        public string Origin { get; set; }


        public bool IsBlack { get; set; }
        public bool IsGreen { get; set; }
        public bool IsRed { get; set; }
        public bool IsCompressed { get; set; }
        public bool IsWhite { get; set; }
        public bool IsYellow { get; set; }
        public bool IsScented { get; set; }
        public bool IsOolong { get; set; }
        public bool IsLessThan50 { get; set; }
        public bool IsBetween50And100 { get; set; }
        public bool IsBetween100And200 { get; set; }
        public bool IsOver200 { get; set; }
    }
}