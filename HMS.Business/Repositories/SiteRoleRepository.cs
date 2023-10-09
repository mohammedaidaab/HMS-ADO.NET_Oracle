//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Infrastructure.Repositories
{
    public class SiteRoleRepository : ISiteRoleRepository
    {
        private IConfiguration _config { get; set; }
        private string con; 

        public SiteRoleRepository(IConfiguration config)
        {
            _config = config;
            con = _config.GetConnectionString("DefaultConnection");

        }

        /// <summary>
        /// Create new Role
        /// Possbible check needed to see if exists?
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_InsertRole";

                        cmd.Parameters.AddWithValue("@Name", role.Name);
                        cmd.Parameters.AddWithValue("@NormalizedName", role.NormalizedName);
                        cmd.Parameters.AddWithValue("@Description", role.Description ?? "");
                        cmd.Parameters.AddWithValue("@Created", DateTime.Now);

                        await cmd.ExecuteNonQueryAsync(cancellationToken);

                        return IdentityResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }

        /// <summary>
        /// Update existing Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateAsync(SiteRole role, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_UpdateRole";

                        cmd.Parameters.AddWithValue("@Id", role.Id);
                        cmd.Parameters.AddWithValue("@Name", role.Name);
                        cmd.Parameters.AddWithValue("@NormalizedName", role.NormalizedName);
                        cmd.Parameters.AddWithValue("@Description", role.Description ?? "");

                        await cmd.ExecuteNonQueryAsync(cancellationToken);

                        return IdentityResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAsync(SiteRole role, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_DeleteRole";

                        cmd.Parameters.AddWithValue("@Id", role.Id);

                        await cmd.ExecuteNonQueryAsync(cancellationToken);

                        return IdentityResult.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });
            }
        }

        /// <summary>
        /// Retrieve role by the supplied name
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<SiteRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            try
            {
                var role = new SiteRole();
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindRoleByName";

                        cmd.Parameters.AddWithValue("@NormalizedRoleName", normalizedRoleName);

                        using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (rdr.Read())
                            {
                                role = new SiteRole
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Name = rdr["Name"].ToString(),
                                    NormalizedName = rdr["NormalizedName"].ToString(),
                                    Description = rdr["Description"].ToString(),
                                    Created = DateTime.Parse(rdr["Created"].ToString())
                                };
                            }
                            return role;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieve role by the supplied Id
        /// </summary>
        /// <param name="normalizedRoleName"></param>
        /// <returns>SiteRole</returns>
        public async Task<SiteRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            try
            {
                var role = new SiteRole();
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindRoleById";

                        cmd.Parameters.AddWithValue("@Id", roleId);

                        using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken))
                        {
                            if (rdr.Read())
                            {
                                role = new SiteRole
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Name = rdr["Name"].ToString(),
                                    NormalizedName = rdr["NormalizedName"].ToString(),
                                    Description = rdr["Description"].ToString(),
                                    Created = DateTime.Parse(rdr["Created"].ToString())
                                };
                            }
                            return role;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of roles the user is in as strings
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Roles</returns>
        public async Task<IList<string>> GetRolesByUserIdAsync(SiteUser user, CancellationToken cancellationToken)
        {
            // IUserStore requires a IQueryable list
            var list = new List<string>();

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_GetUserRolesByUserId";

                        cmd.Parameters.AddWithValue("@UserId", user.Id);

                        SqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            list.Add(rdr["Name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

		public async Task<IEnumerable<SiteRole>> GetAllRoles()
		{
		    List<SiteRole> roles = new List<SiteRole>();

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("identity_GetAllRoles", sqlcon);

                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    SiteRole role = new SiteRole
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
						Description = dr["Description"].ToString(),
                    };

                    roles.Add(role);
                }

                sqlcom.Clone();

                return roles;
            }
		}

		public async Task<SiteRole> GetRolesByUserId(SiteUser user, CancellationToken cancellationToken)
		{
			// IUserStore requires a IQueryable list
			 SiteRole  role = new SiteRole();

			try
			{
				using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
				{
					await conn.OpenAsync(cancellationToken);

					using (SqlCommand cmd = new SqlCommand())
					{
						cmd.Connection = conn;
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "identity_GetUserRoles";

						cmd.Parameters.AddWithValue("@UserId", user.Id);

						SqlDataReader rdr = cmd.ExecuteReader();

						while (rdr.Read())
						{
							role.Id = Convert.ToInt32(rdr["Id"]);
                            role.Name = (rdr["Name"].ToString());
                            role.Description = (rdr["Description"].ToString());
                            
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return role;
		}

	}
}
