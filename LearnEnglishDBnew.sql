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

delete from Vocabulary
insert into Vocabulary (levelStudy, typeWord,imgURL,soundURL,word, mean) values
(N'Beginer',N'noun', N'/img/land.jpg', N'/sound/land.mp3', N'Land', N'Đất,Vùng Đất'),
(N'Beginer',N'noun', N'/img/pollute.jpg', N'/sound/pollute.mp3', N'Pollute', N'Gây ô nhiễm'),
(N'Beginer',N'noun', N'/img/decompose.jpg', N'/sound/decompose.mp3', N'Decompose', N'Phân hủy'),
(N'Beginer',N'noun', N'/img/balance.jpg', N'/sound/balance.mp3', N'Balance', N'Sự cân bằng'),
(N'Beginer',N'noun', N'/img/climate_change.jpg', N'/sound/climate_change.mp3', N'Climate change', N'Thay đổi khí hậu'),
(N'Beginer',N'noun', N'/img/global_warming.jpg', N'/sound/global_warning.mp3', N'Global warning', N'Nóng lên toàn cầu'),
(N'Beginer',N'noun', N'/img/oil_slick.jpg', N'/sound/oil_slick.mp3', N'Oil slick', N'Vết dầu loang'),
(N'Beginer',N'noun', N'/img/ozone_layer.jpg', N'/sound/ozone_layer.mp3', N'Ozone layer', N'Tầng Ozon'),
(N'Beginer',N'noun', N'/img/biodiversity.jpg', N'/sound/biodiversity.mp3', N'Biodiversity', N'Đa dạng sinh học'),
(N'Beginer',N'noun', N'/img/ecology.jpg', N'/sound/ecology.mp3', N'Ecology', N'Sinh thái học'),
(N'Intermediate',N'noun', N'/img/pen.jpg', N'/sound/pen.mp3', N'Pen', N'Bút mực'),
(N'Intermediate',N'noun', N'/img/pencil.jpg', N'/sound/pencil.mp3', N'Pencil', N'Bút chì'),
(N'Intermediate',N'noun', N'/img/paper.jpg', N'/sound/paper.mp3', N'Paper', N'Giấy'),
(N'Intermediate',N'noun', N'/img/eraser.jpg', N'/sound/eraser.mp3', N'Eraser', N'Cục tẩy'),
(N'Intermediate',N'noun', N'/img/pencil_sharpener.jpg', N'/sound/pencil_sharpener.mp3', N'Pencil Sharpener', N'Đồ gọt bút chì'),
(N'Intermediate',N'noun', N'/img/book.jpg', N'/sound/book.mp3', N'Book', N'Sách'),
(N'Intermediate',N'noun', N'/img/pack.jpg', N'/sound/pack.mp3', N'Pack', N'Túi đeo, ba lô'),
(N'Intermediate',N'noun', N'/img/crayon.jpg', N'/sound/crayon.mp3', N'Crayon', N'Bút sáp màu'),
(N'Intermediate',N'noun', N'/img/scissors.jpg', N'/sound/scissors.mp3', N'Scissors', N'Cây kéo'),
(N'Intermediate',N'noun', N'/img/glue.jpg', N'/sound/glue.mp3', N'Glue', N'Keo dán'),
(N'Expert',N'noun', N'/img/play.jpg', N'/sound/play.mp3', N'Play', N'Vở kịch'),
(N'Expert',N'noun', N'/img/circus.jpg', N'/sound/circus.mp3', N'Circus', N'Rạp xiếc'),
(N'Expert',N'noun', N'/img/stadium.jpg', N'/sound/stadium.mp3', N'Stadium', N'Sân vận động'),
(N'Expert',N'noun', N'/img/orchestra.jpg', N'/sound/orchestra.mp3', N'Orchestra', N'Dàn nhạc'),
(N'Expert',N'noun', N'/img/scene.jpg', N'/sound/scene.mp3', N'Scene', N'Cảnh'),
(N'Expert',N'noun', N'/img/opera.jpg', N'/sound/opera.mp3', N'Opera', N'Nhạc kịch'),
(N'Expert',N'noun', N'/img/theater.jpg', N'/sound/theater.mp3', N'Theater', N'Nhà hát'),
(N'Expert',N'noun', N'/img/applaud.jpg', N'/sound/applaud.mp3', N'Applaud', N'Vỗ tay'),
(N'Expert',N'noun', N'/img/perform.jpg', N'/sound/perform.mp3', N'Perform', N'Biểu diễn'),
(N'Expert',N'noun', N'/img/exhibit.jpg', N'/sound/exhibit.mp3', N'Exhibit', N'Vật triển lãm')
