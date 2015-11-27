using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Model.ViewModel
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        public string Contents { get; set; }
        public string Image { get; set; }
        public bool? IsPicDel { get; set; }
        public bool? IsArticleDel { get; set; }
        public bool? IsIndex { get; set; }
        public List<ArticlePicture> ImageList { get; set; }
        public DateTime CrDate { get; set; }
        public string UserPhoto { get; set; }
    }

    public class ArticleUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string NickName { get; set; }
    }
}
