using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.Database
{
    [Table("Operations")]
    public class Operation
    {
        [Key]
        public int OperationID { get; set; }
        public long UserID { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime OperationDate { get; set; }
        public int FileID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("FileID")]
        public File File { get; set; }
    }
}
