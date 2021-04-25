using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Malzamaty.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserAddressID { get; set; }
        [ForeignKey("UserAddressID")]
        public Address UserAddress { get; set; }
        public Guid LibraryAddressID { get; set; }
        [ForeignKey("LibraryAddressID")]
        public Address LibraryAddress { get; set; }
        public Guid FileID { get; set; }
        [ForeignKey("FileID")]
        public File File { get; set; }
        public int OrderStatus { get; set; }
        public DateTimeOffset OrderedDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

    }
}
