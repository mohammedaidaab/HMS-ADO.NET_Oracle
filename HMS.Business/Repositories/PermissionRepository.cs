////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
/////////////////////////////////////////////////////


using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Business.Repositories
{
	public class PermissionRepository : IPermissionRepository
    {
		private readonly ISiteRoleRepository _siteRoleRepository;
		private readonly ISiteUserRepository _userRepository;
		private readonly IConfiguration _configuration;
        private readonly string con;

        public PermissionRepository(IConfiguration configuration,
									ISiteRoleRepository siteRoleRepository,
									ISiteUserRepository siteUserRepository)

        {
            _configuration = configuration;
			_siteRoleRepository = siteRoleRepository;
			_userRepository = siteUserRepository;
            con = _configuration.GetConnectionString("DEfaultConnection");
        } 
                
        public async Task<IEnumerable<Permission>> GetAll()
        {
            using (SqlConnection sqlcon  = new SqlConnection(con))
            {
                List<Permission> permissions = new List<Permission>();

                SqlCommand sqlcom = new SqlCommand("Permission_get_all", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = sqlcom.ExecuteReader();

                while (dr.Read())
                {
                    Permission permission = new Permission();
                    permission.ID = Convert.ToInt32(dr["ID"]);
                    permission.Name = dr["Name"].ToString();

                    permissions.Add(permission);
                }

                return permissions;
            }
        }

        public async Task<List<Permission>> GetAllByRole(SiteRole role)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                List<Permission> RolePermissions = new List<Permission>();
                List<Permission> Permissions = new List<Permission>();


                SqlCommand sqlcom = new SqlCommand("Permission_get_all_By_Role", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("RoleId",role.Id);
                
                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();

                while (dr.Read())
                {
                    Permission RolePermission = new Permission();
                    RolePermission.ID = Convert.ToInt32(dr["RoleID"]);
                    RolePermission.Name = dr["PermissionID"].ToString();

                    RolePermissions.Add(RolePermission);
                }
                sqlcon.Close();

                using (SqlConnection sqlcon2 = new SqlConnection(con))
                {
                    foreach (var perm in RolePermissions)
                    {

                        SqlCommand sqlcom2 = new SqlCommand("Permission_get_by_Id", sqlcon);
                        sqlcom2.CommandType = CommandType.StoredProcedure;
                        sqlcom2.Parameters.AddWithValue("id", perm.Name);

                        sqlcon.Open();
                        SqlDataReader dr2 = sqlcom2.ExecuteReader();

                        while (dr2.Read())
                        {
                            Permission Perms = new Permission();
                            Perms.ID = Convert.ToInt32(dr2["ID"]);
                            Perms.Name = dr2["Name"].ToString();

                            Permissions.Add(Perms);
                        }
                        sqlcon.Close();

                    }
                }

                return Permissions;
            }
        }

		public async Task<List<Permission>> GetByName(string[] permissions)
		{
			List<Permission> RolePermissions = new List<Permission>();

			using (SqlConnection sqlcon = new SqlConnection(con))
			{
				foreach (var perm in permissions)
				{
					SqlCommand sqlcom = new SqlCommand("Permissions_getByName", sqlcon);

					sqlcom.CommandType = CommandType.StoredProcedure;
					sqlcom.Parameters.AddWithValue("PermissionName", perm);
					sqlcon.Open();
					SqlDataReader dr = sqlcom.ExecuteReader();
					while (dr.Read())
					{
						Permission Permission = new Permission();
						Permission.ID = Convert.ToInt32(dr["ID"]);
						Permission.Name = dr["Name"].ToString();
						RolePermissions.Add(Permission);
					}
					sqlcon.Close();
				}

				return RolePermissions;

			}
		}


		public async Task<BaseResponse> update(List<Permission> permission, int roleId)
		{
			using (SqlConnection sqlcon = new SqlConnection(con))
			{
				SqlCommand sqlcom = new SqlCommand("permission_reset", sqlcon);

				sqlcom.CommandType = CommandType.StoredProcedure;
				sqlcom.Parameters.AddWithValue("@RoleID", roleId);
				sqlcon.Open();
				sqlcom.ExecuteNonQuery();
				sqlcon.Close();

				try
				{
					foreach (var perm in permission)
					{
						SqlCommand sqlperm = new SqlCommand("Permission_Insert", sqlcon);
						sqlperm.CommandType = CommandType.StoredProcedure;
						sqlperm.Parameters.AddWithValue("RoleId", roleId);
						sqlperm.Parameters.AddWithValue("PermissionID", perm.ID);

						sqlcon.Open();
						sqlperm.ExecuteNonQuery();
						sqlcon.Close();
					}

					return new BaseResponse
					{
						IsSuccess = true,
						Type = "success",
						Message = "تم تحديث البيانات بنجاح",
					};
				}
				catch (Exception)
				{

					throw;
				}


			}

		}

		public async Task<Permission> GetSingleByName(string permition)
		{
			using (SqlConnection sqlcon = new SqlConnection(con))
			{
				Permission permission = new Permission();
				SqlCommand sqlcom = new SqlCommand("Permissions_getByName", sqlcon);
				sqlcom.CommandType = CommandType.StoredProcedure;
				sqlcom.Parameters.AddWithValue("@PermissionName", permition);

				sqlcon.Open();
				SqlDataReader dr = sqlcom.ExecuteReader();
				while (dr.Read())
				{
					permission.ID = Convert.ToInt32(dr["ID"]);
					permission.Name = dr["Name"].ToString();
				}

				sqlcon.Close();
				return permission;
			}
		}

	
		public async Task<bool> hasPermission(string id, string permission,CancellationToken cancellation)
		{
			SiteUser user = await _userRepository.FindById(id,cancellation);
			
			SiteRole role = await _siteRoleRepository.GetRolesByUserId(user,cancellation);
			
			Permission checkpermission = await GetSingleByName(permission);
			try
			{
				using (var conn = new SqlConnection(con))
				{
					 conn.Open();

					using (SqlCommand cmd = new SqlCommand())
					{
						cmd.Connection = conn;
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "Permission_UserhasPermission";

						cmd.Parameters.AddWithValue("RoleID", role.Id);
						cmd.Parameters.AddWithValue("PermissionID", checkpermission.ID);

						var value = Convert.ToInt32(cmd.ExecuteScalar());

						if (value > 0)
						{
							// Yes, in that role
							return true;
						}
					}

				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return false;
		}
	}
}
