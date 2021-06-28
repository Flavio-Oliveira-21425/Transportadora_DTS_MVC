using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Transportadora.Models
{
    public class Envios
    {
        ///<summary>
        ///Id do Envio
        /// </summary>
        [Key]
        public int Id_envio { get; set; }
        ///<summary>
        ///Custo do Envio
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Custo")]
        public Decimal Custo { get; set; }
        ///<summary>
        ///Data Prevista da entrega do  do Envio
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Data de Entrega Prevista")]
        public DateTime DataPrevista { get; set; }

        ///<summary>
        ///Chave estrangeira do Id da Encomenda
        /// </summary>
        [Display(Name = "Encomenda")]
        [ForeignKey(nameof(Encomenda))]
        public int IdEncomenda { get; set; }
        public Encomendas Encomenda { get; set; }

        ///<summary>
        ///Chave estrangeira do Id do Cliente
        /// </summary>
        [Display(Name = "Cliente")]
        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        public Clientes Cliente { get; set; }

        ///<summary>
        ///Chave estrangeira do Id do Funcionario
        /// </summary>
        [Display(Name = "Funcionario")]
        [ForeignKey(nameof(Funcionario))]
        public int IdTransportador { get; set; }
        public Funcionarios Funcionario { get; set; }
    }
}