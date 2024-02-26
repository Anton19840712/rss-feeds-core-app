# Rss API

### Key Features
Rss API offers the following functionalities:
Search rss news: You can search for news for https://feeds.megaphone.fm/newheights and https://podcastfeeds.nbcnews.com/HL4TzgYC.

### Test Data
The examples below represent test set of insertions for searching rss news.

Example 1: Insert 1, 2 as tags for search. Also insert sample string "EP 18" as a filter for header search field in swagger.

### Docker docker-compose files to use with this app.

PostgreSql
```sh
version: '3.9'

services:
  postgres:
    image: postgres:14-alpine
    ports:
      - 5432:5432
    volumes:
      - ~/apps/postgres:/var/lib/postgresql/data
    environment:
      - POSTGRES_PASSWORD=S3cret
      - POSTGRES_USER=citizix_user
      - POSTGRES_DB=citizix_db
```

Redis
```sh
version: '3.8'
services:
  cache:
    image: redis:6.2-alpine
    restart: always
    ports:
      - '6379:6379'
    command: redis-server --save 20 1 --loglevel warning --requirepass eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81
    volumes: 
      - cache:/data
volumes:
  cache:
    driver: local
```