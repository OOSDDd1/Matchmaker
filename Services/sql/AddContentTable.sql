create table Matchmaker.content
(
    id            int                 not null,
    cache_key     varchar(255)        not null,
    title         varchar(255)        not null,
    overview      nvarchar(max)       not null,
    poster_path   varchar(255)        not null,
    backdrop_path varchar(255)        not null,
    trailer_url   varchar(255)        not null,
    age           int      default 0  not null,
    json          nvarchar(max)       not null,
    updated_at    datetime default getdate(),
    is_show       bit                 not null,
    constraint content_pk
        primary key (cache_key, id)
)
go