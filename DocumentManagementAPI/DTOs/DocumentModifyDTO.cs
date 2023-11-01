using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagementAPI.DTOs
{
    public class DocumentModifyDTO
    {
        [Required]
        public string[] Tags { get; set; }

        [Required]
        public Dictionary<string, string> Data { get; set; }
    }
}
