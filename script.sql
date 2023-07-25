CREATE DATABASE DigitalBank;

USE DigitalBank;

-- Create Table
CREATE TABLE USERS (
    userid UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    name NVARCHAR(100),
    birthdate DATE,
    gender CHAR(1)
);


-- SP Insert User
CREATE PROCEDURE sp_insert_user
    @name NVARCHAR(100),
    @birthdate DATE,
    @gender CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO USERS (name, birthdate, gender)
    VALUES (@name, @birthdate, @gender);
END;


-- SP Delete User
CREATE PROCEDURE sp_delete_user
    @userid UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM USERS
    WHERE userid = @userid;
END;

-- SP Update User
CREATE PROCEDURE sp_update_user
    @userid UNIQUEIDENTIFIER,
    @name NVARCHAR(100),
    @birthdate DATE,
    @gender CHAR(1)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE USERS
    SET name = @name,
        birthdate = @birthdate,
        gender = @gender
    WHERE userid = @userid;
END;

-- SP Get All Users
CREATE PROCEDURE sp_get_all_users
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM USERS;
END;


-- SP Get User By Id
CREATE PROCEDURE sp_get_user_by_id
    @userid UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM USERS
    WHERE userid = @userid;
END;