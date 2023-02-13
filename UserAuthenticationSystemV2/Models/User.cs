using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace UserAuthenticationSystemV2.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(150)]
        [MinLength(5)]
        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(10)")]
        public string Email { get; set; }
        
        [MaxLength(150)]
        [MinLength(5)]
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Password { get; set; }

        public ICollection<ToDoItem> ToDoItems;

        public User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;

            ToDoItems = new List<ToDoItem>();
        }

        public User()
        {
            ToDoItems = new List<ToDoItem>();
        }
    }
}