using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlJenisBarangRepository
    {
        public List<JenisBarang> ListJenisBarang()
        {
            List<JenisBarang> listJenisBarang = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT jb.jenis_barang_id, jb.nama, jb.deskripsi " +
                        "FROM jenis_barang jb "
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            JenisBarang jenisbarang = new JenisBarang();
                            jenisbarang.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            jenisbarang.Nama = (Utility.Utility.IsDBNull(reader.GetValue(1))) ? null : reader.GetString(1);
                            jenisbarang.Deskripsi = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);

                            if (listJenisBarang == null)
                                listJenisBarang = new List<JenisBarang>();

                            listJenisBarang.Add(jenisbarang);
                            jenisbarang = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlJenisBarangRepository.cs - ListJenisBarang() " + e.Message);
            }

            return listJenisBarang;
        }

        public bool AddJenisBarang(string jenisBarang)
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
                        "INSERT INTO jenis_barang (jenis_barang_id, nama) VALUES ('"+ Guid.NewGuid() +"', '"+ jenisBarang +"')", con))
                    {
                        command.Transaction = sqlTransaction;
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

                    Logging.Error("SqlJenisBarangRepository.cs - AddJenisBarang() " + e.Message);
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
