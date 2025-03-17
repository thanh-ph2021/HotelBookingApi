IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[UP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].UP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get User Profile 
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 10/03/2025
-- <Example>
-- EXEC UP0001 'C34A64B6-E6B4-4014-862B-047D0FE81865', ''
-- </Summary>

CREATE PROCEDURE UP0001
(
	@UserId VARCHAR(50),
    @Email VARCHAR(50)
)  
AS
DECLARE @sSQL NVARCHAR(MAX),
		@sWhere NVARCHAR(MAX)
IF @UserId IS NOT NULL SET @sWhere = 'U.Id = ''' +@UserId+ ''''
ELSE IF @Email IS NOT NULL SET @sWhere = 'U.Email = ''' +@Email+ ''''

SET @sSQL = '
BEGIN
    SELECT U.Id, U.FullName, U.Email, U.PasswordHash, U.Phone, U.CreatedAt, U.UpdatedAt, UR.Name AS UserRole, U.UserRoleId
    FROM Users U
	LEFT JOIN UserRole UR ON U.UserRoleId = UR.Id
    WHERE ' +@sWhere+ '
	GROUP BY U.Id, U.FullName, U.Email, U.Phone, U.CreatedAt, U.UpdatedAt, UR.Name, U.UserRoleId, U.PasswordHash
END
'

EXEC (@sSQL)
GO

