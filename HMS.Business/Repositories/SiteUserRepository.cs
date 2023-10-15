//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.Infrastructure.Repositories
{
    public class SiteUserRepository : ISiteUserRepository
    {
        private IConfiguration _config { get; set; }

        public SiteUserRepository(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Retrieve all users from the database
        /// </summary>
        /// <returns>List of Users (SiteUser)</returns>
        public IQueryable<SiteUser> GetUserList()
        {
            // IUserStore requires a IQueryable list
            var list = new List<SiteUser>();

            try
            {
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conn.Open();

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = conn;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_GetAllUsers";

                        OracleParameter res = new OracleParameter { ParameterName = "res", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.RefCursor, Size = 200, };
                        oracom.Parameters.Add(res);

                        OracleDataReader rdr = oracom.ExecuteReader();

                        while (rdr.Read())
                        {
                            SiteUser su = new SiteUser
                            {
                                Id = int.Parse(rdr["Id"].ToString()),
                                Username = rdr["Username"].ToString(),
                                NormalizedUserName = rdr["NormalizedUserName"].ToString(),
                                Forename = rdr["Forename"].ToString(),
                                Surname = rdr["Surname"].ToString(),
                                Email = rdr["Email"].ToString(),
                                NormalizedEmail = rdr["NormalizedEmail"].ToString(),
                                EmailConfirmed = bool.Parse(rdr["EmailConfirmed"].ToString()),
                                PhoneNumber = rdr["PhoneNumber"].ToString(),
                                PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                Created = DateTime.Parse(rdr["Created"].ToString())
                            };

                            list.Add(su);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list.AsQueryable();
        }

        /// <summary>
        /// Create new SiteUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Success or Fail</returns>
        public async Task<IdentityResult> CreateUser(SiteUser user, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                   
                    await conn.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = conn;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_InsertUser";

                        OracleParameter uname = new OracleParameter{ParameterName= "Username", Direction = ParameterDirection.Input,OracleDbType = OracleDbType.NVarchar2,Value=user.Username};
                        OracleParameter NormalizedUserName = new OracleParameter { ParameterName = "NormalizedUserName", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.NormalizedUserName };
                        OracleParameter Forename = new OracleParameter { ParameterName = "Forename", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Forename };
                        OracleParameter Surname = new OracleParameter { ParameterName = "Surname", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Surname };
                        OracleParameter Email = new OracleParameter { ParameterName = "Email", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Email };
                        OracleParameter NormalizedEmail = new OracleParameter { ParameterName = "NormalizedUserName", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.NormalizedEmail };
                        OracleParameter EmailConfirmed = new OracleParameter { ParameterName = "EmailConfirmed", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = user.EmailConfirmed };
                        OracleParameter PasswordHash = new OracleParameter { ParameterName = "PasswordHash", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.PasswordHash };
                        OracleParameter PhoneNumber = new OracleParameter { ParameterName = "PhoneNumber", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.PhoneNumber };
                        OracleParameter PhoneNumberConfirmed = new OracleParameter { ParameterName = "PhoneNumberConfirmed", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = Convert.ToBoolean(user.PhoneNumberConfirmed) };
                        OracleParameter TwoFactorEnabled = new OracleParameter { ParameterName = "TwoFactorEnabled", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = Convert.ToBoolean(user.TwoFactorEnabled) };
                        OracleParameter Created = new OracleParameter { ParameterName = "Created", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Date, Value = DateTime.Now };
                        
                        OracleParameter qres = new OracleParameter { ParameterName = "qres", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.NVarchar2, Size = 200, };

                        oracom.Parameters.Add(uname);
                        oracom.Parameters.Add(NormalizedUserName);
                        oracom.Parameters.Add(Forename);
                        oracom.Parameters.Add(Surname);
                        oracom.Parameters.Add(Email);
                        oracom.Parameters.Add(NormalizedEmail);
                        oracom.Parameters.Add(EmailConfirmed);
                        oracom.Parameters.Add(PasswordHash);
                        oracom.Parameters.Add(PhoneNumber);
                        oracom.Parameters.Add(PhoneNumberConfirmed);
                        oracom.Parameters.Add(TwoFactorEnabled);
                        oracom.Parameters.Add(Created);

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
        /// Update Existing User Details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUser(SiteUser user, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = conn;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_UpdateUser";

                        //==============================parms================================================


                        OracleParameter U_ID = new OracleParameter { ParameterName = "U_ID", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Id };

                        OracleParameter uname = new OracleParameter { ParameterName = "Username", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Username };
                        OracleParameter NormalizedUserName = new OracleParameter { ParameterName = "NormalizedUserName", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.NormalizedUserName };
                        OracleParameter Forename = new OracleParameter { ParameterName = "Forename", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Forename };
                        OracleParameter Surname = new OracleParameter { ParameterName = "Surname", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Surname };
                        OracleParameter Email = new OracleParameter { ParameterName = "Email", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.Email };
                        OracleParameter NormalizedEmail = new OracleParameter { ParameterName = "NormalizedUserName", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.NormalizedEmail };
                        OracleParameter EmailConfirmed = new OracleParameter { ParameterName = "EmailConfirmed", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = user.EmailConfirmed };
                        OracleParameter PasswordHash = new OracleParameter { ParameterName = "PasswordHash", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.PasswordHash };
                        OracleParameter PhoneNumber = new OracleParameter { ParameterName = "PhoneNumber", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = user.PhoneNumber };
                        OracleParameter PhoneNumberConfirmed = new OracleParameter { ParameterName = "PhoneNumberConfirmed", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = Convert.ToBoolean(user.PhoneNumberConfirmed) };
                        OracleParameter TwoFactorEnabled = new OracleParameter { ParameterName = "TwoFactorEnabled", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = Convert.ToBoolean(user.TwoFactorEnabled) };
                        OracleParameter Created = new OracleParameter { ParameterName = "Created", Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Date, Value = DateTime.Now };

                        OracleParameter qres = new OracleParameter { ParameterName = "qres", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.NVarchar2, Size = 200, };

                        oracom.Parameters.Add(U_ID);
                        oracom.Parameters.Add(uname);
                        oracom.Parameters.Add(NormalizedUserName);
                        oracom.Parameters.Add(Forename);
                        oracom.Parameters.Add(Surname);
                        oracom.Parameters.Add(Email);
                        oracom.Parameters.Add(NormalizedEmail);
                        oracom.Parameters.Add(EmailConfirmed);
                        oracom.Parameters.Add(PasswordHash);
                        oracom.Parameters.Add(PhoneNumber);
                        oracom.Parameters.Add(PhoneNumberConfirmed);
                        oracom.Parameters.Add(TwoFactorEnabled);

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
        /// Find user by provided ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>SiteUser</returns>
        public async Task<SiteUser> FindById(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var user = new SiteUser();
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = conn;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_FindById";

                        OracleParameter user_id = new OracleParameter { ParameterName = "U_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = userId };
                        OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

                        oracom.Parameters.Add(user_id);
                        oracom.Parameters.Add(res);

                        //oracom.Parameters.Add("@Id", userId);

                        using (var rdr = await oracom.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (rdr.Read())
                            {
                                user = new SiteUser
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Username = rdr["Username"].ToString(),
                                    NormalizedUserName = rdr["NormalizedUserName"].ToString(),
                                    Forename = rdr["Forename"].ToString(),
                                    Surname = rdr["Surname"].ToString(),
                                    Email = rdr["Email"].ToString(),
                                    NormalizedEmail = rdr["NormalizedEmail"].ToString(),
                                    EmailConfirmed = bool.Parse(rdr["EmailConfirmed"].ToString()),
                                    PasswordHash = rdr["PasswordHash"].ToString(),
                                    PhoneNumber = rdr["PhoneNumber"].ToString(),
                                    PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                    TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                    Created = DateTime.Parse(rdr["Created"].ToString()),
                                };
                            }
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find user by provided Normalized Username
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>SiteUser</returns>
        public async Task<SiteUser> FindByName(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                var user = new SiteUser();
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    //await conn.OpenAsync(cancellationToken);
                   
                    using (OracleCommand cmd = new OracleCommand("identity_FindByName",conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        OracleParameter normalizedusername = new OracleParameter { ParameterName = "normalizedname", OracleDbType=OracleDbType.NVarchar2,Size=200,Direction=ParameterDirection.Input,Value=normalizedUserName };
                        OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

                        cmd.Parameters.Add(normalizedusername);
                        cmd.Parameters.Add(res);

                        conn.Open();
						using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (rdr.Read())
                            {
                                user = new SiteUser
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Username = rdr["Username"].ToString(),
                                    NormalizedUserName = rdr["NormalizedUserName"].ToString(),
                                    Forename = rdr["Forename"].ToString(),
                                    Surname = rdr["Surname"].ToString(),
                                    Email = rdr["Email"].ToString(),
                                    NormalizedEmail = rdr["NormalizedEmail"].ToString(),
                                    EmailConfirmed = bool.Parse(rdr["EmailConfirmed"].ToString()),
                                    PhoneNumber = rdr["PhoneNumber"].ToString(),
                                    PasswordHash = rdr["PasswordHash"].ToString(),
                                    PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                    TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                    Created = DateTime.Parse(rdr["Created"].ToString()),
                                };
                            }
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find user by provided Normalized Email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>SiteUser</returns>
        public async Task<SiteUser> FindByEmail(string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                var user = new SiteUser();
                using (var oracon = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await oracon.OpenAsync(cancellationToken);

                    using (OracleCommand oracom = new OracleCommand())
                    {
                        oracom.Connection = oracon;
                        oracom.CommandType = CommandType.StoredProcedure;
                        oracom.CommandText = "identity_FindByEmail";

						OracleParameter noruseremail = new OracleParameter { ParameterName = "NORUSEREMAIL", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Input, Value = normalizedEmail };
						OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

						oracom.Parameters.Add(noruseremail);
						oracom.Parameters.Add(res);

						//cmd.Parameters.Add("@NormalizedUserName", normalizedEmail);

                        using (var rdr = await oracom.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (rdr.Read())
                            {
                                user = new SiteUser
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Username = rdr["Username"].ToString(),
                                    NormalizedUserName = rdr["NormalizedUserName"].ToString(),
                                    Forename = rdr["Forename"].ToString(),
                                    Surname = rdr["Surname"].ToString(),
                                    Email = rdr["Email"].ToString(),
                                    NormalizedEmail = rdr["NormalizedEmail"].ToString(),
                                    PasswordHash = rdr["PasswordHash"].ToString(),
                                    EmailConfirmed = bool.Parse(rdr["EmailConfirmed"].ToString()),
                                    PhoneNumber = rdr["PhoneNumber"].ToString(),
                                    PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                    TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                    Created = DateTime.Parse(rdr["Created"].ToString()),
                                    //Facebook = rdr["Facebook"].ToString(),
                                    //Twitter = rdr["Twitter"].ToString(),
                                    //Instagram = rdr["Instagram"].ToString(),
                                    //Website = rdr["Website"].ToString()
                                };
                            }
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete SiteUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteUser(SiteUser user, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_DeleteUser";

                        cmd.Parameters.Add("@Id", user.Id);

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

    }
}
