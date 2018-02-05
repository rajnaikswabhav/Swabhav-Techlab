CREATE TABLE Organization(
Id int NOT NULL PRIMARY KEY,
Name varchar(255) NOT NULL,
Contect varchar(255) NOT NULL,
);

CREATE TABLE Exhibition(
Id int NOT NULL,
date varchar(255) NOT NULL,
Name varchar(255) NOT NULL,
Location varchar(255) NOT NULL,
OrganizationId int NOT NULL,
PRIMARY KEY(Id),
CONSTRAINT FK_Organization FOREIGN KEY (OrganizationId)
REFERENCES Organization(Id)
);

INSERT INTO Organization VALUES ('101','ABC','9987787654');
INSERT INTO Exhibition VALUES ('1001','23/02/2018','PQR','Mumabi','101');

--DROP TABLE Exhibition;

