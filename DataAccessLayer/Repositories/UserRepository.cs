using DataAccessLayer.Interface;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IGenericRepository<User>
    {
        readonly string connectionString = "Data Source=REVISION-PC; Initial Catalog=DigitalBank; Integrated Security=true; TrustServerCertificate=true;";
        public async Task<bool> Add(User entityModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_insert_user", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@name", entityModel.Name);
                        command.Parameters.AddWithValue("@birthdate", entityModel.Birthdate);
                        command.Parameters.AddWithValue("@gender", entityModel.Gender);

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_delete_user", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@userid", Guid.Parse(id));

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                List<User> users = new List<User>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_get_all_users", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                // Get user and from the ordinal index get the data
                                User user = new User
                                {
                                    UserId = reader.GetGuid(reader.GetOrdinal("userid")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Birthdate = reader.GetDateTime(reader.GetOrdinal("birthdate")),
                                    Gender = reader.GetString(reader.GetOrdinal("gender"))[0]
                                };

                                users.Add(user);
                            }
                        }
                    }
                }

                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                User user = null;

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_get_user_by_id", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter for the stored procedure
                        command.Parameters.AddWithValue("@userid", Guid.Parse(id));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Get user and from the ordinal index get the data
                                user = new User
                                {
                                    UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Birthdate = reader.GetDateTime(reader.GetOrdinal("Birthdate")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender"))[0] // Get the first character as a char
                                };
                            }
                        }
                    }
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(User entityModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_update_user", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@userid", entityModel.UserId);
                        command.Parameters.AddWithValue("@name", entityModel.Name);
                        command.Parameters.AddWithValue("@birthdate", entityModel.Birthdate);
                        command.Parameters.AddWithValue("@gender", entityModel.Gender);

                        // Execute the stored procedure
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
