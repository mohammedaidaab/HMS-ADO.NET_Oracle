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
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;

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
            

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                //SqlCommand sqlcom = new SqlCommand("Reservation_Create", sqlcon);
                SqlCommand sqlcom = new SqlCommand("Reservation_CreateByConditions", sqlcon);

                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcom.Parameters.AddWithValue("Name", reservation.Name);
                sqlcom.Parameters.AddWithValue("Hall_Id", reservation.Hall_Id);
                sqlcom.Parameters.AddWithValue("Date", reservation.Date);
                sqlcom.Parameters.AddWithValue("Time_Start", reservation.Time_Start);
                sqlcom.Parameters.AddWithValue("Time_End", reservation.Time_End);
                sqlcom.Parameters.AddWithValue("User_Id", reservation.User_id);


                sqlcon.Open();
                if (sqlcom.ExecuteNonQuery() > 0)
                {
                     sqlcon.Close();
                     return new BaseResponse
                     {
                        Message = "تم إضافة حجز القاعة بنجاح",
                        Type = "success",
                        IsSuccess = true
                     };
                }
                else
                {
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        Message = "لم تتم إضافة الحجز بنجاح لوجود حجز مطبق للتريخ والوقت",
						Type = "warning",
						IsSuccess = false
                    };

                }


            }


            //throw new NotImplementedException();
        }

		public async Task<IEnumerable<ReservationHallVM>> GetAll()
		{

            List<ReservationHallVM> reservationList = new List<ReservationHallVM>();

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Reservation_GetAll",sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    ReservationHallVM reservation = new ReservationHallVM
                    {
                        Id = Convert.ToInt32(dr["ID"]),
						Name = dr["Name"].ToString(),
                        Hall_name = dr["Hall_Name"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
						Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
						Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
						User_id = Convert.ToInt32(dr["User_Id"]),
                        User_Name = dr["User_Name"].ToString()
                    };

					reservationList.Add(reservation);
				}

                return reservationList;
                sqlcon.Close();

            }
               // throw new NotImplementedException();
		}

		public async Task<ReservationHallVM> GetById(int id)
		{
            ReservationHallVM reservation = new ReservationHallVM();

			using (SqlConnection sqlcon = new SqlConnection(con))
			{
				SqlCommand sqlcom = new SqlCommand("Reservation_GetById", sqlcon);

				sqlcom.Parameters.AddWithValue("ID", id);

				sqlcom.CommandType = CommandType.StoredProcedure;

				sqlcon.Open();
				SqlDataReader dr = sqlcom.ExecuteReader();
				while (dr.Read())
				{
					ReservationHallVM res = new ReservationHallVM
					{
                        Id= Convert.ToInt32(dr["Id"]),
						Name = dr["Name"].ToString(),
						Hall_name = dr["Hall_Name"].ToString(),
						Date = Convert.ToDateTime(dr["Date"]),
						Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
						Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
						User_id = Convert.ToInt32(dr["User_Id"]),

					};

                    reservation = res;
				}

				return reservation;
				sqlcon.Close();

			}
		}

		public async Task<IEnumerable<ReservationHallVM>> GetByUserId(string id)
        {
            List<ReservationHallVM> reservationList = new List<ReservationHallVM>();

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Reservation_GetByUserId", sqlcon);
                sqlcom.Parameters.AddWithValue("User_Id", id);
                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    ReservationHallVM reservation = new ReservationHallVM
                    {
                        Name = dr["Name"].ToString(),
                        Hall_name = dr["Hall_Name"].ToString(),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Time_Start = Convert.ToDateTime(dr["Time_Start"].ToString()),
                        Time_End = Convert.ToDateTime(dr["Time_End"].ToString()),
                        User_id = Convert.ToInt32(dr["User_Id"]),
                        
                    };

                    reservationList.Add(reservation);
                }

                return reservationList;
                sqlcon.Close();

            }
            //throw new NotImplementedException();
        }

		public async Task<BaseResponse> update(Reservation reservation)
		{
            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Reservation_Update", sqlcon);

                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("ID", reservation.ID);
                sqlcom.Parameters.AddWithValue("Name", reservation.Name);
                sqlcom.Parameters.AddWithValue("Hall_Id", reservation.Hall_Id);
                sqlcom.Parameters.AddWithValue("Date", reservation.Date);
                sqlcom.Parameters.AddWithValue("Time_Start", reservation.Time_Start);
                sqlcom.Parameters.AddWithValue("Time_End", reservation.Time_End);
                sqlcom.Parameters.AddWithValue("User_Id", reservation.User_id);


                sqlcon.Open();
                
                if (sqlcom.ExecuteNonQuery() > 0) {
                    
                    sqlcon.Close();
                    return new BaseResponse
                    {
                        IsSuccess = true,
						Type = "success",
						Message = "تم تعديل بيانت حجز القاعة بنجاح"

                    };
                }
                else
                {
                    sqlcon.Close();

                    return new BaseResponse {
                        IsSuccess = false,
						Type = "warning",
						Message = "الرجاء مراجعة البانات المدخلة حيث انه لم نتمكن من اكمل عملية تحديث البانات" ,
                    };

                }


            }
		}
	}

}
