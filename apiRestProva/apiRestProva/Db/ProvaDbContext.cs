using apiRestProva.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiRestProva.Db
{
    public class ProvaDbContext : DbContext
    {
        public DbSet<Article> Articoli { get; set; }
        public DbSet<AuthToken> Tokens { get; set; }
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Shopping");
        }

       
        public async Task AddToken (AuthToken _token)
        {
            Tokens.Add(_token);
            await this.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<AuthToken> GetToken(AuthToken _token)
        {
            return await Tokens.FirstOrDefaultAsync(t => t.AccessToken == _token.AccessToken).ConfigureAwait(false);  
        }

        public async Task<bool> BurnToken(AuthToken _token)
        {
            var token = GetToken(_token);
            token.Result.validity = false;
            await this.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
        public async Task FillDB()
        {
            Articoli.Add(new Article
            {
                codiceArticolo = "tckt1",
                descrizioneArticolo = "biglietto_autobus_regionale",
                validita = 6,
                prezzo = 1.5,
                idCatalogoArticolo = 12345
            });

            Articoli.Add(new Article
            {
                codiceArticolo = "tckt2",
                descrizioneArticolo = "biglietto_autobus_nazionale",
                validita = 3,
                prezzo = 5,
                idCatalogoArticolo = 16345
            });
            Articoli.Add(new Article
            {
                codiceArticolo = "tckt3",
                descrizioneArticolo = "biglietto_autobus_regionale",
                validita = 16,
                prezzo = 0.5,
                idCatalogoArticolo = 120345
            });
            Articoli.Add(new Article
            {
                codiceArticolo = "tckt4",
                descrizioneArticolo = "biglietto_treno_nazionale",
                validita = 3,
                prezzo = 20,
                idCatalogoArticolo = 1299
            });

            await this.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
