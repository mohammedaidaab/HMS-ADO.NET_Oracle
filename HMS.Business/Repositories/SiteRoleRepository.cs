//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
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
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_InsertRole";


                        OracleParameter R_Name = new OracleParameter { ParameterName = "R_Name", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = role.Name };
                        OracleParameter R_Nor_Name = new OracleParameter { ParameterName = "R_NormalizedName", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = role.NormalizedName };
                        OracleParameter R_Desc = new OracleParameter { ParameterName = "R_Description", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = role.Description };
                        OracleParameter Created = new OracleParameter { ParameterName = "Created", OracleDbType = OracleDbType.Date, Size = 255, Direction = ParameterDirection.Input, Value = DateTime.Now };

                        oracom.Parameters.Add(R_Name);
                        oracom.Parameters.Add(R_Nor_Name);
                        oracom.Parameters.Add(R_Desc);
                        oracom.Parameters.Add(Created);

                        await oracom.ExecuteNonQueryAsync(cancellationToken);

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
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_UpdateRole";

                        OracleParameter R_ID = new OracleParameter {ParameterName="R_id",OracleDbType=OracleDbType.Int32,Direction=ParameterDirection.Input,Value=role.Id };
                        OracleParameter R_Name = new OracleParameter {ParameterName= "R_Name", OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Input,Value=role.Name };
                        OracleParameter R_Nor_Name = new OracleParameter {ParameterName= "R_NormalizedName", OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Input,Value=role.NormalizedName };
                        OracleParameter R_Desc = new OracleParameter {ParameterName= "R_Description", OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Input,Value=role.Description };
                        
                        OracleParameter qres = new OracleParameter {ParameterName= "res", OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Output };

                        oracom.Parameters.Add(R_ID);
                        oracom.Parameters.Add (R_Name);
                        oracom.Parameters.Add(R_Nor_Name);
                        oracom.Parameters.Add(R_Desc);
                        oracom.Parameters.Add(qres);

                        await oracom.ExecuteNonQueryAsync(cancellationToken);

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
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_FindRoleByName";

                        OracleParameter NorRoleName = new OracleParameter {ParameterName= "NorRoleName",OracleDbType=OracleDbType.NVarchar2,Direction=ParameterDirection.Input, Value = normalizedRoleName };
                        OracleParameter res = new OracleParameter {ParameterName= "res",OracleDbType=OracleDbType.RefCursor,Direction=ParameterDirection.Output };

                        oracom.Parameters.Add(NorRoleName);
                        oracom.Parameters.Add(res);

						//oracom.Parameters.AddWithValue("@NormalizedRoleName", normalizedRoleName);

                        using (var rdr = await oracom.ExecuteReaderAsync(CommandBehavior.SingleRow))
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
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_FindRoleById";

                        OracleParameter role_id = new OracleParameter { ParameterName = "R_ID", OracleDbType = OracleDbType.NVarchar2, Direction = ParameterDirection.Input, Value = roleId };
                        OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output };

                        oracom.Parameters.Add(role_id);
                        oracom.Parameters.Add(res);

                        //oracom.Parameters.AddWithValue("@Id", roleId);

                        using (var rdr = await oracom.ExecuteReaderAsync(CommandBehavior.SingleRow, cancellationToken))
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
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_GetUserRolesByUserId";

                        OracleParameter U_Id = new OracleParameter { ParameterName= "U_Id", OracleDbType=OracleDbType.Int32,Direction=ParameterDirection.Input ,Value=user.Id};
                        OracleParameter res = new OracleParameter { ParameterName="res",OracleDbType=OracleDbType.RefCursor,Direction=ParameterDirection.Output };

                        oracom.Parameters.Add(U_Id);
                        oracom.Parameters.Add(res);

                       // oracom.Parameters.AddWithValue("@UserId", user.Id);

                        OracleDataReader rdr = oracom.ExecuteReader();

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

            using (OracleConnection oraccon = new OracleConnection(con))
            {
                
                OracleCommand oracom = new OracleCommand("identity_GetAllRoles", oraccon);
                oracom.CommandType = CommandType.StoredProcedure;
                OracleParameter res = new OracleParameter
                {
                    ParameterName = "res",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output,
                };
                oracom.Parameters.Add(res);

                oraccon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
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

                oraccon.Close();

                return roles;
            }
		}

		public async Task<SiteRole> GetRolesByUserId(SiteUser user, CancellationToken cancellationToken)
		{
			// IUserStore requires a IQueryable list
			 SiteRole  role = new SiteRole();

			try
			{
				using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
				{
					await oracon.OpenAsync(cancellationToken);

					using (OracleCommand oracom = new OracleCommand())
					{
                        oracom.Connection = oracon;
						oracom.CommandType = CommandType.StoredProcedure;
						oracom.CommandText = "identity_GetUserRoles";

                        OracleParameter user_id = new OracleParameter { ParameterName = "U_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input , Value= user.Id };
                        OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };
                        
                        oracom.Parameters.Add(user_id); 
                        oracom.Parameters.Add(res);

                        //oracom.Parameters.AddWithValue("@UserId", user.Id);

						OracleDataReader rdr = oracom.ExecuteReader();

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
