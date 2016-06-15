CREATE TABLE [dbo].[Experiment]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [StartCondition] NVARCHAR(4000) NULL, 
    [EndCondition] NVARCHAR(4000) NULL, 
    [Notes] NVARCHAR(4000) NULL, 
    [ExperimentType] NCHAR(50) NULL
)
