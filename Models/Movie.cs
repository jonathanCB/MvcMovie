using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        //As regras de validação e as cadeias de caracteres de erro são
        //especificadas somente na classe Movie.
        public int Id { get; set; }

        /*  ------------- Required e MinimumLength -------
            Os atributos Required e MinimumLength indicam que uma propriedade 
            deve ter um valor; porém, nada impede que um usuário insira um 
            espaço em branco para atender a essa validação. 
        */
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        /*  ---------------- Display ---------------------
            O atributo Display especifica o que deve ser exibido no nome de 
            um campo (neste caso, “Release Date” em vez de “ReleaseDate”). 
            O atributo DataType especifica o tipo de dados (Data) e, portanto, 
            as informações de hora armazenadas no campo não são exibidas. 
        */
        /*  ---------------- DataType ---------------------
            Os atributos DataType fornecem dicas apenas para que o mecanismo de exibição formate os dados.
            O atributo DataType é usado para especificar um tipo de dados mais específico do que o tipo 
            intrínseco de banco de dados; eles não são atributos de validação.
            A Enumeração DataType fornece muitos tipos de dados, como Date, Time, PhoneNumber, Currency, 
            EmailAddress e muito mais. 
        */
        /*  ---------------- DisplayFormat ---------------------
            O atributo DisplayFormat é usado para especificar explicitamente o formato de data:
        */
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        /*  ---------------- RegularExpression ---------------------
            O atributo RegularExpression é usado para limitar quais caracteres podem ser inseridos. 

            Deve usar apenas letras.
            A primeira letra deve ser maiúscula. Espaços em branco, números e caracteres especiais 
            não são permitidos.
            Exige que o primeiro caractere seja uma letra maiúscula.
            Permite caracteres especiais e números nos espaços subsequentes. "PG-13" é válido para 
            uma classificação, mas é um erro em "Gênero". 
        */
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        /*  ---------------- rowversion ---------------------
            O valor rowversion é um número sequencial que é incrementado sempre que a linha é atualizada. 
            Em um comando Update ou Delete, a cláusula Where inclui o valor original da coluna de 
            acompanhamento (a versão de linha original). Se a linha que está sendo atualizada tiver sido 
            alterada por outro usuário, o valor da coluna rowversion será diferente do valor original; portanto, 
            a instrução Update ou Delete não poderá encontrar a linha a ser atualizada devido à cláusula Where. 
            Quando o Entity Framework descobre que nenhuma linha foi atualizada pelo comando Update ou Delete 
            (ou seja, quando o número de linhas afetadas é zero), ele interpreta isso como um conflito de 
            simultaneidade. 
        */
        /*  ---------------- Timestamp ---------------------
            O atributo Timestamp especifica que essa coluna será incluída na cláusula Where de comandos 
            Update e Delete enviados ao banco de dados. 
        */
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /*  ---------------- [Column(TypeName = "decimal(18, 2)")] ---------------------
            A anotação de dados [Column(TypeName = "decimal(18, 2)")] é necessária 
            para que o Entity Framework Core possa mapear corretamente o Price para 
            a moeda no banco de dados. 
        */
        // O atributo Range restringe um valor a um intervalo especificado.
        //[Range(1, 100)]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        /* ---------------- StringLength ---------------------
           O atributo StringLength permite definir o tamanho máximo de uma propriedade
           de cadeia de caracteres e, opcionalmente, seu tamanho mínimo.
        */

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]        
        public string Rating { get; set; }

        /*  ---------------- Range e DateTime ---------------------
            A validação do jQuery não funciona com os atributos Range e DateTime. 
            Por exemplo, o seguinte código sempre exibirá um erro de validação do 
            lado do cliente, mesmo quando a data estiver no intervalo especificado:
            [Range(typeof(DateTime), "1/1/1966", "1/1/2020")] 
        */

        /*  ---------------- Atributos em uma linha ---------------------
            public int Id { get; set; }

                [StringLength(60, MinimumLength = 3)]
                public string Title { get; set; }

                [Display(Name = "Release Date"), DataType(DataType.Date)]
                public DateTime ReleaseDate { get; set; }

                [RegularExpression(@"^[A-Z]+[a-zA-Z]*$"), Required, StringLength(30)]
                public string Genre { get; set; }

                [Range(1, 100), DataType(DataType.Currency)]
                [Column(TypeName = "decimal(18, 2)")]
                public decimal Price { get; set; }

                [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]
                public string Rating { get; set; } 
        */
    }
}