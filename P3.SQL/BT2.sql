--Câu 1: Xóa những khách hàng có tên là “Le Thi A”.
delete from DONHANG_GIAOHANG where MaKhachHang in 
(select MaKhachHang from KHACHHANG where lower(TenKhachHang) = 'le thi a')
delete from KHACHHANG where lower(TenKhachHang) = 'le thi a'

--Câu 2: Cập nhật những khách hàng đang thường trú ở khu vực “Son Tra” thành khu vực “Ngu Hanh Son”.
update KHUVUC set TenKhuVuc = 'Ngu Hanh Son' where MaKhuVuc in 
(select MaKhuVuc from KHUVUC where lower(TenKhuVuc) = 'son tra') 

--Câu 3: Liệt kê những thành viên (shipper) có họ tên bắt đầu là ký tự 
--‘Tr’ và có độ dài ít nhất là 25 ký tự (kể cả ký tự trắng).
select * from THANHVIENGIAOHANG 
where len(TenThanhVienGiaoHang) >= 25 and TenThanhVienGiaoHang like '%Tr%'

-- Câu 4: Liệt kê những đơn hàng có NgayGiaoHang nằm trong năm 2017 
--và có khu vực giao hàng là “Hai Chau”.
select * 
from DONHANG_GIAOHANG gh inner join KHUVUC kv on gh.MaKhuVucGiaoHang  = kv.MaKhuVuc
where year(gh.NgayGiaoHang) = 2017 and lower(kv.TenKhuVuc) = 'hai chau'


--Câu 5: Liệt kê MaDonHangGiaoHang, MaThanhVienGiaoHang, TenThanhVienGiaoHang,
-- NgayGiaoHang, PhuongThucThanhToan của tất cả những đơn hàng có trạng thái là “Da giao
-- hang”. Kết quả hiển thị được sắp xếp tăng dần theo NgayGiaoHang và giảm dần theo
--PhuongThucThanhToan
select gh.MaDonHangGiaoHang, gh.MaThanhVienGiaoHang, tv.TenThanhVienGiaoHang, gh.NgayGiaoHang,
gh.PhuongThucThanhToan
from DONHANG_GIAOHANG gh join THANHVIENGIAOHANG tv on gh.MaThanhVienGiaoHang = tv.MaThanhVienGiaoHang
where lower(gh.TrangThaiGiaoHang) = 'da giao hang'
order by NgayGiaoHang, PhuongThucThanhToan desc

--Câu 6: Liệt kê những thành viên có giới tính là “Nam” 
--và chưa từng được giao hàng lần nào.
select tv.MaThanhVienGiaoHang, tv.TenThanhVienGiaoHang, tv.GioiTinh, gh.MaDonHangGiaoHang
from THANHVIENGIAOHANG tv left join DONHANG_GIAOHANG gh 
on tv.MaThanhVienGiaoHang = gh.MaThanhVienGiaoHang
where  gh.MaDonHangGiaoHang is null and tv.GioiTinh = 'Nam'

--Câu 7: Liệt kê họ tên của những khách hàng đang có trong hệ thống. 
--Nếu họ tên trùng nhau thì chỉ hiển thị 1 lần. (2 cach)
select * from KHACHHANG where TenKhachHang in
( select distinct TenKhachHang from KHACHHANG)

select * from KHACHHANG where TenKhachHang in
(select TenKhachHang from KHACHHANG group by TenKhachHang)

-- Câu 8: Liệt kê MaKhachHang, TenKhachHang, DiaChiNhanHang, MaDonHangGiaoHang,
-- PhuongThucThanhToan, TrangThaiGiaoHang của tất cả các khách hàng đang 
--có trong hệ thong
select kh.MaKhachHang, kh.TenKhachHang, kh.DiaChiNhanHang, 
gh.MaDonHangGiaoHang, gh.PhuongThucThanhToan
from KHACHHANG kh left join DONHANG_GIAOHANG gh 
on kh.MaKhachHang = gh.MaKhachHang

--Câu 9: Liệt kê những thành viên giao hàng có giới tính là “Nu” và từng giao hàng cho 10
--khách hàng khác nhau ở khu vực giao hàng là “Hai Chau”
select tvgh.MaThanhVienGiaoHang, count(*) as SoLuongDon
from THANHVIENGIAOHANG tvgh join DONHANG_GIAOHANG gh
on tvgh.MaThanhVienGiaoHang = gh.MaThanhVienGiaoHang
join KHUVUC kv on kv.MaKhuVuc =gh.MaKhuVucGiaoHang
where lower(kv.TenKhuVuc) = 'hai chau'
group by tvgh.MaThanhVienGiaoHang having count(*) >= 10

-- Câu 10: Liệt kê những khách hàng đã từng yêu cầu giao hàng tại khu vực “Lien Chieu” và
-- chưa từng được một thành viên giao hàng nào có giới tính là “Nam” nhận giao hàng
select kh.MaKhachHang, kh.TenKhachHang, kv.MaKhuVuc, kv.TenKhuVuc,tvgh.GioiTinh
from DONHANG_GIAOHANG gh join THANHVIENGIAOHANG tvgh 
on gh.MaThanhVienGiaoHang = tvgh.MaThanhVienGiaoHang 
join KHACHHANG kh on kh.MaKhachHang = gh.MaKhachHang
join KHUVUC kv on kv.MaKhuVuc = gh.MaKhuVucGiaoHang
where tvgh.GioiTinh != 'Nam' and gh.MaKhuVucGiaoHang  in
(select MaKhuVuc from KHUVUC where lower(TenKhuVuc) = 'lien chieu')

