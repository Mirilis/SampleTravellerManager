
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2017 20:52:02
-- Generated from EDMX file: C:\Users\Mirilis\documents\visual studio 2017\Projects\SampleTravellerManager\SampleManagerLibrary\SampleTravellerContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SampleTravellers];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Owner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Milestones] DROP CONSTRAINT [FK_Owner];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseMilestone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_ResponseMilestone];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_ResponseQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_ResponseUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MilestoneQuestion_Milestones]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MilestoneQuestion] DROP CONSTRAINT [FK_MilestoneQuestion_Milestones];
GO
IF OBJECT_ID(N'[dbo].[FK_MilestoneQuestion_Questions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MilestoneQuestion] DROP CONSTRAINT [FK_MilestoneQuestion_Questions];
GO
IF OBJECT_ID(N'[dbo].[FK_MilestoneSort]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sorts] DROP CONSTRAINT [FK_MilestoneSort];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionQuestion_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionQuestion] DROP CONSTRAINT [FK_QuestionQuestion_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionQuestion_Question1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionQuestion] DROP CONSTRAINT [FK_QuestionQuestion_Question1];
GO
IF OBJECT_ID(N'[dbo].[FK_SortQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sorts] DROP CONSTRAINT [FK_SortQuestion];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Keywords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Keywords];
GO
IF OBJECT_ID(N'[dbo].[Milestones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Milestones];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Responses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Responses];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Sorts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sorts];
GO
IF OBJECT_ID(N'[dbo].[MilestoneQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MilestoneQuestion];
GO
IF OBJECT_ID(N'[dbo].[QuestionQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionQuestion];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Keywords'
CREATE TABLE [dbo].[Keywords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Milestones'
CREATE TABLE [dbo].[Milestones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Product] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [Owner_Id] int  NOT NULL,
    [Completed] bit  NOT NULL,
    [Successful] bit  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Team] int  NOT NULL,
    [Required] bit  NOT NULL,
    [Request] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [HelpText] nvarchar(max)  NOT NULL,
    [HelpImage] nvarchar(max)  NULL,
    [Template] bit  NOT NULL
);
GO

-- Creating table 'Responses'
CREATE TABLE [dbo].[Responses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [String] nvarchar(max)  NOT NULL,
    [Integer] int  NOT NULL,
    [Double] float  NOT NULL,
    [File] varbinary(max)  NOT NULL,
    [Boolean] bit  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Question_Id] int  NOT NULL,
    [Milestone_Id] int  NOT NULL,
    [User_Id] int  NULL,
    [Completed] bit  NOT NULL,
    [Successful] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First] nvarchar(max)  NOT NULL,
    [Last] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Team] int  NOT NULL
);
GO

-- Creating table 'Sorts'
CREATE TABLE [dbo].[Sorts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Order] int  NOT NULL,
    [MilestoneId] int  NOT NULL,
    [Question_Id] int  NOT NULL
);
GO

-- Creating table 'MilestoneQuestion'
CREATE TABLE [dbo].[MilestoneQuestion] (
    [Milestones_Id] int  NOT NULL,
    [Questions_Id] int  NOT NULL
);
GO

-- Creating table 'QuestionQuestion'
CREATE TABLE [dbo].[QuestionQuestion] (
    [QuestionQuestion_Question1_Id] int  NOT NULL,
    [Corequisites_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Keywords'
ALTER TABLE [dbo].[Keywords]
ADD CONSTRAINT [PK_Keywords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Milestones'
ALTER TABLE [dbo].[Milestones]
ADD CONSTRAINT [PK_Milestones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [PK_Responses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sorts'
ALTER TABLE [dbo].[Sorts]
ADD CONSTRAINT [PK_Sorts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Milestones_Id], [Questions_Id] in table 'MilestoneQuestion'
ALTER TABLE [dbo].[MilestoneQuestion]
ADD CONSTRAINT [PK_MilestoneQuestion]
    PRIMARY KEY CLUSTERED ([Milestones_Id], [Questions_Id] ASC);
GO

-- Creating primary key on [QuestionQuestion_Question1_Id], [Corequisites_Id] in table 'QuestionQuestion'
ALTER TABLE [dbo].[QuestionQuestion]
ADD CONSTRAINT [PK_QuestionQuestion]
    PRIMARY KEY CLUSTERED ([QuestionQuestion_Question1_Id], [Corequisites_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Owner_Id] in table 'Milestones'
ALTER TABLE [dbo].[Milestones]
ADD CONSTRAINT [FK_Owner]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Owner'
CREATE INDEX [IX_FK_Owner]
ON [dbo].[Milestones]
    ([Owner_Id]);
GO

-- Creating foreign key on [Milestone_Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_ResponseMilestone]
    FOREIGN KEY ([Milestone_Id])
    REFERENCES [dbo].[Milestones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseMilestone'
CREATE INDEX [IX_FK_ResponseMilestone]
ON [dbo].[Responses]
    ([Milestone_Id]);
GO

-- Creating foreign key on [Question_Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_ResponseQuestion]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseQuestion'
CREATE INDEX [IX_FK_ResponseQuestion]
ON [dbo].[Responses]
    ([Question_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_ResponseUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseUser'
CREATE INDEX [IX_FK_ResponseUser]
ON [dbo].[Responses]
    ([User_Id]);
GO

-- Creating foreign key on [Milestones_Id] in table 'MilestoneQuestion'
ALTER TABLE [dbo].[MilestoneQuestion]
ADD CONSTRAINT [FK_MilestoneQuestion_Milestones]
    FOREIGN KEY ([Milestones_Id])
    REFERENCES [dbo].[Milestones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Questions_Id] in table 'MilestoneQuestion'
ALTER TABLE [dbo].[MilestoneQuestion]
ADD CONSTRAINT [FK_MilestoneQuestion_Questions]
    FOREIGN KEY ([Questions_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MilestoneQuestion_Questions'
CREATE INDEX [IX_FK_MilestoneQuestion_Questions]
ON [dbo].[MilestoneQuestion]
    ([Questions_Id]);
GO

-- Creating foreign key on [MilestoneId] in table 'Sorts'
ALTER TABLE [dbo].[Sorts]
ADD CONSTRAINT [FK_MilestoneSort]
    FOREIGN KEY ([MilestoneId])
    REFERENCES [dbo].[Milestones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MilestoneSort'
CREATE INDEX [IX_FK_MilestoneSort]
ON [dbo].[Sorts]
    ([MilestoneId]);
GO

-- Creating foreign key on [QuestionQuestion_Question1_Id] in table 'QuestionQuestion'
ALTER TABLE [dbo].[QuestionQuestion]
ADD CONSTRAINT [FK_QuestionQuestion_Question]
    FOREIGN KEY ([QuestionQuestion_Question1_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Corequisites_Id] in table 'QuestionQuestion'
ALTER TABLE [dbo].[QuestionQuestion]
ADD CONSTRAINT [FK_QuestionQuestion_Question1]
    FOREIGN KEY ([Corequisites_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionQuestion_Question1'
CREATE INDEX [IX_FK_QuestionQuestion_Question1]
ON [dbo].[QuestionQuestion]
    ([Corequisites_Id]);
GO

-- Creating foreign key on [Question_Id] in table 'Sorts'
ALTER TABLE [dbo].[Sorts]
ADD CONSTRAINT [FK_SortQuestion]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SortQuestion'
CREATE INDEX [IX_FK_SortQuestion]
ON [dbo].[Sorts]
    ([Question_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------