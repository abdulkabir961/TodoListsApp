using System.ComponentModel.DataAnnotations;

namespace TodoListsApp.Models
{
    public class TodoListViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "List Content")]
        public string ListContent { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        [Required]
        public string Priority { get; set; }
    }
}
