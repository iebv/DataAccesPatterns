using MyShop.Domain.Lazy;
using System;

namespace MyShop.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public IValueHolder<byte[]> ProfilePictureValueHolder { get; set; }
      
        public byte[] ProfilePicture {
            get 
            {
                return ProfilePictureValueHolder.GetValue(Name);

            }
         
            
        }



        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
