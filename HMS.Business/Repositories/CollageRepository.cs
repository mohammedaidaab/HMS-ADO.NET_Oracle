//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Data;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HMS.Business.Repositories
{
    public class CollageRepository : ICollageRepository
    {
        private readonly IConfiguration _config;
        private readonly string con;
        public CollageRepository(IConfiguration config)
        {
            _config = config;
            con = _config.GetConnectionString("DefaultConnection");

        }

        public async Task<BaseResponse> Add(collage model)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                var sqlcom = new SqlCommand("Collages_create", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", model.ID);
                sqlcom.Parameters.AddWithValue("Name", model.Name);
                sqlcom.Parameters.AddWithValue("Code", model.Code);

				sqlcon.Open();
				if (sqlcom.ExecuteNonQuery() > 0)
				{
					sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "تم اضافة بيانات الكلية بنجاح",
                        Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					sqlcon.Close();
					return new BaseResponse
					{
						Message = "لم تتم اضافة البيانات الخاصة بالكية بنجاح",
						Type = "warning",
						IsSuccess = false
					};

				}

			}

            // throw new NotImplementedException();
        }

        public async Task<BaseResponse> Delete(int Id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Collages_Delete", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("@ID", Id);

				sqlcon.Open();
				if (sqlcom.ExecuteNonQuery() > 0)
				{
					sqlcon.Close();
					return new BaseResponse
					{
						Message = "تم حذف البيانات  بنجاح",
						Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					sqlcon.Close();
					return new BaseResponse
					{
						Message = "لم يتم الحذف لوجود مباني متصلة بالكلية",
						Type = "error",
						IsSuccess = false
					};

				}
			}
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<collage>> GetAll()
        {
            List<collage> ColList = new List<collage>();
            using (OracleConnection _SqlConnection = new OracleConnection(con))
            {
               
                OracleCommand sqlcom = new OracleCommand("Collages_GetAll", _SqlConnection);
                sqlcom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter
                {
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor,

                };
                sqlcom.Parameters.Add(res);

                _SqlConnection.Open();
                OracleDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    collage collage = new collage();
                    collage.ID = Convert.ToInt32(dr["id"]);
                    collage.Name = dr["Name"].ToString();
                    collage.Code = Convert.ToInt32(dr["Code"]);
                    ColList.Add(collage);
                }
                _SqlConnection.Close();
                return ColList;
            }
        }

        public async Task<collage> GetById(int Id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                collage collage = new collage();

                sqlcon.CreateCommand();
                SqlCommand sqlcom = new SqlCommand("GET_Collage_by_Id", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", Id);

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {

                    collage.ID = Convert.ToInt32(dr["id"]);
                    collage.Name = dr["name"].ToString();
                    collage.Code = Convert.ToInt32(dr["Code"]);
                }

                sqlcon.Close();
                return collage;
            }

            throw new NotImplementedException();
        }

        public async Task<BaseResponse> Update(collage model)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {

                SqlCommand sqlcom = new SqlCommand("Collages_Update", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", model.ID);
                sqlcom.Parameters.AddWithValue("Name", model.Name);
                sqlcom.Parameters.AddWithValue("Code", model.Code);

				sqlcon.Open();
				if (sqlcom.ExecuteNonQuery() > 0)
				{
					sqlcon.Close();
					return new BaseResponse
					{
						Message = "تم تعديل بيانات الكلية بنجاح",
						Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					sqlcon.Close();
					return new BaseResponse
					{
						Message = "لم تتم تعديل البيانات لوجود بيانات مماثلة",
						Type = "warning",
						IsSuccess = false
					};

				}


			}
            //throw new NotImplementedException();
        }
    }
}
