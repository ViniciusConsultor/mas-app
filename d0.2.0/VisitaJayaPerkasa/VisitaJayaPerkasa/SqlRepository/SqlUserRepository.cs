using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VisitaJayaPerkasa.Entities;
using VisitaJayaPerkasa.Utility.Log;

namespace VisitaJayaPerkasa.SqlRepository
{
    public class SqlUserRepository
    {
        public List<User> GetUsers() {
            List<User> listUser = null;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString)) {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT person_id, username, first_name, last_name, email, address, " + 
                        "date_of_birth, gender, mobile_phone_number FROM [USER] WHERE (deleted is null OR deleted = '0') " + 
                        "ORDER BY first_name ASC, last_name ASC"
                        , con)) {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read()) {
                                User user = new User();
                                user.PersonID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                                user.UserName = reader.GetString(1);

                                user.FirstName = (Utility.Utility.IsDBNull(reader.GetValue(2))) ? null : reader.GetString(2);
                                user.LastName = (Utility.Utility.IsDBNull(reader.GetValue(3))) ? null : reader.GetString(3);
                                user.email = (Utility.Utility.IsDBNull(reader.GetValue(4))) ? null : reader.GetString(4);
                                user.Address = (Utility.Utility.IsDBNull(reader.GetValue(5))) ? null : reader.GetString(5);
                                user.DateOfBirth = (Utility.Utility.IsDBNull(reader.GetValue(6))) ? Utility.Utility.DefaultDateTime() : reader.GetDateTime(6);
                                user.Gender = (Utility.Utility.IsDBNull(reader.GetValue(7))) ? null : reader.GetString(7);
                                user.MobilePhoneNumber = (Utility.Utility.IsDBNull(reader.GetValue(8))) ? null : reader.GetString(8);

                                if (listUser == null)
                                    listUser = new List<User>();

                                listUser.Add(user);
                                user = null;
                            }
                    }
                }
            }
            catch (Exception e) {
                Logging.Error("SqlUserRepository.cs - GetUsers() " + e.Message);
            }

            return listUser;
        }

        public bool DeleteUser(SqlParameter[] sqlParam) {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null ;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Update [User] set deleted = '1' WHERE person_id = " + sqlParam[0].ParameterName, con))
                    {
                        command.Transaction = sqlTransaction;
                        command.Parameters.Add(sqlParam[0]);
                        n = command.ExecuteNonQuery();

                        if (n > 0)
                        {
                            n = 0;

                            using (SqlCommand subCommand = new SqlCommand(
                                "Update [User_Role] set deleted = '1' WHERE user_id = " + sqlParam[0].ParameterName
                                , con))
                            {
                                subCommand.Transaction = sqlTransaction;
                                subCommand.Parameters.Add(sqlParam[0]);
                                n = subCommand.ExecuteNonQuery();
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

                Logging.Error("SqlUserRepository.cs - DeleteUser() " + e.Message);
            }
            finally {
                sqlTransaction.Dispose();
            }
            
            return n > 0;
        }

        public bool CreateUser(SqlParameter[] sqlParam)
        {
            int n = 0;
            SqlConnection con;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();
                    sqlTransaction = con.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(
                        "Insert into [User] values (" +
                        sqlParam[0].ParameterName + ", " + 
                        sqlParam[1].ParameterName + ", " + 
                        sqlParam[2].ParameterName + ", " + 
                        sqlParam[3].ParameterName + ", " + 
                        sqlParam[4].ParameterName + ", " + 
                        sqlParam[5].ParameterName + ", " + 
                        sqlParam[6].ParameterName + ", " + 
                        sqlParam[7].ParameterName + ", " + 
                        sqlParam[8].ParameterName + ", " + 
                        sqlParam[9].ParameterName + ", " + 
                        sqlParam[10].ParameterName + ", " + 
                        sqlParam[11].ParameterName + ", " + 
                        sqlParam[12].ParameterName + 
                        ")", con))
                    {
                        command.Transaction = sqlTransaction;

                        for (int i = 0; i < 13; i++ )
                            command.Parameters.Add(sqlParam[i]);
                        n = command.ExecuteNonQuery();
                        
                        if (n > 0)
                        {
                            n = 0;

                            using (SqlCommand subCommand = new SqlCommand(
                                "Insert Into [User_Role] Values (" + 
                                sqlParam[13].ParameterName + ", " +
                                sqlParam[14].ParameterName + ", " +
                                sqlParam[15].ParameterName + ", " +
                                sqlParam[16].ParameterName + 
                                ")"
                                , con))
                            {
                                subCommand.Transaction = sqlTransaction;

                                for (int i = 13; i < sqlParam.Length; i++ )
                                    subCommand.Parameters.Add(sqlParam[i]);
        
                                n = subCommand.ExecuteNonQuery();
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

                Logging.Error("SqlUserRepository.cs - CreateUser() " + e.Message);
            }
            finally
            {
                sqlTransaction.Dispose();
            }

            return n > 0;
        }

        public void ValidateLogin(SqlParameter[] sqlParam) {
            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 username, role_name, first_name, last_name FROM [user] u JOIN [user_role] ur " +
                        "ON (u.deleted is null OR u.deleted = '0') AND (ur.deleted is null OR ur.deleted = '0') AND u.person_id = ur.user_id AND u.username = " + sqlParam[0].ParameterName + " " +
                        "AND u.password = " + sqlParam[1].ParameterName + " JOIN role r ON r.role_id = ur.role_id", con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        User user = null;
                        while (reader.Read())
                        {
                            user = new User();
                            user.UserName = reader.GetString(0);

                            user.RoleObj = new Role();
                            user.RoleObj.RoleName = reader.GetString(1);
                            user.FirstName = reader.GetString(2);
                            user.LastName = reader.GetString(3);
                        }

                        if (user != null)
                        {
                            UserProfile.user = user;
                            user = null;
                        }
                    }
                }
            }
            catch(Exception e){
                Logging.Error("SqlUserRepository.cs - ValidateLogin() " + e.Message);
            }
        }

        public bool CheckUserName(SqlParameter[] sqlParam)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 username FROM [USER] WHERE username = " + sqlParam[0].ParameterName, con))
                    {
                        foreach (SqlParameter tempSqlParam in sqlParam)
                            command.Parameters.Add(tempSqlParam);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlUserRepository.cs - CheckUserName() " + e.Message);
            }

            return exists;
        }

        public Guid GetUserRole(Guid ID) {
            Guid RoleID = Guid.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(VisitaJayaPerkasa.Constant.VisitaJayaPerkasaApplication.connectionString))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(
                        "SELECT TOP 1 role_id FROM [USER_ROLE] WHERE user_id = " + ID, con))
                    {  
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            RoleID = Utility.Utility.ConvertToUUID(reader.GetValue(0).ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Logging.Error("SqlUserRepository.cs - GetUserRole() " + e.Message);
            }

            return RoleID;
        }
    }
}
