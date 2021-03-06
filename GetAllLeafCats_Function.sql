USE [Tarin]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAllLeafCategories]    Script Date: 7/28/2015 10:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  function [dbo].[GetAllLeafCategories] (@root int)
RETURNS @categories TABLE 
(
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[Image] [varbinary](max) NULL,
	[InsertDateTime] [datetime] NULL,
	[UpdateDateTime] [datetime] NULL,
	[DeleteDateTime] [datetime] NULL,
	[Status] [smallint] NULL
)
AS 
begin
	;with cte as
	(
	select Id,ParentId, Name, rnk=0 from Categories
	where ParentId = @root
  
	union all
  
	select t.id,t.ParentId,t.Name, rnk+1
	from cte join Categories t
	on cte.id = t.ParentId
	)
	insert into @categories(Id,ParentId,Name)
	select id,ParentId,Name--,rnk
	  from cte
	 where not exists
		   (select *
			  from Categories
			 where ParentId = cte.id);

			 return;
end