//////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab
//For : King Faisual University
//Under : ISB integrated sulution business Company
//Halls ManageMent System
/////////////////////////////////////////////////////

using HMS.Domain.Entities;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Business.Repositories
{
    public class DashBoredRepository : IDashBordRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string con;

        public DashBoredRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con = configuration.GetConnectionString("DefaultConnection");
        }

        public int GetActiveHalls()
        {
            int activehalls = 0;

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Dashbord_Active_halls_number", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    int count = 0;
                    count = Convert.ToInt32(dr["Active_Halls"]);
                    activehalls = count;
                }

                sqlcon.Close();
                return activehalls;

            }

            // throw new NotImplementedException();
        }

		public DateTime GetLastReservation()
		{
			DateTime hallsnumber =  Convert.ToDateTime("01-01-1111");

			using (SqlConnection sqlcon = new SqlConnection(con))
			{
				SqlCommand sqlcom = new SqlCommand("Dashbord_LastReservation_Today", sqlcon);
				sqlcom.CommandType = CommandType.StoredProcedure;

				sqlcon.Open();
				SqlDataReader dr = sqlcom.ExecuteReader();
				while (dr.Read())
				{
					DateTime count;
                    if(dr["MaxTime"] != DBNull.Value)
                    {
                        count = Convert.ToDateTime(dr["MaxTime"].ToString());
                        hallsnumber = count;
					}
                    
					
				}

				sqlcon.Close();
				return hallsnumber;

			}
		}

		public int getnumberofalls()
        {
            int hallsnumber = 0;

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Dashbord_halls_number", sqlcon);
                sqlcom.CommandType = CommandType.StoredProcedure;

                sqlcon.Open();
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    int count = 0;
                    count = Convert.ToInt32(dr["HallsNumer"]);
                    hallsnumber = count;
                }

                sqlcon.Close();
                return hallsnumber;

            }
        }

        public int gettodayreservations()
        {
            int todayreservations = 0;

            using (OracleConnection sqlcon = new OracleConnection(con))
            {
                OracleCommand sqlcom = new OracleCommand("Dashbord_today_reservations", sqlcon);
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
                    int count = 0;
                    count = Convert.ToInt32(dr["total"]);
                    todayreservations = count;
                }

                sqlcon.Close();
                return todayreservations;

            }
        }

        public List<ReservationHallVM> reservations()
        {
            List<ReservationHallVM> reservationList = new List<ReservationHallVM>();

            using (SqlConnection sqlcon = new SqlConnection(con))
            {
                SqlCommand sqlcom = new SqlCommand("Reservation_GetAll", sqlcon);
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
                        User_Name = dr["User_Name"].ToString()
                    };

                    reservationList.Add(reservation);
                }

                return reservationList;
            }
        }
    }
}
