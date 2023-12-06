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
using Oracle.ManagedDataAccess.Client;
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

                SqlDataReader dr = (SqlDataReader)await sqlcom.ExecuteReaderAsync();

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
            using (OracleConnection oracon = new OracleConnection(con))
            {
                List<Permission> RolePermissions = new List<Permission>();
                List<Permission> Permissions = new List<Permission>();

                OracleCommand oracom = new OracleCommand("Permission_get_all_By_Role", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

				OracleParameter role_id = new OracleParameter {ParameterName= "ROLE_ID",OracleDbType=OracleDbType.Int32,Direction=ParameterDirection.Input,Value=role.Id };
				OracleParameter res = new OracleParameter {ParameterName= "res",OracleDbType=OracleDbType.RefCursor,Direction=ParameterDirection.Output};

				oracom.Parameters.Add(role_id);
				oracom.Parameters.Add(res);	
                
                oracon.Open();
                OracleDataReader dr = (OracleDataReader)await oracom.ExecuteReaderAsync();
                while (dr.Read())
                {
                    Permission RolePermission = new Permission();
                    RolePermission.ID = Convert.ToInt32(dr["RoleID"]);
                    RolePermission.Name = dr["PermissionID"].ToString();

                    RolePermissions.Add(RolePermission);
                }
                oracon.Close();

                using (OracleConnection oracon2 = new OracleConnection(con))
                {
                    foreach (var perm in RolePermissions)
                    {

                        OracleCommand oracom2 = new OracleCommand("Permission_get_by_Id", oracon2);
                        oracom2.CommandType = CommandType.StoredProcedure;

                        OracleParameter PERM_ID = new OracleParameter { ParameterName = "PERM_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = perm.Name };
                        OracleParameter res2 = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output };

                        oracom2.Parameters.Add(PERM_ID);
                        oracom2.Parameters.Add(res2);

                        oracon2.Open();
                        OracleDataReader dr2 = oracom2.ExecuteReader();

                        while (dr2.Read())
                        {
                            Permission Perms = new Permission();
                            Perms.ID = Convert.ToInt32(dr2["ID"]);
                            Perms.Name = dr2["Name"].ToString();

                            Permissions.Add(Perms);
                        }
                        oracon2.Close();

                    }
                }

                return Permissions;
            }
        }

		public async Task<List<Permission>> GetByName(string[] permissions)
		{
			List<Permission> RolePermissions = new List<Permission>();

			using (OracleConnection oracon = new OracleConnection(con))
			{
				foreach (var perm in permissions)
				{
					OracleCommand oracom = new OracleCommand("Permissions_getByName", oracon);
                    oracom.CommandType = CommandType.StoredProcedure;

                    OracleParameter PermissionName = new OracleParameter { ParameterName = "PermissionName", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = perm };
					oracom.Parameters.Add(PermissionName);
					OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output, };
					oracom.Parameters.Add(res);

					oracon.Open();
					OracleDataReader dr = (OracleDataReader)await oracom.ExecuteReaderAsync();
					while (dr.Read())
					{
						Permission Permission = new Permission();
						Permission.ID = Convert.ToInt32(dr["ID"]);
						Permission.Name = dr["Name"].ToString();
						RolePermissions.Add(Permission);
					}
					oracon.Close();
				}

				return RolePermissions;

			}
		}

		public async Task<BaseResponse> update(List<Permission> permission, int roleId)
		{
			using (OracleConnection oracon = new OracleConnection(con))
			{
				OracleCommand oracom = new OracleCommand("permission_reset", oracon);

				oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter R_ID = new OracleParameter { ParameterName = "R_ID", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = roleId };
                oracom.Parameters.Add(R_ID);
                
				oracon.Open();
				await oracom.ExecuteNonQueryAsync();
				oracon.Close();
				try
				{
					foreach (var perm in permission)
					{
						OracleCommand oracom2 = new OracleCommand("Permission_Insert", oracon);
						oracom2.CommandType = CommandType.StoredProcedure;

                        OracleParameter role_id = new OracleParameter { ParameterName = "Role_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = roleId };
                        OracleParameter perm_id = new OracleParameter { ParameterName = "Perm_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = perm.ID };

                        oracom2.Parameters.Add(role_id);
                        oracom2.Parameters.Add(perm_id);

						oracon.Open();
						oracom2.ExecuteNonQuery();
						oracon.Close();
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
			using (OracleConnection oracon = new OracleConnection(con))
			{
				Permission permission = new Permission();
				OracleCommand oracom = new OracleCommand("Permissions_getByName", oracon);

                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter perm_name = new OracleParameter { ParameterName = "PermissionName", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Input, Value = permition };
                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(perm_name);
                oracom.Parameters.Add(res);

                oracon.Open();
				OracleDataReader dr = (OracleDataReader)await oracom.ExecuteReaderAsync();
				while (dr.Read())
				{
					permission.ID = Convert.ToInt32(dr["ID"]);
					permission.Name = dr["Name"].ToString();
				}

				oracon.Close();
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
				using (var oracon = new OracleConnection(con))
				{
					oracon.Open();
					using (OracleCommand oracom = new OracleCommand())
					{
                        oracom.Connection = oracon;
						oracom.CommandType = CommandType.StoredProcedure;
						oracom.CommandText = "Permission_UserhasPermission";

                        OracleParameter role_id = new OracleParameter { ParameterName = "U_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = role.Id };
                        OracleParameter perm_id = new OracleParameter { ParameterName = "U_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = checkpermission.ID };
                        OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

                        oracom.Parameters.Add(role_id);
                        oracom.Parameters.Add(perm_id);
                        oracom.Parameters.Add(res);

						var value = Convert.ToInt32(oracom.ExecuteScalar());

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
