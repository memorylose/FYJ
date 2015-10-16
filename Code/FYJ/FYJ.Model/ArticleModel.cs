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
    }
}
