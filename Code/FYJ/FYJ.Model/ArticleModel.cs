using System;
using System.Collections.Generic;

namespace FYJ.Model
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
    }
}
