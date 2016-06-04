﻿namespace EngineOverflow.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Common.Models;

    public class Post : AuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        // TODO: Author,
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
