using System;
using System.ComponentModel.DataAnnotations;

namespace Productive.Server.Models
{
	public class AuditableEntity
	{
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DeletedOn { get; set; }
        [MaxLength(256)]
        public string? DeletedBy { get; set; } 
    }
}

