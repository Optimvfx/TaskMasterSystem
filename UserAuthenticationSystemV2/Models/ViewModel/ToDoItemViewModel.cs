using System;
using System.ComponentModel.DataAnnotations;

namespace UserAuthenticationSystemV2.Models
{
    public class ToDoItemViewModel
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
    }
}