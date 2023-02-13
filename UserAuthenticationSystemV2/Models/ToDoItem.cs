using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAuthenticationSystemV2.Models
{
    public class ToDoItem
    {
        [Key]                              
        public Guid Id { get; set; }       
                                           
        [MaxLength(150)]                   
        [MinLength(5)]                     
        [Required]
        public string Title { get; set; }  
        
        [MaxLength(150)]                   
        [MinLength(5)]                     
        [Required]
        public string Description { get; set; }

        public bool IsComplied { get; set; }

        public ToDoItem(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}