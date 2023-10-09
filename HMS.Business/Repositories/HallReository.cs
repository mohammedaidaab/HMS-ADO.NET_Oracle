//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Interfaces.Repositories;
using HMS.Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;

namespace HMS.Infrastructure.Repositories
{
    public class HallReository : IHallrepository
    {
        //private readonly string con = "Data Source=.;Initial Catalog=HMS;User ID=sa;Password=root;TrustServerCertificate=True;";
        private readonly string con = "";

        private IConfiguration _config { get; set; }

        public HallReository(IConfiguration config)
        {
            _config = config;
            con = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<BaseResponse> Add(Hall model)
        {
            using (SqlConnection _sqlconnection = new SqlConnection(con))
            {
                var sqlcom = new SqlCommand("Halls_Create", _sqlconnection);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", model.ID);
                sqlcom.Parameters.AddWithValue("Name", model.Name);
                sqlcom.Parameters.AddWithValue("Number", model.Number);
                sqlcom.Parameters.AddWithValue("Building_ID", model.Building_ID);


                _sqlconnection.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                    _sqlconnection.Close();
                    return new BaseResponse
                    {
                        Message = "تم إضافة القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    _sqlconnection.Close();
                    return new BaseResponse
                    {
                        Message = " لم تتم إضافة القاعة بنجاح لوجود بيانات مماثلة",
                        Type = "warning",
                        IsSuccess = false
                    };

                }


            }
        }

        public async Task<BaseResponse> Delete(int Id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Halls_Delete", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("@ID", Id);

                sqlcon.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "تم حذف بيانات القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "لم تتم حذف البيانات الخاصة بالقاعة بنجاح",
                        Type = "error",
                        IsSuccess = false
                    };

                }
            }

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<HallBuildingVM>> GetAll()
        {

            List<HallBuildingVM> HalList = new List<HallBuildingVM>();
            //using (SqlConnection _SqlConnection = new SqlConnection(con))
            using (SqlConnection _SqlConnection = new SqlConnection(con))
            {
                _SqlConnection.Open();
                _SqlConnection.CreateCommand();
                SqlCommand sqlcom = new SqlCommand("Get_Hall_building", _SqlConnection);
                sqlcom.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = sqlcom.ExecuteReader();

                while (dr.Read())
                {
                    HallBuildingVM hall = new HallBuildingVM();

                    hall.ID = Convert.ToInt32(dr["id"]);
                    hall.Name = dr["HallName"].ToString();
                    hall.Number = Convert.ToInt32(dr["HallNumber"]);
                    hall.Building_ID = Convert.ToInt32(dr["Building_ID"]);
                    hall.Building_Name = dr["BuildingName"].ToString();

                    HalList.Add(hall);
                }
                _SqlConnection.Close();
                return HalList;
            }
        }

        public async Task<Hall> GetById(int Id)
        {
            Hall hall = new Hall();

            using (SqlConnection _SqlConnection = new SqlConnection(con))
            {
                _SqlConnection.CreateCommand();

                SqlCommand sqlcom = new SqlCommand("Halls_GetById", _SqlConnection);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("@ID", Id);

                _SqlConnection.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();

                while (dr.Read())
                {
                    hall.ID = Convert.ToInt32(dr["id"]);
                    hall.Name = dr["name"].ToString();
                    hall.Number = Convert.ToInt32(dr["number"]);
                    hall.Building_ID = Convert.ToInt32(dr["Building_ID"]);
                }
                _SqlConnection.Close();
                return hall;
            }
            //  throw new NotImplementedException();
        }

        public async Task<BaseResponse> Update(Hall model)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Halls_Update", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcom.Parameters.AddWithValue("@ID", model.ID);
                sqlcom.Parameters.AddWithValue("@Name", model.Name);
                sqlcom.Parameters.AddWithValue("@Number", model.Number);
                sqlcom.Parameters.AddWithValue("@Building_Id", model.Building_ID);

                sqlcon.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "تم تعديل بيانات القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "لم تتم تعديل البيانات الخاصة بالقاعة لوجود بيانات مماثلة",
                        Type = "warning",
                        IsSuccess = false
                    };

                }
            }        

        }
    }
}
