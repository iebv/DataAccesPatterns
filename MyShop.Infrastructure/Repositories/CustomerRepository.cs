using MyShop.Domain.Lazy;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ShoppingContext context) : base(context)
        {
        }

        public override IEnumerable<Customer> All()
        {
            return base.All().Select(c =>
            {
                c.ProfilePictureValueHolder = new ValueHolder<byte[]>((parameter) =>
                {
                    return ProfilePictureService.GetFor(parameter.ToString());
                });
                return c;
            });

        }

        public override IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            return base.Find(predicate).Select(c =>
            {
                c.ProfilePictureValueHolder = new ValueHolder<byte[]>((parameter) =>
                {
                    return ProfilePictureService.GetFor(parameter.ToString());
                });
                return c; 
            });
        }

        public override Customer Get(Guid id)
        {
            var customer = base.Get(id);
            customer.ProfilePictureValueHolder = new ValueHolder<byte[]>((parameter) =>
            {
                return ProfilePictureService.GetFor(parameter.ToString());
            });

            return customer;
        }

        public override Customer Update(Customer entity)
        {

            var customer = context.Customers
                .Single(c => c.CustomerId == entity.CustomerId);

            customer.Name = entity.Name;
            customer.City = entity.City;
            customer.PostalCode = entity.PostalCode;
            customer.ShippingAddress = entity.ShippingAddress;
            customer.Country = entity.Country;

            return base.Update(customer);     
        }
    }
}
