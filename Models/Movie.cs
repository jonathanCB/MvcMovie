using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        //As regras de valida��o e as cadeias de caracteres de erro s�o
        //especificadas somente na classe Movie.
        public int Id { get; set; }

        /*  ------------- Required e MinimumLength -------
            Os atributos Required e MinimumLength indicam que uma propriedade 
            deve ter um valor; por�m, nada impede que um usu�rio insira um 
            espa�o em branco para atender a essa valida��o. 
        */
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        /*  ---------------- Display ---------------------
            O atributo Display especifica o que deve ser exibido no nome de 
            um campo (neste caso, �Release Date� em vez de �ReleaseDate�). 
            O atributo DataType especifica o tipo de dados (Data) e, portanto, 
            as informa��es de hora armazenadas no campo n�o s�o exibidas. 
        */
        /*  ---------------- DataType ---------------------
            Os atributos DataType fornecem dicas apenas para que o mecanismo de exibi��o formate os dados.
            O atributo DataType � usado para especificar um tipo de dados mais espec�fico do que o tipo 
            intr�nseco de banco de dados; eles n�o s�o atributos de valida��o.
            A Enumera��o DataType fornece muitos tipos de dados, como Date, Time, PhoneNumber, Currency, 
            EmailAddress e muito mais. 
        */
        /*  ---------------- DisplayFormat ---------------------
            O atributo DisplayFormat � usado para especificar explicitamente o formato de data:
        */
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        /*  ---------------- RegularExpression ---------------------
            O atributo RegularExpression � usado para limitar quais caracteres podem ser inseridos. 

            Deve usar apenas letras.
            A primeira letra deve ser mai�scula. Espa�os em branco, n�meros e caracteres especiais 
            n�o s�o permitidos.
            Exige que o primeiro caractere seja uma letra mai�scula.
            Permite caracteres especiais e n�meros nos espa�os subsequentes. "PG-13" � v�lido para 
            uma classifica��o, mas � um erro em "G�nero". 
        */
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        /*  ---------------- rowversion ---------------------
            O valor rowversion � um n�mero sequencial que � incrementado sempre que a linha � atualizada. 
            Em um comando Update ou Delete, a cl�usula Where inclui o valor original da coluna de 
            acompanhamento (a vers�o de linha original). Se a linha que est� sendo atualizada tiver sido 
            alterada por outro usu�rio, o valor da coluna rowversion ser� diferente do valor original; portanto, 
            a instru��o Update ou Delete n�o poder� encontrar a linha a ser atualizada devido � cl�usula Where. 
            Quando o Entity Framework descobre que nenhuma linha foi atualizada pelo comando Update ou Delete 
            (ou seja, quando o n�mero de linhas afetadas � zero), ele interpreta isso como um conflito de 
            simultaneidade. 
        */
        /*  ---------------- Timestamp ---------------------
            O atributo Timestamp especifica que essa coluna ser� inclu�da na cl�usula Where de comandos 
            Update e Delete enviados ao banco de dados. 
        */
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /*  ---------------- [Column(TypeName = "decimal(18, 2)")] ---------------------
            A anota��o de dados [Column(TypeName = "decimal(18, 2)")] � necess�ria 
            para que o Entity Framework Core possa mapear corretamente o Price para 
            a moeda no banco de dados. 
        */
        // O atributo Range restringe um valor a um intervalo especificado.
        //[Range(1, 100)]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        /* ---------------- StringLength ---------------------
           O atributo StringLength permite definir o tamanho m�ximo de uma propriedade
           de cadeia de caracteres e, opcionalmente, seu tamanho m�nimo.
        */

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]        
        public string Rating { get; set; }

        /*  ---------------- Range e DateTime ---------------------
            A valida��o do jQuery n�o funciona com os atributos Range e DateTime. 
            Por exemplo, o seguinte c�digo sempre exibir� um erro de valida��o do 
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