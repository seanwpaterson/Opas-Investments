-- Inserts for roles
INSERT INTO [dbo].[usersRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           ,[ConcurrencyStamp])
     VALUES
           ('Administrator'
           ,'Administrator'
           ,UPPER('Administrator')
           ,NEWID())

INSERT INTO [dbo].[usersRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           ,[ConcurrencyStamp])
     VALUES
           ('User Management'
           ,'User Management'
           ,UPPER('User Management')
           ,NEWID())

INSERT INTO [dbo].[usersRoles]
           ([Id]
           ,[Name]
           ,[NormalizedName]
           ,[ConcurrencyStamp])
     VALUES
           ('Portfolio Management'
           ,'Portfolio Management'
           ,UPPER('Portfolio Management')
           ,NEWID())
