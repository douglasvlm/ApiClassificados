DROP TABLE Classificados;

USE Classificados


Create Table Classificados (
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Titulo varchar(4000),
	Descricao varchar(4000),
	Valor float
	
);

SELECT * FROM Classificados;