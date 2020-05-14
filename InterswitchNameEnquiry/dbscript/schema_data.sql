USE [InterswitchNameEnquiry]
GO
/****** Object:  Table [dbo].[RequestResponseLog]    Script Date: 05/14/2020 14:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestResponseLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestBody] [varchar](5000) NULL,
	[RequestUrl] [varchar](1000) NULL,
	[HttpMethodType] [varchar](10) NULL,
	[RequestTime] [datetime] NULL,
	[ResponseBody] [varchar](5000) NULL,
	[ResponseTime] [datetime] NULL,
	[ResponseHttpCode] [varchar](10) NULL,
 CONSTRAINT [PK_RequestResponseLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[RequestResponseLog] ON
INSERT [dbo].[RequestResponseLog] ([Id], [RequestBody], [RequestUrl], [HttpMethodType], [RequestTime], [ResponseBody], [ResponseTime], [ResponseHttpCode]) VALUES (1, N'0003321999', NULL, NULL, CAST(0x0000ABBB00E9EE35 AS DateTime), N'{"ResponseCode":"00","CustomerID":"0003321999","CustomerName":{"LastName":"Rilwan","FirstName":"Balarabe","OtherNames":"Musa"},"CustomerAddress":{"AddrLine1":"No 30","AddrLine2":"Benue Road","City":"Otukpo","StateCode":"","PostalCode":""},"CustomerPhoneNo":"08023221100","AccountType":"10","AccountCurrency":"566","CountryCode":"NG","Nationality":"Nigerian","DOB":"05/09/1960","CountryOfBirth":"Nigeria"}', CAST(0x0000ABBB00E9EE35 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[RequestResponseLog] OFF
/****** Object:  Table [dbo].[ExceptionLog]    Script Date: 05/14/2020 14:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExceptionLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrorMessage] [text] NULL,
	[ErrorStacktrace] [text] NULL,
	[ErrorSource] [text] NULL,
	[ErrorDatetime] [datetime] NULL,
 CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
