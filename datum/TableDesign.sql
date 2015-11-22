
-- videos table
create table Videos(
       videoID varchar2(20),
       videoName varchar2(50),
       VideoUrl varchar2(50),
       videoImageUrl varchar2(50),
       videoShortenDesc clob,
       videoDesc clob,
       videoType varchar2(20),
       videoSubmitTime date,
       
       constraint video_pk_VideosID primary key(videoID)
);



-- users table

create table AllUsers(
       UserEmail varchar2(50),
       firstName varchar2(15),
       lastName varchar2(15),
       Pwd varchar2(80),
       createTime date,
       HeadImageUrl varchar2(50),
       gender varchar2(5),
       birthday date,
       isActive varchar2(1),
       constraint AllUsers_check_isActive check(isActive = '0' || isActive = '1'),        -- 是否激活  0 没有激活  1 激活
       constraint AllUsers_PK_Email primary key(UserEmail)
);

-- community
create table communitys(
       communityID varchar2(20),
       communityName varchar2(50),
       communityDesc clob,
       communityHeadImageUrl varchar2(50),
       communityCreateTime date,
       
       constraint community_PK_CommunityID primary key(communityID)
);

-- sources
create table sources(
sourceID varchar2(20),
sourceName varchar2(50),
sourceType varchar2(20),
sourceUrl varchar2(50),
sourceShortDesc clob,
sourcepicUrl varchar2(50),
sourceSubmitDate date,

constraint sources_PK primary key(sourceID)
);

-- listening
create table listening(
listeningID varchar2(20),
listeningName varchar2(50),
listeningUrl varchar2(50),
listeningType varchar2(20),
listeningShortDesc clob,
listeningSubmitDate date,

constraint listening_PK primary key(listeningID)
);

-- picWithArticles
create table picWithArticles(
picWithArticleID varchar2(20),
picWithArticleTitle varchar2(50),
picWithArticleContent clob,
picWithArticleUrl varchar2(50),
picWithArticleSubmitDate date,

constraint picWithArticles_PK primary key(picWithArticleID)
);

--  usercontents
create table  usersContents(
contentID varchar2(20),
contentText clob,
contentPicUrl varchar2(50),
contentSubmitDate date,

constraint  usersContents_PK primary key(contentID)
);

-- comments
create table comments(
commentID varchar2(20),
contentID varchar2(20),
commentDate date,
commentContent clob,
userEmail varchar2(50),

constraint comments_PK primary key(commentID),
constraint comments_FK_contentID foreign key(contentID) references usersContents(contentID),
constraint comments_FK_userEmail foreign key(userEmail) references Allusers(userEmail)
);



--video_comments
create table video_comment(
       VideoID varchar2(20),
       UserEmail varchar2(50),
       CommentTime date,
       commentContent clob,
       
       constraint videoComment_ID_Email_Time primary key(VideoID,UserEmail,CommentTime),
       constraint VideoID_FK foreign key(VideoID) references videos(videoID),
       constraint Email_FK foreign key(UserEmail) references AllUsers(UserEmail)
);



--users-community
create table user_community(
       UserEmail varchar2(50),
       communityID varchar2(20),
       contentID varchar(20),
       
       constraint UC_PK_Email_CID primary key(UserEmail,communityID,contentID),
       constraint UC_FK_Email foreign key(UserEmail) references Allusers(UserEmail),
       constraint UC_FK_contentID foreign key(contentID) references UsersContent(contentID),
       constraint UC_FK_communityID foreign key(communityID) references communitys(communityID)
);
