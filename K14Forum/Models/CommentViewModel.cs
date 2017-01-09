using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K14Forum.Models
{
    public class CreateCommentViewModel
    {
        [Display(Name = "Bình luận (*)"), Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Comment { get; set; }

        [Display(Name = "Bài viết (*)"), Required]
        public Guid ArticleId { get; set; }
    }

    public class EditCommentViewModel
    {
        [Display(Name = "Mã (*)"),Required]
        public Guid Id { get; set; }

        [Display(Name = "Bình luận (*)"), Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Comment { get; set; }

        [Display(Name = "Bài viết (*)"), Required]
        public Guid ArticleId { get; set; }
    }
}