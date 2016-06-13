CREATE TABLE [dbo].[BandTelemtry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CollectionTime] DATETIME NOT NULL, 
    [Barometer] FLOAT NULL, 
    [HeartRate] FLOAT NULL, 
    [UV] FLOAT NULL, 
    [Microphone] FLOAT NULL, 
    [GalvSkinRes] FLOAT NULL, 
    [Acceleromteter] FLOAT NULL, 
    [DomainUserId] INT NOT NULL, 
    [BandId] NCHAR(10) NULL, 
    CONSTRAINT [FK_BandTelemtry_DomainUser] FOREIGN KEY ([DomainUserId]) REFERENCES [DomainUser]([Id])
)
