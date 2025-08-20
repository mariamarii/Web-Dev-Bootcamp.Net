

CREATE DATABASE task_dapper;
GO
USE task_dapper;
GO


CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId)
);
GO

CREATE VIEW vw_ProductWithCategory
AS
SELECT p.ProductId, p.Name AS ProductName, p.Price, c.Name AS CategoryName, p.CategoryId
FROM Product p
JOIN Category c ON p.CategoryId = c.CategoryId;
GO

CREATE FUNCTION fn_CountProductsByCategory(@CategoryId INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Product WHERE CategoryId = @CategoryId;
    RETURN @Count;
END;
GO

CREATE FUNCTION fn_TotalProductsPrice()
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Total DECIMAL(18,2);
    SELECT @Total = SUM(Price) FROM Product;
    RETURN ISNULL(@Total, 0);
END;
GO

-- Create stored procedures
CREATE PROCEDURE sp_AddCategory @Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Category(Name) VALUES(@Name);
END;
GO

CREATE PROCEDURE sp_GetCategories
AS
BEGIN
    SELECT CategoryId, Name AS CategoryName FROM Category;
END;
GO

CREATE PROCEDURE sp_UpdateCategory @CategoryId INT, @Name NVARCHAR(100)
AS
BEGIN
    UPDATE Category SET Name = @Name WHERE CategoryId = @CategoryId;
END;
GO

CREATE PROCEDURE sp_DeleteCategory @CategoryId INT
AS
BEGIN
    DELETE FROM Category WHERE CategoryId = @CategoryId;
END;
GO

CREATE PROCEDURE sp_AddProduct @ProductName NVARCHAR(100), @Price DECIMAL(18,2), @CategoryId INT
AS
BEGIN
    INSERT INTO Product(Name, Price, CategoryId) 
    VALUES(@ProductName, @Price, @CategoryId);
END;
GO

CREATE PROCEDURE sp_GetProducts
AS
BEGIN
    SELECT ProductId, ProductName, Price, CategoryName, CategoryId 
    FROM vw_ProductWithCategory;
END;
GO

CREATE PROCEDURE sp_UpdateProduct @ProductId INT, @ProductName NVARCHAR(100), @Price DECIMAL(18,2), @CategoryId INT
AS
BEGIN
    UPDATE Product 
    SET Name = @ProductName, Price = @Price, CategoryId = @CategoryId
    WHERE ProductId = @ProductId;
END;
GO

CREATE PROCEDURE sp_DeleteProduct @ProductId INT
AS
BEGIN
    DELETE FROM Product WHERE ProductId = @ProductId;
END;
GO

CREATE PROCEDURE GetProductById @ProductId INT
AS
BEGIN
    SELECT ProductId, Name AS ProductName, Price, CategoryId 
    FROM Product 
    WHERE ProductId = @ProductId;
END;
GO

CREATE PROCEDURE GetCategoryById @CategoryId INT
AS
BEGIN
    SELECT CategoryId, Name AS CategoryName 
    FROM Category 
    WHERE CategoryId = @CategoryId;
END;
GO

