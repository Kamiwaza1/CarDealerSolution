using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Model;

namespace CarDealer.Data
{
    public class UserRepository
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["CarDealerDb"].ConnectionString;

        // Authenticate user by email and password
        public User? AuthenticateUser(string email, string password)
     {
        using (SqlConnection conn = new SqlConnection(cs))
        {
     string query = @"SELECT [name], [email], [password] 
           FROM [dbo].[DesktopAuthentication] 
           WHERE [email] = @Email AND [password] = @Password";

      SqlCommand cmd = new SqlCommand(query, conn);
 cmd.Parameters.AddWithValue("@Email", email);
   cmd.Parameters.AddWithValue("@Password", password);
      conn.Open();

       using (SqlDataReader reader = cmd.ExecuteReader())
           {
                 if (reader.Read())
              {
 return new User
 {
          UserId = 0, // Not in database
         Username = reader.GetString(reader.GetOrdinal("email")), // Using email as username
         Password = reader.GetString(reader.GetOrdinal("password")),
             Email = reader.GetString(reader.GetOrdinal("email")),
           FirstName = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
        LastName = null,
   Role = "User",
 IsActive = true,
         CreatedDate = DateTime.Now,
       LastLoginDate = DateTime.Now
    };
          }
 }
            }
        return null;
        }

        // Get user by email
   public User? GetUserByEmail(string email)
    {
          using (SqlConnection conn = new SqlConnection(cs))
            {
           string query = @"SELECT [name], [email], [password] 
  FROM [dbo].[DesktopAuthentication] 
 WHERE [email] = @Email";

          SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@Email", email);
       conn.Open();

      using (SqlDataReader reader = cmd.ExecuteReader())
     {
              if (reader.Read())
         {
   return new User
                   {
            UserId = 0,
         Username = reader.GetString(reader.GetOrdinal("email")),
       Password = reader.GetString(reader.GetOrdinal("password")),
 Email = reader.GetString(reader.GetOrdinal("email")),
       FirstName = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
  LastName = null,
           Role = "User",
      IsActive = true,
          CreatedDate = DateTime.Now,
            LastLoginDate = DateTime.Now
                    };
           }
 }
 }
            return null;
 }

        // Get all users
        public List<User> GetAllUsers()
        {
   List<User> users = new List<User>();

         using (SqlConnection conn = new SqlConnection(cs))
   {
            string query = @"SELECT [name], [email], [password] 
   FROM [dbo].[DesktopAuthentication]";

       SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

using (SqlDataReader reader = cmd.ExecuteReader())
    {
     while (reader.Read())
         {
            users.Add(new User
        {
          UserId = 0,
      Username = reader.GetString(reader.GetOrdinal("email")),
       Password = reader.GetString(reader.GetOrdinal("password")),
 Email = reader.GetString(reader.GetOrdinal("email")),
               FirstName = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
    LastName = null,
      Role = "User",
               IsActive = true,
        CreatedDate = DateTime.Now,
    LastLoginDate = DateTime.Now
     });
    }
    }
 }
            return users;
        }

        // Update last login - does nothing since column doesn't exist
        public bool UpdateLastLogin(int userId)
        {
            // Column doesn't exist in table, return true to avoid errors
   return true;
        }

        // Add new user
      public int AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
    string query = @"INSERT INTO [dbo].[DesktopAuthentication] 
             ([name], [email], [password])
          VALUES (@Name, @Email, @Password)";

           SqlCommand cmd = new SqlCommand(query, conn);
 cmd.Parameters.AddWithValue("@Name", (object?)user.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
                cmd.Parameters.AddWithValue("@Password", user.Password);
 conn.Open();

     int rowsAffected = cmd.ExecuteNonQuery();
       return rowsAffected;
     }
        }

        // Update user
        public bool UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(cs))
        {
        string query = @"UPDATE [dbo].[DesktopAuthentication] 
      SET [name] = @Name, [password] = @Password
            WHERE [email] = @Email";

           SqlCommand cmd = new SqlCommand(query, conn);
      cmd.Parameters.AddWithValue("@Name", (object?)user.FirstName ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", user.Email ?? string.Empty);
      cmd.Parameters.AddWithValue("@Password", user.Password);
      conn.Open();

    int rowsAffected = cmd.ExecuteNonQuery();
         return rowsAffected > 0;
  }
        }

        // Delete user
        public bool DeleteUser(string email)
        {
       using (SqlConnection conn = new SqlConnection(cs))
    {
    string query = "DELETE FROM [dbo].[DesktopAuthentication] WHERE [email] = @Email";

                SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Email", email);
    conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
 return rowsAffected > 0;
            }
        }
    }
}
