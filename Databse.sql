CREATE TABLE Level_Users (
id INT IDENTITY(1,1) PRIMARY KEY,
name VARCHAR(80) ,
status INT(2) 
);

CREATE TABLE Users (
id INT IDENTITY (1,1) PRIMARY KEY,
username VARCHAR(45),
password VARCHAR(300) ,
name VARCHAR(45) ,
lastname VARCHAR(45) ,
addresses VARCHAR(45) ,
birthdate Date ,
email varchar(45) ,
phone varchar(45) ,
postalcode varchar(45) ,
id_level_usuario INT(9) ,
status INT(2) ,
CONSTRAINT fk_leveluser FOREIGN KEY (id_level_usuario) REFERENCES Level_Users(id)
);

CREATE TABLE Friends (
id INT IDENTITY (1,1) PRIMARY KEY,
id_Usu1 INT(9),
id_Usu2 INT(9),
status INT(2),
CONSTRAINT fk_users FOREIGN KEY (id_Usu1) REFERENCE Users(id),
CONSTRAINT fk_users2 FOREIGN KEY (id_Usu2) REFERENCE Users(id)
);


CREATE TABLE Genres (
id INT IDENTITY(1,1) PRIMARY KEY,
name VARCHAR(45) ,
description VARCHAR(200) ,
status INT(2) 
);

CREATE TABLE Artists (
id INT IDENTITY (1,1) PRIMARY KEY,
name VARCHAR(45) ,
description VARCHAR(200) ,
photo VARCHAR(200) ,
id_genere INT(9) ,
status INT(2) ,
CONSTRAINT fk_GenereInArtist FOREIGN KEY (id_genere) REFERENCE Genres(id)
);

CREATE TABLE Albums (
id INT IDENTITY (1,1) PRIMARY KEY,
name VARCHAR(45) ,
description VARCHAR(200),
photo VARCHAR(200) ,
id_artists INT(9) ,
status INT(2) ,
CONSTRAINT fk_ArtistInAlbum FOREIGN KEY (id_artists) REFERENCE Artists(id)
);

CREATE TABLE Songs (
id INT IDENTITY (1,1) PRIMARY KEY,
name VARCHAR(45) ,
description VARCHAR(200) ,
duration VARCHAR(45) ,
rut VARCHAR(450) ,
collaborators VARCHAR(90),
id_albums INT(9) ,
status INT(2) ,
CONSTRAINT fk_AlbumInSongs FOREIGN KEY (id_albums) REFERENCE Albums(id)
);


CREATE TABLE Follow (
id INT IDENTITY (1,1) PRIMARY KEY,
id_Usu1 INT(9) ,
id_Artist INT(9) ,
status INT(2) ,
CONSTRAINT fk_FollowAlbum FOREIGN KEY (id_albums) REFERENCE Albums(id),
CONSTRAINT fk_FollowArtists FOREIGN KEY (id_Artist) REFERENCE Artists(id),
CONSTRAINT fk_FollowUser FOREIGN KEY (id_Usu1) REFERENCE Users(id)
);


CREATE Event  (
id INT IDENTITY (1,1) PRIMARY KEY,
name VARCHAR(45) ,
id_artists INT(9) ,
status INT(2) ,
CONSTRAINT fk_ArtistInEvent FOREIGN KEY (id_artists) REFERENCE Artists(id)
);

CREATE EventAndUser  (
id INT IDENTITY (1,1) PRIMARY KEY,
id_User INT(9) 
id_Event INT(9) ,
CONSTRAINT fk_UserInEvent FOREIGN KEY (id_User) REFERENCE Users(id),
CONSTRAINT fk_EventInEventUser FOREIGN KEY (id_Event) REFERENCE Events(id)
);

CREATE DetailEvent  (
id_Event INT(9),
description VARCHAR(200) ,
country VARCHAR(200) ,
city VARCHAR(45) ,
addresses VARCHAR(45) ,
dateevent Date ,
price DOUBLE(9,2),
status INT(2) ,
CONSTRAINT fk_detailevent FOREIGN KEY (id_Event) REFERENCE Events(id)
);




