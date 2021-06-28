using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportadora.Models
{
    public class Clientes
    {

        public Clientes()
        {
            // Estou a colocar dados na Lista de projetos de cada cliente
            ListaEncomendas = new HashSet<Encomendas>();
        }

        ///<summary>
        ///Id dos Cliente
        /// </summary>
        [Key]
        public int Id_cliente { get; set; }

        ///<summary>
        ///Nome do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(70, ErrorMessage = "Não pode ter mais do que {1} caráteres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,5}", 
            ErrorMessage = "Deve escrever 2 a 6 nomes, começando por Maiúsculas, seguido de  minúsculas.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        ///<summary>
        ///Email do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail invalido")]
        [EmailAddress(ErrorMessage = "Endereço de Email Invalido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        ///<summary>
        ///Contacto do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[239][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 2, 3 ou 9.")]
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }

        ///<summary>
        ///Morada do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        ///<summary>
        ///Codigo Postal do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("([0-9]{4}(-)[0-9]{3})(( | d[aeo](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,7}.*",
            ErrorMessage = "Deve escrever o código postal seguido da localidade ")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        ///<summary>
        ///NIF do Cliente
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[12567][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 1, 2, 5, 6 ou 7.")]
        [Display(Name = "NIF")]
        public string NIF { get; set; }

        // ***********************************************************************************
        // Relacionamento com os dados de Autenticação
        // ***********************************************************************************
        /// <summary>
        /// Atributo utilizado para quando um Cliente se autentica, 
        /// relacionar esse cliente com os seus projetos
        /// </summary>
        public string UserNameId { get; set; }

        ///<summary>
        ///Lista de Encomendas associadas a um Cliente
        /// </summary>
        [Display(Name = "Lista de Encomendas")]
        public ICollection <Encomendas> ListaEncomendas { get; set; }

        /// <summary>
        /// Lista de Envios associados a um cliente
        /// </summary>
        [Display(Name = "Lista de Envios")]
        public ICollection<Envios> ListaEnvios { get; set; }
    }
}