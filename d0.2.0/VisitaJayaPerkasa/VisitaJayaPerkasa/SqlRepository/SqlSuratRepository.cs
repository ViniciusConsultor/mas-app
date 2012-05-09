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
        public List<Surat> ListSurat()
        {
            List<Surat> listSurat = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "Select No_Surat, Tgl, Customer_id, Supplier_id From [Surat]", con))
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
    }
}
