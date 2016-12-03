using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using FarHorizon.Reservations.DataBaseManager;
using FarHorizon.Reservations.Common;
using FarHorizon.Reservations.Common.DataEntities.Masters;
using FarHorizon.Reservations.Common.DataEntities.Client;
using FarHorizon.Reservations.Common.DataEntities;

namespace FarHorizon.Reservations.BusinessTier.BusinessLogic.BookingEngine
{
    internal class BookingRoomReleaseHandler
    {
        public Accomodation GetReleasedRooms(int BookingId)
        {
            Accomodation accomodation;
            DataSet dsAccomodationData;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;

            dsAccomodationData = null;
            accomodation = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_ReleasedRooms";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);

                dsAccomodationData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsAccomodationData = null;
                GF.LogError("clsBookingRoomReleaseHandler.GetReleasedRooms", exp.Message);
            }

            #region Populate the Object
            if (dsAccomodationData != null && dsAccomodationData.Tables[0].Rows.Count > 0)
            {
                accomodation = new Accomodation();                
                List<AccomodationRoomType> accomodationRoomTypes = null;
                List<AccomodationRoom> accomodationRooms = null;                

                for (int i = 0; i < dsAccomodationData.Tables[0].Rows.Count; i++)
                {
                    dr = dsAccomodationData.Tables[0].Rows[i];
                    int AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(3));
                    int bId = Convert.ToInt32(dr.ItemArray.GetValue(1));
                    int CategoryId = Convert.ToInt32(dr.ItemArray.GetValue(11));
                    int TypeId = Convert.ToInt32(dr.ItemArray.GetValue(12));
                    string RoomCategoryName = Convert.ToString(dr.ItemArray.GetValue(13));
                    string RoomTypeName = Convert.ToString(dr.ItemArray.GetValue(14));
                    string RoomNo = Convert.ToString(dr.ItemArray.GetValue(4));

                    if (accomodation.AccomodationDetail == null)
                    {
                        AccomodationDTO accomodationDetail = new AccomodationDTO();
                        accomodationDetail.AccomodationId = AccomodationId;
                        accomodation.AccomodationDetail = accomodationDetail;
                    }

                    AccomodationRoomCategory Category = null;
                    if (accomodation.Categories != null)
                    {
                        Category = accomodation.Categories.Find(delegate(AccomodationRoomCategory CategoryTypes)
                        { return CategoryTypes.RoomCategory.RoomCategoryId == CategoryId; });

                        if (Category != null && Category.RoomTypes != null)
                            accomodationRoomTypes = Category.RoomTypes;
                        else
                            accomodationRoomTypes = null;
                    }
                    if (Category == null)
                    {
                        RoomCategoryDTO newRoomCategory = new RoomCategoryDTO();
                        newRoomCategory.RoomCategoryId = CategoryId;
                        newRoomCategory.RoomCategory = RoomCategoryName;

                        Category = new AccomodationRoomCategory();
                        Category.RoomCategory = newRoomCategory;
                        accomodationRoomTypes = null;
                        accomodationRooms = null;
                    }

                    AccomodationRoomType AccomodationRoomType = null;
                    if (Category.RoomTypes != null)
                    {
                        AccomodationRoomType = Category.RoomTypes.Find(delegate(AccomodationRoomType type)
                        { return type.RoomType.RoomTypeId == TypeId; });
                        if (AccomodationRoomType != null && AccomodationRoomType.Rooms != null)
                            accomodationRooms = AccomodationRoomType.Rooms;
                        else
                            accomodationRooms = null;
                    }
                    if (AccomodationRoomType == null)
                    {
                        RoomTypeDTO newRoomType = new RoomTypeDTO();
                        newRoomType.RoomTypeId = TypeId;
                        newRoomType.RoomType = RoomTypeName;

                        AccomodationRoomType = new AccomodationRoomType();
                        AccomodationRoomType.RoomType = newRoomType;

                        if (accomodationRoomTypes == null)
                            accomodationRoomTypes = new List<AccomodationRoomType>();
                        accomodationRoomTypes.Add(AccomodationRoomType);
                        Category.RoomTypes = accomodationRoomTypes;
                        accomodationRooms = null;
                    }

                    AccomodationRoom AccomodationRoom = null;
                    if (AccomodationRoomType.Rooms != null)
                    {
                        AccomodationRoom = AccomodationRoomType.Rooms.Find(delegate(AccomodationRoom room)
                        { return room.RoomDetail.RoomNo == RoomNo; });
                    }
                    if (AccomodationRoom == null)
                    {
                        RoomDTO newRoom = new RoomDTO();
                        newRoom.RoomNo = RoomNo;

                        AccomodationRoom = new AccomodationRoom();
                        AccomodationRoom.RoomDetail = newRoom;

                        if (accomodationRooms == null)
                            accomodationRooms = new List<AccomodationRoom>();
                        accomodationRooms.Add(AccomodationRoom);

                        AccomodationRoomType.Rooms = accomodationRooms;
                    }

                    if (accomodation.Categories == null)
                    {
                        List<AccomodationRoomCategory> categories = new List<AccomodationRoomCategory>();
                        categories.Add(Category);
                        accomodation.Categories = categories;
                    }
                    else
                    {
                        if (!accomodation.Categories.Exists(delegate(AccomodationRoomCategory CategoryTypes)
                        { return CategoryTypes.RoomCategory.RoomCategoryId == CategoryId; }))
                            accomodation.Categories.Add(Category);
                    }
                }
            }
            #endregion
            return accomodation;
        }

        public BookingDTO[] GetWaitlistedBookingsForReleasedCatType(int BookingId, int RoomCategoryId, int RoomTypeId)
        {
            DataSet dsAccomodationData;
            DataRow dr;
            string sProcName;
            DatabaseManager oDB;
            BookingDTO[] BookingDTO = null;
            dsAccomodationData = null;
            try
            {
                oDB = new DatabaseManager();
                sProcName = "up_Get_Bookings_For_Released_CatType";
                oDB.DbCmd = oDB.GetStoredProcCommand(sProcName);

                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iBookingId", DbType.Int32, BookingId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomCategoryId", DbType.Int32, RoomCategoryId);
                oDB.DbDatabase.AddInParameter(oDB.DbCmd, "@iRoomTypeId", DbType.Int32, RoomTypeId);
                dsAccomodationData = oDB.ExecuteDataSet(oDB.DbCmd);
                oDB = null;
            }
            catch (Exception exp)
            {
                oDB = null;
                dsAccomodationData = null;
                GF.LogError("clsBookingRoomReleaseHandler.GetBookingsForReleasedCatType", exp.Message);
            }
            #region Populate the Object
            if (dsAccomodationData != null && dsAccomodationData.Tables[0].Rows.Count > 0)
            {
                BookingDTO = new BookingDTO[dsAccomodationData.Tables[0].Rows.Count];
                for (int i = 0; i < dsAccomodationData.Tables[0].Rows.Count; i++)
                {
                    dr = dsAccomodationData.Tables[0].Rows[i];
                    BookingDTO[i] = new BookingDTO();
                    BookingDTO[i].BookingId = Convert.ToInt32(dr.ItemArray.GetValue(0));
                    BookingDTO[i].BookingReference = Convert.ToString(dr.ItemArray.GetValue(1));
                    BookingDTO[i].AccomodationId = Convert.ToInt32(dr.ItemArray.GetValue(2));
                    BookingDTO[i].StartDate = Convert.ToDateTime(dr.ItemArray.GetValue(3));
                    BookingDTO[i].EndDate = Convert.ToDateTime(dr.ItemArray.GetValue(4));
                }
            }
            #endregion
            return BookingDTO;
        }
    }
}
