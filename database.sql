-- T?o co s? d? li?u
CREATE DATABASE RestaurantManagement;
GO

USE RestaurantManagement;
GO

-- B?ng User (Ngu?i dùng)
CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CONSTRAINT CHK_Email CHECK (Email LIKE '%_@__%.__%')
);

-- B?ng Account (Tài kho?n dang nh?p)
CREATE TABLE Account (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User',
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [User](UserID) ON DELETE CASCADE
);

-- B?ng Dishes (Món an)
CREATE TABLE Dishes (
    DishID INT IDENTITY(1,1) PRIMARY KEY,
    DishName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(255),
    Stock INT NOT NULL DEFAULT 0,
    CONSTRAINT CHK_Price CHECK (Price >= 0),
    CONSTRAINT CHK_Stock CHECK (Stock >= 0)
);

-- B?ng Order (Ðon hàng)
CREATE TABLE [Order] (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    FOREIGN KEY (UserID) REFERENCES [User](UserID) ON DELETE CASCADE,
    CONSTRAINT CHK_TotalAmount CHECK (TotalAmount >= 0)
);

-- B?ng OrderDetail (Chi ti?t don hàng)
CREATE TABLE OrderDetail (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    DishID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID) ON DELETE CASCADE,
    FOREIGN KEY (DishID) REFERENCES Dishes(DishID) ON DELETE CASCADE,
    CONSTRAINT CHK_Quantity CHECK (Quantity > 0),
    CONSTRAINT CHK_UnitPrice CHECK (UnitPrice >= 0)
);

-- B?ng BookingTable (Ð?t bàn)
CREATE TABLE BookingTable (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    NumberOfPeople INT NOT NULL,
    TableNumber INT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    FOREIGN KEY (UserID) REFERENCES [User](UserID) ON DELETE CASCADE,
    CONSTRAINT CHK_NumberOfPeople CHECK (NumberOfPeople > 0),
    CONSTRAINT CHK_TableNumber CHECK (TableNumber > 0)
);

-- Thêm d? li?u m?u
INSERT INTO [User] (FullName, Email) VALUES 
('Nguyen Van A', 'nguyenvana@gmail.com'),
('Tran Thi B', 'tranthib@gmail.com');

INSERT INTO Account (Username, PasswordHash, Role, UserID) VALUES 
('admin', 'hashedpassword123', 'Admin', 1), -- M?t kh?u nên du?c bam trong th?c t?
('user1', 'hashedpassword456', 'User', 2);

INSERT INTO Dishes (DishName, Price, Description, Stock) VALUES 
('Ph? Bò', 50000, 'Ph? bò truy?n th?ng', 100),
('Com T?m', 40000, 'Com t?m su?n nu?ng', 50);

INSERT INTO [Order] (UserID, OrderDate, TotalAmount, Status) VALUES 
(1, GETDATE(), 150000, 'Completed');

INSERT INTO OrderDetail (OrderID, DishID, Quantity, UnitPrice) VALUES 
(1, 1, 3, 50000);

INSERT INTO BookingTable (UserID, BookingDate, NumberOfPeople, TableNumber, Status) VALUES 
(1, '2025-03-24 18:00:00', 4, 5, 'Confirmed');

SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Dishes';

ALTER TABLE Dishes
ADD Category NVARCHAR(50) NOT NULL DEFAULT 'Other';

INSERT INTO Dishes (DishName, Price, Description, Stock, Category) VALUES 
('Phở Bò', 50000, 'Phở bò truyền thống', 100, 'WaterFood'),
('Hủ Tiếu', 45000, 'Hủ tiếu Nam Vang', 80, 'WaterFood'),
('Bún Bò Huế', 55000, 'Bún bò Huế cay nồng', 60, 'WaterFood'),
('Hoành Thánh', 40000, 'Hoành thánh nhân tôm thịt', 50, 'WaterFood'),
('Cơm Tấm', 40000, 'Cơm tấm sườn nướng', 50, 'DryFood'),
('Cơm Gà', 45000, 'Cơm gà chiên giòn', 40, 'DryFood'),
('Cơm Chiên Trân Châu', 50000, 'Cơm chiên với hải sản', 30, 'DryFood'),
('Combo Cặp Đôi', 120000, 'Combo cho 2 người', 20, 'Combo'),
('Combo Lunch', 80000, 'Combo bữa trưa', 25, 'Combo'),
('Combo 79k', 79000, 'Combo tiết kiệm', 30, 'Combo'),
('Kem Dâu', 20000, 'Kem dâu tây mát lạnh', 50, 'Dessert'),
('Bánh Flan', 15000, 'Bánh flan mềm mịn', 60, 'Dessert'),
('Bánh Kếp', 25000, 'Bánh kếp nhân socola', 40, 'Dessert'),
('Thạch Rau Câu', 10000, 'Thạch rau câu nhiều màu', 70, 'Dessert');