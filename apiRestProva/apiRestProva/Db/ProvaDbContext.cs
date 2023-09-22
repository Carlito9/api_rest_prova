using apiRestProva.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiRestProva.Db
{
    public class ProvaDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<AuthToken> Tokens { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ArticleCart> ArticleCarts { get; set; }
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Shopping");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.articles)
                .WithOne()
                .HasForeignKey(a => a.CartId);
            modelBuilder.Entity<ArticleCart>()
        .HasKey(ac => new { ac.CartId, ac.ArticleCode });
        }
        
        public async Task AddToken(AuthToken _token)
        {
            Tokens.Add(_token);
            await this.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddCart(Cart _cart)
        {
            Carts.Add(_cart);
            await this.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task AddArticleCart(ArticleCart acart)
        {
            ArticleCarts.Add(acart);
            await this.SaveChangesAsync().ConfigureAwait(false);
        }


        
        public async Task<AuthToken> Get(AuthToken _token)
        {
            return await Tokens.FirstOrDefaultAsync(t => t.AccessToken == _token.AccessToken).ConfigureAwait(false);  
        }

        public async Task<bool> BurnToken(AuthToken _token)
        {
            var token = Get(_token);
            token.Result.validity = false;
            await this.SaveChangesAsync().ConfigureAwait(false);
            return true;
            
        }
        public async Task FillDB()
        {
            Articles.Add(new Article
            { 
                articleCode = "tckt1",
                articleDescription = "biglietto_autobus_regionale",
                carrierCode = "atm666",
                carrierName = "atma",
                price = (decimal) 1.2,
                maxQuantity = 100
    });

            Articles.Add(new Article
            {
                articleCode = "tckt2",
                articleDescription = "biglietto_autobus_nazionale",
                carrierCode = "atm6sc6",
                carrierName = "atmar",
                price = (decimal)4.2,
                maxQuantity = 100
            });
            Articles.Add(new Article
            {
                articleCode = "tckt3",
                articleDescription = "biglietto_autobus_mondiale",
                carrierCode = "wrld232",
                carrierName = "world",
                price = (decimal)1002,
                maxQuantity = 100
            });
            Articles.Add(new Article
            {
                articleCode = "tckt4",
                articleDescription = "biglietto_autobus_universale",
                carrierCode = "auniv6456",
                carrierName = "auniv",
                price = (decimal)6002,
                maxQuantity = 1000
            });

            await this.SaveChangesAsync().ConfigureAwait(false);
        }

       
    }
}
