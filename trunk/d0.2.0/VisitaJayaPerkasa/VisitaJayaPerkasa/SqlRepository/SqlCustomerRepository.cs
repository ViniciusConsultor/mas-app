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
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer] WHERE (deleted is null OR deleted = '0') AND customer_name != '" + Constant.VisitaJayaPerkasaApplication.strGeneralCustomer + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            customer.Office = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            customer.Address = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            customer.Phone = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            customer.Fax = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            customer.Email = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            customer.ContactPerson = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                            customer.StatusPPN = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? 0 : Convert.ToInt32(reader.GetBoolean(8));
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


        public List<Customer> listCustomerForPriceList()
        {
            List<Customer> listCustomer = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer] WHERE (deleted is null OR deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            customer.Office = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            customer.Address = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            customer.Phone = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            customer.Fax = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            customer.Email = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            customer.ContactPerson = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                            customer.StatusPPN = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? 0 : Convert.ToInt32(reader.GetBoolean(8));
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
                Logging.Error("SqlCustomerRepository.cs - listCustomerForPriceList() " + e.Message);
            }

            return listCustomer;
        }


        public List<CustomerDetail> ListCustomerDetail(Guid ID)
        {
            List<CustomerDetail> listCustomerDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;


                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Customer_Detail] WHERE customer_id = '" + ID + "' AND (deleted is null OR deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerDetail customer = new CustomerDetail();
                            customer.CustomerDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customer.ID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customer.FirstName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            customer.LastName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            customer.CustomerDetailAddress = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            customer.CustomerDetailPhone = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            customer.CustomerDetailMobilePhone = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);

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

        public bool DeleteCustomer(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();

                    SqlParameter tempSqlParam = sqlParam[0];
                    using (SqlCommand command = new SqlCommand(
                        "Update [Customer_Detail] set deleted = '1' WHERE customer_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        using (SqlCommand subCommand = new SqlCommand(
                            "Update [Customer] set deleted = '1' WHERE customer_id = " + sqlParam[0].ParameterName, con
                            ))
                        {
                            subCommand.Transaction = sqlTransaction;
                            subCommand.Parameters.Add(sqlParam[0]);
                            n = subCommand.ExecuteNonQuery();
                        }
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlCustomerRepository.cs - DeleteCustomer() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool CreateCustomer(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [Customer] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName + ", " +
                        sqlParam[4].ParameterName + ", " +
                        sqlParam[5].ParameterName + ", " +
                        sqlParam[6].ParameterName + ", " +
                        sqlParam[7].ParameterName + ", " +
                        sqlParam[8].ParameterName + ", " +
                        sqlParam[9].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < 10; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        int z = 10;
                        int subz = 10;
                        if ((n > 0) && sqlParam.Length > 10)
                        {
                            //-9 is total sqlparameter minus number of customer master
                            // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                            for (int k = 0; k < ((sqlParam.Length - 10) / 8); k++)
                            {
                                using (SqlCommand subCommand = new SqlCommand(
                                    "Insert into [Customer_Detail] values(" +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName + ", " +
                                    sqlParam[z++].ParameterName +
                                    ")"
                                    , con))
                                {
                                    subCommand.Transaction = sqlTransaction;

                                    for (int i = 0; i < 8; i++)
                                        subCommand.Parameters.Add(sqlParam[subz++]);
                                    n = subCommand.ExecuteNonQuery();
                                    subCommand.Parameters.Clear();

                                    if (n == 0)
                                        break;
                                }
                            }

                        }
                    }

                    if (n > 0)
                        sqlTransaction.Commit();
                    else
                        sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    Logging.Error("SqlCustomerRepository.cs - CreateCustomer() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool EditCustomer(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand deleteCommand = new SqlCommand(
                        "Delete [Customer_Detail] WHERE customer_id = " + sqlParam[0].ParameterName, con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        deleteCommand.Parameters.Add(sqlParam[0]);
                        n = deleteCommand.ExecuteNonQuery();
                        deleteCommand.Parameters.Clear();

                            using (SqlCommand command = new SqlCommand(
                                "Update [Customer] set " +
                                "customer_name = " + sqlParam[1].ParameterName + ", " +
                                "office = " + sqlParam[2].ParameterName + ", " +
                                "address = " + sqlParam[3].ParameterName + ", " +
                                "phone = " + sqlParam[4].ParameterName + ", " +
                                "fax = " + sqlParam[5].ParameterName + ", " +
                                "email = " + sqlParam[6].ParameterName + ", " +
                                "contact_person = " + sqlParam[7].ParameterName + ", " +
                                "status_ppn = " + sqlParam[8].ParameterName + ", " +
                                "deleted = " + sqlParam[9].ParameterName + " WHERE " +
                                "customer_id = " + sqlParam[0].ParameterName
                                , con))
                            {
                                command.Transaction = sqlTransaction;

                                for (int i = 0; i < 10; i++)
                                    command.Parameters.Add(sqlParam[i]);
                                n = command.ExecuteNonQuery();
                                command.Parameters.Clear();

                                int z = 10;
                                int subz = 10;
                                if ((n > 0) && sqlParam.Length > 10)
                                {
                                    //-9 is total sqlparameter minus number of customer master
                                    // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                                    for (int k = 0; k < ((sqlParam.Length - 10) / 8); k++)
                                    {
                                        using (SqlCommand subCommand = new SqlCommand(
                                            "Insert into [Customer_Detail] values(" +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName +
                                            ")"
                                            , con))
                                        {
                                            subCommand.Transaction = sqlTransaction;

                                            for (int i = 0; i < 8; i++)
                                                subCommand.Parameters.Add(sqlParam[subz++]);
                                            n = subCommand.ExecuteNonQuery();
                                            subCommand.Parameters.Clear();

                                            if (n == 0)
                                                break;
                                        }
                                    }

                                }
                            }
                        }

                        if (n > 0)
                            sqlTransaction.Commit();
                        else
                            sqlTransaction.Rollback();
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    n = 0;
                    Logging.Error("SqlCustomerRepository.cs - CreateCustomer() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool CheckCustomer(SqlParameter[] sqlParam, Guid ID, bool checkDeletedData = false)
        {
            bool exists = false;
            String criteria;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    if (!ID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND customer_id != '" + ID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";


                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 customer_id FROM [Customer] WHERE customer_name = " + sqlParam[1].ParameterName + 
                        " AND office = " + sqlParam[2].ParameterName + criteria, con))
                    {
                        command.Parameters.Add(sqlParam[1]);
                        command.Parameters.Add(sqlParam[2]);

                        SqlDataReader reader = command.ExecuteReader();
                        command.Parameters.Clear();
                        while (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlCityRepository.cs - CheckCustomer() " + e.Message);
            }

            return exists;
        }


        public string GetCustomerIdByNameAndOffice(String Name, String office)
        {
            string ID = Guid.Empty.ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 customer_id FROM [Customer] " +
                        "WHERE customer_name = '" + Name + "' AND office = '" + office + "'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = reader.GetGuid(0).ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - GetCustomerIdByNameAndOffice() " + e.Message);
            }

            return ID;
        }


        public bool ActivateCustomer(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            String ID = GetCustomerIdByNameAndOffice(sqlParam[1].Value.ToString(), sqlParam[2].Value.ToString());
            if (ID.Equals(Guid.Empty.ToString()))
                return false;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                try
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    sqlTransaction = con.BeginTransaction();

                    using (SqlCommand deleteCommand = new SqlCommand(
                        "Delete [Customer_Detail] WHERE customer_id = '" + ID + "'", con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        n = deleteCommand.ExecuteNonQuery();

                            using (SqlCommand command = new SqlCommand(
                                "Update [Customer] set " +
                                "address = " + sqlParam[3].ParameterName + ", " +
                                "phone = " + sqlParam[4].ParameterName + ", " +
                                "fax = " + sqlParam[5].ParameterName + ", " +
                                "email = " + sqlParam[6].ParameterName + ", " +
                                "contact_person = " + sqlParam[7].ParameterName + ", " +
                                "status_ppn = " + sqlParam[8].ParameterName + ", " +
                                "deleted = " + sqlParam[9].ParameterName + " WHERE " +
                                "customer_id = '" + ID + "'"
                                , con))
                            {
                                command.Transaction = sqlTransaction;

                                for (int i = 3; i < 10; i++)
                                    command.Parameters.Add(sqlParam[i]);
                                n = command.ExecuteNonQuery();
                                command.Parameters.Clear();

                                int z = 10;
                                int subz = 10;
                                if ((n > 0) && sqlParam.Length > 10)
                                {
                                    //-9 is total sqlparameter minus number of customer master
                                    // / 8 is remain of total sqlparameter minus 9 is customer detail who have 8 number of field
                                    for (int k = 0; k < ((sqlParam.Length - 10) / 8); k++)
                                    {
                                        using (SqlCommand subCommand = new SqlCommand(
                                            "Insert into [Customer_Detail] values(" +
                                            sqlParam[(z+=2) - 2].ParameterName + ", " + //+2 for handle ID who not increment one value
                                            "'" + ID + "', " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName + ", " +
                                            sqlParam[z++].ParameterName +
                                            ")"
                                            , con))
                                        {
                                            subCommand.Transaction = sqlTransaction;

                                            for (int i = 0; i < 8; i++)
                                                subCommand.Parameters.Add(sqlParam[subz++]);
                                            n = subCommand.ExecuteNonQuery();
                                            subCommand.Parameters.Clear();

                                            if (n == 0)
                                                break;
                                        }
                                    }

                                }
                        }

                        if (n > 0)
                            sqlTransaction.Commit();
                        else
                            sqlTransaction.Rollback();
                    }
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    n = 0;
                    Logging.Error("SqlCustomerRepository.cs - ActivateCustomer() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


    }
}
