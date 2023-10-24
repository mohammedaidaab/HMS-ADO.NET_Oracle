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
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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

            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Dashbord_Active_halls_number", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    int count = 0;
                    count = Convert.ToInt32(dr["Active_Halls"]);
                    activehalls = count;
                }

                oracon.Close();
                return activehalls;

            }

            // throw new NotImplementedException();
        }

		public DateTime GetLastReservation()
		{
			DateTime hallsnumber =  Convert.ToDateTime("01-01-1111");

			using (OracleConnection oracon = new OracleConnection(con))
			{
				OracleCommand oracom = new OracleCommand("Dashbord_LastReservation_Today", oracon);
				oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);

                oracon.Open();
				OracleDataReader dr = oracom.ExecuteReader();
				while (dr.Read())
				{
					string count;
                    if(dr["MaxTime"] != DBNull.Value)
                    {
                        count = dr["MaxTime"].ToString();
                        string time  = count.Replace(".", ":");
                        count = DateTime.ParseExact("01-01-1111 "+ time + ":00", "MM-dd-yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToString();
                        hallsnumber = Convert.ToDateTime(count);
					}
				}

				oracon.Close();
				return hallsnumber;

			}
		}

		public int getnumberofalls()
        {
            int hallsnumber = 0;

            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Dashbord_halls_number", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);

                oracon.Open();
                OracleDataReader dr = oracom.ExecuteReader();
                while (dr.Read())
                {
                    int count = 0;
                    count = Convert.ToInt32(dr["HallsNumer"]);
                    hallsnumber = count;
                }
                oracon.Close();
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


                OracleParameter res = new OracleParameter{Direction = ParameterDirection.Output, OracleDbType = OracleDbType.RefCursor,};
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

            using (OracleConnection oracon = new OracleConnection(con))
            {
                OracleCommand oracom = new OracleCommand("Reservation_GetAll", oracon);
                oracom.CommandType = CommandType.StoredProcedure;

                OracleParameter res = new OracleParameter { ParameterName = "res", OracleDbType = OracleDbType.RefCursor, Size = 255, Direction = ParameterDirection.Output };
                oracom.Parameters.Add(res);

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
                        User_Name = dr["User_Name"].ToString()
                    };

                    reservationList.Add(reservation);
                }

                return reservationList;
            }
        }
    }
}
