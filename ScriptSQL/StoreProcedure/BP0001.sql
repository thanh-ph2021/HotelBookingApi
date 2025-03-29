IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[BP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].BP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Booking by id
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 28/03/2025
-- <Example>
-- EXEC BP0001 @BookingId='9F2CB823-60AD-44C4-9460-25CD21E366D6'
-- </Summary>

CREATE PROCEDURE BP0001
(
	@BookingId VARCHAR(50)
)  
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
        B.Id,
		B.RoomId,
        B.CheckInDate,
        B.CheckOutDate,
        B.TotalPrice,
        B.CreatedAt,
        U.FullName AS CreatedUserName,
        B.UserId AS CreatedUserId,
        S.Name AS StatusName,
		B.StatusId,
		R.Name AS RoomName
    FROM Bookings B
    LEFT JOIN Users U ON B.UserId = U.Id
    LEFT JOIN Rooms R ON B.RoomId = R.Id
    LEFT JOIN Status S ON B.StatusId = S.Id
    WHERE B.Id = @BookingId
END
GO

