using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDFeedBackWeb.Models
{
    public class MDFeedBackModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MDFeedBackModelId { get; set; }
        [Required(ErrorMessage = "Ошибка запроса. Укажите ваше имя.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Ошибка запроса. Укажите вашу фамилию.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Ошибка запроса. Укажите текст сообщения.")]
        public string Text { get; set; }
    }
}