using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    class SqlSuratRepository
    {
        public List<Surat> ListSurat(EnumSurat type)
        {
            List<Surat> listSurat = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    String strType = "";
                    if (type == EnumSurat.LeadTime)
                        strType = "LT";
                    else if (type == EnumSurat.PenawaranHarga)
                        strType = "PH";
                    else if (type == EnumSurat.RO)
                        strType = "RO";
                    else
                        strType = "SI";

                    using (SqlCommand command = new SqlCommand(
                        "Select No_Surat, Tgl, [Surat].Customer_id, [Surat].Supplier_id, customer_name From [Surat] left join [Supplier] " + 
                        "on [surat].supplier_id = [supplier].supplier_id Left Join [Customer] " + 
                        "on [Customer].customer_id = [Surat].customer_id Where No_Surat like '%" + strType + "%'", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Surat surat = new Surat();
                            surat.NoSurat = reader.GetString(0);
                            surat.Tgl = reader.GetDateTime(1);

                            if (!Utility.Utility.IsDBNull(reader.GetValue(2)))
                                surat.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            if (!Utility.Utility.IsDBNull(reader.GetValue(3)))
                                surat.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            if (!Utility.Utility.IsDBNull(reader.GetValue(4)))
                                surat.CustomerName = reader.GetString(4);

                            if (listSurat == null)
                                listSurat = new List<Surat>();

                            listSurat.Add(surat);
                            surat = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSuratRepository.cs - ListSurat() " + e.Message);
            }

            return listSurat;
        }


        public List<Surat> ListSuratByCriteria(EnumSurat type, string beginDate, string endDate, string hal, string customerID)
        {
            List<Surat> listSurat = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    String strType = "";
                    if (type == EnumSurat.LeadTime)
                        strType = "LT";
                    else if (type == EnumSurat.PenawaranHarga)
                        strType = "PH";
                    else if (type == EnumSurat.RO)
                        strType = "RO";
                    else
                        strType = "SI";

                    string sql = "Select No_Surat, Tgl, [Surat].Customer_id, [Surat].Supplier_id, customer_name From [Surat] left join [Supplier] " +
                        "on [surat].supplier_id = [supplier].supplier_id Left Join [Customer] " +
                        "on [Customer].customer_id = [Surat].customer_id Where No_Surat like '%" + strType + "%' " +
                        "and (Tgl between cast('" + beginDate + "' as date) and cast('" + endDate + "' as date)) ";
                    if(hal != null)
                        sql += "And No_Surat like '" + hal + "' ";
                    else if(customerID != null)
                        sql += "And [Surat].customer_id = '" + customerID + "' ";

                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Surat surat = new Surat();
                            surat.NoSurat = reader.GetString(0);
                            surat.Tgl = reader.GetDateTime(1);

                            if (!Utility.Utility.IsDBNull(reader.GetValue(2)))
                                surat.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            if (!Utility.Utility.IsDBNull(reader.GetValue(3)))
                                surat.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            if (!Utility.Utility.IsDBNull(reader.GetValue(4)))
                                surat.CustomerName = reader.GetString(4);

                            if (listSurat == null)
                                listSurat = new List<Surat>();

                            listSurat.Add(surat);
                            surat = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSuratRepository.cs - ListSuratByCriteria() " + e.Message);
            }

            return listSurat;
        }



        public Surat GetlastNoSurat(EnumSurat type)
        {
            Surat surat = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;
                    String strType = "";

                    switch (type)
                    { 
                        case EnumSurat.LeadTime:
                            strType = "LT";
                            break;
                        case EnumSurat.PenawaranHarga:
                            strType = "PH";
                            break;
                        case EnumSurat.RO:
                            strType = "RO";
                            break;
                        case EnumSurat.ShippingInstruction:
                            strType = "SI";
                            break;
                    }

                    using (SqlCommand command = new SqlCommand(
                        "Select Top 1 No_Surat, Tgl, Customer_id, Supplier_id From [Surat] Where No_Surat Like '%" + strType + "%' Order by No_Surat desc", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            surat = new Surat();
                            surat.NoSurat = reader.GetString(0);
                            surat.Tgl = reader.GetDateTime(1);

                            if (!Utility.Utility.IsDBNull(reader.GetValue(2)))
                                surat.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            if (!Utility.Utility.IsDBNull(reader.GetValue(3)))
                                surat.SupplierID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlSuratRepository.cs - GetlastNoSurat() " + e.Message);
            }

            return surat;
        }


        public bool CreateSurat(SqlParameter[] sqlParam)
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
                        "Insert into [Surat] values (" +
                        sqlParam[0].ParameterName + ", " +
                        sqlParam[1].ParameterName + ", " +
                        sqlParam[2].ParameterName + ", " +
                        sqlParam[3].ParameterName +
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
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

                    Logging.Error("SqlSuratRepository.cs - CreateSurat() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }
            return n > 0;
        }



        public bool EditSurat(SqlParameter[] sqlParam)
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
                        "Update [Surat] set Tgl = " +
                        sqlParam[1].ParameterName + ", Customer_ID = " +
                        sqlParam[2].ParameterName + ", Supplier_ID = " +
                        sqlParam[3].ParameterName +
                        " Where No_Surat = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < sqlParam.Length; i++)
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
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

                    Logging.Error("SqlSuratRepository.cs - EditSurat() " + e.Message);
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
