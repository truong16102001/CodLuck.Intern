
--Bảng Employees(EmpId, EmpName), Departments(DepId, DepName), 
--Employees_Departments(Id, DepId, EmpId). 
--Viết truy vấn in ra đầy đủ thông tin của các phòng ban có số 
--lượng nhân viên nhiều nhất bao gồm: DepId, Depname, NumberOfEmp

with CountEmpOfDepartment as (
select d.DeptId, d.DeptName, count(*) as NumberOfEmps
from Departments d join Employees_Departments ed 
on d.DeptId = ed.DeptId
group by d.DeptId, d.DeptName
),
MaxEmpsCounted as(
select max(NumberOfEmps) as MaxCount from CountEmpOfDepartment
)

select ce.DeptId, ce.DeptName, ce.NumberOfEmps
from CountEmpOfDepartment ce join MaxEmpsCounted me 
on ce.NumberOfEmps = me.MaxCount



--- tạo procedure không tham số
CREATE PROCEDURE GetEmps
AS
BEGIN
    SELECT * FROM Employees;
END;
--
exec GetEmps;

--- Tạo procedure chứa tham số đầu vào tìm emp by empId
CREATE PROCEDURE GetEmpsById @EmpId int
AS
BEGIN
    SELECT * FROM Employees where EmpId = @EmpId;
END;
--
exec GetEmpsById @EmpId = 2;

--- Tạo procedure chứa tham số đầu vào , tìm các emps trong dept có Id = ?
CREATE PROCEDURE GetEmpsInDept @DeptId int
AS
BEGIN
    SELECT e.EmpId, e.EmpName  
	FROM Employees_Departments ed 
	inner join Employees e on e.EmpId = ed.EmpId
	where ed.DeptId = @DeptId
END;
--
exec GetEmpsInDept @DeptId = 1;


--- Tạo procedure cho câu lệnh insert, dùng try catch để xử lý tình huống lỗi
CREATE PROCEDURE InsertEmp
    @EmpName NVARCHAR(50),
	@Email varchar(100),
	@Phone varchar(20)
AS
BEGIN
    BEGIN TRY
        -- Bắt đầu giao dịch
        BEGIN TRANSACTION;
 
        -- Thực hiện các câu lệnh INSERT và UPDATE
insert into Employees(EmpName, Email, Phone) values(@EmpName, @Email, @Phone);
        -- Kết thúc giao dịch
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Xử lý lỗi và rollback giao dịch
        ROLLBACK TRANSACTION;
        PRINT ERROR_MESSAGE();
    END CATCH;
END;
--
exec InsertEmp @EmpName = N'Trương Trọng Hòa',@Email = 'HoaTT32@fpt.com', @Phone = '0222333444'
exec GetEmps

-------------------------------------
---Trigger là loại procedure đặc biệt được thực thi tự động khi có sự kiện thay đổi dữ liệu xảy ra (thêm, xóa, sửa)
---  Cú pháp:
--CREATE TRIGGER trigger_name ON table_name
--{[AFTER], [INSTEAD OF]} {[INSERT],[UPDATE],[DELETE]}
--AS
--BEGIN
--{sql_statements}
--END;

--- Có 2 loại trigger: 1. AFTER (thực thi sau khi sự kiện xảy ra),
---					2. INSTEAD OF (thực thi trước khi sự kiện xảy ra)

-- Trigger thực hiện xóa các bản ghi ở bảng quan hệ trước khi xóa bản ghi trong bảng chính
CREATE TRIGGER DeleteEmployeeTrigger
ON Employees
INSTEAD OF delete
AS
BEGIN
    -- Lưu trữ EmpId được truyền vào
    DECLARE @EmpId INT;
    SET @EmpId = (SELECT EmpId FROM deleted); 
 
    -- Xóa các bản ghi liên quan trong bảng Emps_Depts
    DELETE FROM Employees_Departments WHERE Employees_Departments.EmpId = @EmpId;
 
    -- Xóa emp trong bảng Emps
    DELETE FROM Employees WHERE EmpId = @EmpId;
END;

--test
delete from Employees where EmpId = 1
exec GetEmps
select * from Employees_Departments



---trigger check email format and check email existed before insert
create trigger InsertEmployeeTrigger on Employees
instead of insert
as begin
	 -- Kiểm tra nếu email mới chứa "@gmail.com"
	if exists(select * from inserted where Email like '%@gmail.com')
		begin
			-- Kiểm tra xem email đã tồn tại trong bảng emps nhưng không phải của bản ghi hiện tại
			IF EXISTS( SELECT *   FROM Employees u INNER JOIN inserted i 
						 ON lower(u.Email) = lower(i.Email) AND u.EmpId <> i.EmpId)
				BEGIN
					-- Nếu email đã tồn tại, rollback thao tác và hiển thị thông báo lỗi
					ROLLBACK TRANSACTION;
					PRINT N'Email đã tồn tại. Thao tác đã bị hủy.'
				END
			ELSE
				BEGIN
					-- Nếu email chưa tồn tại, thực hiện insert
					INSERT INTO Employees(EmpName,Email,Phone)
					SELECT EmpName,Email,Phone FROM inserted;
				END
		end;
	else
		BEGIN
        -- Nếu email không chứa "@gmail.com", rollback thao tác và hiển thị thông báo lỗi
        ROLLBACK TRANSACTION;
        PRINT N'Email phải chứa "@gmail.com". Thao tác đã bị hủy.'
		END;
	END;

--test
insert into Employees values('NDT', 'TruongND27@gmail.com', '0123123123')
select * from Employees


---trigger check email format and check email existed before update
create trigger UpdateEmployeeTrigger on Employees
instead of update
as begin
	 -- Kiểm tra nếu email mới chứa "@gmail.com"
	if exists(select * from inserted where Email like '%@gmail.com')
		begin
			-- Kiểm tra xem email đã tồn tại trong bảng emps nhưng không phải của bản ghi hiện tại
			IF EXISTS( SELECT * FROM Employees u INNER JOIN inserted i 
						 ON lower(u.Email) = lower(i.Email) AND u.EmpId <> i.EmpId)
				BEGIN
					-- Nếu email đã tồn tại, rollback thao tác và hiển thị thông báo lỗi
					ROLLBACK TRANSACTION;
					PRINT N'Email đã tồn tại. Thao tác đã bị hủy.'
				END
			ELSE
				BEGIN
					-- Nếu email chưa tồn tại, thực hiện insert
					update Employees 
					set EmpName = i.EmpName, Email = i.Email, Phone = i.Phone
					from Employees e inner join inserted i on e.EmpId = i.EmpId
				END
		end;
	else
		BEGIN
        -- Nếu email không chứa "@gmail.com", rollback thao tác và hiển thị thông báo lỗi
        ROLLBACK TRANSACTION;
        PRINT N'Email phải chứa "@gmail.com". Thao tác đã bị hủy.'
		END;
	END;

---test
UPDATE Employees
SET EmpName = 'Alice Smith', Email = 'HoaTT32@fpt.com', Phone = '1111111111'
WHERE EmpId = 7;
select * from Employees





