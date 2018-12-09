create database hotel
GO

use hotel
GO

--RoomTypes
--Rooms
--Accounts
--Service
--Bills
--BillInfo

CREATE TABLE RoomTypes (
    ID int identity NOT NULL,
	Name nvarchar(255) NOT NULL, 
	Price float NOT NULL,

    PRIMARY KEY (ID),
);
GO

CREATE TABLE Rooms (
    ID int identity NOT NULL,
	Name varchar(255) not null,
	TypeID int NOT NULL,
	Description nvarchar(255) default N'Không có', 
	Status int NOT NULL default 0,

    PRIMARY KEY (ID),
	CONSTRAINT FK_TypeID FOREIGN KEY (TypeID)
    REFERENCES RoomTypes(ID)
);
GO


CREATE TABLE Accounts (
	id int identity Primary KEY,
    username nvarchar(255) NOT NULL UNIQUE,
	displayed nvarchar(255) NOT NULL  DEFAULT N'Người dùng',
	password nvarchar(255) NOT NULL DEFAULT N'123456',
	type int  NOT NULL DEFAULT 0 CHECK ( type in (0, 1))
);
GO

CREATE TABLE Service
(
	id int identity Primary KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Không tên',
	price FLOAT NOT NULL DEFAULT 0
)
GO

CREATE TABLE Bills
(
	id int identity Primary KEY,
	date_check_in DATE DEFAULT GETDATE(),
	date_check_out DATE,
	room_id INT NOT NULL,
	status INT NOT NULL DEFAULT 0

	FOREIGN KEY (room_id) REFERENCES Rooms(ID) 
)
GO

CREATE TABLE BillInfo
(
	id int identity Primary KEY,
	bill_id INT NOT NULL,
	service_id INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (bill_id) REFERENCES Bills(id),
	FOREIGN KEY (service_id) REFERENCES Service(ID)
)
GO

--===================================================== 
--=====================================================
-- INSERT DATA
--=====================================================
--=====================================================

INSERT INTO Accounts
VALUES (N'admin', N'admin', N'123456', 1); 
GO

INSERT INTO Accounts
VALUES (N'staff', N'staff', N'123456', 0); 
GO

INSERT INTO Accounts
VALUES (N'assistant', N'assistant', N'123456', 0); 
GO
select * from Accounts
--Nhập danh sách ROOMS - PHÒNG vào cơ sở dữ liệu
declare @i int = 0
while @i <= 10
begin
	INSERT INTO Rooms (Name, TypeID) 
	values ((N'Phòng ' + CAST(@i + 1 as nvarchar(255))), (@i % 4 + 1))
	set @i = @i + 1
end
go

--Nhập danh sách RoomTypes - LOẠI PHÒNG vào cơ sở dữ liệu
declare @i int = 0
while @i < 4
begin
	if @i = 0 
	INSERT INTO RoomTypes (Name, Price) 
	values ((N'Phòng bình thường'), (100000))

	if @i = 1 
	INSERT INTO RoomTypes (Name, Price) 
	values ((N'Phòng cao cấp loại 3'), (200000))

	if @i = 2 
	INSERT INTO RoomTypes (Name, Price) 
	values ((N'Phòng cao cấp loại 2'), (500000))

	if @i = 3 
	INSERT INTO RoomTypes (Name, Price) 
	values ((N'Phòng cao cấp loại 1'), (1000000))

	set @i = @i + 1
end
go

--Nhập danh sách các loại dịch vụ
INSERT INTO Service
values (N'Bữa sáng', 100000)
go

INSERT INTO Service
values (N'Bữa trưa', 500000)
go

INSERT INTO Service
values (N'Bữa tối', 700000)
go

INSERT INTO Service
values (N'Dọn phòng', 300000)
go

INSERT INTO Service
values (N'Tổ chức tiệc sinh nhật', 5000000)
go

INSERT INTO Service
values (N'Tổ chức tiệc cưới', 10000000)
go

--Nhập danh sách bill
INSERT INTO Bills
values (GETDATE(), null, 1, 0, 0, 0)
go

INSERT INTO Bills
values (GETDATE(), GETDATE(), 2, 1, 0, 0)
go

INSERT INTO Bills
values (GETDATE()  - 4, GETDATE(), 3, 1, 0, 0)
go
select * from BillInfo

--Nhập danh sách billinfo
INSERT INTO BillInfo
values (1, 1, 1, 1)
go

INSERT INTO BillInfo
values (2, 2, 2, 2)
go

INSERT INTO BillInfo
values (3, 4, 4, 4)
go

INSERT INTO BillInfo
values (3, 5, 1, 4)
go

--===================================================== 
--=====================================================
-- PROCEDURE
--=====================================================
--=====================================================
CREATE PROCEDURE USP_GetAccountByUsername
@username nvarchar(255)
AS
BEGIN
	SELECT * FROM dbo.Accounts
	WHERE username = @username
END
GO

exec USP_GetAccountByUsername 'admin'

CREATE PROCEDURE USP_Login
@username nvarchar(255), @password nvarchar(255)
AS
BEGIN
	SELECT * FROM dbo.accounts 
	WHERE username = @username and password = @password
END
GO

create proc USP_GetRoomList
as 
begin
	select * from Rooms
end
go
exec USP_GetRoomList
update Rooms set Status = 1 where id = 9
go
update Rooms set Status = 1 where id = 20
go

alter table BillInfo
add quantity_of_day int not null default 1

UPDATE Rooms
SET Name = N'Phòng ' + CAST(ID as nvarchar(255))

--Rooms
--RoomTypes
--Bills
--BillInfo
--Service

--private int billID;
--private string roomName;
--private int quantityOFDay;
--private string serviceName;
--private int count;
--private float totalPrice;
--select b.id, r.Name, bf.quantity_of_day, bf.count, sum(rt.Price * bf.quantity_of_day + s.price * bf.count) as totalPrice
----from Bills as b inner join Rooms as r on Bills.room_id = Rooms.ID
----				inner join BillInfo as bf on Bills.id = BillInfo.id
----				inner join RoomTypes as rt on Rooms.TypeID = RoomTypes.ID
----				inner join Service as s on BillInfo.service_id = Service.id
--from Bills as b, Rooms as r, BillInfo as bf, RoomTypes as rt, Service as s
--where b.room_id = r.ID and b.id = bf.bill_id and bf.service_id = s.id and r.TypeID = rt.ID	 
--group by b.id
select Name from RoomTypes
go

--alter PROCEDURE USP_InsertBill
--@room_id int
--AS
--BEGIN
--	insert dbo.Bills  (date_check_in, date_check_out, room_id, status, discount)
--	values (GETDATE(), NULL, @room_id, 0, 0)
--END
--GO

CREATE PROCEDURE USP_GetRoomTypeByID
@id int
AS
BEGIN
	SELECT * FROM RoomTypes where id = @id
END
GO

CREATE PROCEDURE USP_GetRoomTypes
AS
BEGIN
	SELECT * FROM RoomTypes
END
GO

CREATE PROCEDURE USP_DeleteBillInfoByBillID 
@id int
AS
BEGIN
	delete BillInfo where bill_id = @id
END
GO

CREATE PROCEDURE USP_DeleteBillByRoomID 
@id int
AS
BEGIN
	delete Bills where room_id = @id
END
GO

CREATE PROCEDURE USP_DeleteBillInfoByServiceID 
@id int
AS
BEGIN
	delete BillInfo where service_id = @id
END
GO

CREATE PROCEDURE USP_DeleteRoomByID 
@id int
AS
BEGIN
	delete Rooms where id = @id
END
GO

CREATE PROCEDURE USP_DeleteRoomTypeByID 
@id int
AS
BEGIN
	delete RoomTypes where id = @id
END
GO

CREATE PROCEDURE USP_DeleteRoomByRoomTypeID 
@id int
AS
BEGIN
	delete Rooms where TypeID = @id
END
GO

CREATE PROCEDURE USP_DeleteServiceByID 
@id int
AS
BEGIN
	delete Service where id = @id
END
GO

select * from BillInfo
exec USP_GetRoomTypeByID 1
go

CREATE PROCEDURE USP_GetService
AS
BEGIN
	SELECT * FROM Service
END
GO
select * from Accounts

CREATE PROCEDURE USP_GetListAccount
AS
BEGIN
	SELECT * FROM Accounts
END
GO

CREATE PROCEDURE USP_GetServiceByID
@id int
AS
BEGIN
	SELECT * FROM Service where id = @id
END
GO

alter PROCEDURE USP_InsertBill
@room_id int
AS
BEGIN
	insert dbo.Bills(date_check_in, date_check_out, room_id, status, discount, total_price)
	values (GETDATE(), null, @room_id, 0, 0, 0)
END
GO

create proc USP_GetUncheckBillIDByRoomID
@room_id int
as
begin
	select * from Bills where room_id = @room_id and status = 0
end
go

create proc USP_getMaxBillID
as
begin
	select max(id) from Bills
end
go

--thêm thông tin vào billinfo
Alter PROCEDURE USP_InsertBillInfo
@bill_id int, @service_id int, @count int
AS
BEGIN
	declare @isExistBillInfo int
	declare @serviceCount int = 1

	select @isExistBillInfo = id, @serviceCount = bi.count 
	from BillInfo as bi 
	where bill_id = @bill_id and service_id = @service_id
	
	if(@isExistBillInfo > 0)
	begin
		declare @newcount int = @count + @serviceCount

		if(@newcount > 0)
			update BillInfo set count = @newcount where service_id = @service_id
		else
			delete BillInfo where bill_id = @bill_id and service_id = @service_id
	end
	else
	begin
		insert dbo.BillInfo(bill_id, service_id, count, quantity_of_day)
		values (@bill_id, @service_id, @count, 1)
	end

END
GO

create PROCEDURE USP_GetListBillInfoByID
@bill_id int
as
BEGIN
	select * from BillInfo where bill_id = @bill_id;
END
GO

create PROCEDURE USP_InsertService
@name nvarchar(255), @price int
as
BEGIN
	insert Service(name, price)
	values (@name, @price)
END
GO

create PROCEDURE USP_InsertRoomType
@name nvarchar(255), @price int
as
BEGIN
	insert RoomTypes(name, price)
	values (@name, @price)
END
GO

create PROCEDURE USP_InsertAccount
@username nvarchar(255), @pass nvarchar(255), @displayName nvarchar(255), @type int
as
BEGIN
	insert Accounts(username, displayed, password, type)
	values (@username, @displayName, @pass, @type)
END
GO

create proc USP_UpdateAccount
@username nvarchar(255), @displayedName nvarchar(255), @password nvarchar(255), @newPassword nvarchar(255)
as
begin
	declare @isRightPassword int = 0

	select @isRightPassword = COUNT(*) 
	from Accounts
	where username = @username and password = @password

	if (@isRightPassword = 1)
	begin
		if (@newPassword = null or @newPassword = '')
		begin
			update Accounts set displayed = @displayedName where username = @username
		end
		else
		begin
			update Accounts set displayed = @displayedName, password = @newPassword where username = @username
		end
	end
end
go

--exec USP_InsertBillInfo 1, 2, 1
--select * from BillInfo

create PROCEDURE USP_UpdateBill
@id int, @discount int, @total_price double 
as
BEGIN
	update Bills set status = 1, date_check_out = getdate(), discount = @discount, total_price = @total_price where id = @id
END
GO

create PROCEDURE USP_UpdateServiceByID
@id int, @name nvarchar(255), @price int
as
BEGIN
	update Service set name = @name, price = @price where id = @id
END
GO

create PROCEDURE USP_UpdateRoomTypeByID
@id int, @name nvarchar(255), @price int
as
BEGIN
	update RoomTypes set Name = @name, Price = @price where ID = @id
END
GO

create PROCEDURE USP_InsertRoom
@name nvarchar(255), @type int, @des nvarchar(255), @status int
as
BEGIN
	insert Rooms (Name, TypeID, Description, Status)
	values (@name, @type, @des, @status)
END
GO

create PROCEDURE USP_UpdateRoom
@id int, @name nvarchar(255), @type int, @des nvarchar(255), @status int
as
BEGIN
	update Rooms set Name = @name, TypeID = @type, Description =  @des, Status =  @status where ID = @id
END
GO

create PROCEDURE USP_DeleteAccountByUsername
@name nvarchar(255)
as
BEGIN
	delete Accounts where username = @name
END
GO

exec USP_UpdateRoom 14, N'Phòng 14', 1, N'Không có', 0
go

exec USP_InsertRoom N'Phòng 12', 1, N'Không có', 0
go

alter trigger UTG_UpdateBillInfo
on BillInfo for insert, update
as 
begin
	declare @billID int
	select @billID = bill_id from inserted

	declare @roomID int
	select @roomID = room_id from Bills where id = @billID and status = 0

	update Rooms set status = 1 where id = @roomID
end
go

alter trigger UTG_UpdateBill
on Bills for update
as 
begin
	declare @billID int
	select @billID = id from inserted

	declare @roomID int
	select @roomID = room_id from Bills where id = @billID

	declare @count int = 0
	select @count = COUNT(*) from Bills where room_id = @roomID and status = 0
	if (@count = 0)
		update Rooms set status = 0 where id = @roomID
end

--alter table Bills
drop column discount 

update Bills set discount = 0
go

create proc USP_BillFormat
as
begin
	select b.id, 
		   r.Name, 
		   b.date_check_in, 
		   b.date_check_out, 
		   rt.Name as N'Loại phòng', 
		   rt.Price as N'Đơn giá phòng', 
		   bi.quantity_of_day as N'Số ngày nghỉ', 
		   s.name as N'Tên dịch vụ', 
		   s.price as N'Đơn giá dịch vụ', 
		   bi.count as N'Số lần dùng dịch vụ', 
		   discount as N'Giảm giá', 
		   (bi.count * s.price + rt.Price * bi.quantity_of_day) as N'Tổng tiền' 
	from Bills as b, Rooms as r, BillInfo as bi, Service as s, RoomTypes as rt
	where --b.date_check_in >= '2018-12-01' and b.date_check_out <= '2018-12-07' and
									    b.status = 1 
									  and r.ID = b.room_id	
									  and b.id = bi.bill_id
									  and r.TypeID = rt.ID	
									  and bi.service_id = s.id
									 -- group by b.id
end
go

alter proc USP_BillFormat
as
begin
	select b.id as 'Bill ID', 
		   r.Name, 
		   b.date_check_in, 
		   b.date_check_out,  
		   discount as N'Giảm giá', 
		   (bi.count * s.price + rt.Price * bi.quantity_of_day) as N'Tổng tiền' 
	from Bills as b, Rooms as r, BillInfo as bi, Service as s, RoomTypes as rt
	where							
		b.status = 1 
		and r.ID = b.room_id	
		and b.id = bi.bill_id
		and r.TypeID = rt.ID	
		and bi.service_id = s.id
end
go

alter proc USP_GetServiceForUser
@roomID int
as
begin
	select s.name, --as N'Tên dịch vụ', 
		   s.price, --as N'Đơn giá dịch vụ', 
		   bi.count, --as N'Số lần dùng dịch vụ', 
		   bi.count * s.price as N'totalPrice' 
	from Bills as b, Rooms as r, BillInfo as bi, Service as s, RoomTypes as rt
	where r.ID = b.room_id	
		  and b.id = bi.bill_id
		  and r.TypeID = rt.ID	
		  and bi.service_id = s.id	
		  and b.room_id = @roomID
		  and b.status = 0
end						 
go

exec USP_GetServiceForUser 3

--DROP  view BillFormat

update Bills
set date_check_out = GETDATE()
where id = 1

-- kiểm tra type trong bảng Accounts chỉ là 0: user  hoặc 1: admin
ALTER TABLE Accounts
ADD CHECK (type in (0, 1));

-- kiểm tra Status trong bảng Rooms chỉ là 0: trống hoặc 1: đã đặt
ALTER TABLE Rooms
ADD CHECK (Status in (0, 1, 2));

-- kiểm tra status trong bảng Bills chỉ là 0: trống hoặc 1: đã đặt
ALTER TABLE Bills
ADD CHECK (status in (0, 1));

ALTER TABLE Bills
ADD CHECK (date_check_out >= date_check_in);

ALTER TABLE Bills
ADD CONSTRAINT total_price 
DEFAULT 0 FOR total_price;

select * from Rooms
select * from BillInfo
select * from Bills
select * from Service
select * from Accounts
select * from RoomTypes

--delete Bills
delete BillInfo
--delete Rooms
--delete RoomTypes

DBCC CHECKIDENT('BillInfo', RESEED, 0)
go

exec USP_GetRoomTypes

update Rooms
set Status = 0
where id =2
go

alter trigger UTG_DeleteBillInfo
on BillInfo for delete
as
begin
	declare @idBillInfo int
	declare @idBill int
	select @idBillInfo = id, @idBill = bill_id from deleted

	declare @idRoom int
	select @idRoom = room_id from Bills where id = @idBill

	declare @count int = 0
	select @count = COUNT(*) from BillInfo as bi, Bills as b where b.id = bi.bill_id and b.id = @idBill and b.status = 0

	if (@count = 0)
	begin
		update Rooms set Status = 0 where id = @idRoom
	end
end
go

select * from Rooms
go

DROP TABLE Accounts 