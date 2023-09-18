-- Создание базы данных
CREATE DATABASE shoping;

-- Использование созданной базы данных
USE shoping;

-- Создание таблицы Customers
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(32) NOT NULL,
    LastName VARCHAR(32) NOT NULL,
    Email VARCHAR(30) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL
);

-- Создание таблицы Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    OrderName VARCHAR(100) NOT NULL,
    OrderDate DATETIME NOT NULL,
    OrderAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);
