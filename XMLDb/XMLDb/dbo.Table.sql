CREATE TABLE [dbo].[XMLTable] (
    [id]      INT        IDENTITY (1, 1) NOT NULL,
    [name]    NVARCHAR(200)       NOT NULL,
    [version] NVARCHAR (10) NOT NULL,
    [date]    DATETIME   NOT NULL,
    CONSTRAINT [PK_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

