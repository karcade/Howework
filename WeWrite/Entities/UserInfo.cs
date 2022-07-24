using System.ComponentModel.DataAnnotations.Schema;

namespace WeWrite.Entities
{
    internal class UserInfo
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
