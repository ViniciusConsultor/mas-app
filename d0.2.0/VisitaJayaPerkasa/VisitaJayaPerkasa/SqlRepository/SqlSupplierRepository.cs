﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlSupplierRepository
    {
        public List<Supplier> ListSuppliers()
        {
            List<Supplier> listSupplier = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT s.supplier_id, s.category_id, s.supplier_name, s.address, s.phone, s.fax, s.email, s.contact_person, c.category_name, s.deleted FROM [Supplier] s JOIN [CATEGORY] c " +
                        "ON (s.deleted is null OR s.deleted = '0') AND (c.deleted is null OR c.deleted = '0') AND s.category_id = c.category_id", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Id = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            supplier.CategoryId = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            supplier.SupplierName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            supplier.Address = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            supplier.Phone = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            supplier.Fax = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            supplier.Email = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            supplier.ContactPerson = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                            supplier.CategoryName = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? null : reader.GetString(8);

                            if (listSupplier == null)
                                listSupplier = new List<Supplier>();

                            listSupplier.Add(supplier);
                            supplier = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSupplierRepository.cs - ListSuppliers() " + e.Message);
            }

            return listSupplier;
        }


        public List<string> ListTruckingNumber(Guid ID)
        {
            List<string> listTruckingNumber = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT trucking_no FROM [SUPPLIER_TRUCKING] WHERE supplier_id like '" + ID.ToString() + "'", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (listTruckingNumber == null)
                                listTruckingNumber = new List<string>();

                            listTruckingNumber.Add(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSupplierRepository.cs - ListTruckingNumber() " + e.Message);
            }

            return listTruckingNumber;
        }


        public bool CheckSupplier(SqlParameter[] sqlParam, Guid gID, bool checkDeletedData = false)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    String criteria = "";
                    if (!gID.ToString().Equals(Guid.Empty.ToString()))
                        criteria = " AND supplier_id != '" + gID.ToString() + "'";
                    else if (checkDeletedData)
                        criteria = " AND deleted = '1'";
                    else
                        criteria = " AND (deleted is null OR deleted = '0')";

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 supplier_id FROM [SUPPLIER] WHERE supplier_name = '" + sqlParam[2].Value + "' AND category_id = '" + sqlParam[1].Value + "'" + /*" AND email = '" + sqlParam[6].Value + "'" +*/ criteria, con))
                    {
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

                Logging.Error("SqlSupplierRepository.cs - CheckSupplier() " + e.Message);
            }

            return exists;
        }

      


        public bool ActivateSupplier(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;
            Guid ID = GetSupplierID(sqlParam[2].Value.ToString(), sqlParam[1].Value.ToString()/*, sqlParam[6].Value.ToString()*/);


            if (ID.ToString().Equals(Guid.Empty.ToString()))
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
                        "Delete [Supplier_Detail] WHERE supplier_id = '" + ID + "'" , con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        n = deleteCommand.ExecuteNonQuery();

                        using (SqlCommand command = new SqlCommand(
                            "Update [Supplier] set " +
                            "category_id = " + sqlParam[1].ParameterName + ", " +
                            "supplier_name = " + sqlParam[2].ParameterName + ", " +
                            "address = " + sqlParam[3].ParameterName + ", " +
                            "phone = " + sqlParam[4].ParameterName + ", " +
                            "fax = " + sqlParam[5].ParameterName + ", " +
                            "email = " + sqlParam[6].ParameterName + ", " +
                            "contact_person = " + sqlParam[7].ParameterName + ", " +
                            "deleted = " + sqlParam[8].ParameterName + " WHERE " +
                            "supplier_id = '" + ID + "'"
                            , con))
                        {
                            command.Transaction = sqlTransaction;

                            for (int i = 0; i < 9; i++)
                                command.Parameters.Add(sqlParam[i]);
                            n = command.ExecuteNonQuery();
                            command.Parameters.Clear();

                            int z = 9;
                            int subz = 9;
                            if ((n > 0) && sqlParam.Length > 9)
                            {
                                //-10 is total sqlparameter minus number of customer master
                                // / 9 is remain of total sqlparameter minus 10 is customer detail who have 9 number of field
                                for (int k = 0; k < ((sqlParam.Length - 10) / 8); k++)
                                {
                                    using (SqlCommand subCommand = new SqlCommand(
                                        "Insert into [Supplier_Detail] values(" +
                                        sqlParam[(z += 2) - 2].ParameterName + ", " + // for handle index of ID
                                        "'" + ID + "', " +
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

                                        for (int i = 0; i < 9; i++)
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
                    Logging.Error("SqlCustomerRepository.cs - CreateSupplier() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }



        public Guid GetSupplierID(String supplierName, String categoryID/*, String email*/)
        {
            Guid ID = Guid.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 supplier_id FROM [SUPPLIER] WHERE supplier_name = '" + supplierName + "' AND category_id = '" + categoryID + "'" + /* AND email = '" + email + "'*/ " AND deleted = '1'", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlSupplierRepository.cs - GetSupplierID() " + e.Message);
            }

            return ID;
        }

        public List<Supplier> GetListSupplierForRecipient()
        {
            List<Supplier> listSupplier = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT s.supplier_id, s.supplier_name FROM [Supplier] s WHERE (s.deleted is null OR s.deleted = '0') ", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Id = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            supplier.SupplierName = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);

                            if (listSupplier == null)
                                listSupplier = new List<Supplier>();

                            listSupplier.Add(supplier);
                            supplier = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSupplierRepository.cs - GetListSupplierForRecipient() " + e.Message);
            }

            return listSupplier;
        }

        public List<SupplierDetail> ListSupplierDetail(Guid ID)
        {
            List<SupplierDetail> listSupplierrDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT * FROM [Supplier_Detail] WHERE supplier_id = '" + ID + "' AND (deleted is null OR deleted = '0')"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            SupplierDetail supplier = new SupplierDetail();
                            supplier.SupplierDetailId = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            supplier.Id = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            supplier.FirstName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            supplier.LastName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            supplier.SupplierDetailPhone = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            supplier.SupplierDetailMobilePhone = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            supplier.SupplierDetailAddress = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            supplier.SupplierMobileExt = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? null : reader.GetString(8);
                            if (listSupplierrDetail == null)
                                listSupplierrDetail = new List<SupplierDetail>();

                            listSupplierrDetail.Add(supplier);
                            supplier = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerRepository.cs - ListSupplierDetail() " + e.Message);
            }

            return listSupplierrDetail;
        }

        public bool DeleteSupplier(SqlParameter[] sqlParam)
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
                        "Update [Supplier_Detail] set deleted = '1' WHERE supplier_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        using (SqlCommand subCommand = new SqlCommand(
                            "Update [Supplier] set deleted = '1' WHERE supplier_id = " + sqlParam[0].ParameterName, con
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

                    Logging.Error("SqlCustomerRepository.cs - DeleteSupplier() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool CreateSupplier(SqlParameter[] sqlParam, int totalRecordTrucking)
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
                        "Insert into [Supplier] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName + ", " +
                        sqlParam[4].ParameterName + ", " +
                        sqlParam[5].ParameterName + ", " +
                        sqlParam[6].ParameterName + ", " +
                        sqlParam[7].ParameterName + ", " +
                        sqlParam[8].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < 9; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        int z = 9;
                        int subz = 9;
                        if ((n > 0) && (sqlParam.Length - (totalRecordTrucking * 3)) > 9)
                        {
                            //sqlparam.Length - 9 is total sqlParam minus total field of master supplier
                            //totalRecordTrucking*3 is total field of supplier trucking
                            //divide 9 is total field of supplier detail 
                            for (int k = 0; k < (((sqlParam.Length - 9)-(totalRecordTrucking*3)) / 9); k++)
                            {
                                using (SqlCommand subCommand = new SqlCommand(
                                    "Insert into [Supplier_Detail] values(" +
                                    sqlParam[z++].ParameterName + ", " +
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

                                    for (int i = 0; i < 9; i++)
                                        subCommand.Parameters.Add(sqlParam[subz++]);
                                    n = subCommand.ExecuteNonQuery();
                                    subCommand.Parameters.Clear();

                                    if (n == 0)
                                        break;
                                }
                            }
                        }


                        int tempZvalue = z;
                        if (n > 0)
                        {
                            if (totalRecordTrucking > 0)
                            {
                                //3 is total field of trucking
                                for (int k = 0; k < ((sqlParam.Length - tempZvalue) / 3); k++)
                                {
                                    using (SqlCommand subCommand2 = new SqlCommand("Insert into [SUPPLIER_TRUCKING] Values(" +
                                        sqlParam[z++].ParameterName + ", " +
                                        sqlParam[z++].ParameterName + ", " +
                                        sqlParam[z++].ParameterName + ")",
                                        con))
                                    {
                                        subCommand2.Transaction = sqlTransaction;

                                        for (int i = 0; i < 3; i++)
                                            subCommand2.Parameters.Add(sqlParam[subz++]);
                                        n = subCommand2.ExecuteNonQuery();
                                        subCommand2.Parameters.Clear();

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
                    Logging.Error("SqlCustomerRepository.cs - CreateSupplier() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public bool EditSupplier(SqlParameter[] sqlParam, int totalRecordTrucking)
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
                        "Delete [Supplier_Detail] WHERE supplier_id = " + sqlParam[0].ParameterName, con))
                    {
                        deleteCommand.Transaction = sqlTransaction;
                        deleteCommand.Parameters.Add(sqlParam[0]);
                        n = deleteCommand.ExecuteNonQuery();
                        deleteCommand.Parameters.Clear();

                        if (n > 0)
                        {
                            if (totalRecordTrucking > 0)
                            {
                                using (SqlCommand deleteTruckingCommand = new SqlCommand(
                                    "Delete [SUPPLIER_TRUCKING] WHERE supplier_id = " + sqlParam[0].ParameterName, con))
                                {
                                    deleteTruckingCommand.Transaction = sqlTransaction;
                                    deleteTruckingCommand.Parameters.Add(sqlParam[0]);
                                    n = deleteTruckingCommand.ExecuteNonQuery();
                                    deleteTruckingCommand.Parameters.Clear();
                                }
                            }



                            if (n > 0)
                            {
                                using (SqlCommand command = new SqlCommand(
                                    "Update [Supplier] set " +
                                    "category_id = " + sqlParam[1].ParameterName + ", " +
                                    "supplier_name = " + sqlParam[2].ParameterName + ", " +
                                    "address = " + sqlParam[3].ParameterName + ", " +
                                    "phone = " + sqlParam[4].ParameterName + ", " +
                                    "fax = " + sqlParam[5].ParameterName + ", " +
                                    "email = " + sqlParam[6].ParameterName + ", " +
                                    "contact_person = " + sqlParam[7].ParameterName + ", " +
                                    "deleted = " + sqlParam[8].ParameterName + " WHERE " +
                                    "supplier_id = " + sqlParam[0].ParameterName
                                    , con))
                                {
                                    command.Transaction = sqlTransaction;

                                    for (int i = 0; i < 9; i++)
                                        command.Parameters.Add(sqlParam[i]);
                                    n = command.ExecuteNonQuery();
                                    command.Parameters.Clear();

                                    int z = 9;
                                    int subz = 9;
                                    if ((n > 0) && (sqlParam.Length - (totalRecordTrucking * 3)) > 9)
                                    {
                                        //sqlparam.Length - 9 is total sqlParam minus total field of master supplier
                                        //totalRecordTrucking*3 is total field of supplier trucking
                                        //divide 9 is total field of supplier detail 
                                        for (int k = 0; k < (((sqlParam.Length - 9) - (totalRecordTrucking * 3)) / 9); k++)
                                        {
                                            using (SqlCommand subCommand = new SqlCommand(
                                                "Insert into [Supplier_Detail] values(" +
                                                sqlParam[z++].ParameterName + ", " +
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

                                                for (int i = 0; i < 9; i++)
                                                    subCommand.Parameters.Add(sqlParam[subz++]);
                                                n = subCommand.ExecuteNonQuery();
                                                subCommand.Parameters.Clear();

                                                if (n == 0)
                                                    break;
                                            }
                                        }
                                    }


                                    int tempZvalue = z;
                                    if (totalRecordTrucking > 0 && n > 0)
                                    {
                                        //3 is total field of trucking
                                        for (int k = 0; k < ((sqlParam.Length - tempZvalue) / 3); k++)
                                        {
                                            using (SqlCommand subCommand2 = new SqlCommand("Insert into [SUPPLIER_TRUCKING] Values(" +
                                                sqlParam[z++].ParameterName + ", " +
                                                sqlParam[z++].ParameterName + ", " +
                                                sqlParam[z++].ParameterName + ")",
                                                con))
                                            {
                                                subCommand2.Transaction = sqlTransaction;

                                                for (int i = 0; i < 3; i++)
                                                    subCommand2.Parameters.Add(sqlParam[subz++]);
                                                n = subCommand2.ExecuteNonQuery();
                                                subCommand2.Parameters.Clear();

                                                if (n == 0)
                                                    break;
                                            }
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
                }
                catch (Exception e)
                {
                    if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    n = 0;
                    Logging.Error("SqlCustomerRepository.cs - CreateSupplier() " + e.Message);
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
