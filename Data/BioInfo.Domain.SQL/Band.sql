CREATE TABLE [dbo].[Band]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DomainUserId] INT NULL, 
    [IsActive] BIT NOT NULL, 
    [IoTHubKey] NVARCHAR(50) NULL
)
