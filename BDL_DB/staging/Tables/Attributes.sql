CREATE TABLE STAGING.Attributes
(
	[Id] [int],
	[Name] [nvarchar](255),
	[Symbol] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
CONSTRAINT [PK_ATTRIBUTES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))