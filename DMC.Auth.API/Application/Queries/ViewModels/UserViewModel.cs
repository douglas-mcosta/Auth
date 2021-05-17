using System;
using System.ComponentModel.DataAnnotations;

namespace DMC.Auth.API.Application.Queries.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="O Nome é obrigátorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O Sobrenome é obrigátorio.")]
        public string LastName { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        [Required(ErrorMessage = "O E-mail é obrigátorio.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Nome é obrigátorio.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O Nome é obrigátorio.")]
        public int Education { get; set; }

        public string EducationName { get; set; }
    }
}