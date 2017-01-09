using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K14Forum.Models
{
    public class CreateArticleViewModel
    {
        [Display(Name = "Tiêu đề (*)"), Required(ErrorMessage ="Bạn chưa nhập tiêu đề bài viết.")]
        public string Title { get; set; }

        [Display(Name = "Nội dung (*)"), Required(ErrorMessage ="Bạn chưa nhập nội dung bài viết.")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ArticleContent { get; set; }

        [Display(Name = "Gắn thẻ")]
        public string Tags { get; set; }
    }
    public class EditArticleViewModel
    {
        [Display(Name = "Mã (*)"), Required]
        public Guid Id { get; set; }

        [Display(Name = "Tiêu đề (*)"), Required(ErrorMessage = "Bạn chưa nhập tiêu đề bài viết.")]
        public string Title { get; set; }

        [Display(Name = "Nội dung (*)"), Required(ErrorMessage = "Bạn chưa nhập nội dung bài viết.")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string ArticleContent { get; set; }

        [Display(Name = "Gắn thẻ")]
        public string Tags { get; set; }
    }
}