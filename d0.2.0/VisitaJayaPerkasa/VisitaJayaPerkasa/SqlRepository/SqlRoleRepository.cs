using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VisitaJayaPerkasa.Entities;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlRoleRepository
    {
        public List<Role> GetRoles() {
            List<Role> listRole = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    Constant.VisitaJayaPerkasaApplication.anyConnection = false;
                    con.Open();
                    Constant.VisitaJayaPerkasaApplication.anyConnection = true;

                    using (SqlCommand command = new SqlCommand(
                        "SELECT role_id, role_name FROM [ROLE] WHERE deleted is null OR deleted = '0'"
                        , con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Role role = new Role();
                            role.ID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                            role.RoleName = reader.GetString(1);

                            if (listRole == null)
                                listRole = new List<Role>();

                            listRole.Add(role);
                            role = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("SqlRoleRepository.cs - GetRoles() " + e.Message);
            }

            return listRole;
        }
    }
}
