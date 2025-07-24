-- Tạo các bảng mặc định của identity
CREATE TABLE AspNetRoles (
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    Name NVARCHAR(256),
    NormalizedName NVARCHAR(256),
    ConcurrencyStamp NVARCHAR(MAX)
);

CREATE TABLE AspNetUsers (
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(256),
    NormalizedUserName NVARCHAR(256),
    Email NVARCHAR(256),
    NormalizedEmail NVARCHAR(256),
    EmailConfirmed BIT NOT NULL,
    PasswordHash NVARCHAR(MAX),
    SecurityStamp NVARCHAR(MAX),
    ConcurrencyStamp NVARCHAR(MAX),
    PhoneNumber NVARCHAR(MAX),
    PhoneNumberConfirmed BIT NOT NULL,
    TwoFactorEnabled BIT NOT NULL,
    LockoutEnd DATETIMEOFFSET,
    LockoutEnabled BIT NOT NULL,
    AccessFailedCount INT NOT NULL
);

CREATE TABLE AspNetRoleClaims (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    RoleId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX),
    ClaimValue NVARCHAR(MAX),
    CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserClaims (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX),
    ClaimValue NVARCHAR(MAX),
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserLogins (
    LoginProvider NVARCHAR(450) NOT NULL,
    ProviderKey NVARCHAR(450) NOT NULL,
    ProviderDisplayName NVARCHAR(MAX),
    UserId NVARCHAR(450) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserRoles (
    UserId NVARCHAR(450) NOT NULL,
    RoleId NVARCHAR(450) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId)
        REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserTokens (
    UserId NVARCHAR(450) NOT NULL,
    LoginProvider NVARCHAR(450) NOT NULL,
    Name NVARCHAR(450) NOT NULL,
    Value NVARCHAR(MAX),
    PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId)
        REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- end tạo các bảng mặc định của identity

-- Tạo các bảng của module quản lý học phần
CREATE TABLE HocKy (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaHocKy NVARCHAR(MAX),
    TenHocKy NVARCHAR(MAX),
    NamHoc NVARCHAR(MAX),
    ThuTu INT NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE Khoa (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaKhoa NVARCHAR(MAX),
    TenKhoa NVARCHAR(MAX),
    IdTruongKhoa INT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);
CREATE TABLE GiangVien (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaGiangVien NVARCHAR(MAX),
    HoTen NVARCHAR(MAX),
    NgaySinh DATETIME,
    GioiTinh NVARCHAR(MAX),
    HinhDaiDien NVARCHAR(MAX),
    DiaChi NVARCHAR(MAX),
    UserId NVARCHAR(MAX),
    IdKhoa INT,
    IsActive BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);
CREATE TABLE Lop (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaLop NVARCHAR(MAX),
    TenLop NVARCHAR(MAX),
    IdKhoa INT,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE MonHoc (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaMonHoc NVARCHAR(MAX),
    TenMonHoc NVARCHAR(MAX),
    IdKhoa INT,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE SinhVien (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaSinhVien NVARCHAR(MAX),
    HoTen NVARCHAR(MAX),
    NgaySinh DATETIME,
    GioiTinh INT,
    HinhDaiDien NVARCHAR(MAX),
    DiaChi NVARCHAR(MAX),
    UserId NVARCHAR(MAX),
    IdLop INT,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE HocPhan (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaHocPhan NVARCHAR(MAX),
    TenHocPhan NVARCHAR(MAX),
    IdHocKy INT NOT NULL,
    SoTinChi INT,
    IdMonHoc INT NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE LopHocPhan (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaLopHocPhan NVARCHAR(MAX),
    TenLopHocPhan NVARCHAR(MAX),
    SiSoToiDa INT,
    ThoiGianHoc NVARCHAR(MAX),
    DiaDiemHoc NVARCHAR(MAX),
    GhiChu NVARCHAR(MAX),
    HeSoChuyenCan DECIMAL(18,2),
    HeSoGiuaKy DECIMAL(18,2),
    HeSoThucHanh DECIMAL(18,2),
    HeSoCuoiKy DECIMAL(18,2),
    IdHocPhan INT NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE DangKyHocPhan (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdSinhVien INT NOT NULL,
    IdLopHocPhan INT NOT NULL,
    TrangThai INT NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE Diem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DiemChuyenCan DECIMAL(18,2),
    DiemGiuaKy DECIMAL(18,2),
    DiemThucHanh DECIMAL(18,2),
    DiemCuoiKy DECIMAL(18,2),
    IdDangKyHocPhan INT NOT NULL UNIQUE,
    DiemTrungBinh DECIMAL(18,2),
    XepLoai NVARCHAR(MAX),
    NgayTao DATETIME NOT NULL DEFAULT GETUTCDATE(),
    NgayCapNhat DATETIME NULL
);

CREATE TABLE PhanCongGiangDay (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdLopHocPhan INT NOT NULL,
    IdGiangVien INT NOT NULL,
    NgayPhanCong DATETIME NOT NULL,
    VaiTro INT NOT NULL,
    GhiChu NVARCHAR(MAX)
);

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

CREATE TABLE ThanhToanHocPhi (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdSinhVien INT NOT NULL,
    IdHocKy INT NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    PhuongThuc INT,
    TrangThai INT,
    NgayThanhToan DATETIME NOT NULL DEFAULT GETDATE()
);

-- End tạo các bảng của module quản lý học phần

-- Các ràng buộc khóa ngoại
ALTER TABLE DangKyHocPhan ADD CONSTRAINT FK_DKHP_SinhVien FOREIGN KEY (IdSinhVien) REFERENCES SinhVien(Id);
ALTER TABLE DangKyHocPhan ADD CONSTRAINT FK_DKHP_LopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan(Id);
ALTER TABLE Diem ADD CONSTRAINT FK_Diem_DangKyHocPhan FOREIGN KEY (IdDangKyHocPhan) REFERENCES DangKyHocPhan(Id);
ALTER TABLE PhanCongGiangDay ADD CONSTRAINT FK_PCGD_LopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan(Id);
ALTER TABLE PhanCongGiangDay ADD CONSTRAINT FK_PCGD_GiangVien FOREIGN KEY (IdGiangVien) REFERENCES GiangVien(Id);
ALTER TABLE BaiHoc ADD CONSTRAINT FK_BaiHoc_LopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan(Id);
ALTER TABLE ThanhToanHocPhi ADD CONSTRAINT FK_TTHP_SinhVien FOREIGN KEY (IdSinhVien) REFERENCES SinhVien(Id);
ALTER TABLE ThanhToanHocPhi ADD CONSTRAINT FK_TTHP_HocKy FOREIGN KEY (IdHocKy) REFERENCES HocKy(Id);
-- end các ràng buộc khóa ngoại

-- Tạo bảng CauHinhHeThong
CREATE TABLE CauHinhHeThong (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MaCauHinh NVARCHAR(100) NOT NULL,
    TenCauHinh NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    GiaTri NVARCHAR(MAX) NULL
);

