USE [EuphoriaDB]
GO
INSERT [euphoria].[ORGANIZER] ([Id], [TenantId], [Name], [Description], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version]) VALUES (N'0cb713f4-9a71-4a93-ac6b-5615e78bcbfd', 100, N'TEchlabs', N'Techlabs', CAST(0x0000A59300DB6D7D AS DateTime), N'test_user', CAST(0x0000A59300DB6D7D AS DateTime), N'test_user', 0, 0)
GO
INSERT [euphoria].[ORGANIZER] ([Id], [TenantId], [Name], [Description], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version]) VALUES (N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105, N'GS Mktg labs', N'Techlbas', CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', 0, 0)
GO
INSERT [euphoria].[CATEGORY] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId]) VALUES (N'd2eb3cc8-c9ce-451a-ad2a-36f0dd18d362', N'Shoes', N'Shoes', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105)
GO
INSERT [euphoria].[CATEGORY] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId]) VALUES (N'ac458af6-d6b6-48ff-a762-c11c8f512189', N'Bags', N'Bags', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105)
GO
INSERT [euphoria].[EXHIBITOR] ([Id], [Name], [Description], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId]) VALUES (N'54998058-d940-43bf-af39-aa33261b2628', N'Nike', N'Nike', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105)
GO
INSERT [euphoria].[ExhibitorCategories] ([Exhibitor_Id], [Category_Id]) VALUES (N'54998058-d940-43bf-af39-aa33261b2628', N'd2eb3cc8-c9ce-451a-ad2a-36f0dd18d362')
GO
INSERT [euphoria].[ExhibitorCategories] ([Exhibitor_Id], [Category_Id]) VALUES (N'54998058-d940-43bf-af39-aa33261b2628', N'ac458af6-d6b6-48ff-a762-c11c8f512189')
GO
INSERT [euphoria].[VENUE] ([Id], [City], [Address], [State], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId]) VALUES (N'b59ff854-72b0-4efa-9b99-cc30208c7e4c', N'Mumbai', N'Mumbai', N'Maharashtra', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105)
GO
INSERT [euphoria].[EXHIBITION] ([Id], [Name], [Description], [StartDate], [EndDate], [isActive], [TicketBookingStatus], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId], [Venue_Id], [Exhibitor_Id]) VALUES (N'063da5be-b183-4708-8731-933549407e5d', N'Travel and Tourism', N'mega trade', CAST(0x0000A593014D7ED9 AS DateTime), CAST(0x0000A593014D7ED9 AS DateTime), 1, 0, CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105, N'b59ff854-72b0-4efa-9b99-cc30208c7e4c', N'54998058-d940-43bf-af39-aa33261b2628')
GO
INSERT [euphoria].[PAVILION] ([Id], [Name], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Exhibition_Id]) VALUES (N'c6184218-d641-41e0-8e58-7a826c57cd61', N'pavilion A', CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', CAST(0x0000A59300F2DBB9 AS DateTime), N'test_user', 0, 0, N'063da5be-b183-4708-8731-933549407e5d')
GO
INSERT [euphoria].[STALL] ([Id], [StallNo], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Exhibitor_Id], [Pavilion_Id]) VALUES (N'43f4ec11-39d8-423a-8ec9-250c6c7c8f0d', 103, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'54998058-d940-43bf-af39-aa33261b2628', N'c6184218-d641-41e0-8e58-7a826c57cd61')
GO
INSERT [euphoria].[STALL] ([Id], [StallNo], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Exhibitor_Id], [Pavilion_Id]) VALUES (N'e9ac12e0-d5e2-42a7-9076-9ffdc1d3601c', 101, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'54998058-d940-43bf-af39-aa33261b2628', N'c6184218-d641-41e0-8e58-7a826c57cd61')
GO
INSERT [euphoria].[STALL] ([Id], [StallNo], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Exhibitor_Id], [Pavilion_Id]) VALUES (N'28754d16-aea3-43c8-9f70-c66ad5ba516e', 102, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'54998058-d940-43bf-af39-aa33261b2628', N'c6184218-d641-41e0-8e58-7a826c57cd61')
GO
INSERT [euphoria].[TICKETTYPE] ([Id], [Name], [Description], [Price], [NumberOfDaysIncluded], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Venue_Id]) VALUES (N'59032fd7-d129-4caa-a93a-2c6d50c78262', N'1day', N'1day', 20, 0, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'b59ff854-72b0-4efa-9b99-cc30208c7e4c')
GO
INSERT [euphoria].[TICKETTYPE] ([Id], [Name], [Description], [Price], [NumberOfDaysIncluded], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Venue_Id]) VALUES (N'81cbe9e9-e33a-4edf-8310-f9d764dfd9bc', N'2day', N'2day', 50, 0, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'b59ff854-72b0-4efa-9b99-cc30208c7e4c')
GO
INSERT [euphoria].[VISITOR] ([Id], [FirstName], [LastName], [Address], [Pincode], [MobileNo], [EmailId], [DateOfBirth], [Gender], [Educatoin], [Income], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [Organizer_Id], [Organizer_TenantId]) VALUES (N'3c2fb7ea-7b17-4ba4-bea8-08195beb4f80', N'kannan', N'Sudhakaran', N'Andheri', 400023, 988989898, N'mayursaid20@gmail.com', CAST(0x0000A59300F2DBBA AS DateTime), 0, NULL, NULL, CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'96b337e2-892d-4ae3-90f7-e29d4317681b', 105)
GO
INSERT [euphoria].[TICKET] ([Id], [TicketNumber], [PaymentMode], [TransactionTime], [ReferenceNumber], [IPAddress], [TransactionStatus], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [IsDeleted], [Version], [TicketType_Id], [Visitor_Id]) VALUES (N'19db49f7-89d8-40fb-88f7-f41d5fc8d3d6', N'1001', N'C', CAST(0x0000A593014D7EDA AS DateTime), NULL, N'ip', N'S', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', CAST(0x0000A59300F2DBBA AS DateTime), N'test_user', 0, 0, N'81cbe9e9-e33a-4edf-8310-f9d764dfd9bc', N'3c2fb7ea-7b17-4ba4-bea8-08195beb4f80')
GO
