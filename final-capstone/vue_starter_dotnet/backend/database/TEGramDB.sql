
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='TEGramDB')
DROP DATABASE TEGramDB;
GO

-- Create a new DemoDB Database
CREATE DATABASE TEGramDB;
GO

-- Switch to the DemoDB Database
USE TEGramDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			   int	     identity(1,1),
    isAdmin        bit       not null default 0,
	isDeactivated  bit       not null default 0,
	dateJoined     dateTime  not null default current_TimeStamp,
	username	   varchar(50)	not null,
	email varchar(50) not null,
	password	varchar(50)	not null,
	profileImage varchar(500) not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);

CREATE TABLE photos 
(
    id int identity(1,1),
	userId int not null,
    imageUrl varchar(500) not null, 
	dateAdded dateTime  not null default current_TimeStamp,
	isRemoved bit not null default 0,
	totalLikes int not null default 0, --Might take away later
	isVisible bit not null default 1,

	constraint fk_usersPhotos  foreign key (userId) References users(id),
	constraint pk_photos primary key (id)
);

Create table likes
(
    id int identity(1,1),
	photoId int not null,
	userId int not null,
	dateLiked dateTime  not null default current_TimeStamp,

	constraint fk_usersLikes  foreign key (userId) References users(id),
	constraint fk_photosLikes  foreign key (photoId) References photos(id),
	constraint pk_likes primary key (id)
);

Create table comments 
(
	id int identity(1,1),
	photoId int not null,
	userId int not null,
	dateCommented dateTime not null default current_TimeStamp,

	constraint fk_usersComments  foreign key (userId) References users(id),
	constraint fk_photosComments  foreign key (photoId) References photos(id),
	constraint pk_comments primary key (id)
);

Create table captions 
(
	id int identity(1,1),
	photoId int not null,
	dateCreated dateTime not null default current_TimeStamp,
	isRemoved bit not null default 0,

	constraint fk_photosCaptions  foreign key (photoId) References photos(id),
	constraint pk_captions primary key (id)
);

create table favorites 
(
	id int identity(1,1),
	photoId int not null,
	userId int not null,
	dateFavorited dateTime not null default current_TimeStamp,

	constraint pk_favorites primary key (id),
	constraint fk_photosFavorites  foreign key (photoId) References photos(id),
	constraint fk_usersFavorites  foreign key (userId) References users(id),
);
COMMIT TRANSACTION;