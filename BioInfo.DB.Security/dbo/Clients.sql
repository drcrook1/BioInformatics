CREATE TABLE [dbo].[Clients] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Secret]               NVARCHAR (MAX) NOT NULL,
    [Name]                 NVARCHAR (100) NOT NULL,
    [ApplicationType]      INT            NOT NULL,
    [Active]               BIT            NOT NULL,
    [RefreshTokenLifeTime] INT            NOT NULL,
    [AllowedOrigin]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

