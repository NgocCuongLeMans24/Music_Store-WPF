create database Music_Store_PRN212;
go

use Music_Store_PRN212;
go

create table roles(
	role_id int identity(1,1) primary key not null,
	name nvarchar(50) not null
);
go

create table Users(
	userid int identity(1,1) primary key not null,
	name nvarchar(100) not null,
	username nvarchar(100) not null,
	password nvarchar(255) not null,
	email nvarchar(100) not null,
	phone_number nvarchar(24) not null,
	address nvarchar(255) not null,
	roleId int not null,
	created_at DATETIME DEFAULT GETDATE() not null,
	status NVARCHAR(50) CHECK (status IN ('active', 'inactive')) not null,
	totalAmount numeric(10, 2) not null,
	FOREIGN KEY (roleId) REFERENCES roles(role_id)
);
go

create table Genres(
	GenreId int identity(1,1) primary key not null,
	name nvarchar(120) not null,
	Description nvarchar(1000) not null,
);
go

create table Artists(
	ArtistId int identity(1,1) primary key not null,
	name nvarchar(120) not null 
);
go

create table Albums(
	AlbumId int identity(1,1) primary key not null,
	GenreId int not null,
	ArtistId int not null,
	Title nvarchar(160) not null,
	Price numeric(10,2) not null,
	AlbumUrl nvarchar(1024) not null,
	Stock int not null, -- default 100 
	IsTop10BestSeller BIT DEFAULT 0, -- Add the new column for best-selling albums (default is FALSE)
	FOREIGN KEY (GenreId) REFERENCES Genres(GenreId),
	FOREIGN KEY (ArtistId) REFERENCES Artists(ArtistId)
);
go

create table Orders(
	OrderId int identity(1,1) primary key not null,
	OrderDate datetime DEFAULT GETDATE() not null, --auto create
	userid int not null,  
	Address nvarchar(255) not null,
	total numeric(10,2) not null,
	status NVARCHAR(50) CHECK (status IN ('pending','delivering', 'delivered')) not null,
	FOREIGN KEY (userid) REFERENCES Users(userid)  
);
go

create table OrderDetails(
	OrderDetailId int identity(1,1) primary key not null,
	OrderId int not null,
	AlbumId int not null,
	Quantity int not null,
	Price numeric(10,2) not null,
	FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
	FOREIGN KEY (AlbumId) REFERENCES Albums(AlbumId)
);
go

-- Insert data into roles
INSERT INTO roles (name) VALUES ('Admin');
INSERT INTO roles (name) VALUES ('Customer');

-- Insert data into Users
INSERT INTO Users (name, username, password, email, phone_number, address, roleId, status, totalAmount)
VALUES 
('Ha Ngoc Cuong', 'cuong', 'cuong1', 'cuong@gmail.com', '123456789', 'Bac Giang', 1, 'active', 0.00),
('Chu Phuc Anh', 'phucanh', 'phucanh1', 'phucanh@gmail.com', '987654321', 'Trung Nam Bo', 1, 'active', 0.00),
('Alice', 'alice', 'alice1', 'alice@gmail.com', '987654321', 'Trung Nam Bo', 2, 'active', 0.00),
('Phuc', 'Phuc', 'phuc2', 'phuc@gmail.com', '987654321', 'Bac Giang', 2, 'active', 0.00);

-- Insert data into Genres
INSERT INTO Genres (Name, Description) VALUES 
(N'Rock', N'Rock and Roll is a form of rock music developed in the 1950s and 1960s. Rock music combines many kinds of music from the United States, such as country music, folk music, church music, work songs, blues and jazz.'),
(N'Jazz', N'Jazz is a type of music which was invented in the United States. Jazz music combines African-American music with European music. Some common jazz instruments include the saxophone, trumpet, piano, double bass, and drums.'),
(N'Metal', N'Heavy Metal is a loud, aggressive style of Rock music. The bands who play heavy-metal music usually have one or two guitars, a bass guitar and drums. In some bands, electronic keyboards, organs, or other instruments are used. Heavy metal songs are loud and powerful-sounding, and have strong rhythms that are repeated. There are many different types of Heavy Metal, some of which are described below. Heavy metal bands sometimes dress in jeans, leather jackets, and leather boots, and have long hair. Heavy metal bands sometimes behave in a dramatic way when they play their instruments or sing. However, many heavy metal bands do not like to do this.'),
(N'Alternative', N'Alternative rock is a type of rock music that became popular in the 1980s and became widely popular in the 1990s. Alternative rock is made up of various subgenres that have come out of the indie music scene since the 1980s, such as grunge, indie rock, Britpop, gothic rock, and indie pop. These genres are sorted by their collective types of punk, which laid the groundwork for alternative music in the 1970s.'),
(N'Disco', N'Disco is a style of pop music that was popular in the mid-1970s. Disco music has a strong beat that people can dance to. People usually dance to disco music at bars called disco clubs. The word "disco" is also used to refer to the style of dancing that people do to disco music, or to the style of clothes that people wear to go disco dancing. Disco was at its most popular in the United States and Europe in the 1970s and early 1980s. Disco was brought into the mainstream by the hit movie Saturday Night Fever, which was released in 1977. This movie, which starred John Travolta, showed people doing disco dancing. Many radio stations played disco in the late 1970s.'),
(N'Blues', N'The blues is a form of music that started in the United States during the start of the 20th century. It was started by former African slaves from spirituals, praise songs, and chants. The first blues songs were called Delta blues. These songs came from the area near the mouth of the Mississippi River.'),
(N'Latin', N'Latin American music is the music of all countries in Latin America (and the Caribbean) and comes in many varieties. Latin America is home to musical styles such as the simple, rural conjunto music of northern Mexico, the sophisticated habanera of Cuba, the rhythmic sounds of the Puerto Rican plena, the symphonies of Heitor Villa-Lobos, and the simple and moving Andean flute. Music has played an important part recently in Latin America''s politics, the nueva canción movement being a prime example. Latin music is very diverse, with the only truly unifying thread being the use of Latin-derived languages, predominantly the Spanish language, the Portuguese language in Brazil, and to a lesser extent, Latin-derived creole languages, such as those found in Haiti.'),
(N'Reggae', N'Reggae is a music genre first developed in Jamaica in the late 1960s. While sometimes used in a broader sense to refer to most types of Jamaican music, the term reggae more properly denotes a particular music style that originated following on the development of ska and rocksteady.'),
(N'Pop', N'Pop music is a music genre that developed from the mid-1950s as a softer alternative to rock ''n'' roll and later to rock music. It has a focus on commercial recording, often oriented towards a youth market, usually through the medium of relatively short and simple love songs. While these basic elements of the genre have remained fairly constant, pop music has absorbed influences from most other forms of popular music, particularly borrowing from the development of rock music, and utilizing key technological innovations to produce new variations on existing themes.'),
(N'Classical', N'Classical music is a very general term which normally refers to the standard music of countries in the Western world. It is music that has been composed by musicians who are trained in the art of writing music (composing) and written down in music notation so that other musicians can play it. Classical music can also be described as "art music" because great art (skill) is needed to compose it and to perform it well. Classical music differs from pop music because it is not made just in order to be popular for a short time or just to be a commercial success.');
GO

-- Insert data into Artists
INSERT INTO Artists (Name) VALUES 
(N'The Beatles'),
(N'Elton John'),
(N'Miles Davis'),
(N'Michael Jackson'),
(N'The Rolling Stones'),
(N'Eminem'),	
(N'Taylor Swift'),
(N'The Weeknd'),
(N'Ed Sheeran'),
(N'Kendrick Lamar')

-- Insert data into Albums
INSERT INTO Albums (GenreId, ArtistId, Title, Price, AlbumUrl, Stock, IsTop10BestSeller)
VALUES 
(1, 1, 'Abbey Road', 19.99, 'https://i.ytimg.com/vi/B0xzGlZy6WA/maxresdefault.jpg', 100, 1),
(2, 2, 'Greatest Hits', 15.99, 'https://i.scdn.co/image/ab67616d0000b273995be10a6b380541b2717be8', 50, 0),
(3, 3, 'Kind of Blue', 17.99, 'https://www.jazzmessengers.com/71521-large_default/kindofblue-deluxeboxset-lpbookcd-.jpg', 75, 1);




