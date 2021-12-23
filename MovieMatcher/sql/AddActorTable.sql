create table Matchmaker.actor
(
    id             int          not null,
    content_id     int          not null,
    name           varchar(255) not null,
    is_show        bit          not null,
    character_name varchar(255) not null,
    constraint actor_pk
        primary key (content_id, is_show, id, character_name)
)
go