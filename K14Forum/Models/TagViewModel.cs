using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K14Forum.Models
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual IList<ApplicationTag> Tags { get; set; }
        public string Controller { get; set; }
    }
}