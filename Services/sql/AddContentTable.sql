create table Matchmaker.content
(
    id            int                 not null,
    cache_key     varchar(255)        not null,
    title         varchar(255)        null,
    overview      nvarchar(max)       null,
    poster_path   varchar(255)        null,
    backdrop_path varchar(255)        null,
    trailer_url   varchar(255)        null,
    age           int      default 0  null,
    json          nvarchar(max)       null,
    updated_at    datetime default getdate(),
    is_show       bit                 null,
    constraint content_pk
        primary key (cache_key, id)
)
go