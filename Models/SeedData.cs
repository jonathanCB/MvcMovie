using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                /*  ------------------- Movie.Any() ------------------- 
                    Look for any movies.

                    Se houver um filme no BD, o inicializador de semeadura será retornado 
                    e nenhum filme será adicionado.
                */
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        /*  ------------------- Rating ------------------- 
                            O atributo "Rating" foi adicionado depois, por isso devemos atualizar o banco:
                        
                            Add-Migration Rating
                            Update-Database

                            O comando Add-Migration informa a estrutura de migração para examinar o atual 
                            modelo Movie com o atual esquema de BD Movie e criar o código necessário para 
                            migrar o BD para o novo modelo.
                            
                            O nome “Rating” é arbitrário e é usado para nomear o arquivo de migração. 
                            É útil usar um nome significativo para o arquivo de migração.
                            
                            Se você excluir todos os registros do BD, o método de inicialização propagará 
                            o BD e incluirá o campo Rating. 
                        */
                        Rating = "R",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Rating = "R",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Rating = "R",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}