using apiRestProva.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiRestProva.Db
{
    public class ProvaDbContext : DbContext
    {
        public DbSet<Articolo> Articoli { get; set; }
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Shopping");
        }

       

        public async Task FillDB()
        {
            Articoli.Add(new Articolo
            {
                codiceArticolo = "tckt1",
                descrizioneArticolo = "biglietto_autobus_regionale",
                validita = 6,
                prezzo = 1.5,
                idCatalogoArticolo = 12345
            });

            Articoli.Add(new Articolo
            {
                codiceArticolo = "tckt2",
                descrizioneArticolo = "biglietto_autobus_nazionale",
                validita = 3,
                prezzo = 5,
                idCatalogoArticolo = 16345
            });
            Articoli.Add(new Articolo
            {
                codiceArticolo = "tckt3",
                descrizioneArticolo = "biglietto_autobus_regionale",
                validita = 16,
                prezzo = 0.5,
                idCatalogoArticolo = 120345
            });
            Articoli.Add(new Articolo
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
