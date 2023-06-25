CREATE TABLE IF NOT EXISTS 
Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR,
    Role INTEGER,
    PasswordHash BYTEA,
    PasswordSalt BYTEA
);