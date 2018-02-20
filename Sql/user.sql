CREATE TABLE Bookmark (
ID int NOT NULL IDENTITY(1,1) ,
UNAME VARCHAR(20) NOT NULL,
UPASS VARCHAR(12) NOT NULL,
ULOC VARCHAR(20) NOT NULL,
PRIMARY KEY (ID),
);

ALTER TABLE Bookmark 
ADD EMAIL VARCHAR(20);

INSERT INTO Bookmark VALUES ('Akash','akash','Ahmedabad',NULL);
INSERT INTO Bookmark VALUES ('Parth','parth','Ahmedabad','parth@parth.com');

DELETE FROM Bookmark
WHERE ID=3;
DELETE FROM Bookmark
WHERE ID=4; 

ALTER TABLE Bookmark
ALTER COLUMN EMAIL VARCHAR(244);

ALTER TABLE Bookmark
ALTER COLUMN ULOC VARCHAR(244);

SELECT * FROM Bookmark;

CREATE TABLE UserBookmarks(
BNAME VARCHAR(255) NOT NULL,
bookmarkId int ,
CONSTRAINT fk_bookmarkId FOREIGN KEY (bookmarkId) REFERENCES Bookmark(ID)
);

INSERT INTO UserBookmarks(BNAME,bookmarkId) VALUES(
'www.google.com' ,1
)

SELECT * FROM UserBookmarks

