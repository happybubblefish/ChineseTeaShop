//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChineseTea3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductComment
    {
        public ProductComment()
        {
            this.Ratings = new HashSet<Rating>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string CommentContent { get; set; }
        public System.DateTime AddTime { get; set; }
        public string CommentTitle { get; set; }
        public string UserName { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual TeaProduct TeaProduct { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}