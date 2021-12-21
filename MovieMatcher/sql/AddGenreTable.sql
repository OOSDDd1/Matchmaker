create table Matchmaker.genre
(
    id         int          not null,
    content_id int          not null,
    name       varchar(255) not null,
    constraint genre_pk
        primary key (content_id, id)
)
go