## Projeto completamente modificado para Catálogo de Classificados
### Autor
#### Douglas Martin
--------------------------------------------------------
--Tabela SQLServer
Create Table Classificados (
	Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Titulo varchar(4000),
	Descricao varchar(4000),
	Valor float
	
);

--Connection String 
"Server=(localdb)\mssqllocaldb;Database=Classificados;Integrated Security=True"

--------------------------------------------------------
### DESCRIÇÃO
#### Sua missão neste lab será construir uma arquitetura base para uma aplicação .net do zero.
Original
https://hermes.digitalinnovation.one/lab_projects/files/463794e1-f381-46c9-85f4-3bdd2b4ba572.zip

