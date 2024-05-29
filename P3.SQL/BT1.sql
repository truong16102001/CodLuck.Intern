
--Câu 1:Liệt kê MaDatPhong, MaDV, SoLuong của tất cả các dịch vụ 
-- có số lượng lớn hơn 3 và nhỏ hơn 10. 
select * from CHI_TIET_SU_DUNG_DV where SOLUONG between 4 and 9

-- Câu 2: Cập nhật dữ liệu trên trường GiaPhong thuộc bảng PHONG 
--tăng lên 10,000 VNĐ so với giá phòng hiện tại, chỉ cập nhật giá phòng
--của những phòng có số khách tối đa lớn hơn 10. 
update PHONG set 
GIAPHONG = (GIAPHONG + 10.000) 
where SOKHACHTOIDA > 10

--Câu 3: Xóa tất cả những đơn đặt phòng (từ bảng DAT_PHONG) 
--có trạng thái đặt (TrangThaiDat) là “Da huy”. 

delete from CHI_TIET_SU_DUNG_DV where MADATPHONG in 
 (select MADATPHONG from DAT_PHONG where upper(TRANGTHAIDAT) = 'DA HUY')

delete from DAT_PHONG where UPPER(TRANGTHAIDAT) = 'DA HUY'; 

--Câu 4: Hiển thị TenKH của những khách hàng có tên bắt đầu là 
--một trong các ký tự “H”, “N”, “M” và có độ dài tối đa là 20 ký tự. 
select TENKH from KHACH_HANG 
where LEN(TENKH) <= 20 and 
(TENKH like 'H%' or TENKH like 'N%' or TENKH like 'M%')

--Câu 5: Hiển thị TenKH của tất cả các khách hàng có trong hệ thống, 
--TenKH nào trùng nhau thì chỉ hiển thị một lần. 
--Sinh viên sử dụng hai cách khác nhau để thực hiện yêu cầu trên 

--C1:
select distinct TENKH from KHACH_HANG
--C2:
select TENKH from KHACH_HANG  group by TENKH

--Câu 6: Hiển thị MaDV, TenDV, DonViTinh, DonGia của những dịch vụ 
--đi kèm có DonViTinh là “lon” và có DonGia lớn hơn 10,000 VNĐ 
--hoặc những dịch vụ đi kèm có DonViTinh là “Cai” 
--và có DonGia nhỏ hơn 5,000 VNĐ. 
select * from DICH_VU_DI_KEM 
where (lower(DONVITINH) = 'lon' and DONGIA > 10.000) or
	(LOWER(DONVITINH) = 'cai' and DONGIA < 5.000)

--Câu 7: Hiển thị MaDatPhong, MaPhong, LoaiPhong, SoKhachToiDa, 
--GiaPhong, MaKH, TenKH, SoDT, NgayDat, GioBatDau, GioKetThuc, 
--MaDichVu, SoLuong, DonGia của những đơn đặt phòng có năm đặt phòng 
--là “2016”, “2017” và đặt những phòng có giá phòng > 50,000 VNĐ/ 1 giờ. 

select dp.MADATPHONG, p.MAPHONG, p.LOAIPHONG, p.SOKHACHTOIDA, p.GIAPHONG,
dp.MAKH, k.TENKH, k.SODT, dp.NGAYDAT, dp.GIOBATDAU, dp.GIOKETTHUC,
dvdk.MADV, ctdv.SOLUONG, dvdk.DONGIA
from DAT_PHONG dp join KHACH_HANG k on dp.MAKH = k.MAKH
 join PHONG p on dp.MAPHONG = p.MAPHONG 
 left join CHI_TIET_SU_DUNG_DV ctdv on dp.MADATPHONG = ctdv.MADATPHONG -- ko phải tất cả đơn đặt phòng đều sdung dịch vụ đi kèm
 left join DICH_VU_DI_KEM dvdk on dvdk.MADV = ctdv.MADV
where YEAR(NGAYDAT) in (2016, 2017)
		and p.GIAPHONG > 50.000


-- Câu 8: Hiển thị MaDatPhong, MaPhong, LoaiPhong, GiaPhong, TenKH, NgayDat, 
--TongTienHat, TongTienSuDungDichVu, TongTienThanhToan tương ứng với 
--từng mã đặt phòng có trong bảng DAT_PHONG. Những đơn đặt phòng nào 
--không sử dụng dịch vụ đi kèm thì cũng liệt kê thông tin của đơn đặt phòng đó ra. 
--TongTienHat = GiaPhong * (GioKetThuc – GioBatDau)
--TongTienSuDungDichVu = SoLuong * DonGia
--TongTienThanhToan = TongTienHat + sum (TongTienSuDungDichVu)

select dp.MADATPHONG, dp.MAPHONG, p.LOAIPHONG, p.GIAPHONG, k.TENKH, dp.NGAYDAT,
COALESCE(p.GIAPHONG * (DATEDIFF(HOUR, dp.GIOBATDAU, dp.GIOKETTHUC)),0) AS TongTienHat,
COALESCE(SUM(dvdk.DONGIA * ctdv.SOLUONG),0) AS TongTienSuDungDichVu, -- sum vì sdung nhiều dvu nên phải + tổng vào
COALESCE(p.GIAPHONG * (DATEDIFF(HOUR, dp.GIOBATDAU, dp.GIOKETTHUC)),0) 
+ COALESCE(SUM(dvdk.DONGIA * ctdv.SOLUONG),0) AS TongTienThanhToanfrom 
from DAT_PHONG dp
inner join PHONG p on  dp.MAPHONG = p.MAPHONG
inner join KHACH_HANG k on dp.MAKH = k.MAKH
left join CHI_TIET_SU_DUNG_DV ctdv on ctdv.MADATPHONG = dp.MADATPHONG
left join DICH_VU_DI_KEM dvdk on dvdk.MADV = ctdv.MADV
group by dp.MADATPHONG, dp.MAPHONG, p.LOAIPHONG, p.GIAPHONG,
k.TENKH, dp.NGAYDAT, dp.GIOBATDAU, dp.GIOKETTHUC; -- khi lấy trường nào ra sdung đều phải group by trường đó, dù đó là select hay là sdung trong hàm tính toán

--Câu 9: Hiển thị MaKH, TenKH, DiaChi, SoDT 
--của những khách hàng đã từng đặt phòng karaoke có địa chỉ ở “Hoa xuan”.
select dp.MAKH, k.TENKH, k.DIACHI, k.SODT
from DAT_PHONG dp inner join KHACH_HANG k on dp.MAKH = k.MAKH
where lower(k.DIACHI) = 'hoa xuan'

--Câu 10: Hiển thị MaPhong, LoaiPhong, SoKhachToiDa, GiaPhong, SoLanDat 
--của những phòng được khách hàng đặt có số lần đặt lớn hơn 2 lần 
--và trạng thái đặt là “Da dat”. 
select p.MAPHONG,p.LOAIPHONG, p.SOKHACHTOIDA, p.GIAPHONG, count(*) as SOLANDAT
from PHONG p  inner join DAT_PHONG dp on p.MAPHONG = dp.MAPHONG -- để mà có order dat_phong thì phải bắt buộc có mã phòng nên sdung inner join
where lower(dp.TRANGTHAIDAT) = 'da dat'
group by p.MAPHONG, p.LOAIPHONG, p.SOKHACHTOIDA, p.GIAPHONG
having count(*) > 2




select * from DAT_PHONG
select * from PHONG
