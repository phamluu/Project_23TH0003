--create database QLHocPhan_23TH0003
--use QLHocPhan_23TH0003
-- Tạo bảng BaiHoc
CREATE TABLE BaiHoc (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenBaiHoc NVARCHAR(MAX) NULL,
    NoiDung NVARCHAR(MAX) NULL,
    Video NVARCHAR(MAX) NULL,
    TaiLieu NVARCHAR(MAX) NULL,
    IdLopHocPhan INT NULL,
    CONSTRAINT FK_BaiHoc_LopHocPhan_IdLopHocPhan FOREIGN KEY (IdLopHocPhan)
        REFERENCES LopHocPhan(Id)
);

CREATE INDEX IX_BaiHoc_IdLopHocPhan ON BaiHoc(IdLopHocPhan);

-- Tạo bảng CauHinhHeThong
CREATE TABLE CauHinhHeThong (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaCauHinh NVARCHAR(100) NOT NULL,
    TenCauHinh NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    GiaTri NVARCHAR(MAX) NULL
);


DELETE FROM [dbo].DangKyHocPhan;
DBCC CHECKIDENT ('dbo.DangKyHocPhan', RESEED, 0);

DELETE FROM [dbo].[GiangVien];
DBCC CHECKIDENT ('dbo.GiangVien', RESEED, 0);

DELETE FROM [dbo].[SinhVien];
DBCC CHECKIDENT ('dbo.SinhVien', RESEED, 0);
DBCC CHECKIDENT ('dbo.SinhVien', RESEED, 0);

DELETE FROM [dbo].[LopHocPhan];
DBCC CHECKIDENT ('dbo.LopHocPhan', RESEED, 0);
DELETE FROM [dbo].[HocPhan];
DBCC CHECKIDENT ('dbo.HocPhan', RESEED, 0);
DELETE FROM [dbo].[MonHoc];
DBCC CHECKIDENT ('dbo.MonHoc', RESEED, 0);
DELETE FROM [dbo].[Lop];
DBCC CHECKIDENT ('dbo.Lop', RESEED, 0);
DELETE FROM [dbo].[Khoa];
DBCC CHECKIDENT ('dbo.Khoa', RESEED, 0);
DELETE FROM [dbo].[HocKy];
DBCC CHECKIDENT ('dbo.HocKy', RESEED, 0);


-- Tạo dữ liệu cho bảng Học kỳ
INSERT INTO [dbo].[HocKy] ([MaHocKy],[TenHocKy],[NamHoc],[ThuTu], [NgayTao], [NgayCapNhat])
VALUES
('HK2022_1', N'Học kỳ 1', '2022-2023', 1, GETDATE(), GETDATE()),
('HK2022_2', N'Học kỳ 2', '2022-2023', 2, GETDATE(), GETDATE()),
('HK2022_3', N'Học kỳ hè', '2022-2023', 3, GETDATE(), GETDATE()),

('HK2023_1', N'Học kỳ 1', '2023-2024', 1, GETDATE(), GETDATE()),
('HK2023_2', N'Học kỳ 2', '2023-2024', 2, GETDATE(), GETDATE()),
('HK2023_3', N'Học kỳ hè', '2023-2024', 3, GETDATE(), GETDATE()),

('HK2024_1', N'Học kỳ 1', '2024-2025', 1, GETDATE(), GETDATE()),
('HK2024_2', N'Học kỳ 2', '2024-2025', 2, GETDATE(), GETDATE()),
('HK2024_3', N'Học kỳ hè', '2024-2025', 3, GETDATE(), GETDATE()),

('HK2025_1', N'Học kỳ 1', '2025-2026', 1, GETDATE(), GETDATE());

-- Tạo dữ liệu cho bảng Khoa
/*DELETE FROM [dbo].[Khoa];
DBCC CHECKIDENT ('dbo.Khoa', RESEED, 0); */

INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('CNTT',N'Công nghệ thông tin',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('KT',N'Kinh tế',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('XD',N'Xây dựng',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('KeToan',N'Kế toán',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('NN',N'Ngoại ngữ',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('CT',N'Chính trị',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('MT',N'Mỹ thuật',Null,0,GETDATE(),NULL)
INSERT INTO [dbo].[Khoa]([MaKhoa],[TenKhoa],[IdTruongKhoa],[IsDeleted],[NgayTao],[NgayCapNhat])
     VALUES ('TS',N'Thủy sản',Null,0,GETDATE(),NULL)

-- Tạo dữ liệu cho bảng Lớp
INSERT INTO [dbo].[Lop] ([MaLop],[TenLop],[IdKhoa],[IsDeleted],[NgayTao],[NgayCapNhat]
)
VALUES
('DHKTPM15A', N'Lớp KTPM 15A', 1, 0, GETDATE(), GETDATE()),
('DHKTPM15B', N'Lớp KTPM 15B', 1, 0, GETDATE(), GETDATE()),
('DHKTPM16A', N'Lớp KTPM 16A', 1, 0, GETDATE(), GETDATE()),
('DHHTTT15A', N'Lớp HTTT 15A', 2, 0, GETDATE(), GETDATE()),
('DHHTTT15B', N'Lớp HTTT 15B', 2, 0, GETDATE(), GETDATE()),
('DHMMT15A', N'Lớp MMT 15A', 3, 0, GETDATE(), GETDATE()),
('DHMMT16A', N'Lớp MMT 16A', 3, 0, GETDATE(), GETDATE()),
('DHMMT16B', N'Lớp MMT 16B', 3, 0, GETDATE(), GETDATE()),
('DHCNTT15A', N'Lớp CNTT 15A', 1, 0, GETDATE(), GETDATE()),
('DHCNTT16A', N'Lớp CNTT 16A', 1, 0, GETDATE(), GETDATE());
GO

-- Tạo dữ liệu cho bảng Môn học
INSERT INTO [dbo].[MonHoc] ([MaMonHoc],[TenMonHoc],[IdKhoa],[IsDeleted],[NgayTao],[NgayCapNhat]
)
VALUES
('MH001', N'Lập trình C', 1, 0, GETDATE(), Null),
('MH002', N'Cơ sở dữ liệu', 1, 0, GETDATE(), Null),
('MH003', N'Mạng máy tính', 2, 0, GETDATE(), Null),
('MH004', N'Trí tuệ nhân tạo', 1, 0, GETDATE(), Null),
('MH005', N'Hệ điều hành', 2, 0, GETDATE(), Null),
('MH006', N'Phân tích thiết kế hệ thống', 1, 0, GETDATE(), Null),
('MH007', N'Lập trình Web', 1, 0, GETDATE(), Null),
('MH008', N'Kiến trúc máy tính', 3, 0, GETDATE(), Null),
('MH009', N'Thiết kế giao diện', 3, 0, GETDATE(), Null),
('MH010', N'Nhập môn Công nghệ thông tin', 1, 0, GETDATE(), Null);
GO

-- Tạo dữ liệu cho bảng Học phần
INSERT INTO [dbo].[HocPhan] ([MaHocPhan],[TenHocPhan],[IdHocKy],[SoTinChi], [IdMonHoc],[IsDeleted], [NgayTao],[NgayCapNhat])
VALUES
('HP001', N'Lập trình C - HK1', 1, 3, 1, 0, GETDATE(), GETDATE()),
('HP002', N'Cơ sở dữ liệu - HK1', 1, 3, 2, 0, GETDATE(), GETDATE()),
('HP003', N'Mạng máy tính - HK2', 2, 3, 3, 0, GETDATE(), GETDATE()),
('HP004', N'Trí tuệ nhân tạo - HK2', 2, 4, 4, 0, GETDATE(), GETDATE()),
('HP005', N'Hệ điều hành - HK3', 3, 3, 5, 0, GETDATE(), GETDATE()),
('HP006', N'Phân tích thiết kế HT - HK1', 1, 3, 6, 0, GETDATE(), GETDATE()),
('HP007', N'Lập trình Web - HK2', 2, 3, 7, 0, GETDATE(), GETDATE()),
('HP008', N'Kiến trúc máy tính - HK3', 3, 2, 8, 0, GETDATE(), GETDATE()),
('HP009', N'Thiết kế giao diện - HK1', 1, 2, 9, 0, GETDATE(), GETDATE()),
('HP010', N'Nhập môn CNTT - HK1', 1, 3, 10, 0, GETDATE(), GETDATE());
GO

-- Tạo dữ liệu mẫu cho bảng Lớp học phần

INSERT INTO [dbo].[LopHocPhan] (
    [MaLopHocPhan], [TenLopHocPhan], [SiSoToiDa], [ThoiGianHoc], [DiaDiemHoc], [GhiChu],
    [HeSoChuyenCan], [HeSoGiuaKy], [HeSoThucHanh], [HeSoCuoiKy],
    [IdHocPhan], [IsDeleted], [NgayTao], [NgayCapNhat]
)
VALUES
('LHP001', N'LHP Lập trình C - Nhóm 1', 60, N'T2 - Ca 1', 'P.101', NULL, 0.1, 0.2, 0.2, 0.5, 1, 0, GETDATE(), GETDATE()),
('LHP002', N'LHP Lập trình C - Nhóm 2', 60, N'T4 - Ca 3', 'P.102', NULL, 0.1, 0.2, 0.2, 0.5, 1, 0, GETDATE(), GETDATE()),

('LHP003', N'LHP CSDL - Nhóm 1', 50, N'T3 - Ca 2', 'P.201', NULL, 0.1, 0.2, 0.3, 0.4, 2, 0, GETDATE(), GETDATE()),
('LHP004', N'LHP CSDL - Nhóm 2', 50, N'T5 - Ca 1', 'P.202', NULL, 0.1, 0.2, 0.3, 0.4, 2, 0, GETDATE(), GETDATE()),

('LHP005', N'LHP Mạng máy tính - Nhóm 1', 40, N'T2 - Ca 4', 'P.301', NULL, 0.1, 0.2, 0.3, 0.4, 3, 0, GETDATE(), GETDATE()),
('LHP006', N'LHP Mạng máy tính - Nhóm 2', 40, N'T6 - Ca 2', 'P.302', NULL, 0.1, 0.2, 0.3, 0.4, 3, 0, GETDATE(), GETDATE()),

('LHP007', N'LHP AI - Nhóm 1', 35, N'T3 - Ca 1', 'P.401', NULL, 0.1, 0.2, 0.1, 0.6, 4, 0, GETDATE(), GETDATE()),
('LHP008', N'LHP AI - Nhóm 2', 35, N'T5 - Ca 3', 'P.402', NULL, 0.1, 0.2, 0.1, 0.6, 4, 0, GETDATE(), GETDATE()),

('LHP009', N'LHP HĐH - Nhóm 1', 45, N'T2 - Ca 2', 'P.103', NULL, 0.1, 0.2, 0.2, 0.5, 5, 0, GETDATE(), GETDATE()),
('LHP010', N'LHP HĐH - Nhóm 2', 45, N'T6 - Ca 1', 'P.104', NULL, 0.1, 0.2, 0.2, 0.5, 5, 0, GETDATE(), GETDATE()),

('LHP011', N'LHP PTTH - Nhóm 1', 40, N'T3 - Ca 4', 'P.203', NULL, 0.1, 0.2, 0.3, 0.4, 6, 0, GETDATE(), GETDATE()),
('LHP012', N'LHP PTTH - Nhóm 2', 40, N'T5 - Ca 2', 'P.204', NULL, 0.1, 0.2, 0.3, 0.4, 6, 0, GETDATE(), GETDATE()),

('LHP013', N'LHP Web - Nhóm 1', 50, N'T2 - Ca 3', 'P.105', NULL, 0.1, 0.2, 0.3, 0.4, 7, 0, GETDATE(), GETDATE()),
('LHP014', N'LHP Web - Nhóm 2', 50, N'T4 - Ca 1', 'P.106', NULL, 0.1, 0.2, 0.3, 0.4, 7, 0, GETDATE(), GETDATE()),

('LHP015', N'LHP KTC - Nhóm 1', 35, N'T3 - Ca 2', 'P.305', NULL, 0.1, 0.2, 0.3, 0.4, 8, 0, GETDATE(), GETDATE()),
('LHP016', N'LHP KTC - Nhóm 2', 35, N'T5 - Ca 4', 'P.306', NULL, 0.1, 0.2, 0.3, 0.4, 8, 0, GETDATE(), GETDATE()),

('LHP017', N'LHP Giao diện - Nhóm 1', 30, N'T2 - Ca 1', 'P.205', NULL, 0.1, 0.2, 0.3, 0.4, 9, 0, GETDATE(), GETDATE()),
('LHP018', N'LHP Giao diện - Nhóm 2', 30, N'T4 - Ca 4', 'P.206', NULL, 0.1, 0.2, 0.3, 0.4, 9, 0, GETDATE(), GETDATE()),

('LHP019', N'LHP Nhập môn CNTT - Nhóm 1', 60, N'T3 - Ca 1', 'P.107', NULL, 0.1, 0.2, 0.2, 0.5, 10, 0, GETDATE(), GETDATE()),
('LHP020', N'LHP Nhập môn CNTT - Nhóm 2', 60, N'T6 - Ca 3', 'P.108', NULL, 0.1, 0.2, 0.2, 0.5, 10, 0, GETDATE(), GETDATE());
GO

-- Tạo dữ liệu cho bảng GiangVien
INSERT INTO [dbo].[GiangVien] ([MaGiangVien], [HoTen],[NgaySinh],[GioiTinh],[HinhDaiDien],
    [DiaChi],[UserId],[IdKhoa],[IsDeleted],[NgayTao],[NgayCapNhat]
)
VALUES
('GV001', N'Nguyễn Văn A', '1980-03-15', N'Nam', '', N'Hà Nội', '', 1, 0, GETDATE(), NULL),
('GV002', N'Trần Thị B', '1985-06-22', N'Nữ', '', N'Hồ Chí Minh', '', 1, 0, GETDATE(), NULL),
('GV003', N'Lê Văn C', '1978-11-09', N'Nam', '', N'Đà Nẵng', '', 2, 0, GETDATE(), NULL),
('GV004', N'Phạm Thị D', '1983-01-12', N'Nữ', '', N'Hải Phòng', '', 2, 0, GETDATE(), NULL),
('GV005', N'Hoàng Văn E', '1990-09-05', N'Nam', '', N'Cần Thơ', '', 3, 0, GETDATE(), NULL),
('GV006', N'Vũ Thị F', '1987-07-19', N'Nữ', '', N'Bình Dương', '', 3, 0, GETDATE(), NULL),
('GV007', N'Đỗ Văn G', '1975-12-01', N'Nam', '', N'Nghệ An', '', 1, 0, GETDATE(), NULL),
('GV008', N'Ngô Thị H', '1992-02-28', N'Nữ', '', N'Thái Bình', '', 2, 0, GETDATE(), NULL),
('GV009', N'Tạ Văn I', '1981-04-18', N'Nam', '', N'Nam Định', '', 3, 0, GETDATE(), NULL),
('GV010', N'Bùi Thị K', '1986-10-10', N'Nữ', '', N'Hòa Bình', '', 1, 0, GETDATE(), NULL);
GO

-- Tạo dữ liệu cho bang SinhVien
INSERT INTO [dbo].[SinhVien] ([MaSinhVien],[HoTen],[NgaySinh],[GioiTinh],[HinhDaiDien],
    [DiaChi],[UserId],[IdLop],[IsDeleted],[NgayTao],[NgayCapNhat])
VALUES
('SV001', N'Nguyễn Văn An', '2002-01-15', N'Nam', '', N'Hà Nội', '', 1, 0, GETDATE(), NULL),
('SV002', N'Trần Thị Bình', '2002-03-20', N'Nữ', '', N'Hồ Chí Minh', '', 1, 0, GETDATE(), NULL),
('SV003', N'Lê Văn Cường', '2001-09-10', N'Nam', '', N'Đà Nẵng', '', 2, 0, GETDATE(), NULL),
('SV004', N'Phạm Thị Duyên', '2002-05-25', N'Nữ', '', N'Hải Phòng', '', 2, 0, GETDATE(),NULL),
('SV005', N'Hoàng Văn Em', '2001-12-05', N'Nam', '', N'Cần Thơ', '', 3, 0, GETDATE(), NULL),
('SV006', N'Vũ Thị Phương', '2002-07-11', N'Nữ', '', N'Ninh Bình', '', 3, 0, GETDATE(), NULL),
('SV007', N'Đỗ Văn Giang', '2002-04-17', N'Nam', '', N'Nghệ An', '', 1, 0, GETDATE(), NULL),
('SV008', N'Ngô Thị Hạnh', '2001-08-09', N'Nữ', '', N'Thái Bình', '', 1, 0, GETDATE(), NULL),
('SV009', N'Tạ Văn Khang', '2002-02-27', N'Nam', '', N'Nam Định', '', 2, 0, GETDATE(), NULL),
('SV010', N'Bùi Thị Lan', '2001-11-03', N'Nữ', '', N'Hòa Bình', '', 3, 0, GETDATE(), NULL);
GO

-- Tạo dữ liệu cho bảng PhanCongGiangDay
INSERT INTO [dbo].[PhanCongGiangDay]([IdLopHocPhan], [IdGiangVien], [NgayPhanCong], [VaiTro], [GhiChu])
VALUES
(1, 1, GETDATE(), 1, N'Phụ trách chính'),
(2, 2, GETDATE(), 1, N'Phụ trách chính'),
(3, 3, GETDATE(), 2, N'Phụ trách thực hành'),
(4, 4, GETDATE(), 1, N'Phụ trách chính'),
(5, 5, GETDATE(), 2, N'Giảng viên phụ'),
(1, 2, GETDATE(), 2, N'Hỗ trợ môn học'),
(2, 3, GETDATE(), 1, N'Chính giảng'),
(3, 4, GETDATE(), 2, N'Hướng dẫn thảo luận'),
(4, 5, GETDATE(), 1, N'Phụ trách chính'),
(5, 1, GETDATE(), 2, N'Giảng viên hỗ trợ');
GO

-- Tạo dữ liệu cho đăng ký học phần

INSERT INTO [dbo].[DangKyHocPhan]([IdSinhVien],[IdLopHocPhan],[TrangThai],[NgayTao],[NgayCapNhat])
VALUES
(1, 1, 1, GETDATE(), GETDATE()),
(1, 2, 1, GETDATE(), GETDATE()),
(2, 2, 0, GETDATE(), GETDATE()),
(2, 1, 1, GETDATE(), GETDATE()),
(3, 2, 2, GETDATE(), GETDATE()),
(3, 3, 1, GETDATE(), GETDATE()),
(4, 4, 0, GETDATE(), GETDATE()),
(4, 5, 1, GETDATE(), GETDATE()),
(5, 6, 2, GETDATE(), GETDATE()),
(5, 7, 1, GETDATE(), GETDATE()),
(6, 1, 1, GETDATE(), GETDATE()),
(6, 2, 2, GETDATE(), GETDATE()),
(7, 1, 1, GETDATE(), GETDATE()),
(7, 2, 1, GETDATE(), GETDATE()),
(8, 1, 0, GETDATE(), GETDATE()),
(8, 2, 1, GETDATE(), GETDATE()),
(9, 1, 2, GETDATE(), GETDATE()),
(9, 2, 1, GETDATE(), GETDATE()),
(10, 1, 0, GETDATE(), GETDATE()),
(10, 2, 1, GETDATE(), GETDATE());

-- Tạo dữ liệu cho bảng điểm

INSERT INTO [dbo].[Diem]([DiemChuyenCan],[DiemGiuaKy],[DiemThucHanh],[DiemCuoiKy]
           ,[IdDangKyHocPhan],[DiemTrungBinh],[XepLoai],[NgayTao],[NgayCapNhat])
VALUES
(9.0, 7.5, 8.0, 7.0, 1, 7.6, N'Khá', GETDATE(), GETDATE()),
(10.0, 8.0, 9.0, 9.0, 2, 8.9, N'Giỏi', GETDATE(), GETDATE()),
(8.0, 6.5, 7.0, 6.0, 3, 6.9, N'Trung bình', GETDATE(), GETDATE()),
(7.0, 5.5, 6.0, 5.0, 4, 5.9, N'Trung bình yếu', GETDATE(), GETDATE()),
(6.0, 4.5, 5.0, 4.0, 5, 4.9, N'Yếu', GETDATE(), GETDATE()),
(9.5, 8.5, 9.0, 9.5, 6, 9.1, N'Giỏi', GETDATE(), GETDATE()),
(7.5, 7.0, 7.5, 8.0, 7, 7.5, N'Khá', GETDATE(), GETDATE()),
(6.5, 6.0, 6.5, 7.0, 8, 6.5, N'Trung bình', GETDATE(), GETDATE()),
(10.0, 9.0, 9.5, 10.0, 9, 9.6, N'Xuất sắc', GETDATE(), GETDATE()),
(5.0, 4.0, 4.5, 5.0, 10, 4.6, N'Yếu', GETDATE(), GETDATE()),
(8.5, 7.5, 8.0, 8.5, 11, 8.1, N'Khá', GETDATE(), GETDATE()),
(7.0, 6.5, 6.0, 7.0, 12, 6.6, N'Trung bình', GETDATE(), GETDATE()),
(9.0, 8.0, 9.0, 9.0, 13, 8.8, N'Giỏi', GETDATE(), GETDATE()),
(4.0, 3.5, 4.0, 4.0, 14, 3.9, N'Kém', GETDATE(), GETDATE()),
(6.0, 6.5, 6.0, 6.5, 15, 6.3, N'Trung bình', GETDATE(), GETDATE()),
(9.5, 9.0, 9.5, 10.0, 16, 9.5, N'Xuất sắc', GETDATE(), GETDATE()),
(7.0, 6.0, 6.5, 7.0, 17, 6.6, N'Trung bình', GETDATE(), GETDATE()),
(8.0, 7.0, 8.0, 8.0, 18, 7.8, N'Khá', GETDATE(), GETDATE()),
(5.5, 5.0, 5.0, 5.5, 19, 5.3, N'Yếu', GETDATE(), GETDATE()),
(6.0, 6.5, 6.0, 6.0, 20, 6.1, N'Trung bình', GETDATE(), GETDATE());