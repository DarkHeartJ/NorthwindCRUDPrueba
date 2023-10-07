--Stored Procedures de la tabla Customers

--Obtener todos los registros
CREATE PROCEDURE CustomersGetAll
AS
SELECT CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax
FROM Customers

--Obtener los registros por Id
CREATE PROCEDURE CustomersGetById
	@CustomerId NCHAR(5)
AS
SELECT CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax
FROM Customers
WHERE CustomerId = @CustomerId

--Agregar un nuevo registro
CREATE PROCEDURE CustomersAdd
	@CustomerId NCHAR(5),
	@CompanyName NVARCHAR(40),
	@ContactName NVARCHAR(30),
	@ContactTitle NVARCHAR(30), 
	@Address NVARCHAR(60), 
	@City NVARCHAR(15), 
	@Region NVARCHAR(15), 
	@PostalCode NVARCHAR(10), 
	@Country NVARCHAR(15), 
	@Phone NVARCHAR(24), 
	@Fax NVARCHAR(24)
AS
INSERT INTO Customers(CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) 
VALUES(@CustomerId, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)

--Actualizar un registro
CREATE PROCEDURE CustomersUpdate
	@CustomerId NCHAR(5),
	@CompanyName NVARCHAR(40),
	@ContactName NVARCHAR(30),
	@ContactTitle NVARCHAR(30), 
	@Address NVARCHAR(60), 
	@City NVARCHAR(15), 
	@Region NVARCHAR(15), 
	@PostalCode NVARCHAR(10), 
	@Country NVARCHAR(15), 
	@Phone NVARCHAR(24), 
	@Fax NVARCHAR(24)
AS
UPDATE Customers 
SET CustomerId = @CustomerId, CompanyName = @CompanyName, ContactName = @ContactName, ContactTitle = @ContactTitle, Address = @Address, City = @City, Region = @Region, PostalCode = @PostalCode, Country = @Country, Phone = @Phone, Fax = @Fax
WHERE CustomerId = @CustomerId

--Eliminar un registro
CREATE PROCEDURE CustomersDelete
	@CustomerId NCHAR(5)
AS
DELETE FROM Customers
WHERE CustomerId = @CustomerId

--SP para el Login
CREATE PROCEDURE CustomerGetByCustomerId
	@CustomerId NCHAR(5)
AS
SELECT
	CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax
FROM Customers WHERE CustomerId = @CustomerId


--Stored Procedures para la tabla Shippers

--Obtener todos los registros
CREATE PROCEDURE ShippersGetAll
AS
SELECT ShipperID, CompanyName, Phone
FROM Shippers

--Obtener los registros por Id
CREATE PROCEDURE ShippersGetById
	@ShipperID INT
AS
SELECT ShipperID, CompanyName, Phone
FROM Shippers
WHERE ShipperID = @ShipperID

--Agregar un nuevo registro
CREATE PROCEDURE ShippersAdd
	@CompanyName NVARCHAR(40),
	@Phone NVARCHAR(24)
AS
INSERT INTO Shippers(CompanyName, Phone) VALUES(@CompanyName, @Phone)

--Actualizar un registro
CREATE PROCEDURE ShippersUpdate
	@ShipperID INT,
	@CompanyName NVARCHAR(40),
	@Phone NVARCHAR(24)
AS
UPDATE Shippers SET CompanyName = @CompanyName, Phone = @Phone
WHERE ShipperID = @ShipperID

--Eliminar un registro
CREATE PROCEDURE ShippersDelete
	@ShipperID INT
AS
DELETE FROM Shippers WHERE ShipperID = @ShipperID

--SP para generar reporte JSON
CREATE PROCEDURE Reporte
AS
SELECT Orders.[OrderID]
      ,Orders.[CustomerID]
      ,Employees.[EmployeeID]
      ,Orders.[OrderDate]
      ,Orders.[RequiredDate]
      ,Orders.[ShippedDate]
      ,Orders.[ShipVia]
      ,Orders.[Freight]
      ,Orders.[ShipName]
      ,Orders.[ShipAddress]
      ,Orders.[ShipCity]
      ,Orders.[ShipRegion]
      ,Orders.[ShipPostalCode]
      ,Orders.[ShipCountry]
	  --ORDER DETAILS
	  ,[Order Details].ProductID
	  ,Products.ProductName
	  ,[Order Details].Quantity
	  ,[Order Details].Discount
	  ,[Order Details].UnitPrice
	  ,Suppliers.SupplierID
	  ,Suppliers.CompanyName
	  ,Suppliers.Address
  FROM [Orders]
  INNER JOIN Employees ON Employees.EmployeeID = Orders.EmployeeID
  INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID
  INNER JOIN Products ON [Order Details].ProductID = Products.ProductID
  INNER JOIN Suppliers ON Products.SupplierID = Suppliers.SupplierID