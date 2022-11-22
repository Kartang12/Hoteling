Create database AuthorizationApi;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AuthorizationApi].[dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PWHash] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


insert into [AuthorizationApi].[dbo].[Users]
values ('a567c4aa-388e-4a9f-60aa-08dab0276093',
'string123@gmail.com',
'by[.�#[;M(�i��6g���W��ـ�e37',
'/4LgKFQ4Kv8SborvjhMGO0JIkm8XsxjmjDLxsg+pCxs=',
'2022-11-22 12:38:50.7289206',
2)

insert into [AuthorizationApi].[dbo].[Users]
values ('12385c02-7b82-4df0-f790-08dab350adfa',
'test1234@gmail.com',
'�~�_�H�IISl�[�5�&� /�\0�,��"�"D',
'zDdEvRiYLlWgEI0ZYICdqFKiX62EviSBdtS1mDYOIPk=',
'2022-10-28 13:40:32.3182955',
0)


CREATE DATABASE Hotels;

CREATE TABLE [Hotels].[dbo].[Hotels](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Location] [varchar](50) NOT NULL,
	[RoomsAmount] [int] NOT NULL,
	[Rating] [float] NOT NULL,
	[TotalVisitors] [int] NOT NULL
) ON [PRIMARY]
GO


insert into  [Hotels].[dbo].[Hotels] VALUES(
    '64b71417-f19d-4b81-acd3-eb180d0d638f',
    'Afterlife',
    'Night City',
    10,
    0,
    0
);



Create Database Booking;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Booking].[dbo].[Bookings](
	[Id] [uniqueidentifier] NOT NULL,
	[RoomId] [uniqueidentifier] NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[HotelName] [nvarchar](max) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EdnDate] [datetime2](7) NOT NULL,
	[GuestsAmount] [int] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

insert into [Booking].[dbo].[Bookings]
values('9681c1b5-6ced-4b2d-a158-4a5bf2fe361c',
'1468813b-ff11-4d82-9f88-9c2e7ba0d4bd',
10,
'64b71417-f19d-4b81-acd3-eb180d0d638f',
'Afterlife',
'2022-10-22 00:00:00.0000000',
'2022-12-22 00:00:00.0000000',
10)



Create database Reviews;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reviews].[dbo].[Reviews](
	[Id] [uniqueidentifier] NOT NULL,
	[HotelId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[HotelName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Score] [int] NOT NULL,
	[Feedback] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

insert into [Reviews].[dbo].[Reviews]
values ('4ad938b6-fae3-4f28-bacd-d840f5ff8ef7',
'64b71417-f19d-4b81-acd3-eb180d0d638f',
'a567c4aa-388e-4a9f-60aa-08dab0276093',
'Afterlife',
'testUser',
10,
'Great bds, cheap beer',
'2022-10-24 10:10:10.7234190')


create database Users;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[BirthDate] [datetime2](7) NOT NULL,
	[ReviewsAmount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
