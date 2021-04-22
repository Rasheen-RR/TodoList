using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String UserEmail { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime Addeddate { get; set; }
        public DateTime DueDate { get; set; }
        public Boolean Done { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}
