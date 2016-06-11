namespace EngineOverflow.Web.InputModels.Questions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AskInputModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}
