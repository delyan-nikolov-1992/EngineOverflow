﻿namespace EngineOverflow.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using EngineOverflow.Data.Common.Models;

    public class Feedback : AuditInfo, IDeletableEntity
    {
        public Feedback()
        {
            this.Votes = new HashSet<FeedbackVote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<FeedbackVote> Votes { get; set; }
    }
}
