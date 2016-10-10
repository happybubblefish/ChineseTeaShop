using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using ChineseTea3.Controllers;
using PagedList;

namespace ChineseTea3.Models
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            //must be declared
            this.Pv = new PurchaseViewModel();
            this.Rv = new RelatedContentViewModel();
            //this.RatingList = new List<int>{1, 2, 3, 4, 5};
        }

        public PurchaseViewModel Pv { get; set; }

        public RelatedContentViewModel Rv { get; set; }

        public List<ProductComment> CommentsList { get; set; }

        public ProductComment CurrentComment { get; set; }

        public IPagedList<ProductComment> CommentsPagedList { get; set; }

        public int ProductId { get; set; }

        //public bool IsOneStar { get; set; }
        //public bool IsTwoStar { get; set; }
        //public bool IsThreeStar { get; set; }
        //public bool IsFourStar { get; set; }
        //public bool IsFiveStar { get; set; }

        public int SelectedRating { get; set; }

        public List<int> RatingList { get; set; } 
        public List<int> RatingCollection { get; set; } 
    }
}