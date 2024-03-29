﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;
using System.Data;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlCustomerTransRepository
    {
        public List<CustomerTrans> ListCustomerTrans()
        {
            List<CustomerTrans> listCustomerTrans = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "Select u.id, u.customer_id, u.tgl_transaksi, ur.customer_name FROM [Customer_Trans] u JOIN [Customer] ur " +
                        "ON (u.deleted is null OR u.deleted = '0') AND (ur.deleted is null OR ur.deleted = '0') AND u.customer_id = ur.customer_id", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTrans customerTrans = new CustomerTrans();
                            customerTrans.CustomerTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customerTrans.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customerTrans.TransDate = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(2);
                            customerTrans.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            

                            if (listCustomerTrans == null)
                                listCustomerTrans = new List<CustomerTrans>();

                            listCustomerTrans.Add(customerTrans);
                            customerTrans = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - listCustomerTrans() " + e.Message);
            }

            return listCustomerTrans;
        }

        public List<CustomerTrans> ListCustomerTrans(Guid customerID)
        {
            List<CustomerTrans> listCustomerTrans = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "Select u.id, u.customer_id, u.tgl_transaksi, ur.customer_name FROM [Customer_Trans] u JOIN [Customer] ur " +
                        "ON (u.deleted is null OR u.deleted = '0') AND (ur.deleted is null OR ur.deleted = '0') AND u.customer_id = ur.customer_id " +
                        "WHERE u.customer_id = '" + customerID.ToString() + "'", con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTrans customerTrans = new CustomerTrans();
                            customerTrans.CustomerTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customerTrans.CustomerID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customerTrans.TransDate = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(2);
                            customerTrans.CustomerName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);


                            if (listCustomerTrans == null)
                                listCustomerTrans = new List<CustomerTrans>();

                            listCustomerTrans.Add(customerTrans);
                            customerTrans = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - listCustomerTrans(Guid) " + e.Message);
            }

            return listCustomerTrans;
        }

        public List<CustomerTransDetailSimplified> ListCustomerTransDetailSimplified(Guid customerTransID)
        {
            List<CustomerTransDetailSimplified> listCustomerTransDetails = new List<CustomerTransDetailSimplified>();

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    string query = "Select ctd.id, tc.type_name, pd.vessel_name, co.city_name, cd.city_name, " +
                        "       cnd.condition_name, w.address, r.recipient_name " +
                        "FROM [Customer_Trans_Detail] ctd " +
                        "LEFT OUTER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "LEFT OUTER JOIN [Pelayaran_Detail] pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                        "LEFT OUTER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "LEFT OUTER JOIN [City] co ON co.city_id = ctd.origin " +
                        "LEFT OUTER JOIN [City] cd ON cd.city_id = ctd.destination " +
                        "LEFT OUTER JOIN [Condition] cnd ON cnd.condition_id = ctd.condition_id " +
                        "LEFT OUTER JOIN [Warehouse] w ON w.stuffing_place_id = ctd.stuffing_place " +
                        "LEFT OUTER JOIN [RECIPIENT] r ON r.recipient_id = ctd.recipient_id " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.customer_trans_id = '" + customerTransID.ToString() + "'";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTransDetailSimplified ctds = new CustomerTransDetailSimplified();
                            ctds.CustomerDetailTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            ctds.TypeName = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            ctds.VesselName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                            ctds.OriginName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                            ctds.DestinationName = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                            ctds.ConditionName = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                            ctds.WarehouseName = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? null : reader.GetString(6);
                            ctds.RecipientName = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);

                            listCustomerTransDetails.Add(ctds);
                            ctds = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - ListCustomerTransDetailSimplified() " + e.Message);
            }

            return listCustomerTransDetails;
        }

        public bool DeleteCustomerTrans(SqlParameter[] sqlParam)
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
                        "Update [Customer_Trans_Detail] set deleted = '1' WHERE customer_trans_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        using (SqlCommand subCommand = new SqlCommand(
                            "Update [Customer_Trans] set deleted = '1' WHERE id = " + sqlParam[0].ParameterName, con
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

                    Logging.Error("SqlCustomerTransRepository.cs - DeleteCustomerTrans() " + e.Message);
                }
                finally
                {
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }

        public DataTable ReportCustomerTransDetail(Guid ID)
        {
            DataTable dt = null;
            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            try
            {
                Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                con.Open();
                Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                string strQry = "Select tc.type_name, pd.vessel_name, co.city_name as origin, cd.city_name as destination, " +
                        "cnd.condition_name as condition, ctd.no_seal, ctd.truck_number, ctd.voy, " +
                        "ctd.stuffing_date, ctd.stuffing_place, ctd.etd, ctd.td, ctd.eta, ctd.ta, ctd.unloading, ctd.price, " +
                         "r.recipient_name, ctd.jenis_barang, ctd.no_container, ctd.quantity " +
                        "ctd.sj1, ctd.sj2, ctd.sj3, ctd.sj4, ctd.sj5, ctd.sj6, ctd.sj7, ctd.sj8, ctd.sj9, ctd.sj10, " +
                        "ctd.sj11, ctd.sj12, ctd.sj13, ctd.sj14, ctd.sj15, ctd.sj16, ctd.sj17, ctd.sj18, ctd.sj19, ctd.sj20, " +
                        "ctd.sj21, ctd.sj22, ctd.sj23, ctd.sj24, ctd.sj25, ctd.terima_toko, ctd.keterangan, ctd.no_ba " +
                        "FROM [Customer_Trans_Detail] ctd " +
                        "INNER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "INNER JOIN [Pelayaran_Detail] pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                        "INNER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "INNER JOIN [City] co ON co.city_id = ctd.origin " +
                        "INNER JOIN [City] cd ON cd.city_id = ctd.destination " +
                        "INNER JOIN [Condition] cnd ON cnd.condition_id = ctd.condition_id " +
                        "INNER JOIN [RECIPIENT] r ON r.recipient_id = ctd.recipient_id " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.customer_trans_id = '" + ID + "'";
                SqlDataAdapter da = new SqlDataAdapter(strQry, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
                ShippingMainDataSet ds = new ShippingMainDataSet();
                da.Fill(ds, "REPORT_CUST");
                dt = ds.Tables[3];
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - ReportCustomerTransDetail() " + e.Message);
            }
            con.Close();
            return dt;
        }

        public List<CustomerTransDetail> ListCustomerTransDetail(Guid ID)
        {
            List<CustomerTransDetail> listCustomerTransDetail = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    string query =
                        "Select ctd.id, ctd.customer_trans_id, ctd.type_id, ctd.pelayaran_detail_id, ctd.origin, " +
                        "ctd.destination, ctd.condition_id, ctd.no_seal, ctd.truck_number, ctd.voy, " +
                        "ctd.stuffing_date, ctd.stuffing_place, ctd.etd, ctd.td, ctd.eta, ctd.ta, ctd.unloading, ctd.price, " +
                        "tc.type_name, (SELECT TOP 1 supplier_name FROM [SUPPLIER] s WHERE s.supplier_id = p.supplier_id) as pelayaran_name, " +
                        "pd.vessel_name, co.city_name as origin, cd.city_name as destination, " +
                        "cnd.condition_name as condition, pd.status_pinjaman, w.[address], " +
                        "ctd.recipient_id, r.recipient_name, ctd.jenis_barang, ctd.no_container, ctd.quantity, " +
                        "ctd.sj1, ctd.sj2, ctd.sj3, ctd.sj4, ctd.sj5, ctd.sj6, ctd.sj7, ctd.sj8, ctd.sj9, ctd.sj10, " +
                        "ctd.sj11, ctd.sj12, ctd.sj13, ctd.sj14, ctd.sj15, ctd.sj16, ctd.sj17, ctd.sj18, ctd.sj19, ctd.sj20, " +
                        "ctd.sj21, ctd.sj22, ctd.sj23, ctd.sj24, ctd.sj25, ctd.terima_toko, ctd.keterangan, ctd.no_ba " +
                        "FROM [Customer_Trans_Detail] ctd " +
                        "LEFT OUTER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "LEFT OUTER JOIN [Pelayaran_Detail] pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                        "LEFT OUTER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "LEFT OUTER JOIN [City] co ON co.city_id = ctd.origin " +
                        "LEFT OUTER JOIN [City] cd ON cd.city_id = ctd.destination " +
                        "LEFT OUTER JOIN [Condition] cnd ON cnd.condition_id = ctd.condition_id " +
                        "LEFT OUTER JOIN [Warehouse] w ON w.stuffing_place_id = ctd.stuffing_place " +
                        "LEFT OUTER JOIN [RECIPIENT] r ON r.recipient_id = ctd.recipient_id " +
                        "Where (ctd.deleted is null OR ctd.deleted = '0') " +
                        "AND ctd.customer_trans_id = '" + ID + "'";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CustomerTransDetail customerTransDetail = new CustomerTransDetail();
                            customerTransDetail.CustomerDetailTransID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            customerTransDetail.CustomerTransID = Utility.Utility.ConvertToUUID(reader.GetValue(1).ToString());
                            customerTransDetail.TypeID = Utility.Utility.ConvertToUUID(reader.GetValue(2).ToString());
                            customerTransDetail.PelayaranDetailID = Utility.Utility.ConvertToUUID(reader.GetValue(3).ToString());
                            customerTransDetail.Origin = Utility.Utility.ConvertToUUID(reader.GetValue(4).ToString());
                            customerTransDetail.Destination = Utility.Utility.ConvertToUUID(reader.GetValue(5).ToString()); ;
                            customerTransDetail.ConditionID = Utility.Utility.ConvertToUUID(reader.GetValue(6).ToString()); ;
                            customerTransDetail.NoSeal = reader.GetString(7);
                            customerTransDetail.TruckNo = reader.GetString(8);
                            customerTransDetail.Voyage = reader.GetString(9);
                            customerTransDetail.StuffingDate = reader.GetDateTime(10);
                            customerTransDetail.StuffingPlace = Utility.Utility.ConvertToUUID(reader.GetValue(11).ToString());
                            customerTransDetail.ETD = reader.GetDateTime(12);
                            customerTransDetail.TD = reader.GetDateTime(13);
                            customerTransDetail.ETA = reader.GetDateTime(14);
                            customerTransDetail.TA = reader.GetDateTime(15);
                            customerTransDetail.Unloading = reader.GetDateTime(16);
                            customerTransDetail.Price = reader.GetDecimal(17);

                            customerTransDetail.TypeName = reader.GetString(18);
                            bool isLoan = false;
                            string vesselName = "";
                            string pelayaranName = "";
                            if (reader.GetValue(24) != DBNull.Value)
                                isLoan = reader.GetBoolean(24);
                            if (reader.GetValue(20) != DBNull.Value)
                                vesselName = reader.GetString(20);
                            if (reader.GetValue(19) != DBNull.Value)
                                pelayaranName = reader.GetString(19);
                            if (isLoan)
                            {
                                customerTransDetail.VesselName = vesselName + " - " + pelayaranName + " [loan]";
                            }
                            else
                                customerTransDetail.VesselName = vesselName + " - " + pelayaranName;

                            customerTransDetail.OriginName = reader.GetString(21);
                            customerTransDetail.DestinationName = reader.GetString(22);
                            customerTransDetail.ConditionName = reader.GetString(23);
                            customerTransDetail.WarehouseName = reader.GetString(25);

                            customerTransDetail.RecipientID = Utility.Utility.ConvertToUUID(reader.GetValue(26).ToString());
                            customerTransDetail.RecipientName = reader.GetString(27);
                            customerTransDetail.JenisBarang = Utility.Utility.IsDBNull(reader.GetValue(28)) ? null : reader.GetString(28);
                            customerTransDetail.NoContainer = Utility.Utility.IsDBNull(reader.GetValue(29)) ? null : reader.GetString(29);
                            customerTransDetail.Quantity = Utility.Utility.IsDBNull(reader.GetValue(30)) ? null : reader.GetString(30);
                            customerTransDetail.Sj1 = Utility.Utility.IsDBNull(reader.GetValue(31)) ? null : reader.GetString(31);
                            customerTransDetail.Sj2 = Utility.Utility.IsDBNull(reader.GetValue(32)) ? null : reader.GetString(32);
                            customerTransDetail.Sj3 = Utility.Utility.IsDBNull(reader.GetValue(33)) ? null : reader.GetString(33);
                            customerTransDetail.Sj4 = Utility.Utility.IsDBNull(reader.GetValue(34)) ? null : reader.GetString(34);
                            customerTransDetail.Sj5 = Utility.Utility.IsDBNull(reader.GetValue(35)) ? null : reader.GetString(35);
                            customerTransDetail.Sj6 = Utility.Utility.IsDBNull(reader.GetValue(36)) ? null : reader.GetString(36);
                            customerTransDetail.Sj7 = Utility.Utility.IsDBNull(reader.GetValue(37)) ? null : reader.GetString(37);
                            customerTransDetail.Sj8 = Utility.Utility.IsDBNull(reader.GetValue(38)) ? null : reader.GetString(38);
                            customerTransDetail.Sj9 = Utility.Utility.IsDBNull(reader.GetValue(39)) ? null : reader.GetString(39);
                            customerTransDetail.Sj10 = Utility.Utility.IsDBNull(reader.GetValue(40)) ? null : reader.GetString(40);
                            customerTransDetail.Sj11 = Utility.Utility.IsDBNull(reader.GetValue(41)) ? null : reader.GetString(41);
                            customerTransDetail.Sj12 = Utility.Utility.IsDBNull(reader.GetValue(42)) ? null : reader.GetString(42);
                            customerTransDetail.Sj13 = Utility.Utility.IsDBNull(reader.GetValue(43)) ? null : reader.GetString(43);
                            customerTransDetail.Sj14 = Utility.Utility.IsDBNull(reader.GetValue(44)) ? null : reader.GetString(44);
                            customerTransDetail.Sj15 = Utility.Utility.IsDBNull(reader.GetValue(45)) ? null : reader.GetString(45);
                            customerTransDetail.Sj16 = Utility.Utility.IsDBNull(reader.GetValue(46)) ? null : reader.GetString(46);
                            customerTransDetail.Sj17 = Utility.Utility.IsDBNull(reader.GetValue(47)) ? null : reader.GetString(47);
                            customerTransDetail.Sj18 = Utility.Utility.IsDBNull(reader.GetValue(48)) ? null : reader.GetString(48);
                            customerTransDetail.Sj19 = Utility.Utility.IsDBNull(reader.GetValue(49)) ? null : reader.GetString(49);
                            customerTransDetail.Sj20 = Utility.Utility.IsDBNull(reader.GetValue(50)) ? null : reader.GetString(50);
                            customerTransDetail.Sj21 = Utility.Utility.IsDBNull(reader.GetValue(51)) ? null : reader.GetString(51);
                            customerTransDetail.Sj22 = Utility.Utility.IsDBNull(reader.GetValue(52)) ? null : reader.GetString(52);
                            customerTransDetail.Sj23 = Utility.Utility.IsDBNull(reader.GetValue(53)) ? null : reader.GetString(53);
                            customerTransDetail.Sj24 = Utility.Utility.IsDBNull(reader.GetValue(54)) ? null : reader.GetString(54);
                            customerTransDetail.Sj25 = Utility.Utility.IsDBNull(reader.GetValue(55)) ? null : reader.GetString(55);
                            customerTransDetail.TerimaToko = Utility.Utility.IsDBNull(reader.GetValue(56)) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(56);
                            customerTransDetail.Keterangan = Utility.Utility.IsDBNull(reader.GetValue(57)) ? null : reader.GetString(57);
                            customerTransDetail.NoBA = Utility.Utility.IsDBNull(reader.GetValue(58)) ? null : reader.GetString(58);

                            if (listCustomerTransDetail == null)
                                listCustomerTransDetail = new List<CustomerTransDetail>();

                            listCustomerTransDetail.Add(customerTransDetail);
                            customerTransDetail = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - ListCustomerTransDetail() " + e.Message);
            }

            return listCustomerTransDetail;
        }

        public bool CreateCustomerTrans(SqlParameter[] sqlParamInsert, SqlParameter[] sqlParamMaster)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                con.Open();
                Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                sqlTransaction = con.BeginTransaction();
                try
                {

                    using (SqlCommand masterCommand = new SqlCommand(
                        "INSERT INTO [customer_trans] VALUES(" + sqlParamMaster[0].ParameterName + ", " + 
                        sqlParamMaster[1].ParameterName + ", " + 
                        sqlParamMaster[2].ParameterName + ", " + 
                        sqlParamMaster[3].ParameterName + 
                        ")", 
                        con
                        )) 
                        {
                            masterCommand.Transaction = sqlTransaction;

                            for (int i = 0; i < sqlParamMaster.Length; i++)
                                masterCommand.Parameters.Add(sqlParamMaster[i]);
                            n = masterCommand.ExecuteNonQuery();
                    
                            if(n > 0){
                                int k = 0;
                                int value = 0;
                                for(int i=0; i<sqlParamInsert.Length/51; i++){
                                    n = 0;
                                    using (SqlCommand command = new SqlCommand(
                                        "Insert into [Customer_Trans_Detail] values (" +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName +
                                        ")", con))
                                    {
                                        command.Transaction = sqlTransaction;

                                        for (int j = 0; j < 51; j++)
                                            command.Parameters.Add(sqlParamInsert[value++]);
                                        n = command.ExecuteNonQuery();
                                        command.Parameters.Clear();

                                        if(n == 0)
                                            break;
                                    }
                                }
                            }
                        }

                    }
                catch (Exception e)
                {
                    //if (sqlTransaction != null)
                    //    sqlTransaction.Rollback();

                    Logging.Error("SqlCustomerTransRepository.cs - CreateCustomerTrans() " + e.Message);
                }
                finally
                {
                    if (n > 0)
                        sqlTransaction.Commit();
                    else if (sqlTransaction != null)
                        sqlTransaction.Rollback();
                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }


        public bool EditCustomerTrans(SqlParameter[] sqlParamInsert, SqlParameter[] sqlParamMaster)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
            {
                Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                con.Open();
                Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                sqlTransaction = con.BeginTransaction();
                
                try
                {
                    using (SqlCommand deletedCommand = new SqlCommand(
                        "Delete [Customer_Trans_Detail] WHERE customer_trans_id = " + sqlParamMaster[0].ParameterName,
                        con
                        ))
                    {
                        deletedCommand.Transaction = sqlTransaction;
                        deletedCommand.Parameters.Add(sqlParamMaster[0]);
                        n = deletedCommand.ExecuteNonQuery();
                        deletedCommand.Parameters.Clear();

                        if (n >= 0)
                        {
                            n = 0;

                            using (SqlCommand masterCommand = new SqlCommand(
                                "Update [customer_trans] set customer_id = " + sqlParamMaster[1].ParameterName + " WHERE id = " + sqlParamMaster[0].ParameterName,
                                con
                                ))
                            {
                                masterCommand.Transaction = sqlTransaction;
                                masterCommand.Parameters.Add(sqlParamMaster[1]);
                                masterCommand.Parameters.Add(sqlParamMaster[0]);
                                n = masterCommand.ExecuteNonQuery();

                                if (n > 0)
                                {
                                    int k = 0;
                                    int value = 0;
                                    for (int i = 0; i < sqlParamInsert.Length / 51; i++)
                                    {
                                        n = 0;
                                        using (SqlCommand command = new SqlCommand(
                                            "Insert into [Customer_Trans_Detail] values (" +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName + ", " +
                                        sqlParamInsert[k++].ParameterName +
                                            ")", con))
                                        {
                                            command.Transaction = sqlTransaction;

                                            for (int j = 0; j < 51; j++)
                                                command.Parameters.Add(sqlParamInsert[value++]);
                                            n = command.ExecuteNonQuery();
                                            command.Parameters.Clear();

                                            if (n == 0)
                                                break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.Error("SqlCustomerTransRepository.cs - EditCustomerTrans() " + e.Message);
                }
                finally
                {
                    if (n > 0)
                        sqlTransaction.Commit();
                    else if (sqlTransaction != null)
                        sqlTransaction.Rollback();

                    sqlTransaction.Dispose();
                }
            }

            return n > 0;
        }
        
        public DataTable ReportInvoice(Guid ID)
        {
            DataTable dt = null;
            SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
            try
            {
                Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                con.Open();
                Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                string strQry = "Select ctd.no_ba, ctd.no_container, tc.type_name, pd.vessel_name, ctd.ta, ctd.terima_toko, cd.city_name, ctd.price " +
                        "FROM [Customer_Trans_Detail] ctd " +
                        "INNER JOIN [Type_Cont] tc ON tc.type_id = ctd.type_id " +
                        "INNER JOIN [Pelayaran_Detail] pd ON pd.pelayaran_detail_id = ctd.pelayaran_detail_id " +
                        "INNER JOIN [Pelayaran] p ON p.pelayaran_id = pd.pelayaran_id " +
                        "INNER JOIN [City] cd ON cd.city_id = ctd.destination " +
                        "Where ctd.customer_trans_id = '" + ID + "'";
                SqlDataAdapter da = new SqlDataAdapter(strQry, VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString);
                ShippingMainDataSet ds = new ShippingMainDataSet();
                da.Fill(ds, "INVOICE_NONPPN");
                dt = ds.Tables[5];
            }
            catch (Exception e)
            {
                Logging.Error("SqlCustomerTransRepository.cs - ReportCustomerTransDetail() " + e.Message);
            }
            con.Close();
            return dt;
        }
    }
}
