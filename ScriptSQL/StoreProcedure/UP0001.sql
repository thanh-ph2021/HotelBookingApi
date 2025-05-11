IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[UP0001]') AND OBJECTPROPERTY(ID, N'IsProcedure') = 1)
	DROP PROCEDURE [DBO].[UP0001]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
-- Get User Profile
-- <History>
-- CREATE BY: HOAI THANH, DATE: 10/03/2025
-- <Example>
-- EXEC UP0001 NULL, NULL, '122154032498500360'
-- </Summary>

CREATE PROCEDURE UP0001
(
	@UserId UNIQUEIDENTIFIER = NULL,
	@Email NVARCHAR(256) = NULL,
	@FacebookId NVARCHAR(50) = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 
		U.Id, U.FullName, U.Email, U.PasswordHash, U.Phone, 
		U.CreatedAt, U.UpdatedAt, UR.Name AS UserRole, U.UserRoleId,
		U.FacebookId, U.Avatar, U.Country
	FROM Users U
	LEFT JOIN UserRole UR ON U.UserRoleId = UR.Id
	WHERE
		(@UserId IS NOT NULL AND U.Id = @UserId)
		OR (@Email IS NOT NULL AND U.Email = @Email)
		OR (@FacebookId IS NOT NULL AND U.FacebookId = @FacebookId)
END
GO