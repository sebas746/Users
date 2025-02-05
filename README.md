## Users Readme

Restul API to manage Users.

CRUD operations added.

Multilayer project in order to accomplish different patterns like Solid, Dry.

There are different layers in charge of the following
- Infrastructure: Contains the repository
- Application: Contains the services and additional business rules needed to be added.
- Api: Presentation layer.
- Core: Contains contracts and classes that are used across the other layers.

## Script to create the Database

```sql
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](128) NOT NULL,
	[LastName] [varchar](128) NULL,
	[Email] [varchar](250) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[PhoneNumber] [numeric](18, 0) NOT NULL,
 CONSTRAINT [UQ_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
