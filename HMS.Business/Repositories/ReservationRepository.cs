//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace HMS.Business.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string con;

        public ReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<BaseResponse> create(Reservation reservation)
        {
            
            using (OracleConnection oracon = new OracleConnection(con))
            {
                //OracleCommand oracom = new OracleCommand("Reservation_Create", oracon);
                OracleCommand oracom = new OracleCommand("RESERVATION_CHECK_DOBICATE", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter R_Id = new OracleParameter { ParameterName = "R_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.ID };
                OracleParameter R_Name = new OracleParameter { ParameterName = "R_Name", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Name };
                OracleParameter R_Hall_Id = new OracleParameter { ParameterName = "R_Hall_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Hall_Id };
                OracleParameter R_Date = new OracleParameter { ParameterName = "R_Date", OracleDbType = OracleDbType.Date, Size = 255, Direction = ParameterDirection.Input,Value= reservation.Date };
                OracleParameter R_Time_Start = new OracleParameter { ParameterName = "R_Time_Start", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input,Value = reservation.Time_Start };
                OracleParameter R_Time_End = new OracleParameter { ParameterName = "R_Time_End", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input , Value = reservation.Time_End};
                OracleParameter R_User_Id = new OracleParameter { ParameterName = "R_User_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input , Value= reservation.User_id };
                OracleParameter R_Q_state = new OracleParameter { ParameterName = "R_Q_state", OracleDbType = OracleDbType.Varchar2, Size = 255, Direction = ParameterDirection.Input, Value = "create" };

                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(R_Id);
                oracom.Parameters.Add(R_Name);
                oracom.Parameters.Add(R_Hall_Id);
                oracom.Parameters.Add(R_Date);
                oracom.Parameters.Add(R_Time_Start);
                oracom.Parameters.Add(R_Time_End);
                oracom.Parameters.Add(R_User_Id);
                oracom.Parameters.Add(qres);
                oracom.Parameters.Add(R_Q_state);

                oracon.Open(); 
                oracom.ExecuteNonQuery(); 
                if (oracom.Parameters["qres"].Value.ToString() == "true")
                {
                    oracon.Close();
                    using (OracleConnection oracon2 = new OracleConnection(con))
                    {
                        OracleCommand oracom2 = new OracleCommand("RESERVATION_CREATEBYCONDITIONS", oracon2);
                        oracom2.CommandType = CommandType.StoredProcedure;

                        OracleParameter R_Name2 = new OracleParameter { ParameterName = "R_Name2", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Name };
                        OracleParameter R_Hall_Id2 = new OracleParameter { ParameterName = "R_Hall_Id2", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Hall_Id };
                        OracleParameter R_Date2 = new OracleParameter { ParameterName = "R_Date2", OracleDbType = OracleDbType.Date, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Date };
                        OracleParameter R_Time_Start2 = new OracleParameter { ParameterName = "R_Time_Start2", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_Start };
                        OracleParameter R_Time_End2 = new OracleParameter { ParameterName = "R_Time_End2", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_End };
                        OracleParameter R_User_Id2 = new OracleParameter { ParameterName = "R_User_Id2", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.User_id };

                        OracleParameter qres2 = new OracleParameter { ParameterName = "qres2", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                        oracom2.Parameters.Add(R_Name2);
                        oracom2.Parameters.Add(R_Hall_Id2);
                        oracom2.Parameters.Add(R_Date2);
                        oracom2.Parameters.Add(R_Time_Start2);
                        oracom2.Parameters.Add(R_Time_End2);
                        oracom2.Parameters.Add(R_User_Id2);
                        oracom2.Parameters.Add(qres2);

                        oracon2.Open();
                        oracom2.ExecuteNonQuery();
                        if (oracom2.Parameters["qres2"].Value.ToString() == "success")
                        {
                            oracon2.Close();
                            return new BaseResponse
                            {
                                Message = "تم إضافة حجز القاعة بنجاح",
                                Type = "success",
                                IsSuccess = true
                            };
                        }
                        else
                        {
                            oracon2.Close();
                            return new BaseResponse
                            {
                                Message = "لم تتم إضافة الحجز بنجاح الرجاء التحقق من وقت البداية النهاية ",
                                Type = "error",
                                IsSuccess = false
                            };
                        }
                    }                   
                }
                else
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "لم تتم إضافة الحجز امل لوجود حجز مماثل او خطا في اوقات الحجز ",
                        Type = "error",
                        IsSuccess = false
                    };
                }
            }

        }

		public async Task<BaseResponse> Delete(int id)
		{
            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("RESERVATION_DELETE", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter R_Id = new OracleParameter { ParameterName = "R_Id", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Input, Value = id };
                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(R_Id);
                oracom.Parameters.Add (qres);

                oracon.Open();
                oracom.ExecuteNonQuery();
                if (oracom.Parameters["qres"].Value.ToString() == "success")
                {
                    return new BaseResponse
                    {
                        Message = "تم إضافة حجز القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                    };
                }
                else
                {
					return new BaseResponse
					{
						Message = "لم يتم حذف الحجز ",
						Type = "error",
						IsSuccess = false
					};
				}
            }
		}

		public async Task<IEnumerable<ReservationHallVM>> GetAll()
		{

            List<ReservationHallVM> reservationList = new List<ReservationHallVM>();

            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Reservation_GetAll",oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);


                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    ReservationHallVM reservation = new ReservationHallVM
                    {
                        Id = Convert.ToInt32(dr["ID"]),
						Name = dr["Name"].ToString(),
                        Hall_name = dr["Hall_Name"].ToString(),
                        Date = Convert.ToDateTime(dr["RESERVATION_DATE"]),
						Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
						Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
						User_id = Convert.ToInt32(dr["User_Id"]),
                        User_Name = dr["User_Name"].ToString()
                    };

					reservationList.Add(reservation);
                    
                }
                oracon.Close();
                return reservationList;
              

            }
               // throw new NotImplementedException();
		}

        public  List<ReservationHallVM> GetAllpaging(Nullable<int> pageno, string filter, Nullable<int> pagesize, string sorting, string sortOrder)
        {

            //var pagenoParameter = pageno.HasValue ?
            //    new ObjectParameter("Pageno", pageno) :
            //    new ObjectParameter("Pageno", typeof(int));

            //var filterParameter = filter != null ?
            //    new ObjectParameter("filter", filter) :
            //    new ObjectParameter("filter", typeof(string));

            //var pagesizeParameter = pagesize.HasValue ?
            //    new ObjectParameter("pagesize", pagesize) :
            //    new ObjectParameter("pagesize", typeof(int));

            //var sortingParameter = sorting != null ?
            //    new ObjectParameter("Sorting", sorting) :
            //    new ObjectParameter("Sorting", typeof(string));

            //var sortOrderParameter = sortOrder != null ?
            //    new ObjectParameter("SortOrder", sortOrder) :
            //    new ObjectParameter("SortOrder", typeof(string));

            //var d =  ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReservationHallVM>("RESERVATION_GETALL_PAGING", pagenoParameter, filterParameter, pagesizeParameter, sortingParameter, sortOrderParameter);
            
            using (OracleConnection oracon = new OracleConnection(con))
            {
                List<ReservationHallVM> reservationList = new List<ReservationHallVM>();
                int totalreservations;

                OracleCommand oracom = new OracleCommand("RESERVATION_GETALL_PAGING", oracon);
                oracom.CommandType = CommandType.StoredProcedure;
                

                
                OracleParameter dbfilter = new OracleParameter { ParameterName = "dbfilter", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input,Value=filter};
                OracleParameter dbsorting = new OracleParameter { ParameterName = "dbsorting", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input,Value= sorting };
                OracleParameter dbsortingtype = new OracleParameter { ParameterName = "dbsortingtype", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = sortOrder };
                OracleParameter total = new OracleParameter { ParameterName = "total", OracleDbType = OracleDbType.Int32, Direction = ParameterDirection.Output };

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };



               
                oracom.Parameters.Add(dbfilter);
                oracom.Parameters.Add(dbsorting);
                oracom.Parameters.Add(dbsortingtype);
                oracom.Parameters.Add(total);


                oracom.Parameters.Add(res);


                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {                    
                    ReservationHallVM reservation = new ReservationHallVM
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Name = dr["Name"].ToString(),
                        Hall_name = dr["Hall_Name"].ToString(),
                        Date = Convert.ToDateTime(dr["RESERVATION_DATE"]),
                        Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
                        Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
                        User_id = Convert.ToInt32(dr["User_Id"]),
                        User_Name = dr["User_Name"].ToString()
                    };
                    reservationList.Add(reservation);

                }
                if (dr.NextResult() == true)
                {
                    while (dr.Read())
                    {
                        //Second cursor goes here
                    }
                }
                oracon.Close();

                totalreservations = Convert.ToInt32(oracom.Parameters["total"].Value);


                return reservationList;


            }

        }

        // throw new NotImplementedException();


        public async Task<ReservationHallVM> GetById(int id) 
		{
            ReservationHallVM reservation = new ReservationHallVM();

			using (OracleConnection oracon = new OracleConnection(con))
			{
				OracleCommand oracom = new OracleCommand("Reservation_GetById", oracon);

                OracleParameter R_ID = new OracleParameter { ParameterName = "R_ID", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = id };
                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(R_ID);
                oracom.Parameters.Add(res);
                //oracom.Parameters.AddWithValue("ID", id);

				oracom.CommandType = CommandType.StoredProcedure;

				oracon.Open();
				OracleDataReader dr = oracom.ExecuteReader();
				while (dr.Read())
				{
					ReservationHallVM reserv = new ReservationHallVM
					{
                        Id= Convert.ToInt32(dr["Id"]),
						Name = dr["Name"].ToString(),
						Hall_name = dr["Hall_Name"].ToString(),
						Date = Convert.ToDateTime(dr["RESERVATION_DATE"]),
						Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
						Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
						User_id = Convert.ToInt32(dr["User_Id"]),

					};

                    reservation = reserv;
				}
                oracon.Close();
                return reservation;
				

			}
		}

		public async Task<IEnumerable<ReservationHallVM>> GetByUserId(string id)
        {
            List<ReservationHallVM> reservationList = new List<ReservationHallVM>();

            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Reservation_GetByUserId", oracon);


                OracleParameter R_User_Id = new OracleParameter { ParameterName = "R_User_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = id };
                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output};
                oracom.Parameters.Add(R_User_Id);
                oracom.Parameters.Add(res);

                //oracom.Parameters.AddWithValue("User_Id", id);
                oracom.CommandType = CommandType.StoredProcedure;

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    ReservationHallVM reservation = new ReservationHallVM
                    {
                        Name = dr["Name"].ToString(),
                        Hall_name = dr["Hall_Name"].ToString(),
                        Date = Convert.ToDateTime(dr["RESERVATION_DATE"]),
                        Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
                        Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
                        User_id = Convert.ToInt32(dr["User_Id"]),
                        
                    };

                    reservationList.Add(reservation);
                }
                oracon.Close();
                return reservationList;
               

            }
            //throw new NotImplementedException();
        }


		public async Task<BaseResponse> update(Reservation reservation)
		{
            using(OracleConnection oracon = new OracleConnection(con)) 
            { 

                //OracleCommand oracom = new OracleCommand("Reservation_Create", oracon);
                OracleCommand oracom = new OracleCommand("RESERVATION_CHECK_DOBICATE", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter R_Id = new OracleParameter { ParameterName = "R_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.ID };
                OracleParameter R_Name = new OracleParameter { ParameterName = "R_Name", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Name };
                OracleParameter R_Hall_Id = new OracleParameter { ParameterName = "R_Hall_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Hall_Id };
                OracleParameter R_Date = new OracleParameter { ParameterName = "R_Date", OracleDbType = OracleDbType.Date, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Date };
                OracleParameter R_Time_Start = new OracleParameter { ParameterName = "R_Time_Start", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_Start };
                OracleParameter R_Time_End = new OracleParameter { ParameterName = "R_Time_End", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_End };
                OracleParameter R_User_Id = new OracleParameter { ParameterName = "R_User_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.User_id };
                OracleParameter R_Q_state = new OracleParameter { ParameterName = "R_Q_state", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = "update" };

                OracleParameter qres = new OracleParameter { ParameterName = "qres", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                oracom.Parameters.Add(R_Id);
                oracom.Parameters.Add(R_Name);
                oracom.Parameters.Add(R_Hall_Id);
                oracom.Parameters.Add(R_Date);
                oracom.Parameters.Add(R_Time_Start);
                oracom.Parameters.Add(R_Time_End);
                oracom.Parameters.Add(R_User_Id);
                oracom.Parameters.Add(qres);
                oracom.Parameters.Add(R_Q_state);

                oracon.Open();
                oracom.ExecuteNonQuery();
                if (oracom.Parameters["qres"].Value.ToString() == "true")
                {
                    oracon.Close();
                    using (OracleConnection oracon2 = new OracleConnection(con))
                    {
                        OracleCommand oracom2 = new OracleCommand("RESERVATION_UPDATE", oracon2);
                        oracom2.CommandType = CommandType.StoredProcedure;
                        OracleParameter R_Id2 = new OracleParameter { ParameterName = "R_Id", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.ID };
                        OracleParameter R_Name2 = new OracleParameter { ParameterName = "R_Name2", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Name };
                        OracleParameter R_Hall_Id2 = new OracleParameter { ParameterName = "R_Hall_Id2", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Hall_Id };
                        OracleParameter R_Date2 = new OracleParameter { ParameterName = "R_Date2", OracleDbType = OracleDbType.Date, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Date };
                        OracleParameter R_Time_Start2 = new OracleParameter { ParameterName = "R_Time_Start2", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_Start };
                        OracleParameter R_Time_End2 = new OracleParameter { ParameterName = "R_Time_End2", OracleDbType = OracleDbType.TimeStamp, Size = 255, Direction = ParameterDirection.Input, Value = reservation.Time_End };
                        OracleParameter R_User_Id2 = new OracleParameter { ParameterName = "R_User_Id2", OracleDbType = OracleDbType.Int32, Size = 255, Direction = ParameterDirection.Input, Value = reservation.User_id };

                        OracleParameter qres2 = new OracleParameter { ParameterName = "qres2", OracleDbType = OracleDbType.NVarchar2, Size = 255, Direction = ParameterDirection.Output };

                        oracom2.Parameters.Add(R_Id2);
                        oracom2.Parameters.Add(R_Name2);
                        oracom2.Parameters.Add(R_Hall_Id2);
                        oracom2.Parameters.Add(R_Date2);
                        oracom2.Parameters.Add(R_Time_Start2);
                        oracom2.Parameters.Add(R_Time_End2);
                        oracom2.Parameters.Add(R_User_Id2);

                        oracom2.Parameters.Add(qres2);

                        oracon2.Open();
                        oracom2.ExecuteNonQuery();
                        if (oracom2.Parameters["qres2"].Value.ToString() == "success")
                        {
                            oracon2.Close();
                            return new BaseResponse
                            {
                                Message = "تم تعديل حجز القاعة بنجاح",
                                Type = "success",
                                IsSuccess = true
                            };
                        }
                        else
                        {
                            oracon2.Close();
                            return new BaseResponse
                            {
                                Message = "لم يتم تعديل الحجز بنجاح لوجود حجز مطبق للتريخ والوقت",
                                Type = "error",
                                IsSuccess = false
                            };
                        }
                    }
                }
                else
                {
                    oracon.Close();
                    return new BaseResponse
                    {
                        Message = "لم يتم تعديل  الحجز بنجاح لوجود حجز مطبق للتريخ والوقت",
                        Type = "error",
                        IsSuccess = false
                    };
                }
            }
        }
	}
}