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
            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Collages_create", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter Col_Name = new OracleParameter {ParameterName="Col_Name",OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Input,Value=model.Name };
                OracleParameter Col_Code = new OracleParameter {ParameterName="Col_Code",OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Input,Value=model.Code };
                OracleParameter qres     = new OracleParameter {ParameterName="qres",OracleDbType=OracleDbType.NVarchar2,Size=255,Direction=ParameterDirection.Output };

                oracom.Parameters.Add(Col_Name);
                oracom.Parameters.Add(Col_Code);
                oracom.Parameters.Add(qres);

                //oracom.Parameters.AddWithValue("ID", model.ID);
                //oracom.Parameters.AddWithValue("Name", model.Name);
                //oracom.Parameters.AddWithValue("Code", model.Code);

				oracon.Open();
                oracom.ExecuteNonQuery();
				if (oracom.Parameters["qres"].Value.ToString() == "success")
				{
					oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم اضافة بيانات الكلية بنجاح",
                        Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					oracon.Close();
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
            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Collages_Delete", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

				OracleParameter Col_ID = new OracleParameter { ParameterName = "Col_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = Id };
				OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.Varchar2,Size=256, Direction = ParameterDirection.Output };
               
                oracom.Parameters.Add(Col_ID);
                oracom.Parameters.Add(qres);

				//oracom.Parameters.AddWithValue("@ID", Id);

				oracon.Open();
                oracom.ExecuteNonQuery();
				if (oracom.Parameters["qres"].Value.ToString() == "success")
				{
					oracon.Close();
					return new BaseResponse
					{
						Message = "تم حذف البيانات  بنجاح",
						Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					oracon.Close();
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
            using (OracleConnection oracon = new OracleConnection(con))
            {
               
                OracleCommand oracom = new OracleCommand("Collages_GetAll", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter
                {
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor,

                };
                oracom.Parameters.Add(res);

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    collage collage = new collage();
                    collage.ID = Convert.ToInt32(dr["id"]);
                    collage.Name = dr["Name"].ToString();
                    collage.Code = Convert.ToInt32(dr["Code"]);
                    ColList.Add(collage);
                }
                oracon.Close();
                return ColList;
            }
        }

        public async Task<collage> GetById(int Id)
        {
            using (OracleConnection oracon = new OracleConnection(con))
            {
                collage collage = new collage();

                oracon.CreateCommand();
                OracleCommand oracom = new OracleCommand("Collages_GET_byId", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


				OracleParameter Col_ID = new OracleParameter { ParameterName = "Col_ID", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input,Value=Id };
				OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output };

				oracom.Parameters.Add(Col_ID);
				oracom.Parameters.Add(res);

				//oracom.Parameters.AddWithValue("ID", Id);

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {

                    collage.ID = Convert.ToInt32(dr["id"]);
                    collage.Name = dr["name"].ToString();
                    collage.Code = Convert.ToInt32(dr["Code"]);
                }

                oracon.Close();
                return collage;
            }

            throw new NotImplementedException();
        }

        public async Task<BaseResponse> Update(collage model)
        {
            using (OracleConnection oracon = new OracleConnection(con))
            {

                OracleCommand oracom = new OracleCommand("Collages_Update", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


				OracleParameter Col_ID = new OracleParameter { ParameterName = "Col_Name", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = model.ID };
				OracleParameter Col_Name = new OracleParameter { ParameterName = "Col_Name", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = model.Name };
				OracleParameter Col_Code = new OracleParameter { ParameterName = "Col_Code", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = model.Code };
				
                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

				oracom.Parameters.Add(Col_ID);
				oracom.Parameters.Add(Col_Name);
				oracom.Parameters.Add(Col_Code);
				oracom.Parameters.Add(qres);

				//oracom.Parameters.AddWithValue("ID", model.ID);
                //oracom.Parameters.AddWithValue("Name", model.Name);
                //oracom.Parameters.AddWithValue("Code", model.Code);

				oracon.Open();
                oracom.ExecuteNonQuery();
				if (oracom.Parameters["qres"].Value.ToString() == "success")
				{
					oracon.Close();
					return new BaseResponse
					{
						Message = "تم تعديل بيانات الكلية بنجاح",
						Type = "success",
						IsSuccess = true
					};
				}
				else
				{
					oracon.Close();
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
