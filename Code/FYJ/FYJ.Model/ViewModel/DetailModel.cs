using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Model.ViewModel
{
    public class DetailModel
    {
        public string Comments { get; set; }
        public string Contents { get; set; }
        public DateTime CrDate { get; set; }
        public int ArticleId { get; set; }
        public DateTime CrCommentDate { get; set; }
        public string UserName { get; set; }
        public string Photo { get; set; }
        public int CommentId { get; set; }
        public string Desc { get; set; }
    }
}
