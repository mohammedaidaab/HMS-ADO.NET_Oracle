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
using Oracle.ManagedDataAccess.Client;
using System.Reflection;

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
            using (OracleConnection oracon= new OracleConnection(con))
            {
                var oracom = new OracleCommand("Halls_Create", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter H_id = new OracleParameter { ParameterName = "H_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.ID };
                OracleParameter HallName = new OracleParameter { ParameterName = "HallName", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Input, Value = model.Name };
                OracleParameter HallNumber = new OracleParameter { ParameterName = "HallNumber", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.Number };
                OracleParameter Building_Id = new OracleParameter { ParameterName = "Building_Id", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.Building_ID };
                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Output };


                oracom.Parameters.Add(H_id);
                oracom.Parameters.Add(HallName);
                oracom.Parameters.Add(HallNumber);
                oracom.Parameters.Add(Building_Id);
                oracom.Parameters.Add(qres);

                oracon.Open();
                oracom.ExecuteNonQuery();
                if (oracom.Parameters["qres"].Value.ToString() == "success")
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم إضافة القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    oracon.Close();
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
            using (OracleConnection oracon= new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Halls_Delete", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


                OracleParameter H_id = new OracleParameter { ParameterName = "H_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = Id };
                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.Varchar2,Size = 256, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(H_id);
                oracom.Parameters.Add(qres);


                //oracom.Parameters.AddWithValue("@ID", Id);

                oracon.Open();
                oracom.ExecuteNonQuery();
                if (oracom.Parameters["qres"].Value.ToString() == "success")
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم حذف بيانات القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    oracon.Close();
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
            using (OracleConnection oracon = new OracleConnection(con))
            {
              
                oracon.CreateCommand();
                OracleCommand oracom = new OracleCommand("Get_Hall_building", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);


                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
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
                oracon.Close();
                return HalList;
            }
        }

        public async Task<Hall> GetById(int Id)
        {
            Hall hall = new Hall();

            using (OracleConnection oracon= new OracleConnection(con))
            {
                oracon.CreateCommand();

                OracleCommand oracom = new OracleCommand("Halls_GetById", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter Hall_Id = new OracleParameter { ParameterName = "Hall_Id", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = Id };
                OracleParameter res = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.RefCursor, Size = 200, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(Hall_Id);
                oracom.Parameters.Add(res);


                //oracom.Parameters.AddWithValue("@ID", Id);

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();

                while (dr.Read())
                {
                    hall.ID = Convert.ToInt32(dr["id"]);
                    hall.Name = dr["name"].ToString();
                    hall.Number = Convert.ToInt32(dr["Hall_Number"]);
                    hall.Building_ID = Convert.ToInt32(dr["Building_ID"]);
                }
                oracon.Close();
                return hall;
            }
            //  throw new NotImplementedException();
        }

        public async Task<BaseResponse> Update(Hall model)
        {
            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Halls_Update", oracon);
                oracom.CommandType = CommandType.StoredProcedure;



                OracleParameter H_id = new OracleParameter { ParameterName = "H_ID", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.ID };
                OracleParameter HallName = new OracleParameter { ParameterName = "HallName", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Input, Value = model.Name };
                OracleParameter HallNumber = new OracleParameter { ParameterName = "HallNumber", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.Number };
                OracleParameter Building_Id = new OracleParameter { ParameterName = "Building_Id", OracleDbType = OracleDbType.Int32, Size = 200, Direction = ParameterDirection.Input, Value = model.Building_ID };
                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 200, Direction = ParameterDirection.Output };


                oracom.Parameters.Add(H_id);
                oracom.Parameters.Add(HallName);
                oracom.Parameters.Add(HallNumber);
                oracom.Parameters.Add(Building_Id);

                oracom.Parameters.Add(qres);

                oracon.Open();
                oracom.ExecuteNonQuery();
                if (oracom.Parameters["qres"].Value.ToString() == "success")
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم تعديل بيانات القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
                    oracon.Close();
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
