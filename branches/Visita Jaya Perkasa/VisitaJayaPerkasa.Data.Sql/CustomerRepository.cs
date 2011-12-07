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

            List<Customer> customers = repo.Fetch<Customer>("SELECT * FROM [CUSTOMER] WHERE (deleted is null OR deleted = '0')").ToList<Customer>();

            repo.CloseSharedConnection();

            return customers;
        }

        public Customer GetCustomerByID(Guid ID)
        {
            var repo = new PetaPoco.Database(_mainConnectionString);

            repo.OpenSharedConnection();

            Customer customers = repo.SingleOrDefault<Customer>("SELECT * FROM CUSTOMER WHERE customer_id=@0 and (deleted is null OR deleted = '0')", ID);

            repo.CloseSharedConnection();

            return customers;
        }

        public void DeleteCustomer(Guid ID) 
        {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();
            Customer customers = GetCustomerByID(ID);
            repo.Update("CUSTOMER", "customer_id", customers);
            repo.CloseSharedConnection(); 
        }


        public IEnumerable<Customer> GetCustomerBySearch(string searchWord) {
            var repo = new PetaPoco.Database(_mainConnectionString);
            repo.OpenSharedConnection();

            IEnumerable<Customer> customers = repo.Fetch<Customer>("SELECT * FROM [CUSTOMER] " +
             "WHERE (deleted is null OR deleted = '0') AND (office like '%" + searchWord + "%' OR customer_name like '%" + searchWord + "%' OR " + 
             "Fax like '%" + searchWord + "%' OR address like '%" + searchWord + "%' OR phone like '%" + searchWord + "%' " + 
             "OR email like '%" + searchWord + "%' OR contact_person like '%" + searchWord + "%')").ToList<Customer>();

            repo.CloseSharedConnection();
            return customers;
        }
    }
}
