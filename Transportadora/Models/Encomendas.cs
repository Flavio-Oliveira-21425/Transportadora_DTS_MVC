using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Transportadora.Models
{
    public class Encomendas
    {

        public Encomendas()
        {
            // Estou a colocar dados na Lista de clientes de cada Encomenda
            ListaFuncionarios = new HashSet<Funcionarios>();
        }

        ///<summary>
        ///Id da Encomenda
        /// </summary>
        [Key] 
        public int Id_encomenda { get; set; }

        ///<summary>
        ///Nome da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(150, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,5}",
                          ErrorMessage = "Deve escrever 2 a 6 nomes, começando por Maiúsculas, seguido de  minúsculas.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        ///<summary>
        ///Tipo da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        ///<summary>
        ///Descrição da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        ///<summary>
        ///Estado da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        ///<summary>
        ///Morada da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        ///<summary>
        ///Codigo Postal da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("([0-9]{4}(-)[0-9]{3})(( | d[aeo](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,7}.*",
            ErrorMessage = "Deve escrever o código postal seguido da localidade ")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        ///<summary>
        ///Data de Envio da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Data de Envio")]
        public DateTime DataEnvio { get; set; }

        ///<summary>
        ///Data de Entrega da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Data de Entrega")]
        public DateTime DataEntrega { get; set; }

        ///<summary>
        ///Altura da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Altura")]
        public float Altura { get; set; }

        ///<summary>
        ///Largura da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Largura")]
        public float Largura { get; set; }

        ///<summary>
        ///Comprimento da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Comprimento")]
        public float Comprimento { get; set; }

        ///<summary>
        ///Peso da Encomenda
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Peso")]
        public Decimal Peso { get; set; }

        ///<summary>
        ///Chave estrangeira do Id do Cliente
        /// </summary>
        [Display(Name = "Cliente")]
        [ForeignKey(nameof(Cliente))]
        public static int IdCliente { get; set; }
        public Clientes Cliente { get; set; }

        ///<summary>
        ///Lista de Funcionarios da Encomenda
        /// </summary>
        [Display(Name = "Lista de Funcionarios")]
        public ICollection<Funcionarios> ListaFuncionarios { get; set; }
    }
}