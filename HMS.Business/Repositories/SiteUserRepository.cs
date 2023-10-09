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

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_GetAllUsers";

                        OracleDataReader rdr = cmd.ExecuteReader();

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

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_InsertUser";

                        cmd.Parameters.Add("@Username", user.Username);
                        cmd.Parameters.Add("@NormalizedUserName", user.NormalizedUserName);
                        cmd.Parameters.Add("@Forename", user.Forename ?? "");
                        cmd.Parameters.Add("@Surname", user.Surname ?? "");
                        cmd.Parameters.Add("@Email", user.Email);
                        cmd.Parameters.Add("@NormalizedEmail", user.NormalizedEmail);
                        cmd.Parameters.Add("@EmailConfirmed", user.EmailConfirmed);
                        cmd.Parameters.Add("@PasswordHash", user.PasswordHash ?? "");
                        cmd.Parameters.Add("@PhoneNumber", user.PhoneNumber ?? "");
                        cmd.Parameters.Add("@PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                        cmd.Parameters.Add("@TwoFactorEnabled", user.TwoFactorEnabled);
                        cmd.Parameters.Add("@Created", DateTime.Now);

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

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_UpdateUser";

                        //OracleParameter res = new OracleParameter
                        //{
                        //    Direction = ParameterDirection.Output,
                        //    OracleDbType = OracleDbType.RefCursor,

                        //};

                        //cmd.Parameters.Add(res);
                        //cmd.CommandType = CommandType.StoredProcedure;




                        cmd.Parameters.Add("@Id", user.Id);
                        cmd.Parameters.Add("@Username", user.Username);
                        cmd.Parameters.Add("@NormalizedUserName", user.NormalizedUserName);
                        cmd.Parameters.Add("@Forename", user.Forename);
                        cmd.Parameters.Add("@Surname", user.Surname);
                        cmd.Parameters.Add("@NormalizedEmail", user.NormalizedEmail);
                        cmd.Parameters.Add("@Email", user.Email);
                        cmd.Parameters.Add("@EmailConfirmed", user.EmailConfirmed);
                        cmd.Parameters.Add("@PasswordHash", user.PasswordHash);
                        cmd.Parameters.Add("@PhoneNumber", user.PhoneNumber);
                        cmd.Parameters.Add("@PhoneNumberConfirmed", user.PhoneNumberConfirmed);
                        cmd.Parameters.Add("@TwoFactorEnabled", user.TwoFactorEnabled);

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

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindById";

                        cmd.Parameters.Add("@Id", userId);

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
                                    PasswordHash = rdr["PasswordHash"].ToString(),
                                    PhoneNumber = rdr["PhoneNumber"].ToString(),
                                    PhoneNumberConfirmed = bool.Parse(rdr["PhoneNumberConfirmed"].ToString()),
                                    TwoFactorEnabled = bool.Parse(rdr["TwoFactorEnabled"].ToString()),
                                    Created = DateTime.Parse(rdr["Created"].ToString()),
                                    // Facebook = rdr["Facebook"].ToString(),
                                    // Twitter = rdr["Twitter"].ToString(),
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
                        conn.Open();
                        //cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandText = "identity_FindByName";

                        cmd.Parameters.Add("@NormalizedUserName", normalizedUserName);

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
                using (var conn = new OracleConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "identity_FindByEmail";

                        cmd.Parameters.Add("@NormalizedUserName", normalizedEmail);

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
