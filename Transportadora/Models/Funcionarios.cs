using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportadora.Models
{
    public class Funcionarios
    {
        public Funcionarios()
        {
            // Estou a colocar dados na Lista de projetos de cada Funcionario
            ListaEncomendas = new HashSet<Encomendas>();

            // Estou a colocar dados na Lista de formulários de cada Funcionario
            //ListaEnvios = new HashSet<Envios>();
        }
        ///<summary>
        ///Id do Funcionario
        /// </summary>
        [Key]
        public int Id_funcionario { get; set; }
        ///<summary>
        ///Nome do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        ///<summary>
        ///Cargo do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

        ///<summary>
        ///Email do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [EmailAddress(ErrorMessage = "Endereço de Email Invalido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        ///<summary>
        ///Contacto do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever exatamente {1} algarismos no {0}.")]
        [RegularExpression("[239][0-9]{8}", ErrorMessage = "Deve escrever um nº, com 9 algarismos, começando por 2, 3 ou 9.")]
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }

        ///<summary>
        ///Morada do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Morada")]
        public string Morada { get; set; }

        ///<summary>
        ///Codigo Postal do Funcionario
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("([0-9]{4}(-)[0-9]{3})(( | d[aeo](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,7}.*",
            ErrorMessage = "Deve escrever o código postal seguido da localidade ")]
        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        ///<summary>
        ///Nif do Funcionario
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
        /// Atributo utilizado para quando um Funcionario se autentica, 
        /// relacionar esse cliente com os seus projetos
        /// </summary>
        public string UserNameId { get; set; }

        ///<summary>
        ///Lista de Encomendas do Funcionario
        /// </summary>
        [Display(Name = "Lista de Encomendas")]
        public ICollection <Encomendas> ListaEncomendas { get; set; }
        ///<summary>
        ///Lista de Encomendas do Funcionaro
        /// </summary>
        [Display(Name = "Lista de Envios")]
        public ICollection<Encomendas> ListaEnvios { get; set; }
    }
}