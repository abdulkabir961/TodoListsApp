using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoListsApp.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "List Content")]
        public string ListContent { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        [Required]
        public string Priority { get; set; }
    }

    public static class Encryption
    {
        public static string encrypt(string ToEncrypt)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
        }
        public static string decrypt(string cypherString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
        }
    }

}
