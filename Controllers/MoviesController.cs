using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        #region Outros tipos de metodos Index()
        // GET: Movies --------------------------------------------------------------
        //public async Task<IActionResult> Index(string searchString)
        //{
        // consulta LINQ para selecionar os filmes:
        //    var movies = from m in _context.Movie
        //                 select m;

        /*
            Se o parâmetro searchString contiver uma cadeia de caracteres, a 
            consulta de filmes será modificada para filtrar o valor da cadeia de caracteres de pesquisa: 
        */
        //   if (!String.IsNullOrEmpty(searchString))
        //   {
        //        movies = movies.Where(s => s.Title.Contains(searchString));
        //   }
        /*
         o código cria um objeto List quando ele chama o método View. 
         O código passa esta lista Movies do método de ação Index para a exibição:
        */
        //    return View(await movies.ToListAsync());
        // A partir de agora podemos utilizar algo como: https://localhost:5001/Movies?searchString=Ghost
        //}
        // --------------------------------------------------------------------------------------------------

        /*
            Agora você pode passar o título de pesquisa como dados de rota (um segmento de URL), em vez de 
            como um valor de cadeia de consulta. 
        */
        // A partir de agora podemos utilizar algo como: https://localhost:5001/Movies/Index/ghost
        //public async Task<IActionResult> Index(string id)
        //{
        //    var movies = from m in _context.Movie
        //                 select m;

        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        movies = movies.Where(s => s.Title.Contains(id));
        //    }

        //    return View(await movies.ToListAsync());
        //}
        #endregion

        // GET: Movies/Details/5 ------------------------------------------------------------

        /*  ---------------- Id ---------------------
            O parâmetro id geralmente é passado como dados de rota. Por exemplo,
            https://localhost:5001/movies/details/1 define:
            - O controller para o controller movies (o primeiro segmento de URL).
            - A ação para details (o segundo segmento de URL).
            - A ID como 1 (o último segmento de URL).

            Você também pode passar a id com uma cadeia de consulta da seguinte maneira:
            https://localhost:5001/movies/details?id=1

            O id parâmetro é definido como um tipo anulável ( int? ) no caso de um valor de ID não ser fornecido.
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*  ---------------- Expressão lambda ---------------------
                 Um expressão lambda é passada para FirstOrDefaultAsync para selecionar as 
                 entidades de filmes que correspondem ao valor da cadeia de consulta ou de 
                 dados da rota.
             */
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            /*  ---------------- View(movie) ---------------------
                Se for encontrado um filme, uma instância do modelo Movie será passada para 
                a exibição Details:
            */
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /*  ---------------- Create ---------------------
            O primeiro método de ação (HTTP GET) Create exibe o formulário Criar inicial. A segunda 
            versão ([HttpPost]) manipula a postagem de formulário. O segundo método Create (a versão [HttpPost]) 
            chama ModelState.IsValid para verificar se o filme tem erros de validação. A chamada a 
            esse método avalia os atributos de validação que foram aplicados ao objeto. Se o objeto 
            tiver erros de validação, o método Create exibirá o formulário novamente. Se não houver 
            erros, o método salvará o novo filme no banco de dados. Em nosso exemplo de filme, o 
            formulário não é postado no servidor quando há erros de validação detectados no lado do cliente; 
            o segundo método Create nunca é chamado quando há erros de validação do lado do cliente. 
            Se você desabilitar o JavaScript no navegador, a validação do cliente será desabilitada 
            e você poderá testar o método Create HTTP POST ModelState.IsValid detectando erros de validação. 
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            /*  ---------------- ModelState.IsValid ---------------------
                A ModelState.IsValid propriedade verifica se os dados enviados no formulário 
                podem ser usados para modificar (editar ou atualizar) um objeto Movie.
                Se os dados forem válidos, eles serão salvos.
            */

            /*  ---------------- SaveChangesAsync ---------------------
                Os dados de filmes atualizados (editados) são salvos no banco de dados 
                chamando o método SaveChangesAsync do contexto de banco de dados.  
            */
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);

            /*
                Depois de salvar os dados, o código redireciona o usuário para o 
                método de ação Index da classe MoviesController, que exibe a coleção 
                de filmes, incluindo as alterações feitas recentemente. 
            */
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
                O método HttpGet Edit usa o parâmetro ID de filme, pesquisa o filme usando 
                o método FindAsync do Entity Framework e retorna o filme selecionado para a 
                exibição de Edição. Se um filme não for encontrado, NotFound (HTTP 404) será retornado.
            */
            var movie = await _context.Movie.FindAsync(id);
            /*var movie = await _context.Movie
                            .Include(i => i.Title)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.Id == id);*/
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        /*  ---------------- [Bind] ---------------------
            O atributo [Bind] é uma maneira de proteger contra o excesso de postagem. Você somente 
            deve incluir as propriedades do atributo [Bind] que deseja alterar. 
        */

        /*  ---------------- HttpPost ---------------------
            O atributo HttpPost especifica que esse método Edit pode ser invocado somente para 
            solicitações POST. Você pode aplicar o atributo [HttpGet] ao primeiro método de edição, 
            mas isso não é necessário porque [HttpGet] é o padrão.
        */

        /*   ---------------- ValidateAntiForgeryToken ---------------------
             O atributo ValidateAntiForgeryToken é usado para prevenir a falsificação de uma solicitação
             e é associado a um token antifalsificação gerado no arquivo de exibição de edição (Views/Movies/Edit.cshtml). 
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            //string[] fieldsToBind = new string[] { "Id,Title,ReleaseDate,Genre,Price,Rating" };

            if (id == null)
            {
                return NotFound();
            }

            Movie movieToUpdate = await _context.Movie.FindAsync(id);

            if (movieToUpdate == null)
            {
                Movie deletedMovie = new Movie();
                await TryUpdateModelAsync(movieToUpdate);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The movie was deleted by another user.");
                return View(deletedMovie);
            }

            _context.Entry(movieToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Movie>(
                movieToUpdate,
                "",
                s => s.Title, s => s.ReleaseDate, s => s.Genre, s => s.Price, s => s.Rating))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Movie)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The movie was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Movie)databaseEntry.ToObject();

                        if (databaseValues.Title != clientValues.Title)
                        {
                            ModelState.AddModelError("Title", $"Current value: {databaseValues.Title}");
                        }
                        if (databaseValues.ReleaseDate != clientValues.ReleaseDate)
                        {
                            ModelState.AddModelError("Release Date", $"Current value: {databaseValues.ReleaseDate:c}");
                        }
                        if (databaseValues.Genre != clientValues.Genre)
                        {
                            ModelState.AddModelError("Genre", $"Current value: {databaseValues.Genre:d}");
                        }
                        if (databaseValues.Price != clientValues.Price)
                        {
                            ModelState.AddModelError("Genre", $"Current value: {databaseValues.Price:d}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        movieToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }

            return View(movieToUpdate);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
