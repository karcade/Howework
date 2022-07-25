using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeWrite.Entities
{
    [Table("Readers")]
    internal class Reader:User
    {
        public List<ReadingList> ReadingLists { get; set; }
        public string Status { get; set; }
    }
}
