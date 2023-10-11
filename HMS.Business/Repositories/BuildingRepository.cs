//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using Oracle.ManagedDataAccess.Client;

namespace HMS.Business.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string con;

        public BuildingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<BaseResponse> create(BuildingCollegeVM buildingCollegeVM)
        {
            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand sqlcom = new OracleCommand("Building_create", oracon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                OracleParameter qres = new OracleParameter
                {
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.Int32,
                };

                OracleParameter BName = new OracleParameter
                {
                    Direction = ParameterDirection.Input,
                    OracleDbType = OracleDbType.NVarchar2,
                    Value = buildingCollegeVM.buldingName.ToString(),
                };

                OracleParameter BNumber = new OracleParameter
                {
                    Direction = ParameterDirection.Input,
                    OracleDbType = OracleDbType.Int32,
                    Value = buildingCollegeVM.buldingnumber,
                };

                OracleParameter BCollege_Id = new OracleParameter
                {
                    Direction = ParameterDirection.Input,
                    OracleDbType = OracleDbType.Int32,
                    Value = buildingCollegeVM.BuldingCollageNumber,
                };

                sqlcom.Parameters.Add(BName);
                sqlcom.Parameters.Add(BNumber);
                sqlcom.Parameters.Add(BCollege_Id);
                sqlcom.Parameters.Add(qres);

                try
                {
                    oracon.Open();
                   // sqlcom.ExecuteScalar();
                    var dr = sqlcom.ExecuteNonQuery();
                    
                    if (sqlcom.("qres").value == -1)
                    {
                        oracon.Close();
                        return new BaseResponse
                        {
                            Message = "تم اضافة بيانات المبنى بنجاح",
                            Type = "success",
                            IsSuccess = true
                        };
                    }
                    else
                    {
                        oracon.Close();
                        return new BaseResponse
                        {
                            Message = "لم تتم اضافة البيانات الخاصة بالمبنى بنجاح لوجود بيانات مطابقة ",
                            Type = "warning",
                            IsSuccess = false
                        };

                    }
                    
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "لم تتم اضافة البيانات الخاصة بالمبنى بنجاح لوجود بيانات مطابقة ",
                        Type = "warning",
                        IsSuccess = false
                    };

                }
                catch (Exception e)
                {
                    oracon.Close();
					return new BaseResponse
					{
						Message = e.ToString(),
						IsSuccess = false
					};
				}
               
            }
            //throw new NotImplementedException();
        }

        public async Task<BaseResponse> delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Building_Delete", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("Id", id);

                sqlcon.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "تم حذف بيانات المبنى بنجاح",
                        Type = "success",
                        IsSuccess = true

                    };
                }
                else
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "لم يتم حذف البيانات الخاصة بالمبنى بنجاح لوجود قاعة او اكثر مرتبطة بالمبنى المراد حذفه",
                        Type = "error",
                        IsSuccess = false
                    };

                }
            }

        }

        public async Task<IEnumerable<BuildingCollegeVM>> GetAll()
        {
            List<BuildingCollegeVM> BulList = new List<BuildingCollegeVM>();

            using (OracleConnection sqlcon = new OracleConnection(con))
            {
                OracleCommand sqlcom = new OracleCommand("Get_College_Building", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter
                {
                    Direction = ParameterDirection.Output,
                    OracleDbType = OracleDbType.RefCursor,

                };
                sqlcom.Parameters.Add(res);

                sqlcon.Open();
                OracleDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    BuildingCollegeVM buldding = new BuildingCollegeVM();

                    buldding.ID = Convert.ToInt32(dr["ID"]);
                    buldding.buldingName = dr["buldingName"].ToString();
                    buldding.buldingnumber = Convert.ToInt32(dr["buldingnumber"]);
                    buldding.BuldingCollageName = (dr["BuldingCollageName"]).ToString();

                    BulList.Add(buldding);
                }

                sqlcon.Close();
                return BulList;
            }
        }

        public async Task<BuildingCollegeVM> GetById(int id)
        {
            BuildingCollegeVM BuildingInfo = new BuildingCollegeVM();

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Get_Building_By_ID", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", id);

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    BuildingInfo.ID = Convert.ToInt32(dr["ID"]);
                    BuildingInfo.buldingName = dr["buldingName"].ToString();
                    BuildingInfo.buldingnumber = Convert.ToInt32(dr["buldingnumber"]);
                    BuildingInfo.BuldingCollageName = dr["BuldingCollageName"].ToString();
                    BuildingInfo.BuldingCollageNumber = Convert.ToInt32(dr["BuldingCollageNumber"]);
                }
                sqlcon.Close();

                return BuildingInfo;
            }
            //throw new NotImplementedException();
        }

        public async Task<BaseResponse> update(Building building)
        {
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Building_Update", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("Id", building.ID);
                sqlcom.Parameters.AddWithValue("Name", building.Name);
                sqlcom.Parameters.AddWithValue("Number", building.Number);
                sqlcom.Parameters.AddWithValue("College_Id", building.Collage_ID);


                sqlcon.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "تم تعديل بيانات المبنى بنجاح",
                        Type = "success",
                        IsSuccess = true

                    };
                }
                else
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "لم يتم تعديل البيانات الخاصة بالمبنى بنجاح لوجود بيانات مماثلة",
                        Type = "success",
                        IsSuccess = false
                    };

                }
            }

            //throw new NotImplementedException();
        }

    }
}
