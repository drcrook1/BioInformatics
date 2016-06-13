CREATE TABLE [dbo].[DomainUser]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [First Name] NVARCHAR(50) NOT NULL, 
    [Last Name] NVARCHAR(50) NOT NULL, 
    [BirthDate] DATETIME NOT NULL, 
    [Gender] NCHAR(10) NOT NULL, 
    [HeightInches] FLOAT NOT NULL, 
    [Weight] FLOAT NOT NULL, 
    [Race] NCHAR(10) NOT NULL, 
    [Current Medications] NVARCHAR(255) NULL , 
    [Medical History] NVARCHAR(8000) NULL, 
    [HasGlasses] BIT NOT NULL DEFAULT 0
)
