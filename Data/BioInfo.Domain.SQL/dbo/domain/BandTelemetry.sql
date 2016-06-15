CREATE TABLE [dbo].[BandTelemetry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CollectionTime] DATETIME NOT NULL, 
    [Longitude] FLOAT NULL, 
    [Latitude] FLOAT NULL, 
    [BarometricPressure] FLOAT NULL, 
    [Capacitance] FLOAT NULL, 
    [HeartRate] FLOAT NULL, 
    [AmbientLight] FLOAT NULL, 
    [SkinTemperature] INT NULL, 
    [BandId] NCHAR(10) NOT NULL, 
    [UV] FLOAT NULL, 
    [GalvSkinRes] FLOAT NULL, 
    [Accelerometer] FLOAT NULL, 
    [Microphone] FLOAT NULL, 
    CONSTRAINT [FK_BandTelemtry_DomainUser] FOREIGN KEY ([SkinTemperature]) REFERENCES [DomainUser]([Id])
)
