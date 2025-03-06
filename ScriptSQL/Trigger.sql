CREATE TRIGGER trg_UpdateUsersUpdatedAt
ON Users
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Users
    SET UpdatedAt = GETDATE()
    FROM Users INNER JOIN inserted i ON Users.Id = i.Id;
END;


CREATE TRIGGER trg_UpdateHotelsUpdatedAt
ON Hotels
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Hotels
    SET UpdatedAt = GETDATE()
    FROM Hotels INNER JOIN inserted i ON Hotels.Id = i.Id;
END;

CREATE TRIGGER trg_UpdateRoomsUpdatedAt
ON Rooms
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Rooms
    SET UpdatedAt = GETDATE()
    FROM Rooms INNER JOIN inserted i ON Rooms.Id = i.Id;
END;

CREATE TRIGGER trg_UpdateBookingsUpdatedAt
ON Bookings
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Bookings
    SET UpdatedAt = GETDATE()
    FROM Bookings INNER JOIN inserted i ON Bookings.Id = i.Id;
END;