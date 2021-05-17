using System;
using System.ComponentModel.DataAnnotations;

namespace DMC.Auth.API.Application.Queries.ViewModels
{
    public class UpdateUserViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Education { get; set; }
    }
}