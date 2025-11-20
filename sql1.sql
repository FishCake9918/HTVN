
CREATE DATABASE QLTCCN;
GO
USE QLTCCN;
GO

--  BẢNG VAI TRÒ
CREATE TABLE VAI_TRO (
    MaVaiTro INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro NVARCHAR(50) NOT NULL
);

-- BẢNG TÀI KHOẢN
CREATE TABLE TAI_KHOAN (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    MatKhau NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MaVaiTro INT NOT NULL
);

-- BẢNG ADMIN
CREATE TABLE ADMIN (
    MaAdmin INT IDENTITY(1,1) PRIMARY KEY,
    HoTenAdmin NVARCHAR(100) NOT NULL,
    MaTaiKhoan INT NOT NULL
);

-- BẢNG NGƯỜI DÙNG
CREATE TABLE NGUOI_DUNG (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    MaTaiKhoan INT NOT NULL
);

-- BẢNG LOẠI TÀI KHOẢN
CREATE TABLE LOAI_TAI_KHOAN (
    MaLoaiTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiTaiKhoan NVARCHAR(50) NOT NULL
);

-- BẢNG TÀI KHOẢN THANH TOÁN
CREATE TABLE TAI_KHOAN_THANH_TOAN (
    MaTaiKhoanThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    TenTaiKhoan NVARCHAR(100) NOT NULL,
    SoDuBanDau DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(50),
    MaNguoiDung INT NOT NULL,
    MaLoaiTaiKhoan INT NOT NULL
);

--BẢNG LOẠI GIAO DỊCH
CREATE TABLE LOAI_GIAO_DICH (
    MaLoaiGiaoDich INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiGiaoDich NVARCHAR(50) NOT NULL
);

-- BẢNG DANH MỤC CHI TIÊU
CREATE TABLE DANH_MUC_CHI_TIEU (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100) NOT NULL,
    DanhMucCha INT NULL,
    MaNguoiDung INT NOT NULL
);

-- BẢNG ĐỐI TƯỢNG GIAO DỊCH
CREATE TABLE DOI_TUONG_GIAO_DICH (
    MaDoiTuongGiaoDich INT IDENTITY(1,1) PRIMARY KEY,
    TenDoiTuong NVARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(255),
    MaNguoiDung INT NOT NULL
);

-- BẢNG GIAO DỊCH
CREATE TABLE GIAO_DICH (
    MaGiaoDich INT IDENTITY(1,1) PRIMARY KEY,
    TenGiaoDich NVARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(255),
    SoTien DECIMAL(18,2) NOT NULL,
    NgayGiaoDich DATE NOT NULL,
    MaDanhMuc INT,
    MaTaiKhoanThanhToan INT,
    MaLoaiGiaoDich INT,
    MaNguoiDung INT,
    MaDoiTuongGiaoDich INT
);

-- BẢNG NGÂN SÁCH
CREATE TABLE BANG_NGAN_SACH (
    MaNganSach INT IDENTITY(1,1) PRIMARY KEY,
    SoTien DECIMAL(18,2) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    MaNguoiDung INT,
    MaDanhMuc INT,
    MaGiaoDich INT,
);

-- BẢNG TRUNG GIAN DANH MỤC - NGÂN SÁCH
CREATE TABLE DANH_MUC_CHI_TIEU_NGAN_SACH (
    MaDanhMuc INT,
    MaNganSach INT,
    PRIMARY KEY (MaDanhMuc, MaNganSach)
);

-- BẢNG TRUNG GIAN GIAO DỊCH - NGÂN SÁCH
CREATE TABLE GIAO_DICH_NGAN_SACH (
    MaGiaoDich INT,
    MaNganSach INT,
    PRIMARY KEY (MaGiaoDich, MaNganSach)
);

-- BẢNG TRUNG GIAN TÀI KHOẢN THANH TOÁN - NGÂN SÁCH
CREATE TABLE TAI_KHOAN_THANH_TOAN_NGAN_SACH (
    MaTaiKhoanThanhToan INT,
    MaNganSach INT,
    PRIMARY KEY (MaTaiKhoanThanhToan, MaNganSach)
);

-- BẢNG TRUNG GIAN ĐỐI TƯỢNG GIAO DỊCH - NGÂN SÁCH
CREATE TABLE DOI_TUONG_GIAO_DICH_NGAN_SACH (
    MaDoiTuongGiaoDich INT,
    MaNganSach INT,
    PRIMARY KEY (MaDoiTuongGiaoDich, MaNganSach)
);

-- BẢNG THÔNG BÁO
CREATE TABLE THONG_BAO (
    MaThongBao INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(200) NOT NULL,
    NoiDung NVARCHAR(MAX),
    NgayTao DATETIME DEFAULT GETDATE(),
    MaAdmin INT
);
GO

-- RÀNG BUỘC CHO QUAN HỆ NGOẠI

-- VAI TRÒ - TÀI KHOẢN
ALTER TABLE TAI_KHOAN
ADD CONSTRAINT FK_TAIKHOAN_VAITRO FOREIGN KEY (MaVaiTro)
REFERENCES VAI_TRO(MaVaiTro);

-- TÀI KHOẢN - ADMIN
ALTER TABLE ADMIN
ADD CONSTRAINT FK_ADMIN_TAIKHOAN FOREIGN KEY (MaTaiKhoan)
REFERENCES TAI_KHOAN(MaTaiKhoan)
ON DELETE CASCADE;

-- TÀI KHOẢN - NGƯỜI DÙNG
ALTER TABLE NGUOI_DUNG
ADD CONSTRAINT FK_NGUOIDUNG_TAIKHOAN FOREIGN KEY (MaTaiKhoan)
REFERENCES TAI_KHOAN(MaTaiKhoan)
ON DELETE CASCADE;

-- NGƯỜI DÙNG - TÀI KHOẢN THANH TOÁN
ALTER TABLE TAI_KHOAN_THANH_TOAN
ADD CONSTRAINT FK_TKTT_NGUOIDUNG FOREIGN KEY (MaNguoiDung)
REFERENCES NGUOI_DUNG(MaNguoiDung)
ON DELETE CASCADE;

-- LOẠI TÀI KHOẢN - TÀI KHOẢN THANH TOÁN
ALTER TABLE TAI_KHOAN_THANH_TOAN
ADD CONSTRAINT FK_TKTT_LOAITAICHOAN FOREIGN KEY (MaLoaiTaiKhoan)
REFERENCES LOAI_TAI_KHOAN(MaLoaiTaiKhoan);

-- NGƯỜI DÙNG - DANH MỤC CHI TIÊU
ALTER TABLE DANH_MUC_CHI_TIEU
ADD CONSTRAINT FK_DMCT_NGUOIDUNG FOREIGN KEY (MaNguoiDung)
REFERENCES NGUOI_DUNG(MaNguoiDung)
ON DELETE CASCADE;

-- DANH MỤC CHA - DANH MỤC CON
ALTER TABLE DANH_MUC_CHI_TIEU
ADD CONSTRAINT FK_DMCT_CHA FOREIGN KEY (DanhMucCha)
REFERENCES DANH_MUC_CHI_TIEU(MaDanhMuc);

-- NGƯỜI DÙNG - ĐỐI TƯỢNG GIAO DỊCH
ALTER TABLE DOI_TUONG_GIAO_DICH
ADD CONSTRAINT FK_DTGD_NGUOIDUNG FOREIGN KEY (MaNguoiDung)
REFERENCES NGUOI_DUNG(MaNguoiDung)
ON DELETE CASCADE;

-- GIAO DỊCH - DANH MỤC
ALTER TABLE GIAO_DICH
ADD CONSTRAINT FK_GD_DMCT FOREIGN KEY (MaDanhMuc)
REFERENCES DANH_MUC_CHI_TIEU(MaDanhMuc);

-- GIAO DỊCH - TÀI KHOẢN THANH TOÁN
ALTER TABLE GIAO_DICH
ADD CONSTRAINT FK_GD_TKTT FOREIGN KEY (MaTaiKhoanThanhToan)
REFERENCES TAI_KHOAN_THANH_TOAN(MaTaiKhoanThanhToan);

-- GIAO DỊCH - LOẠI GIAO DỊCH
ALTER TABLE GIAO_DICH
ADD CONSTRAINT FK_GD_LOAIGD FOREIGN KEY (MaLoaiGiaoDich)
REFERENCES LOAI_GIAO_DICH(MaLoaiGiaoDich);

-- GIAO DỊCH - NGƯỜI DÙNG
ALTER TABLE GIAO_DICH
ADD CONSTRAINT FK_GD_NGUOIDUNG FOREIGN KEY (MaNguoiDung)
REFERENCES NGUOI_DUNG(MaNguoiDung);

-- GIAO DỊCH - ĐỐI TƯỢNG GIAO DỊCH
ALTER TABLE GIAO_DICH
ADD CONSTRAINT FK_GD_DTGD FOREIGN KEY (MaDoiTuongGiaoDich)
REFERENCES DOI_TUONG_GIAO_DICH(MaDoiTuongGiaoDich);

-- NGÂN SÁCH - NGƯỜI DÙNG
ALTER TABLE BANG_NGAN_SACH
ADD CONSTRAINT FK_NS_NGUOIDUNG FOREIGN KEY (MaNguoiDung)
REFERENCES NGUOI_DUNG(MaNguoiDung);

-- NGÂN SÁCH - DANH MỤC
ALTER TABLE BANG_NGAN_SACH
ADD CONSTRAINT FK_NS_DMCT FOREIGN KEY (MaDanhMuc)
REFERENCES DANH_MUC_CHI_TIEU(MaDanhMuc);

-- DANH MỤC - NGÂN SÁCH
ALTER TABLE DANH_MUC_CHI_TIEU_NGAN_SACH
ADD CONSTRAINT FK_DMCTNS_DMCT FOREIGN KEY (MaDanhMuc)
REFERENCES DANH_MUC_CHI_TIEU(MaDanhMuc)
ON DELETE CASCADE;

ALTER TABLE DANH_MUC_CHI_TIEU_NGAN_SACH
ADD CONSTRAINT FK_DMCTNS_NS FOREIGN KEY (MaNganSach)
REFERENCES BANG_NGAN_SACH(MaNganSach)
ON DELETE CASCADE;

-- GIAO DỊCH - NGÂN SÁCH
ALTER TABLE GIAO_DICH_NGAN_SACH
ADD CONSTRAINT FK_GDNS_GD FOREIGN KEY (MaGiaoDich)
REFERENCES GIAO_DICH(MaGiaoDich)
ON DELETE CASCADE;

ALTER TABLE GIAO_DICH_NGAN_SACH
ADD CONSTRAINT FK_GDNS_NS FOREIGN KEY (MaNganSach)
REFERENCES BANG_NGAN_SACH(MaNganSach)
ON DELETE CASCADE;

-- TÀI KHOẢN THANH TOÁN - NGÂN SÁCH
ALTER TABLE TAI_KHOAN_THANH_TOAN_NGAN_SACH
ADD CONSTRAINT FK_TKTTNS_TKTT FOREIGN KEY (MaTaiKhoanThanhToan)
REFERENCES TAI_KHOAN_THANH_TOAN(MaTaiKhoanThanhToan)
ON DELETE CASCADE;

ALTER TABLE TAI_KHOAN_THANH_TOAN_NGAN_SACH
ADD CONSTRAINT FK_TKTTNS_NS FOREIGN KEY (MaNganSach)
REFERENCES BANG_NGAN_SACH(MaNganSach)
ON DELETE CASCADE;

-- ĐỐI TƯỢNG GIAO DỊCH - NGÂN SÁCH
ALTER TABLE DOI_TUONG_GIAO_DICH_NGAN_SACH
ADD CONSTRAINT FK_DTGDNS_DTGD FOREIGN KEY (MaDoiTuongGiaoDich)
REFERENCES DOI_TUONG_GIAO_DICH(MaDoiTuongGiaoDich)
ON DELETE CASCADE;

ALTER TABLE DOI_TUONG_GIAO_DICH_NGAN_SACH
ADD CONSTRAINT FK_DTGDNS_NS FOREIGN KEY (MaNganSach)
REFERENCES BANG_NGAN_SACH(MaNganSach)
ON DELETE CASCADE;

-- THÔNG BÁO - ADMIN
ALTER TABLE THONG_BAO
ADD CONSTRAINT FK_THONGBAO_ADMIN FOREIGN KEY (MaAdmin)
REFERENCES ADMIN(MaAdmin)
ON DELETE SET NULL;
GO

USE QLTCCN;
GO

-- 1. BẢNG VAI TRÒ (admin, người dùng)
SET IDENTITY_INSERT VAI_TRO ON;
INSERT INTO VAI_TRO (MaVaiTro, TenVaiTro) VALUES
(1, N'admin'),
(2, N'người dùng');
SET IDENTITY_INSERT VAI_TRO OFF;
GO

-- 2. BẢNG LOẠI TÀI KHOẢN (Tiền mặt, Ngân hàng)
SET IDENTITY_INSERT LOAI_TAI_KHOAN ON;
INSERT INTO LOAI_TAI_KHOAN (MaLoaiTaiKhoan, TenLoaiTaiKhoan) VALUES
(1, N'Tiền mặt'),
(2, N'Ngân hàng');
SET IDENTITY_INSERT LOAI_TAI_KHOAN OFF;
GO

-- 3. BẢNG LOẠI GIAO DỊCH (thu, chi)
SET IDENTITY_INSERT LOAI_GIAO_DICH ON;
INSERT INTO LOAI_GIAO_DICH (MaLoaiGiaoDich, TenLoaiGiaoDich) VALUES
(1, N'thu'),
(2, N'chi');
SET IDENTITY_INSERT LOAI_GIAO_DICH OFF;
GO

-- 4. BẢNG TÀI KHOẢN (Tạo 1 admin, 1 người dùng)
SET IDENTITY_INSERT TAI_KHOAN ON;
INSERT INTO TAI_KHOAN (MaTaiKhoan, MatKhau, Email, MaVaiTro) VALUES
(1, N'hashed_admin_password_123', N'admin@email.com', 1),  -- Tài khoản admin (tham chiếu MaVaiTro 1)
(2, N'hashed_user_password_456', N'user_A@email.com', 2); -- Tài khoản người dùng (tham chiếu MaVaiTro 2)
SET IDENTITY_INSERT TAI_KHOAN OFF;
GO

-- 5. BẢNG ADMIN (Liên kết với MaTaiKhoan 1)
SET IDENTITY_INSERT ADMIN ON;
INSERT INTO ADMIN (MaAdmin, HoTenAdmin, MaTaiKhoan) VALUES
(1, N'Trần Quản Trị Viên', 1);
SET IDENTITY_INSERT ADMIN OFF;
GO

-- 6. BẢNG NGƯỜI DÙNG (Liên kết với MaTaiKhoan 2)
SET IDENTITY_INSERT NGUOI_DUNG ON;
INSERT INTO NGUOI_DUNG (MaNguoiDung, HoTen, GioiTinh, NgaySinh, MaTaiKhoan) VALUES
(1, N'Nguyễn Văn An', N'Nam', '1995-08-15', 2); -- Đây là người dùng chính (MaNguoiDung = 1)
SET IDENTITY_INSERT NGUOI_DUNG OFF;
GO

-- 7. BẢNG TÀI KHOẢN THANH TOÁN (CẬP NHẬT: Đã bỏ DonViTienTe, GhiChu)
SET IDENTITY_INSERT TAI_KHOAN_THANH_TOAN ON;
INSERT INTO TAI_KHOAN_THANH_TOAN (MaTaiKhoanThanhToan, TenTaiKhoan, SoDuBanDau, TrangThai, MaNguoiDung, MaLoaiTaiKhoan) VALUES
(1, N'Ví tiền mặt', 1500000, N'Đang hoạt động', 1, 1), -- MaLoaiTaiKhoan 1: Tiền mặt
(2, N'Tài khoản Vietcombank', 20000000, N'Đang hoạt động', 1, 2), -- MaLoaiTaiKhoan 2: Ngân hàng
(3, N'Tài khoản TPBank (cũ)', 0, N'Đóng', 1, 2); -- MaLoaiTaiKhoan 2: Ngân hàng
SET IDENTITY_INSERT TAI_KHOAN_THANH_TOAN OFF;
GO

-- 8. BẢNG ĐỐI TƯỢNG GIAO DỊCH (CẬP NHẬT: Thêm GhiChu)
SET IDENTITY_INSERT DOI_TUONG_GIAO_DICH ON;
INSERT INTO DOI_TUONG_GIAO_DICH (MaDoiTuongGiaoDich, TenDoiTuong, GhiChu, MaNguoiDung) VALUES
(1, N'Công ty TNHH ABC (Lương)', N'Đối tác trả lương hàng tháng', 1),
(2, N'Highlands Coffee', N'Chi nhánh gần công ty', 1),
(3, N'EVN (Tiền điện)', N'Thanh toán hóa đơn điện', 1),
(4, N'Grab', N'Dịch vụ di chuyển', 1),
(5, N'CGV Cinema', N'Giải trí cuối tuần', 1),
(6, N'Shopee', N'Mua sắm online', 1),
(7, N'Bệnh viện ĐH Y Dược', N'Nơi khám sức khỏe', 1),
(8, N'Nhà sách Fahasa', N'Mua sách', 1);
SET IDENTITY_INSERT DOI_TUONG_GIAO_DICH OFF;
GO

-- 9. BẢNG DANH MỤC CHI TIÊU (Của MaNguoiDung = 1)
SET IDENTITY_INSERT DANH_MUC_CHI_TIEU ON;
-- 9A. DANH MỤC CHA
INSERT INTO DANH_MUC_CHI_TIEU (MaDanhMuc, TenDanhMuc, DanhMucCha, MaNguoiDung) VALUES
(1, N'Ăn uống', NULL, 1),
(2, N'Hóa đơn', NULL, 1),
(3, N'Giải trí', NULL, 1),
(4, N'Di chuyển', NULL, 1),
(5, N'Mua sắm', NULL, 1),
(6, N'Sức khỏe', NULL, 1),
(7, N'Giáo dục', NULL, 1),
(8, N'Thu nhập', NULL, 1); -- Danh mục cho Thu nhập

-- 9B. DANH MỤC CON
INSERT INTO DANH_MUC_CHI_TIEU (MaDanhMuc, TenDanhMuc, DanhMucCha, MaNguoiDung) VALUES
(101, N'Cafe', 1, 1),
(102, N'Nhà hàng', 1, 1),
(103, N'Đi chợ/Siêu thị', 1, 1),
(201, N'Tiền điện', 2, 1),
(202, N'Tiền nước', 2, 1),
(203, N'Internet', 2, 1),
(204, N'Điện thoại', 2, 1),
(301, N'Xem phim', 3, 1),
(302, N'Nghe nhạc (Spotify)', 3, 1),
(303, N'Du lịch', 3, 1),
(401, N'Xăng xe', 4, 1),
(402, N'Gửi xe', 4, 1),
(403, N'Grab/Be', 4, 1),
(501, N'Quần áo', 5, 1),
(502, N'Đồ gia dụng', 5, 1),
(503, N'Mỹ phẩm', 5, 1),
(601, N'Khám bệnh', 6, 1),
(602, N'Thuốc', 6, 1),
(603, N'Bảo hiểm y tế', 6, 1),
(701, N'Học phí', 7, 1),
(702, N'Mua sách', 7, 1),
(703, N'Khóa học online', 7, 1),
(801, N'Lương', 8, 1),
(802, N'Thưởng', 8, 1),
(803, N'Thu nhập phụ', 8, 1);
SET IDENTITY_INSERT DANH_MUC_CHI_TIEU OFF;
GO

-- 10. BẢNG THÔNG BÁO (Gửi từ MaAdmin = 1)
SET IDENTITY_INSERT THONG_BAO ON;
INSERT INTO THONG_BAO (MaThongBao, TieuDe, NoiDung, NgayTao, MaAdmin) VALUES
(1, N'Chào mừng bạn đến với ứng dụng!', N'Hãy bắt đầu quản lý chi tiêu của bạn ngay hôm nay.', GETDATE(), 1),
(2, N'Cập nhật tính năng Ngân sách', N'Chúng tôi vừa ra mắt tính năng Ngân sách giúp bạn kiểm soát chi tiêu tốt hơn.', GETDATE(), 1);
SET IDENTITY_INSERT THONG_BAO OFF;
GO

-- 11. BẢNG GIAO DỊCH (Của MaNguoiDung = 1)
SET IDENTITY_INSERT GIAO_DICH ON;
-- Giao dịch THU (MaLoaiGiaoDich = 1)
INSERT INTO GIAO_DICH (MaGiaoDich, TenGiaoDich, GhiChu, SoTien, NgayGiaoDich, MaDanhMuc, MaTaiKhoanThanhToan, MaLoaiGiaoDich, MaNguoiDung, MaDoiTuongGiaoDich) VALUES
(1, N'Nhận lương tháng 11', N'Lương performance tốt', 25000000, '2025-11-05', 801, 2, 1, 1, 1); -- MaDanhMuc 801: Lương, TKTT 2: VCB, LoaiGD 1: Thu, DTGD 1: Cty ABC

-- Giao dịch CHI (MaLoaiGiaoDich = 2)
INSERT INTO GIAO_DICH (MaGiaoDich, TenGiaoDich, GhiChu, SoTien, NgayGiaoDich, MaDanhMuc, MaTaiKhoanThanhToan, MaLoaiGiaoDich, MaNguoiDung, MaDoiTuongGiaoDich) VALUES
(2, N'Cafe sáng', N'Cafe muối', 65000, '2025-11-06', 101, 1, 2, 1, 2), -- DM 101: Cafe, TKTT 1: Tiền mặt, LoaiGD 2: Chi, DTGD 2: Highlands
(3, N'Đóng tiền điện T10', N'Hóa đơn điện tháng 10', 850000, '2025-11-07', 201, 2, 2, 1, 3), -- DM 201: Tiền điện, TKTT 2: VCB, LoaiGD 2: Chi, DTGD 3: EVN
(4, N'Đi Grab đi làm', N'', 45000, '2025-11-08', 403, 1, 2, 1, 4), -- DM 403: Grab, TKTT 1: Tiền mặt, LoaiGD 2: Chi, DTGD 4: Grab
(5, N'Mua áo khoác Shopee', N'Chuẩn bị mùa đông', 450000, '2025-11-08', 501, 2, 2, 1, 6), -- DM 501: Quần áo, TKTT 2: VCB, LoaiGD 2: Chi, DTGD 6: Shopee
(6, N'Xem phim cuối tuần', N'Phim "Hành tinh cát"', 250000, '2025-11-09', 301, 1, 2, 1, 5), -- DM 301: Xem phim, TKTT 1: Tiền mặt, LoaiGD 2: Chi, DTGD 5: CGV
(7, N'Khám sức khỏe tổng quát', N'Kiểm tra định kỳ', 1200000, '2025-11-10', 601, 2, 2, 1, 7), -- DM 601: Khám bệnh, TKTT 2: VCB, LoaiGD 2: Chi, DTGD 7: BV Y Dược
(8, N'Mua sách "Tâm lý học"', N'Sách chuyên ngành', 180000, '2025-11-11', 702, 1, 2, 1, 8); -- DM 702: Mua sách, TKTT 1: Tiền mặt, LoaiGD 2: Chi, DTGD 8: Fahasa

SET IDENTITY_INSERT GIAO_DICH OFF;
GO

-- 12. BẢNG NGÂN SÁCH (Của MaNguoiDung = 1, liên kết với Danh mục CHA)
-- Lưu ý: Bảng của bạn có vẻ bị lặp cột MaNganSach, tôi giả định đó là lỗi gõ máy và chỉ insert vào các cột đúng
SET IDENTITY_INSERT BANG_NGAN_SACH ON;
INSERT INTO BANG_NGAN_SACH (MaNganSach, SoTien, NgayBatDau, NgayKetThuc, MaNguoiDung, MaDanhMuc) VALUES
(1, 4000000, '2025-11-01', '2025-11-30', 1, 1), -- Ngân sách 4tr cho 'Ăn uống' (MaDanhMuc = 1)
(2, 2000000, '2025-11-01', '2025-11-30', 1, 3), -- Ngân sách 2tr cho 'Giải trí' (MaDanhMuc = 3)
(3, 3000000, '2025-11-01', '2025-11-30', 1, 5); -- Ngân sách 3tr cho 'Mua sắm' (MaDanhMuc = 5)
SET IDENTITY_INSERT BANG_NGAN_SACH OFF;
GO

-- 13. DANH MỤC - NGÂN SÁCH (Liên kết các danh mục CON với ngân sách CHA)
-- Ngân sách 1 (Ăn uống)
INSERT INTO DANH_MUC_CHI_TIEU_NGAN_SACH (MaDanhMuc, MaNganSach) VALUES
(101, 1), (102, 1), (103, 1);

-- Ngân sách 2 (Giải trí)
INSERT INTO DANH_MUC_CHI_TIEU_NGAN_SACH (MaDanhMuc, MaNganSach) VALUES
(301, 2), (302, 2), (303, 2);

-- Ngân sách 3 (Mua sắm)
INSERT INTO DANH_MUC_CHI_TIEU_NGAN_SACH (MaDanhMuc, MaNganSach) VALUES
(501, 3), (502, 3), (503, 3);
GO

-- 14. GIAO DỊCH - NGÂN SÁCH (Liên kết các giao dịch CHI với ngân sách tương ứng)
INSERT INTO GIAO_DICH_NGAN_SACH (MaGiaoDich, MaNganSach) VALUES
(2, 1), -- Giao dịch 2 (Cafe) -> Ngân sách 1 (Ăn uống)
(5, 3), -- Giao dịch 5 (Mua áo) -> Ngân sách 3 (Mua sắm)
(6, 2); -- Giao dịch 6 (Xem phim) -> Ngân sách 2 (Giải trí)
GO

-- 15. TÀI KHOẢN THANH TOÁN - NGÂN SÁCH (Ngân sách này áp dụng cho tài khoản nào)
INSERT INTO TAI_KHOAN_THANH_TOAN_NGAN_SACH (MaTaiKhoanThanhToan, MaNganSach) VALUES
(1, 1), (2, 1), -- NS 1 (Ăn uống) áp dụng cho Tiền mặt (1) và VCB (2)
(1, 2), (2, 2), -- NS 2 (Giải trí) áp dụng cho Tiền mặt (1) và VCB (2)
(1, 3), (2, 3); -- NS 3 (Mua sắm) áp dụng cho Tiền mặt (1) và VCB (2)
GO

-- 16. ĐỐI TƯỢNG GIAO DỊCH - NGÂN SÁCH
INSERT INTO DOI_TUONG_GIAO_DICH_NGAN_SACH (MaDoiTuongGiaoDich, MaNganSach) VALUES
(6, 3); -- Shopee (DTGD 6) -> Ngân sách Mua sắm (NS 3)
GO







