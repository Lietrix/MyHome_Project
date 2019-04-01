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
