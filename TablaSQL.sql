USE [bank]
GO

/****** Object:  Table [dbo].[debts]    Script Date: 13/09/2021 12:24:23 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[debts]') AND type in (N'U'))
DROP TABLE [dbo].[debts]
GO

/****** Object:  Table [dbo].[debts]    Script Date: 13/09/2021 12:24:23 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[debts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account] [varchar](255) NOT NULL,
	[debt] [decimal](18, 2) NULL,
	[paid] [decimal](18, 2) NULL,
	[datePayment] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


