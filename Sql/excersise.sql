CREATE TABLE Persons (
    ID int NOT NULL UNIQUE,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Age int
);

INSERT INTO Persons
VALUES(101,'Abc','Pqr',21);
INSERT INTO Persons
VALUES (102,'Xyz',null,null);
SELECT * FROM Persons;

ALTER TABLE Persons
ADD PRIMARY KEY (Id);

ALTER TABLE Persons
ADD Salary money ;

ALTER TABLE Persons
ADD CONSTRAINT CHK_SALARY  CHECK (Salary >= 10000 AND SALARY <= 20000);

INSERT INTO Persons 
VALUES (103,'Malaviya','Akash',21,15000);

INSERT INTO Persons 
VALUES (103,'Malaviya','Akash',21,9000);

SELECT * FROM Persons
WHERE Id = 101;
