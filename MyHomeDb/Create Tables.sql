CREATE TABLE [dbo].[Rooms]
(
	[Value] MONEY NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [RoomID] INT NOT NULL, 
    [FK_ID] NVARCHAR (128) NOT NULL, 
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([RoomID]), 
    CONSTRAINT [FK_UserID] FOREIGN KEY ([FK_ID]) REFERENCES [AspNetUsers]([Id]) 
)
CREATE TABLE [dbo].[Items]
(
	[Name] NVARCHAR(50) NOT NULL , 
    [Value] MONEY NULL, 
    [ExpirationDate] DATE NULL, 
    [RoomID] INT NOT NULL, 
    [ItemID] INT NOT NULL, 
    CONSTRAINT [FK_RoomID] FOREIGN KEY ([RoomID]) REFERENCES [Rooms]([RoomID]), 
    CONSTRAINT [PK_Items] PRIMARY KEY ([ItemID])
)
