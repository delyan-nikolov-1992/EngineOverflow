namespace EngineOverflow.Web.InputModels.Feedback
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CreateInputModel
    {
        [Required]
        [AllowHtml]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}
