CREATE TABLE [dbo].[BandExperiment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ExperimentId] INT NOT NULL, 
    [BandId] INT NOT NULL, 
    [BandTelemetryId] INT NOT NULL, 
    [DomainUserId] INT NOT NULL, 
    CONSTRAINT [FK_BandExperiment_Experiment] FOREIGN KEY ([ExperimentId]) REFERENCES [Experiment]([Id]), 
    CONSTRAINT [FK_BandExperiment_Band] FOREIGN KEY ([BandId]) REFERENCES [Band]([Id]), 
    CONSTRAINT [FK_BandExperiment_Telemetry] FOREIGN KEY ([BandTelemetryId]) REFERENCES [BandTelemetry]([Id]), 
    CONSTRAINT [FK_BandExperiment_User] FOREIGN KEY ([DomainUserId]) REFERENCES [DomainUser]([Id])
)
