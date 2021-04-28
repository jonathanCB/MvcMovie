using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        //Lista de filmes
        public List<Movie> Movies { get; set; }

        /* ----------------------- SelectList -----------------------
            SelectList que contém a lista de gêneros. Isso permite que o usuário selecione um gênero na lista.

            A SelectList de gêneros é criada com a projeção dos gêneros distintos(não desejamos que nossa lista 
            de seleção tenha gêneros duplicados).
        */
        public SelectList Genres { get; set; }

        //Gênero selecionado
        public string MovieGenre { get; set; }

        //Texto que os usuários inserem na caixa de texto de pesquisa
        public string SearchString { get; set; }
    }
}