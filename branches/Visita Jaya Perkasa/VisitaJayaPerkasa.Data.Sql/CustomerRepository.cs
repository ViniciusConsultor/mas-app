using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Business.Entities;

namespace VisitaJayaPerkasa.Data.Sql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly String _mainConnectionString;

        public CustomerRepository(string mainConnectionString)
        {
            _mainConnectionString = mainConnectionString;
        }

        public void SaveCustomer(Customer customer)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            using (var scope = repo.GetTransaction())
            {
                if (GetCustomerByID(customer.ID) == null)
                {
                    //Create new
                    repo.Insert(customer);
                }
                else
                {
                    //Update it

                    repo.Update("CUSTOMER", "customer_id", customer);
                }

                scope.Complete();
            }

            repo.CloseSharedConnection();

        }


        public IEnumerable<Customer> GetListCustomer()
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            List<Customer> customers = repo.Fetch<Customer>("SELECT * FROM [CUSTOMER]").ToList<Customer>();

            repo.CloseSharedConnection();

            return customers;
        }

        public Customer GetCustomerByID(Guid ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Customer customers = repo.SingleOrDefault<Customer>("SELECT * FROM CUSTOMER WHERE customer_id=@0", ID);

            repo.CloseSharedConnection();

            return customers;
        }

        public void DeleteCustomer(Guid ID) 
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Customer customers = GetCustomerByID(ID);
            repo.Delete("CUSTOMER", "customer_id", customers);
            repo.CloseSharedConnection(); 
        }

    }
}
