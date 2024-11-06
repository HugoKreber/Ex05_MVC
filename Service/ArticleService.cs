using BO;

namespace Ex05_MVC.Service
{
    public class ArticleService
    {
        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Add(Article article)
        {
            dbContext.ArticleReferences.Add(article);
        }

        public List<Article> GetArticles()
        {
            return dbContext.ArticleReferences;
        }

        public void Remove(Article article)
        {
            dbContext.ArticleReferences.Remove(article);
        }

        internal void Update(Article articleUpdated)
        {
            var orderBdd = dbContext.ArticleReferences.SingleOrDefault(a => a.Id == articleUpdated.Id);
            orderBdd = articleUpdated;
        }
    }
}
