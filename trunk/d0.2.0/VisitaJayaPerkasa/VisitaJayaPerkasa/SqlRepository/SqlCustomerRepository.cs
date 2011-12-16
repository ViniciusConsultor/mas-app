using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCustomerRepository
    {
        public List<Customer> ListCustomers()
        {
            List<Customer> listCustomer = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer] WHERE deleted is null OR deleted = '0'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.CustomerName = reader.GetString(1);
                            customer.Office = reader.GetString(2);
                            customer.Address = reader.GetString(3);
                            customer.Phone = reader.GetString(4);
                            customer.Fax = reader.GetString(5);
                            customer.Email = reader.GetString(6);
                            customer.ContactPerson = reader.GetString(7);

                            if (listCustomer == null)
                                listCustomer = new List<Customer>();

                            listCustomer.Add(customer);
                            customer = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - listCustomers() " + e.Message);
            }

            return listCustomer;
        }


        public List<CustomerDetail> ListCustomerDetail()
        {
            List<CustomerDetail> listCustomerDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer_Detail] WHERE deleted is null OR deleted = '0'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerDetail customer = new CustomerDetail();
                            customer.CustomerDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customer.FirstName = reader.GetString(2);
                            customer.LastName = reader.GetString(3);
                            customer.CustomerDetailAddress = reader.GetString(4);
                            customer.CustomerDetailPhone = reader.GetString(5);
                            customer.CustomerDetailMobilePhone = reader.GetString(6);

                            if (listCustomerDetail == null)
                                listCustomerDetail = new List<CustomerDetail>();

                            listCustomerDetail.Add(customer);
                            customer = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - listCustomerDetail() " + e.Message);
            }

            return listCustomerDetail;
        }
    }
}
