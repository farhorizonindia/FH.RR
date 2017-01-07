﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


public class DALBookingLock
{
    string strCon = string.Empty;
    public DALBookingLock()
    {
        strCon = ConfigurationManager.ConnectionStrings["ReservationConnectionString"].ConnectionString;
    }

    #region Get Data
    public void PlaceLock(BALBookingLock balBookingLock)
    {
        try
        {
            if (balBookingLock == null || balBookingLock.LockRooms == null)
                return;

            SqlConnection cn = new SqlConnection(strCon);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            foreach (var lockRoom in balBookingLock.LockRooms)
            {
                string query = string.Format("insert into tblBookingLock(AccomId, LockIdentifier, RoomCategoryId, RoomNo, LockExpireAt) values ({0},'{1}',{2},'{3}','{4}')",
                    balBookingLock.AccomId,
                    balBookingLock.LockIdentifier,
                    lockRoom.RoomCategoryId,
                    lockRoom.RoomNo,
                    balBookingLock.LockExpireAt);

                cmd.CommandText = query;
                var exists = cmd.ExecuteNonQuery();
            }
            cn.Close();
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public void ReleaseLock(BALBookingLock balBookingLock)
    {
        try
        {
            string query = string.Format("delete from tblBookingLock where AccomId = {0} and LockIdentifier = '{1}'",
                balBookingLock.AccomId,
                balBookingLock.LockIdentifier);

            SqlConnection cn = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cn.Open();

            cmd.ExecuteNonQuery();

            #region Also Delete All the expired locks, if there are any.
            query = string.Format("delete from tblBookingLock where LockExpireAt < {0}",
                                DateTime.Now);
            cmd.ExecuteNonQuery();
            #endregion

            cn.Close();
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }

    public bool IsLocked(BALBookingLock balBookingLock)
    {
        bool lockFound = false;
        try
        {
            SqlConnection cn = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cn.Open();

            foreach (var lockRoom in balBookingLock.LockRooms)
            {
                string query = string.Format("select isnull(Id, 0) from tblBookingLock where AccomId = {0} and RoomCategoryId = {1} and RoomNo = {2} and LockExpireAt > {4}",
                balBookingLock.AccomId,
                lockRoom.RoomCategoryId,
                lockRoom.RoomNo, DateTime.Now);
                cmd.CommandText = query;
                var exists = cmd.ExecuteScalar();

                if (int.Parse(exists.ToString()) > 0)
                {
                    lockFound = true;
                    break;
                }
            }
            cn.Close();
            return lockFound;
        }
        catch (Exception exp)
        {
            throw exp;
        }
    }
    #endregion
}