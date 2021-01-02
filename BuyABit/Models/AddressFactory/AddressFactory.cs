using BuyABit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BuyABit.Interfaces;

namespace BuyABit.Models.AddressFactory
{
    public interface IFactory
    {
        bool Load();
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    public class Shipping : IFactory
    {
        private readonly Task<bool> _isCreated;
        public Shipping(IDatabaseService dbcontext)
        {
            _isCreated = dbcontext.CreateShippingAddressAsync(new ShippingAddress());
        }
        public bool Load()
        {
            Console.WriteLine("Shipping address loaded...");
            return _isCreated.Result;
        }
    }


    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    public class Billing : IFactory
    {

        private readonly bool _isCreated;
        public Billing(IDatabaseService dbcontext)
        {
            _isCreated = dbcontext.CreateBillingAddressAsync(new BillingAddress()).Result;
        }
        public bool Load()
        {
            Console.WriteLine("Billing address loaded...");
            return _isCreated;
        }
    }

    /// <summary>
    /// The Creator Abstract Class
    /// </summary>
    public abstract class AddressFactory
    {
        public abstract IFactory CreateAddress(int AddressType);
    }


    /// <summary>
    /// A 'Concrete Creator' class
    /// </summary>
    public class ConcreteAddressFactory : AddressFactory
    {
        private readonly IDatabaseService _dbcontext;
        public ConcreteAddressFactory(IDatabaseService dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public override IFactory CreateAddress(int addressType)
        {
            switch (addressType)
            {
                case (int)AddressTypes.Billing:
                    return new Billing(_dbcontext);
                case (int)AddressTypes.Shipping:
                    return new Shipping(_dbcontext);
                //     case (int)AddressTypes.BothBillingAndShipping:
                // return new Shipping(_dbcontext);
                default:
                    throw new ApplicationException(string.Format("Addresss '{0}' cannot be created", addressType));
            }
        }
    }
}