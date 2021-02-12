using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRules.Models
{
    public class RuleModel
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; init; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string AreaId { get; set; }
        [Required]
        public string Status { get; set; }
        public List<string> Tags { get; set; }
    }
}
