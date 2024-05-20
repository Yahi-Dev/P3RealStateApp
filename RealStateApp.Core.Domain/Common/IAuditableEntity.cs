using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Common
{
    public interface IAuditableEntity 
    {
        public int Id { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? Deleted { get; set; }
        public string? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
