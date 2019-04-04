CREATE DATABASE Sales
USE Sales


CREATE TABLE Clients(
Id INT NOT NULL PRIMARY KEY IDENTITY,
NAME NVARCHAR(100) NOT NULL,
DtBirth DATETIME NOT NULL,
Gender NVARCHAR(14) NOT NULL,
Email NVARCHAR(100) NOT NULL,
Active BIT DEFAULT 0
)

CREATE TABLE Product (
Id INT NOT NULL PRIMARY KEY IDENTITY,
ClientId INT NOT NULL,
ProductName NVARCHAR(100)
CONSTRAINT fk_CliPro FOREIGN KEY (ClientId) REFERENCES Clients (Id)
)


INSERT INTO Clients (DtBirth, Gender, Email, Active)  VALUES ('JOÃO DA SILVA', CAST('01/01/1984' AS datetime), 'MASCULINO', 'jsilva@stefanini.com', 1 )
INSERT INTO Clients (DtBirth, Gender, Email, Active)  VALUES ('MARIA DA SILVA', CAST('12/02/1984' AS datetime), 'FEMININO', 'msilva@stefanini.com', 1 )
INSERT INTO Clients (DtBirth, Gender, Email)  VALUES ('ANTONIO,DA SILVA', CAST('01/12/1984' AS datetime), 'MASCULINO', 'asilva@stefanini.com' )

INSERT INTO Product (ProductName, ClientId) VALUES (1, 'PRODUTO A')
INSERT INTO Product (ProductName, ClientId) VALUES (1, 'PRODUTO B')
INSERT INTO Product (ProductName, ClientId) VALUES (1, 'PRODUTO C')
INSERT INTO Product (ProductName, ClientId) VALUES (2, 'PRODUTO A')
INSERT INTO Product (ProductName, ClientId) VALUES (2, 'PRODUTO B')
