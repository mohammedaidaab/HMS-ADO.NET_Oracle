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
using System.Reflection;
using HMS.Data;
using System.Security.Cryptography;

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
			using (OracleConnection oracon = new(con))
            {
                OracleCommand oracom = new ("Building_create", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter qres = new(){ ParameterName = "qres", Direction = ParameterDirection.Output, OracleDbType = OracleDbType.NVarchar2, Size = 200, };
                OracleParameter BName = new(){ Direction = ParameterDirection.Input, OracleDbType = OracleDbType.NVarchar2, Value = buildingCollegeVM.buldingName.ToString(), };
                OracleParameter BNumber = new(){ Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = buildingCollegeVM.buldingnumber, };
                OracleParameter BCollege_Id = new(){ Direction = ParameterDirection.Input, OracleDbType = OracleDbType.Int32, Value = buildingCollegeVM.BuldingCollageNumber, };

                oracom.Parameters.Add(BName);
                oracom.Parameters.Add(BNumber);
                oracom.Parameters.Add(BCollege_Id);
                oracom.Parameters.Add(qres);

                try
				{
                    oracon.Open();
                    await oracom.ExecuteNonQueryAsync();

					if (oracom.Parameters["qres"].Value.ToString() == "success" )
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
        }

        public async Task<BaseResponse> delete(int id)
        {
            using (OracleConnection oracon = new(con))
            {
                OracleCommand oracom = new ("Building_Delete", oracon);

                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter B_id = new(){ ParameterName = "B_id", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = id };
                OracleParameter qres = new(){ ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(B_id);
                oracom.Parameters.Add(qres);
                

                oracon.Open();
                await oracom.ExecuteNonQueryAsync();

                if (oracom.Parameters["qres"].Value.ToString() ==  "success")
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم حذف بيانات المبنى بنجاح",
                        Type = "success",
                        IsSuccess = true

                    };
                }
                else
                {
                    oracon.Close();
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

            using (OracleConnection oracon = new(con))
            {
                OracleCommand oracom = new ("Get_College_Building", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new (){Direction = ParameterDirection.Output,OracleDbType = OracleDbType.RefCursor,};

                oracom.Parameters.Add(res);

              
                using (OracleDataAdapter sda = new OracleDataAdapter("Get_College_Building", oracon))
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                    OracleParameter res2 = new(){ Direction = ParameterDirection.Output, OracleDbType = OracleDbType.RefCursor, };

                    sda.SelectCommand.Parameters.Add(res2);

                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    var d = ds.Tables;

					var empList = ds.Tables[0].AsEnumerable()
	                .Select(dataRow => new HallBuildingVM
	                {
		                Name = dataRow.Field<string>("buldingName")
	                }).ToList();
				}

                oracon.Open();
                OracleDataReader dr = (OracleDataReader)await oracom.ExecuteReaderAsync();

                while (dr.Read())
                {
                    BuildingCollegeVM buldding = new BuildingCollegeVM();

                    buldding.ID = Convert.ToInt32(dr["ID"]);
                    buldding.buldingName = dr["buldingName"].ToString();
                    buldding.buldingnumber = Convert.ToInt32(dr["buldingnumber"]);
                    buldding.BuldingCollageName = (dr["BuldingCollageName"]).ToString();

                    BulList.Add(buldding);
                }

                oracon.Close();
                return BulList;
            }
        }

        public async Task<BuildingCollegeVM> GetById(int id)
        {
            BuildingCollegeVM BuildingInfo = new BuildingCollegeVM();

            using (OracleConnection oracon = new(con))
            {
                OracleCommand oracom = new ("BUILDING_GET_BYID", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


                OracleParameter B_id = new(){ ParameterName = "B_id", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = id };
                OracleParameter res = new(){ ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(B_id);
                oracom.Parameters.Add(res);
               
                oracon.Open();
                OracleDataReader dr = (OracleDataReader)await oracom.ExecuteReaderAsync();
                while (dr.Read())
                {
                    BuildingInfo.ID = Convert.ToInt32(dr["ID"]);
                    BuildingInfo.buldingName = dr["buldingName"].ToString();
                    BuildingInfo.buldingnumber = Convert.ToInt32(dr["buldingnumber"]);
                    BuildingInfo.BuldingCollageName = dr["BuldingCollageName"].ToString();
                    BuildingInfo.BuldingCollageNumber = Convert.ToInt32(dr["BuldingCollageNumber"]);
                }
                oracon.Close();

                return BuildingInfo;
            }
        }

        public async Task<BaseResponse> update(Building building)
        {
            using (OracleConnection oracon = new(con))
            {
                OracleCommand oracom = new ("Building_Update", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


                OracleParameter B_Id = new(){ ParameterName = "B_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = building.ID };
                OracleParameter B_Name = new(){ParameterName = "B_Id", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = building.Name };
                OracleParameter B_Number = new(){ParameterName = "B_Number", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = building.Number };
                OracleParameter College_Id = new(){ParameterName = "College_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = building.Collage_ID };
                OracleParameter qres = new(){ ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(B_Id);
                oracom.Parameters.Add(B_Name);
                oracom.Parameters.Add(B_Number);
                oracom.Parameters.Add(College_Id);
                oracom.Parameters.Add(qres);

                oracon.Open();
                await oracom.ExecuteNonQueryAsync();
                if (oracom.Parameters["qres"].Value.ToString() == "success")
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "تم تعديل بيانات المبنى بنجاح",
                        Type = "success",
                        IsSuccess = true

                    };
                }
                else
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "لم يتم تعديل البيانات الخاصة بالمبنى بنجاح لوجود بيانات مماثلة",
                        Type = "success",
                        IsSuccess = false
                    };

                }
            }

        }

        public BuildingCollegePagingVM GetAllpaging(Nullable<int> pageno, string filter, Nullable<int> pagesize, string sorting, string sortOrder)
        {
            using (OracleConnection oracon = new(con))
            {

                List<BuildingCollegeVM> BulList = new List<BuildingCollegeVM>();
                int totalreservations;

                OracleCommand oracom = new ("GET_COLLEGE_BUILDING_PAGING", oracon);
                oracom.CommandType = CommandType.StoredProcedure;


                OracleParameter dbpageno = new(){ ParameterName = "dbpageno", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = pageno };
                OracleParameter dbpagesize = new(){ ParameterName = "dbpagesize", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = pagesize };
                OracleParameter dbfilter = new(){ ParameterName = "dbfilter", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = filter };
                OracleParameter dbsorting = new(){ ParameterName = "dbsorting", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = sorting };
                OracleParameter dbsortingtype = new(){ ParameterName = "dbsortingtype", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = sortOrder };
                OracleParameter total = new(){ ParameterName = "total", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Output };

                OracleParameter res = new(){ ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };


                oracom.Parameters.Add(dbpageno);
                oracom.Parameters.Add(dbpagesize);
                oracom.Parameters.Add(dbfilter);
                oracom.Parameters.Add(dbsorting);
                oracom.Parameters.Add(dbsortingtype);
                oracom.Parameters.Add(total);


                oracom.Parameters.Add(res);


                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    BuildingCollegeVM buldding = new BuildingCollegeVM();

                    buldding.ID = Convert.ToInt32(dr["ID"]);
                    buldding.buldingName = dr["buldingName"].ToString();
                    buldding.buldingnumber = Convert.ToInt32(dr["buldingnumber"]);
                    buldding.BuldingCollageName = (dr["BuldingCollageName"]).ToString();

                    BulList.Add(buldding);
                }

                oracon.Close();
                totalreservations = int.Parse(oracom.Parameters["total"].Value.ToString()); ;

                BuildingCollegePagingVM Buildinglist = new BuildingCollegePagingVM
                {
                    colleges = BulList,
                    totalPages = totalreservations
                };

                return Buildinglist;


            }

        }

    }
}
