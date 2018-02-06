CREATE TABLE Merchant(
id int NOT NULL PRIMARY KEY,
name varchar(255) NOT NULL ,
balance int NOT NULL ,
);

INSERT INTO Merchant VALUES (101,'ABC',10000);
INSERT INTO Merchant VALUES (102,'XYZ',100000);

ALTER TABLE Merchant
DROP COLUMN name; 

UPDATE	Merchant
SET id=103 , balance=150000
WHERE id=101;

DELETE FROM Merchant 
WHERE balance = 10000;
 
TRUNCATE TABLE Merchant;

INSERT INTO Merchant VALUES (101,100000);

CREATE TABLE Customer(
id int NOT NULL PRIMARY KEY,
balance int NOT NULL,
merchantId int NOT NULL,
CONSTRAINT FK_Merchant FOREIGN KEY (merchantId)
REFERENCES Merchant(id)
);

INSERT INTO Customer VALUES (1001,1000,102);

BEGIN TRANSACTION;
UPDATE Merchant
SET balance = balance + 250;

UPDATE Customer
SET balance = balance - 250;

COMMIT TRANSACTION;

SELECT * FROM Merchant;
SELECT * FROM Customer;

BEGIN TRY
BEGIN TRANSACTION;
UPDATE Merchant
SET balance = balance + 250 ;

SELECT * FROM Merchant;
SELECT * FROM Customer;

UPDATE Customer
SET balance = balance - '500.50' ;

COMMIT TRANSACTION;

END TRY

BEGIN CATCH
PRINT 'Inside Catch'
ROLLBACK TRANSACTION ;

END CATCH;

SELECT * FROM Merchant;
SELECT * FROM Customer;