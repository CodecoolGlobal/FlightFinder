Create PROC Sp_Register
@User_name NVARCHAR(20),
@User_password NVARCHAR(50)
AS
BEGIN
INSERT INTO Login ([Name], [Password]) VALUES (@User_name,@User_password)
end
