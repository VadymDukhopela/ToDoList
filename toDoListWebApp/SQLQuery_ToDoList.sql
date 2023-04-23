CREATE DATABASE ToDoList
USE ToDoList

CREATE TABLE Categories(
	CategoryId int IDENTITY(1,1) PRIMARY KEY,
	CategoryName nvarchar(max)
)

DROP TABLE Categories


CREATE TABLE Tasks(
	TaskId int IDENTITY(1,1) PRIMARY KEY,
	Title nvarchar(max) NOT NULL,
	StartDate date DEFAULT GETDATE(),
	CompletionDate date DEFAULT NULL,
	TaskStatus int  NOT NULL DEFAULT 0,
	CategoryId int NOT NULL FOREIGN KEY REFERENCES  Categories(CategoryId) 
)

DROP TABLE Tasks


INSERT INTO Categories
VALUES(N'Без категорії');
INSERT INTO Categories
VALUES(N'Спорт');
INSERT INTO Categories
VALUES(N'Хоббі');
INSERT INTO Categories
VALUES(N'Навчання');




INSERT INTO Tasks(Title,CompletionDate,CategoryId)
VALUES('Поспать','2002-12-20',2);

SELECT t.Title, t.TaskStatus, c.CategoryName
FROM Tasks AS t
INNER JOIN Categories AS c
ON t.CategoryId = c.CategoryId





DROP DATABASE ToDoList




Select * From Categories

