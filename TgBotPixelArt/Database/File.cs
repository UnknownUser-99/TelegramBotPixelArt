using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.Database
{
    [Table("Files")]
    public class File
    {
        [Key]
        public int FileID { get; set; }
        public string FileFormat { get; set; }
    }
}
