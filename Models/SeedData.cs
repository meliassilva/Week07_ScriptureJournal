using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ScriptureJournalContext(
                serviceProvider.GetRequiredService<DbContextOptions<ScriptureJournalContext>>()))
            {
                // Look for any Scriptures.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Journal
                    {
                        Book = "Genesis",
                        Chapter = 1,
                        Verse = 1,
                        Note = "Heavenly Father created us as His image",
                        DateAdded = DateTime.Parse("2018-11-5")
                    },

                    new Journal
                    {
                        Book = "1 Nephi",
                        Chapter = 1,
                        Verse = 1,
                        Note = "Lehi was such a good leader",
                        DateAdded = DateTime.Parse("2018-11-5")
                    }

                
                );
                context.SaveChanges();
            }
        }
    }
}
