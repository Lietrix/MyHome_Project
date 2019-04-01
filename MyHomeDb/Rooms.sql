CREATE TABLE [dbo].[Rooms]
(
	[Value] MONEY NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [RoomID] INT NOT NULL, 
    [FK_ID] INT NOT NULL, 
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([RoomID]), 
    CONSTRAINT [FK_UserID] FOREIGN KEY ([FK_ID]) REFERENCES [UserInfo]([UserID]) 
)
