using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Mechanist_Website.Models
{
    public class DownloadInfo
    {
        [Key]
        public int DownloadID { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }
        public string Credit { get; set; }
        public string Description { get; set; }
        public DateTime DateUploaded { get; set; }
        public bool Depreciated { get; set; }
    }
}
