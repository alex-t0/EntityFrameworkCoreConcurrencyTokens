CREATE TABLE dbo.AwesomeEntity (
	Uid uniqueidentifier NOT NULL,
	Name nvarchar(256) NULL,
	Timestamp timestamp NOT NULL
);

GO

ALTER TABLE dbo.AwesomeEntity ADD CONSTRAINT AwesomeEntity_PK PRIMARY KEY (Uid);
