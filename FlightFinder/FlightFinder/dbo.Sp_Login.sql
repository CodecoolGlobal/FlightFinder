Create PROC Sp_Login
@User_name NVARCHAR(20),
@User_password NVARCHAR(50),
@Isvalid BIT OUT
AS
BEGIN
SET @Isvalid = (SELECT COUNT(1) FROM Login WHERE Name = @User_name AND Password = @User_password)
end