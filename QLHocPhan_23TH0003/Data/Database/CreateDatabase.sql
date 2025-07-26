IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[CauHinhHeThong] (
    [Id] int NOT NULL IDENTITY,
    [MaCauHinh] nvarchar(max) NOT NULL,
    [TenCauHinh] nvarchar(max) NULL,
    [MoTa] nvarchar(max) NULL,
    [GiaTri] nvarchar(max) NULL,
    CONSTRAINT [PK_CauHinhHeThong] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[HocKy] (
    [Id] int NOT NULL IDENTITY,
    [MaHocKy] nvarchar(max) NULL,
    [TenHocKy] nvarchar(max) NULL,
    [NamHoc] nvarchar(max) NULL,
    [ThuTu] int NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_HocKy] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[BaiHoc] (
    [Id] int NOT NULL IDENTITY,
    [TenBaiHoc] nvarchar(max) NULL,
    [NoiDung] nvarchar(max) NULL,
    [Video] nvarchar(max) NULL,
    [TaiLieu] nvarchar(max) NULL,
    [IdLopHocPhan] int NULL,
    CONSTRAINT [PK_BaiHoc] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[DangKyHocPhan] (
    [Id] int NOT NULL IDENTITY,
    [IdSinhVien] int NOT NULL,
    [IdLopHocPhan] int NOT NULL,
    [TrangThai] int NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_DangKyHocPhan] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Diem] (
    [Id] int NOT NULL IDENTITY,
    [DiemChuyenCan] decimal(18,2) NULL,
    [DiemGiuaKy] decimal(18,2) NULL,
    [DiemThucHanh] decimal(18,2) NULL,
    [DiemCuoiKy] decimal(18,2) NULL,
    [IdDangKyHocPhan] int NOT NULL,
    [DiemTrungBinh] decimal(18,2) NULL,
    [XepLoai] nvarchar(max) NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_Diem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Diem_DangKyHocPhan_IdDangKyHocPhan] FOREIGN KEY ([IdDangKyHocPhan]) REFERENCES [DangKyHocPhan] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[GiangVien] (
    [Id] int NOT NULL IDENTITY,
    [MaGiangVien] nvarchar(max) NULL,
    [HoTen] nvarchar(max) NULL,
    [NgaySinh] datetime2 NULL,
    [GioiTinh] nvarchar(max) NULL,
    [HinhDaiDien] nvarchar(max) NULL,
    [DiaChi] nvarchar(max) NULL,
    [UserId] nvarchar(max) NULL,
    [IdKhoa] int NULL,
    [IsActive] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_GiangVien] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Khoa] (
    [Id] int NOT NULL IDENTITY,
    [MaKhoa] nvarchar(max) NULL,
    [TenKhoa] nvarchar(max) NULL,
    [IdTruongKhoa] int NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_Khoa] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Khoa_GiangVien_IdTruongKhoa] FOREIGN KEY ([IdTruongKhoa]) REFERENCES [GiangVien] ([Id])
);

CREATE TABLE [dbo].[Lop] (
    [Id] int NOT NULL IDENTITY,
    [MaLop] nvarchar(max) NULL,
    [TenLop] nvarchar(max) NULL,
    [IdKhoa] int NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_Lop] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lop_Khoa_IdKhoa] FOREIGN KEY ([IdKhoa]) REFERENCES [Khoa] ([Id])
);

CREATE TABLE [dbo].[MonHoc] (
    [Id] int NOT NULL IDENTITY,
    [MaMonHoc] nvarchar(max) NULL,
    [TenMonHoc] nvarchar(max) NULL,
    [IdKhoa] int NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_MonHoc] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MonHoc_Khoa_IdKhoa] FOREIGN KEY ([IdKhoa]) REFERENCES [Khoa] ([Id])
);

CREATE TABLE [dbo].[SinhVien] (
    [Id] int NOT NULL IDENTITY,
    [MaSinhVien] nvarchar(max) NULL,
    [HoTen] nvarchar(max) NULL,
    [NgaySinh] datetime2 NULL,
    [GioiTinh] int NULL,
    [HinhDaiDien] nvarchar(max) NULL,
    [DiaChi] nvarchar(max) NULL,
    [UserId] nvarchar(max) NULL,
    [IdLop] int NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_SinhVien] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SinhVien_Lop_IdLop] FOREIGN KEY ([IdLop]) REFERENCES [Lop] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [dbo].[HocPhan] (
    [Id] int NOT NULL IDENTITY,
    [MaHocPhan] nvarchar(max) NULL,
    [TenHocPhan] nvarchar(max) NULL,
    [IdHocKy] int NOT NULL,
    [SoTinChi] int NULL,
    [IdMonHoc] int NOT NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_HocPhan] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HocPhan_HocKy_IdHocKy] FOREIGN KEY ([IdHocKy]) REFERENCES [HocKy] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_HocPhan_MonHoc_IdMonHoc] FOREIGN KEY ([IdMonHoc]) REFERENCES [MonHoc] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [dbo].[ThanhToanHocPhi] (
    [Id] int NOT NULL IDENTITY,
    [IdSinhVien] int NOT NULL,
    [IdHocKy] int NOT NULL,
    [HocKyId] int NOT NULL,
    [SoTien] decimal(18,2) NOT NULL,
    [PhuongThuc] int NOT NULL,
    [TrangThai] int NOT NULL,
    [NgayThanhToan] datetime2 NOT NULL,
    CONSTRAINT [PK_ThanhToanHocPhi] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ThanhToanHocPhi_HocKy_HocKyId] FOREIGN KEY ([HocKyId]) REFERENCES [HocKy] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ThanhToanHocPhi_SinhVien_IdSinhVien] FOREIGN KEY ([IdSinhVien]) REFERENCES [SinhVien] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[LopHocPhan] (
    [Id] int NOT NULL IDENTITY,
    [MaLopHocPhan] nvarchar(max) NULL,
    [TenLopHocPhan] nvarchar(max) NULL,
    [SiSoToiDa] int NULL,
    [ThoiGianHoc] nvarchar(max) NULL,
    [DiaDiemHoc] nvarchar(max) NULL,
    [GhiChu] nvarchar(max) NULL,
    [HeSoChuyenCan] decimal(18,2) NULL,
    [HeSoGiuaKy] decimal(18,2) NULL,
    [HeSoThucHanh] decimal(18,2) NULL,
    [HeSoCuoiKy] decimal(18,2) NULL,
    [IdHocPhan] int NOT NULL,
    [IsDeleted] bit NOT NULL,
    [NgayTao] datetime2 NOT NULL,
    [NgayCapNhat] datetime2 NULL,
    CONSTRAINT [PK_LopHocPhan] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LopHocPhan_HocPhan_IdHocPhan] FOREIGN KEY ([IdHocPhan]) REFERENCES [HocPhan] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [dbo].[PhanCongGiangDay] (
    [Id] int NOT NULL IDENTITY,
    [IdLopHocPhan] int NOT NULL,
    [IdGiangVien] int NOT NULL,
    [NgayPhanCong] datetime2 NOT NULL,
    [VaiTro] int NOT NULL,
    [GhiChu] nvarchar(max) NULL,
    CONSTRAINT [PK_PhanCongGiangDay] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PhanCongGiangDay_GiangVien_IdGiangVien] FOREIGN KEY ([IdGiangVien]) REFERENCES [GiangVien] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PhanCongGiangDay_LopHocPhan_IdLopHocPhan] FOREIGN KEY ([IdLopHocPhan]) REFERENCES [LopHocPhan] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_BaiHoc_IdLopHocPhan] ON [BaiHoc] ([IdLopHocPhan]);

CREATE INDEX [IX_DangKyHocPhan_IdLopHocPhan] ON [DangKyHocPhan] ([IdLopHocPhan]);

CREATE INDEX [IX_DangKyHocPhan_IdSinhVien] ON [DangKyHocPhan] ([IdSinhVien]);

CREATE UNIQUE INDEX [IX_Diem_IdDangKyHocPhan] ON [Diem] ([IdDangKyHocPhan]);

CREATE INDEX [IX_GiangVien_IdKhoa] ON [GiangVien] ([IdKhoa]);

CREATE INDEX [IX_HocPhan_IdHocKy] ON [HocPhan] ([IdHocKy]);

CREATE INDEX [IX_HocPhan_IdMonHoc] ON [HocPhan] ([IdMonHoc]);

CREATE UNIQUE INDEX [IX_Khoa_IdTruongKhoa] ON [Khoa] ([IdTruongKhoa]) WHERE [IdTruongKhoa] IS NOT NULL;

CREATE INDEX [IX_Lop_IdKhoa] ON [Lop] ([IdKhoa]);

CREATE INDEX [IX_LopHocPhan_IdHocPhan] ON [LopHocPhan] ([IdHocPhan]);

CREATE INDEX [IX_MonHoc_IdKhoa] ON [MonHoc] ([IdKhoa]);

CREATE INDEX [IX_PhanCongGiangDay_IdGiangVien] ON [PhanCongGiangDay] ([IdGiangVien]);

CREATE INDEX [IX_PhanCongGiangDay_IdLopHocPhan] ON [PhanCongGiangDay] ([IdLopHocPhan]);

CREATE INDEX [IX_SinhVien_IdLop] ON [SinhVien] ([IdLop]);

CREATE INDEX [IX_ThanhToanHocPhi_HocKyId] ON [ThanhToanHocPhi] ([HocKyId]);

CREATE INDEX [IX_ThanhToanHocPhi_IdSinhVien] ON [ThanhToanHocPhi] ([IdSinhVien]);

ALTER TABLE [BaiHoc] ADD CONSTRAINT [FK_BaiHoc_LopHocPhan_IdLopHocPhan] FOREIGN KEY ([IdLopHocPhan]) REFERENCES [LopHocPhan] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [DangKyHocPhan] ADD CONSTRAINT [FK_DangKyHocPhan_LopHocPhan_IdLopHocPhan] FOREIGN KEY ([IdLopHocPhan]) REFERENCES [LopHocPhan] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [DangKyHocPhan] ADD CONSTRAINT [FK_DangKyHocPhan_SinhVien_IdSinhVien] FOREIGN KEY ([IdSinhVien]) REFERENCES [SinhVien] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [GiangVien] ADD CONSTRAINT [FK_GiangVien_Khoa_IdKhoa] FOREIGN KEY ([IdKhoa]) REFERENCES [Khoa] ([Id]) ON DELETE NO ACTION;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250726121839_InitDatabase', N'9.0.0');

COMMIT;
GO

