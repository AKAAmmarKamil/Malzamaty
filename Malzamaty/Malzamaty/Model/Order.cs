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
        public bool IsDelivered { get; set; }
    }
}
