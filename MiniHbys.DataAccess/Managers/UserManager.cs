using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class UserManager : IUserManager
{
    public void CreateUser(User user)
    {
        using(var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO [User] (UserEmail,Password,UserName,UserSurname,BirthDate,RoleID) VALUES
        (@UserEmail,@Password,@UserName,@UserSurname,@BirthDate,@RoleID)";
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@UserSurname", user.UserSurname);
                command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                command.Parameters.AddWithValue("@RoleID", user.RoleID);

                command.ExecuteNonQuery();
            }
        }
    }

    public User Login(string username, string password)
    {
        User user = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandString = @"SELECT * FROM [User] WHERE UserEmail = @Username AND Password = @Password";
            using (var command = new SqlCommand(commandString,connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User();
                    user.UserID = reader["UserID"].ToInt32();
                    user.UserSurname = reader["UserSurname"] != DBNull.Value
                        ? reader["UserSurname"].ToString()
                        : string.Empty;
                    user.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty;
                    user.UserEmail = reader["UserEmail"] != DBNull.Value
                        ? reader["UserEmail"].ToString()
                        : string.Empty;
                    user.RoleID = reader["RoleID"] != DBNull.Value ? reader["RoleID"].ToInt32() : default;
                    user.BirthDate = reader["Birthdate"] != DBNull.Value ? reader["Birthdate"].ToDateTime() : null;
                    user.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : string.Empty;
                    user.Role = new RoleManager().GetRoleById(user.RoleID);
                }
            }
        }
        return user;
    }

    public bool IsUserExist(string email)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM [User] WHERE Email = @Username";
            using (var command=new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@UserEmail", email);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                    return true;
            }
        }
        return false;
    }

    public void UpdateUser(User user)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE [User] SET 
                UserEmail = @UserEmail,
                Password  = @Password,
                UserName  = @UserName,
                UserSurname = @UserSurname,
                BirthDate = @BirthDate,
                RoleID = @RoleID
                WHERE UserID = @UserID";
            using (var command= new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@UserID", user.UserID);
                command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@UserSurname", user.UserSurname);
                command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                command.Parameters.AddWithValue("@RoleId", user.RoleID);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<User> GetAllUsers()
    {
        var users = new List<User>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM [User]";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var user = new User();
                    user.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : string.Empty;
                    user.BirthDate = reader["BirthDate"] != DBNull.Value
                        ? reader["BirthDate"].ToDateTime()
                        : DateTime.MinValue;
                    user.UserEmail = reader["UserEmail"] !=DBNull.Value ? reader["UserEmail"].ToString():string.Empty;
                    user.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty;
                    user.UserSurname = reader["UserSurname"] != DBNull.Value
                        ? reader["UserSurname"].ToString()
                        : string.Empty;
                    user.RoleID = reader["RoleID"] != DBNull.Value ? reader["RoleID"].ToInt32() : default;
                    user.UserID = reader["UserID"] != DBNull.Value ? reader["UserID"].ToInt32() : default;
                    user.Role = new RoleManager().GetRoleById(user.RoleID);
                    users.Add(user);
                }
            }
        }
        return users;
    }

    public User GetUserById(int userId)
    {
        User user = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM [User] WHERE UserID = @UserID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("UserID", userId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User();
                    user.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : string.Empty;
                    user.BirthDate = reader["BirthDate"] != DBNull.Value
                        ? reader["BirthDate"].ToDateTime()
                        : DateTime.MinValue;
                    user.UserEmail = reader["UserEmail"] !=DBNull.Value ? reader["UserEmail"].ToString():string.Empty;
                    user.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty;
                    user.UserSurname = reader["UserSurname"] != DBNull.Value
                        ? reader["UserSurname"].ToString()
                        : string.Empty;
                    user.RoleID = reader["RoleID"] != DBNull.Value ? reader["RoleID"].ToInt32() : default;
                    user.UserID = reader["UserID"] != DBNull.Value ? reader["UserID"].ToInt32() : default;
                    user.Role = new RoleManager().GetRoleById(user.RoleID);
                }
            }
        }

        return user;
    }

    public void DeleteUser(int userId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM [User] WHERE UserID = @UserID";
            using (var command = new SqlCommand(commandText,connection) )
            {
                command.Parameters.AddWithValue("@UserID", userId);
                command.ExecuteNonQuery();
            }
        }
    }
}