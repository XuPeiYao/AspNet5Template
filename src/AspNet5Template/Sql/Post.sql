CREATE TABLE [dbo].[Post]
(
	[PostId] INT NOT NULL PRIMARY KEY,
	[BlogId] INT NOT NULL,
	[AuthorId] INT NOT NULL,
	[Title] nvarchar(max),
	[Content] nvarchar(max), 
    CONSTRAINT [FK_Blog] FOREIGN KEY ([BlogId]) REFERENCES [Blog]([BlogId]),
	Constraint [FK_Author] foreign key([AuthorId]) references [Author]([AuthorId])
)
