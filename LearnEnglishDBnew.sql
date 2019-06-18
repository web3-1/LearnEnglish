create database LearnEnglish
use LearnEnglish
go

create table CommentTopic(
	stt int IDENTITY(1,1),
	topic nvarchar(255),
	CONSTRAINT pk_tbl_temp  PRIMARY KEY (topic)
)

insert into CommentTopic values (N'Giao diện'),(N'Bài học'),(N'Tính năng')

create table UserComment(
	stt int IDENTITY(1,1) PRIMARY KEY,
	email nvarchar(255),
	topic nvarchar(255),
	comment nvarchar(255),
	dateComment date,
	statusCmt bit,
	CONSTRAINT FK_UserComment_CommentTopic FOREIGN KEY (topic)
    REFERENCES CommentTopic(topic)
)
insert into UserComment values (N'lexuankha2409@gmail.com',N'Giao diện',N'Từ trước đến giờ chưa thấy giao diện nào đẹp như web này <3', '4/6/2019', 0),
						   (N'lexuankha2409@gmail.com',N'Bài học',N'Từ vựng còn hơi ít, hi vọng có nhiều hơn', '3/6/2019', 0)


create table UserInfo(
	username nvarchar(255) primary key,
	pass nvarchar(255),
	dateRegister date,
	fullname nvarchar(255),
	dateOfBirth date,
	sdt int, 
	addr nvarchar(255),
	levelStudy nvarchar(255)
)

insert into UserInfo values (N'lexuankha2409@gmail.com', N'123', '06/8/2019', N'Lê Xuân Kha', '09/24/1998', 0934104430, N'123/20 Đặng Văn Bi, phường Trường Thọ, quận Thủ Đức, tp Hồ Chí Minh', 'beginer')
insert into UserInfo values	(N'ngoduckha@gmail.com', N'123', '06/18/2019', N'Ngô Đức Kha', '7/27/1998', 0979896524, N'ngã 5 chuồng chó, quận Tân Bình, tp Hồ Chí Minh', 'beginer')
insert into UserInfo values	(N'1111111@gmail.com', N'123', '06/19/2019', N'Ngô Đức Kha', '7/27/1998', 0979896524, N'ngã 5 chuồng chó, quận Tân Bình, tp Hồ Chí Minh', 'beginer')


create table Vocabulary(
	id int identity(1,1),
	word nvarchar(255) primary key,
	mean nvarchar(255),
	typeWord nvarchar(255),
	levelStudy nvarchar(255),
	imgURL nvarchar(255),
	soundURL nvarchar(255)
)

insert into Vocabulary values (N'morning', N'buổi sáng', N'noun', N'beginer', N'', N'')
